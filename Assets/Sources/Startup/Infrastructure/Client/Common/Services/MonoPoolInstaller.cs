using CardGame.Services.MonoPoolService;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
{
    public class MonoPoolInstaller :Installer<MonoPoolInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<MonoPool>()
                .AsSingle();
        }
    }
}