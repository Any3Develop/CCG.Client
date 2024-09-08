using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Client.Services.UIService
{
    public interface IUIPrototypeProvider
    {
        IEnumerable<Object> GetAll();
        IEnumerable<Object> GetAll(string groupId);
        Object Get(string groupId, Type type);
    }
}