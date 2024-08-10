using System;
using System.Linq;
using Services.UIService.Abstractions;

namespace Services.UIService
{
    public class UIAnimationSourceFactory : IUIAnimationSourceFactory
    {
        protected readonly IUIAnimationsFactory AnimationsFactory;
        
        public UIAnimationSourceFactory(IUIAnimationsFactory animationsFactory) 
            => AnimationsFactory = animationsFactory;

        public IUIAnimationSource Create(IUIWindow window)
        {
            if (window == null)
                throw new NullReferenceException($"{nameof(IUIWindow)} missing window.");

            var configProvider = window.Parent.GetComponentInChildren<IUIAnimationConfigsProvider>(true);
            if (configProvider == null)
                return new UINoAnimationSource();
            
            var animations = configProvider.LoadAll().Select(config => AnimationsFactory.Create(window, config)).ToArray();
            return new UIAnimationSource().Init(window, animations);
        }
    }
}