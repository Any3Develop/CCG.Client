using Server.Application.Messenger;
using Server.Application.Sessions;
using Zenject;

namespace Startup.Infrastructure.Server
{
    public class ServerApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<MessengerHandler>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<TcpMessengerService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<RuntimeSessionsRepository>()
                .AsSingle()
                .NonLazy();
                        
            Container
                .BindInterfacesTo<SessionFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}