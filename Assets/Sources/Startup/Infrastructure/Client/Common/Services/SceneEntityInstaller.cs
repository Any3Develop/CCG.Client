using CardGame.Services.SceneEntity;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
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