using System.Collections.Generic;
using UnityEngine;

namespace Client.Common.Services.UIService
{
    public interface IUIPrototypeProvider
    {
        IEnumerable<Object> GetAll();
    }
}