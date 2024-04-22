using Zenject;

namespace Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputSystemInstaller.Install(Container);
            TypeRegistryInstaller.Install(Container);
            StatsServiceInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            BootstrapInstaller.Install(Container);
            MonoPoolInstaller.Install(Container);
            UIServiceInstaller.Install(Container);
            HttpClientInstaller.Install(Container);
        }
    }
}