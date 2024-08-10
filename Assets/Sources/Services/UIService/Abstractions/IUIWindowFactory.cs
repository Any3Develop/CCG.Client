using System.Collections.Generic;
using UnityEngine;

namespace Services.UIService.Abstractions
{
    public interface IUIWindowFactory
    {
        T Create<T>(string groupId, Transform parent = null) where T : IUIWindow;
        IEnumerable<IUIWindow> Create(string groupId, Transform parent = null);
        IEnumerable<IUIWindow> CreateAll(Transform parent = null);
    }
}