using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public class UIWindowPrototypeProvider : IUIPrototypeProvider, IDisposable
    {
        private readonly Dictionary<Type, UIWindow> storage;

        public UIWindowPrototypeProvider(string path)
        {
            storage = Resources.LoadAll<UIWindow>(path).ToDictionary(obj => obj.GetType());
        }

        public IEnumerable<Object> GetAll()
        {
            return storage.Values.ToArray();
        }
        
        public Object Get(Type type)
        {
            return storage.TryGetValue(type, out var prototype) ? prototype : null;
        }

        public void Dispose()
        {
            storage.Clear();
        }
    }
}