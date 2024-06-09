using System.Collections.Generic;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public class UIResourcePrototypeProvider : IUIPrototypeProvider
    {
        private readonly string path;

        public UIResourcePrototypeProvider(string path)
        {
            this.path = path;
        }

        public IEnumerable<Object> GetAll()
        {
            return Resources.LoadAll<UIWindow>(path);
        }
    }
}