using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IUIWindowFactory
    {
        IUIWindow Create(Type specificType, Transform parent = null);
        IEnumerable<IUIWindow> CreateAll(Transform parent = null);
    }
}