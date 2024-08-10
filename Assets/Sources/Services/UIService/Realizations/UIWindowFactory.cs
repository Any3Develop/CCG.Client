using System.Collections.Generic;
using System.Linq;
using Services.UIService.Abstractions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.UIService
{
    public class UIWindowFactory : IUIWindowFactory
    {
        private readonly IUIPrototypeProvider prototypeProvider;
        private readonly IUIAudioSourceFactory audioSourceFactory;
        private readonly IUIAnimationSourceFactory animationSourceFactory;

        public UIWindowFactory(
            IUIPrototypeProvider prototypeProvider,
            IUIAudioSourceFactory audioSourceFactory,
            IUIAnimationSourceFactory animationSourceFactory)
        {
            this.prototypeProvider = prototypeProvider;
            this.audioSourceFactory = audioSourceFactory;
            this.animationSourceFactory = animationSourceFactory;
        }

        public T Create<T>(string groupId, Transform parent = null) where T : IUIWindow
            => Create(prototypeProvider.Get(groupId, typeof(T)), parent).GetComponent<T>();

        public IEnumerable<IUIWindow> CreateAll(Transform parent = null)
            => prototypeProvider.GetAll().Select(prototype => Create(prototype, parent));

        public IEnumerable<IUIWindow> Create(string groupId, Transform parent = null)
            => prototypeProvider.GetAll(groupId).Select(prototype => Create(prototype, parent)).ToArray();

        private UIWindowBase Create(Object prototype, Transform parent = null)
            => Initialize((UIWindowBase)Object.Instantiate(prototype, parent));

        private UIWindowBase Initialize(UIWindowBase window)
        {
            var animationSource = animationSourceFactory.Create(window);
            var audioSource = audioSourceFactory.Create(window);
            return window.Init(animationSource, audioSource);
        }
    }
}