using Startup.Infrastructure.Client.Common;
using Startup.Infrastructure.Client.Common.Services;
using Startup.Infrastructure.Client.Core;
using Zenject;

namespace Startup.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // TypeRegistryInstaller.Install(Container);
            // StatsServiceInstaller.Install(Container);
            // BootstrapInstaller.Install(Container);
            // MonoPoolInstaller.Install(Container);
            
            InputSystemInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            UIServiceInstaller.Install(Container);
            NetworkInstaller.Install(Container);
        }
    }
}