using Client.Common.Network.HttpClient;
using Zenject;

namespace Startup.Infrastructure.Client.Common
{
    public class NetworkInstaller : Installer<NetworkInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<UnityHttpClient>()
                .AsSingle()
                .NonLazy();
        }
    }
}