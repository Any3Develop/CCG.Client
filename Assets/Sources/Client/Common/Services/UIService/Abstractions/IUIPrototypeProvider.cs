using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Client.Common.Services.UIService
{
    public interface IUIPrototypeProvider
    {
        IEnumerable<Object> GetAll();
        Object Get(Type type);
    }
}