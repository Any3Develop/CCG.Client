﻿using System;
using Client.Common.Abstractions.Network;
using Client.Game.Network;
using Cysharp.Threading.Tasks;
using Shared.Common.Logger;
using Shared.Common.Network.Data;
using Zenject;

namespace Startup.Infrastructure.Client
{
    public class ClientTestFlyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ClientTcpMessengerServiceTest>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<TestGameMessageHandler>()
                .AsSingle()
                .NonLazy();
        }
        
        public override async void Start()
        {
            base.Start();
            try
            {
                var messanger = Container.Resolve<IMessengerService>();
                await messanger.ConnectAsync();
                await UniTask.Delay(2000);
                
                await messanger.SendAsync(Route.Command, new object[]
                {
                    "AAAAAAAAAAAAAAAA",
                    "BBBBBBBBBBBBBBBB",
                    "CCCCCCCCCCCCCCCC",
                    "DDDDDDDDDDDDDDDD",
                    "EEEEEEEEEEEEEEEE",
                    "FFFFFFFFFFFFFFFF",
                    "GGGGGGGGGGGGGGGG",
                    "HHHHHHHHHHHHHHHH",
                });
                SharedLogger.Log($"[Client.{GetType().Name}] Sent all the messages.");
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }
    }
}