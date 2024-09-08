using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Shared.Abstractions.Common.Network;
using Shared.Common.Network.Attributes;

namespace Shared.Common.Network
{
    public class NetworkHubCaller : INetworkHubCaller
    {
        private readonly Dictionary<string, MethodInfo> endpoints;

        public NetworkHubCaller(Type hubType)
        {
            endpoints = hubType
                .GetMethods()
                .Where(m => m.GetCustomAttribute<HubEndpointAttribute>() != null)
                .ToDictionary(m => m.GetCustomAttribute<HubEndpointAttribute>().Target);
        }

        public virtual async Task InvokeAsync(object hubInstance, string target, CancellationToken token, params object[] parameters)
        {
            var hubTask = InternalInvoke(hubInstance, target, parameters) switch
            {
                Task task => task,
                _ => Task.CompletedTask
            };

            while (!token.IsCancellationRequested 
                   && !hubTask.IsCompleted 
                   && !hubTask.IsCanceled 
                   && !hubTask.IsFaulted 
                   && !hubTask.IsCompletedSuccessfully)
            {
                await Task.Yield();
            }
        }

        protected object InternalInvoke(object hubInstance, string target, params object[] parameters)
        {
            if (!endpoints.TryGetValue(target.ToLower(), out var methodInfo))
                throw new Exception($"Endpoint : '{target}' doesn't exist."); // TODO make understandable exception

            return methodInfo.Invoke(hubInstance, parameters);
        }
    }
}