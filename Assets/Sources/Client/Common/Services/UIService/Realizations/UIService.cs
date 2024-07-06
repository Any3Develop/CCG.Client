using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public class UIService : IUIService
    {
        private readonly IUIRoot uiRoot;
        private readonly IUIWindowFactory windowFactory;
        private readonly Dictionary<Type, IUIWindow> instanceStorage;

        public UIService(
            IUIRoot uiRoot,
            IUIWindowFactory windowFactory)
        {
            this.uiRoot = uiRoot;
            this.windowFactory = windowFactory;
            instanceStorage = new Dictionary<Type, IUIWindow>();
        }

        public void Initialize()
        {
            foreach (var window in windowFactory.CreateAll())
                instanceStorage.Add(window.GetType(), window);
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
            return windowFactory.Create(typeof(T), DefaultIfEmpty(parent)).Container.GetComponent<T>();
        }

        public T Get<T>() where T : IUIWindow
        {
            return TryGet(out T result) ? result : default;
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

        private Transform DefaultIfEmpty(Transform parent)
        {
            return parent ? parent : uiRoot.MiddleContainer;
        }
    }
}