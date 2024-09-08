using Client.Services.UIService;
using Client.Services.UIService.FullFade;
using Client.Services.UIService.Options;
using Zenject;

namespace Startup.Infrastructure.Client.Common.Services
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<UIRoot>()
                .FromComponentInNewPrefabResource(nameof(UIRoot))
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<UIService>()
                .AsSingle()
                .WithArguments("DefaultUIGroup");

            Container
                .BindInterfacesTo<UIWindowPrototypeProvider>()
                .AsSingle()
                .WithArguments("");

            Container
                .BindInterfacesTo<UIWindowFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIAudioSourceFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIAudioHandlersFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIAnimationSourceFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIAnimationsFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIOptionsFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIServiceRepository>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIFullFadePresenter>()
                .AsSingle();

            // Container
            //     .BindInterfacesTo<UIAudioListener>()
            //     .AsSingle();
            //
            // Container
            //     .BindInterfacesTo<SetupDefaultUIGroup>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}