using System;
using System.Linq;
using Newtonsoft.Json;
using Server.Application.Contracts.Network;
using Server.Application.Contracts.Sessions;
using Shared.Abstractions.Game.Commands;
using Shared.Abstractions.Game.Context;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Shared.Common.Network.Exceptions;
using Shared.Game.Utils;

namespace Server.Application.Network
{
    public class ServerMessageHandler : IMessageHandler
    {
        private readonly IRuntimeSessionRepository sessionRepository;
        private readonly ISharedConfig sharedConfig;
        private readonly IDatabase database;
        public event Action<IClient, Message> CallBack;

        public ServerMessageHandler(
            IRuntimeSessionRepository sessionRepository,
            ISharedConfig sharedConfig,
            IDatabase database)
        {
            this.sessionRepository = sessionRepository;
            this.sharedConfig = sharedConfig;
            this.database = database;
        }

        public void Handle(IClient client, Message message)
        {
            switch (message.Route)
            {
                case Route.Command:
                {
                    if (!client.IsAuthorized)
                        throw new NotAuthrorizedException();
                    
                    var session = sessionRepository.GetByUserId(client.UserId);
                    if (session == null)
                        throw new Exception($"There's no any available session for client : {client.UserId}.");
                    
                    var model = JsonConvert.DeserializeObject<ICommandModel>(message.Data, SerializeExtensions.GetDeserializeSettingsByType<ICommandModel>());
                    session.Context.CommandProcessor.Execute(client.UserId, model);
                    break;
                }
                
                case Route.Auth:
                {
                    if (client.IsAuthorized)
                        throw new Exception("Client already authorized.");
                    
                    var session = sessionRepository.GetFreeSession();
                    if (session == null)
                        throw new Exception($"There's no any free session.");

                    var player = session.Context.PlayersCollection.FirstOrDefault(x => !x.RuntimeData.Ready);
                    if (player == null)
                        throw new Exception("There's not free players in session.");
                    
                    client.SetUserId(player.RuntimeData.OwnerId);
                    CallBack?.Invoke(client, new Message
                    {
                        Route = Route.Auth,
                        Data = player.RuntimeData.OwnerId
                    });
                    break;
                }

                case Route.Config:
                {
                    CallBack?.Invoke(client, new Message
                    {
                        Data = JsonConvert.SerializeObject(sharedConfig, SerializeExtensions.SerializeSettings),
                        Route = Route.Database
                    });
                    break;
                }

                case Route.Database:
                {
                    CallBack?.Invoke(client, new Message
                    {
                        Data = JsonConvert.SerializeObject(database.GetModel(), SerializeExtensions.SerializeSettings),
                        Route = Route.Config
                    });
                    break;
                }

                case Route.DropConnection:
                case Route.GameEvent:
                default: SharedLogger.Warning($"Server cant handle the route : {message.Route}");
                    break;
            }
        }

        public void Dispose()
        {
            CallBack = null;
        }
    }
}