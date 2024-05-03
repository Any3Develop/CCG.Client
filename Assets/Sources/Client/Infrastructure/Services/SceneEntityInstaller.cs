using CardGame.Services.SceneEntity;
using Zenject;

namespace Infrastructure
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