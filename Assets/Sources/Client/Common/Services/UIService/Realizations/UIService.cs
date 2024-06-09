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
        private readonly Dictionary<Type, IWindow> instanceStorage = new();

        public UIService(IUIRoot uiRoot, IAbstractFactory abstractFactory, IUIPrototypeProvider uiPrototypeProvider)
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
        
        public void Dispose<T>() where T : IWindow
        {
            if (!TryGet(out T view))
                return;

            instanceStorage.Remove(typeof(T));

            if (view != null && view.Container && view.Container.gameObject)
                Object.Destroy(view.Container.gameObject);
        }
        
        public void Create<T>(Transform parent = null) where T : IWindow
        {
            Create(typeof(T), parent);
        }

        public T Get<T>() where T : IWindow
        {
            return TryGet<T>(out var view) ? view : default;
        }

        public bool TryGet<T>(out T result) where T : IWindow
        {
            result = default;
            return instanceStorage.TryGetValue(typeof(T), out var view)
                   && view?.Container
                   && view.Container.TryGetComponent(out result);
        }
        
        public async UniTask<T> ShowAsync<T>(Transform parent = null) where T : IWindow
        {
            if (!TryGet<T>(out var view))
                return default;
            
            Move(view, parent);
            
            // always resize to screen size
            if (view.Container)
            {
                view.Container.offsetMin = Vector2.zero;
                view.Container.offsetMax = Vector2.zero;
            }

            await view.ShowAsync();
            return view;
        }
        
        public async UniTask<T> HideAsync<T>() where T : IWindow
        {
            if (!TryGet<T>(out var view))
                return default;

            await view.HideAsync();
            return Move(view, uiRoot.DeactivatedContainer);
        }
        
        public void Move<T>(Transform parent) where T : IWindow
        {
            if (!instanceStorage.TryGetValue(typeof(T), out var view) || view == null)
                return;

            Move(view, parent);
        }
        
        private T Move<T>(T view, Transform parent) where T : IWindow
        {
            if (view == null)
                return default;

            view.Container.SetParent(ParentFix(parent), false);
            return view;
        }
        
        private void Create(Type type, Transform parent = null)
        {
            if (instanceStorage.ContainsKey(type) || !prototypeStorage.TryGetValue(type, out var prototype))
                return;

            instanceStorage.Add(type, abstractFactory.InstantiatePrototype<IWindow>(prototype, ParentFix(parent)));
        }

        private Transform ParentFix(Transform parent)
        {
            return parent ? parent : uiRoot.MiddleContainer;
        }
    }
}