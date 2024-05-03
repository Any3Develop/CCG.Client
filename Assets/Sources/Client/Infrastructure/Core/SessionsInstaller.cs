using CardGame.Session;
using Zenject;

namespace Infrastructure
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