using Core.Network.HttpClient;
using Zenject;

namespace Infrastructure
{
    public class HttpClientInstaller : Installer<HttpClientInstaller>
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