using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Server.API;
using Server.Application.Contracts.Network;
using Shared.Abstractions.Common.Network;
using Shared.Common.Logger;
using Shared.Common.Network.Data;

namespace Server.Application.Network
{
    public class NetworkApplication : INetworkApplication
    {
        private readonly INetworkHubFactory networkHubFactory;
        private readonly INetworkSerializer networkSerializer;
        private readonly INetworkHubConnectionFactory hubConnectionFactory;
        private readonly INetworkHubCallerFactory networkHubCallerFactory;
        
        private readonly ConcurrentDictionary<IClient, Task> callerWorkers;
        private readonly ConcurrentDictionary<IClient, Queue<Message>> callerQueue;
        private readonly ConcurrentDictionary<Type, INetworkHubCaller> hubCallers;
        
        private CancellationTokenSource lifeTime;
        private INetworkHubConnection connection;
        
        public NetworkApplication(
            INetworkHubFactory networkHubFactory, 
            INetworkSerializer networkSerializer,
            INetworkHubConnectionFactory hubConnectionFactory,
            INetworkHubCallerFactory networkHubCallerFactory)
        {
            this.networkHubFactory = networkHubFactory;
            this.networkSerializer = networkSerializer;
            this.hubConnectionFactory = hubConnectionFactory;
            this.networkHubCallerFactory = networkHubCallerFactory;
            
            callerWorkers = new ConcurrentDictionary<IClient, Task>();
            callerQueue = new ConcurrentDictionary<IClient, Queue<Message>>();
            hubCallers = new ConcurrentDictionary<Type, INetworkHubCaller>();
        }

        public async Task StartAsync()
        {
            if (connection is {IsConnected: true})
                return;
            
            Stop();
            lifeTime = new CancellationTokenSource();
            connection = hubConnectionFactory.Create();
            connection.OnMessageReceived += OnMessageReceived;
            await connection.ConnectAsync(Urls.Port, lifeTime.Token);
        }
        
        public void Stop()
        {
            if (connection == null)
                return;
            
            lifeTime?.Cancel();
            lifeTime?.Dispose();
            lifeTime = null;
            connection.OnMessageReceived -= OnMessageReceived;
            connection.CloseAsync()
                .ConfigureAwait(false)
                .GetAwaiter();
        }

        private void OnMessageReceived(IClient caller, Message message)
        {
            if (caller is not {IsConnected: true} || connection is not {IsConnected: true})
                return;
            
            AddToQueue(caller, message);
            StartWorker(caller, lifeTime.Token);
        }

        private void StartWorker(IClient caller, CancellationToken token)
        {
            if (!caller.IsConnected || connection is not {IsConnected: true})
            {
                callerWorkers.TryRemove(caller, out _);
                callerQueue.TryRemove(caller, out _);
                return;
            }
            
            if (callerWorkers.TryGetValue(caller, out var worker)) 
                return;
            
            callerWorkers[caller] = worker = ExecuteQueueAsync(caller, token);
            worker.ConfigureAwait(false).GetAwaiter();
        }

        private void AddToQueue(IClient caller, Message message)
        {
            if (!callerQueue.TryGetValue(caller, out var queue))
                callerQueue[caller] = queue = new Queue<Message>();
            
            queue.Enqueue(message);
        }

        private async Task ExecuteQueueAsync(IClient caller, CancellationToken token)
        {
            if (!callerQueue.TryGetValue(caller, out var queue))
                return;

            while (queue.Count > 0)
            {
                try
                {
                    if (caller is not {IsConnected: true} || connection is not {IsConnected: true})
                        break;
                    
                    var message = queue.Dequeue();
                    await ExecuteCallAsync(caller, message, token);
                }
                catch (Exception e)
                {
                    SharedLogger.Error(e);
                }
            }

            callerWorkers.TryRemove(caller, out _);
            if (queue.Count == 0)
                callerQueue.TryRemove(caller, out _);
            else
                StartWorker(caller, token);
        }

        private async Task ExecuteCallAsync(IClient caller, Message message, CancellationToken token)
        {
            var parameters = message.Args.Select(networkSerializer.Deserialize).ToArray();
            var hub = networkHubFactory.Create(connection, caller);
            var hubType = hub.GetType();
            if (!hubCallers.TryGetValue(hubType, out var hubCaller))
                hubCallers[hubType] = hubCaller = networkHubCallerFactory.Create(hubType);
            
            await hubCaller.InvokeAsync(hub, message.Target, token, parameters);
        }
    }
}