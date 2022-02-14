using Zenject;

namespace CardGame.Session
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