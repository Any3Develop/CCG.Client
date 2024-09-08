namespace Client.Services.UIService
{
    public class UIAnimationsFactory : IUIAnimationsFactory
    {
        public virtual IUIAnimation Create(IUIWindow window, IUIAnimationConfig config)
        {
            return config switch
            {
#if DOTWEEN
                UIPopupAnimationConfig => new UIPopupDoTweenAnimation().Init(window, config),
                UIAppearAnimationConfig => new UIAppearDoTweenAnimation().Init(window, config),
#else
                UIPopupAnimationConfig => new UIPopupUnityAnimation().Init(window, config),
                UIAppearAnimationConfig => new UIAppearUnityAnimation().Init(window, config),
#endif

                _ => new UINoAnimation()
            };
        }
    }
}