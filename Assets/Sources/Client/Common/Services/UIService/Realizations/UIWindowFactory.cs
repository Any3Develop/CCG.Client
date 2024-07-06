using System;
using System.Collections.Generic;
using System.Linq;
using Client.Common.Abstractions.DependencyInjection;
using Client.Common.Services.EventSourceService;
using Client.Common.Services.UIService.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public class UIWindowFactory : IUIWindowFactory
    {
        private readonly IAbstractFactory abstractFactory;
        private readonly IUIPrototypeProvider prototypeProvider;
        private readonly IEventSourceFactory eventSourceFactory;

        public UIWindowFactory(
            IAbstractFactory abstractFactory,
            IUIPrototypeProvider prototypeProvider,
            IEventSourceFactory eventSourceFactory)
        {
            this.abstractFactory = abstractFactory;
            this.prototypeProvider = prototypeProvider;
            this.eventSourceFactory = eventSourceFactory;
        }

        public IUIWindow Create(Type specificType, Transform parent = null)
        {
            return Create(prototypeProvider.Get(specificType), parent);
        }

        public IEnumerable<IUIWindow> CreateAll(Transform parent = null)
        {
            return prototypeProvider.GetAll().Select(prototype => Create(prototype, parent)).ToArray();
        }
        
        private IUIWindow Create(Object prototype, Transform parent = null)
        {
            if (!prototype)
                return default;

            var objectView = abstractFactory.InstantiatePrototype(prototype, parent);
            if (!objectView.TryGetComponent(out UIWindow window))
                throw new InvalidOperationException($"Unknown {nameof(prototype)} for UI : {prototype}");

            window.Init(eventSourceFactory.Crete());
            foreach (var animation in window.GetComponents<UIAnimationBase>())
                animation.Init(window);
                    
            return window;
        }
    }
}