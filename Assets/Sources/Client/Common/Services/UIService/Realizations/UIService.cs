using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public class UIService : IUIService
    {
        private readonly IInstantiator _instantiator;
        private readonly UIRoot _uiRoot;

        private readonly Dictionary<Type, UIWindow> _viewStorage = new Dictionary<Type, UIWindow>();
        private readonly Dictionary<Type, UIWindow> _instViews = new Dictionary<Type, UIWindow>();

        public UIService(IInstantiator instantiator, UIRoot uiRoot)
        {
            _instantiator = instantiator;
            _uiRoot = uiRoot;
            LoadWindows();
            InitWindows();
        }

        public void LoadWindows()
        {
            var windows = Resources.LoadAll<UIWindow>("UIWindows");
            foreach (var window in windows)
            {
                _viewStorage.Add(window.GetType(), window);
            }
        }

        public void InitWindows()
        {
            foreach (var uiWindow in _viewStorage)
            {
                Init(uiWindow.Key, _uiRoot.DeactivatedContainer);
            }
        }
        
        /// <summary>
        /// Turns on screen display
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Show<T>() where T : UIWindow
        {
            return Show<T>(null);
        }

        /// <summary>
        /// Turns on screen display
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Show<T>(Transform parent) where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type];
                Move<T>(parent);
                var component = view.GetComponent<T>();

                // always resize to screen size
                var rect = component.transform as RectTransform;
                if (rect != null)
                {
                    rect.offsetMin = Vector2.zero;
                    rect.offsetMax = Vector2.zero;
                }
                
                component.Show();
                return component;
            }
            return null;
        }

        public void Move<T>(Transform parent) where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                _instViews[type].transform.SetParent(parent ? parent : _uiRoot.MiddleContainer , false);
            }
        }

        /// <summary>
        /// Turns off screen display
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Hide<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type].GetComponent<T>();
                view.HidedEvent += () =>
                {
                    view.transform.SetParent(_uiRoot.DeactivatedContainer);
                };
                view.Hide();
            }
        }

        /// <summary>
        /// Screen creates with specified parent(optional).
        /// </summary>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        public void Init<T>(Transform parent = null) where T : UIWindow
        {
            Init(typeof(T), parent);
        }

        private void Init(Type t, Transform parent = null)
        {
            if (_viewStorage.ContainsKey(t) && !_instViews.ContainsKey(t))
            {
                parent = parent ? parent : _uiRoot.MiddleContainer;
                _instViews.Add(t, (UIWindow)_instantiator.InstantiatePrefabForComponent(t, _viewStorage[t], parent, new object[0]));
            }
        }

        /// <summary>
        /// Returns screen by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_instViews.ContainsKey(type))
            {
                var view = _instViews[type];
                return view.GetComponent<T>();
            } 
            return null;
        }

        /// <summary>
        /// Removes the screen from the scene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Disponse<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (!_instViews.ContainsKey(type))
            {
                return;
            }

            var window = _instViews[type];
            _instViews.Remove(type);
            if (window)
            {
                Object.Destroy(window.gameObject); 
            }
        }
    }
}