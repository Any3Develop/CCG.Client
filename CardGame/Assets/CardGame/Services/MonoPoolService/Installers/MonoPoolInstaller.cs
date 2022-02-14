using Zenject;

namespace CardGame.Services.MonoPoolService
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