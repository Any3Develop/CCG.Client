using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Services.UIService.Abstractions
{
    public interface IUIPrototypeProvider
    {
        IEnumerable<Object> GetAll();
        IEnumerable<Object> GetAll(string groupId);
        Object Get(string groupId, Type type);
    }
}