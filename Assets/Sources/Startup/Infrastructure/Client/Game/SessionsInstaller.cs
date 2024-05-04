using CardGame.Session;
using Zenject;

namespace Startup.Infrastructure.Client.Core
{
    public class SessionsInstaller : Installer<SessionsInstaller>

    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SessionSceneObjectStorage>()
                .AsSingle();
        }
    }
}