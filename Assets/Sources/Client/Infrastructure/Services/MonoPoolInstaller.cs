using CardGame.Services.MonoPoolService;
using Zenject;

namespace Infrastructure
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