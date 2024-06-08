using Startup.Logger;
using Zenject;

namespace Startup.Infrastructure.Project
{
    public class ProjectLoggerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ProjectLogger>()
                .AsSingle()
                .NonLazy();
        }
    }
}