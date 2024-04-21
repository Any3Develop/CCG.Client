using Zenject;

namespace CardGame.Services.SceneEntity
{
    public class SceneEntityInstaller : Installer<SceneEntityInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SceneEntityStorage>()
                .AsSingle()
                .NonLazy();
        }
    }
}