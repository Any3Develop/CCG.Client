using System;
using System.Collections.Generic;
using Client.Common.Abstractions.DependencyInjection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public class UIService : IUIService
    {
        private readonly IUIRoot uiRoot;
        private readonly IAbstractFactory abstractFactory;
        private readonly IUIPrototypeProvider uiPrototypeProvider;
        private readonly Dictionary<Type, Object> prototypeStorage = new();
        private readonly Dictionary<Type, IUIWindow> instanceStorage = new();

        public UIService(
            IUIRoot uiRoot, 
            IAbstractFactory abstractFactory, 
            IUIPrototypeProvider uiPrototypeProvider)
        {
            this.uiRoot = uiRoot;
            this.abstractFactory = abstractFactory;
            this.uiPrototypeProvider = uiPrototypeProvider;
        }
        
        public void Initialize()
        {
            foreach (var window in uiPrototypeProvider.GetAll())
                prototypeStorage.Add(window.GetType(), window);
            
            foreach (var pair in prototypeStorage)
                Create(pair.Key, uiRoot.DeactivatedContainer);
        }
        
        public void Dispose<T>() where T : IUIWindow
        {
            if (!TryGet(out T view))
                return;

            instanceStorage.Remove(typeof(T));

            if (view != null && view.Container && view.Container.gameObject)
                Object.Destroy(view.Container.gameObject);
        }
        
        public T Create<T>(Transform parent = null) where T : IUIWindow
        {
            return Create(typeof(T), parent).GetComponent<T>();
        }

        public T Get<T>() where T : IUIWindow
        {
            return instanceStorage[typeof(T)].Container.GetComponent<T>();
        }

        public bool TryGet<T>(out T result) where T : IUIWindow
        {
            result = default;
            return instanceStorage.TryGetValue(typeof(T), out var view)
                   && view?.Container
                   && view.Container.TryGetComponent(out result);
        }
        
        public T Move<T>(Transform parent, int? sibling = null) where T : IUIWindow
        {
            return TryGet(out T view) 
                ? Move(view, parent, sibling) 
                : default;
        }
        
        public async UniTask<T> ShowAsync<T>(Transform parent = null) where T : IUIWindow
        {
            if (!TryGet<T>(out var view) || !view?.Container)
                return default;
            
            Move(view, parent);
            
            // always resize to screen size
            view.Container.offsetMin = Vector2.zero;
            view.Container.offsetMax = Vector2.zero;

            await view.ShowAsync();
            return view;
        }
        
        public async UniTask<T> HideAsync<T>() where T : IUIWindow
        {
            if (!TryGet<T>(out var view))
                return default;

            await view.HideAsync();
            return Move(view, uiRoot.DeactivatedContainer);
        }
        
        private T Move<T>(T view, Transform parent, int? sibling = null) where T : IUIWindow
        {
            if (!view?.Container)
                return default;

            view.Container.SetParent(DefaultIfEmpty(parent), false);
            if (sibling.HasValue)
                view.Container.SetSiblingIndex(sibling.Value);
            else 
                view.Container.SetAsLastSibling();
            
            return view;
        }
        
        private GameObject Create(Type type, Transform parent = null)
        {
            if (!prototypeStorage.TryGetValue(type, out var prototype))
                return default;

            var objectView = abstractFactory.InstantiatePrototype(prototype, DefaultIfEmpty(parent));
            if (objectView.TryGetComponent(out IUIWindow view))
                instanceStorage.TryAdd(type, view);
            
            return objectView;
        }

        private Transform DefaultIfEmpty(Transform parent)
        {
            return parent ? parent : uiRoot.MiddleContainer;
        }
    }
}