using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Services.DIService
{
    public interface IAbstractFactory
    {
        T Instantiate<T>(params object[] args);
        object Instantiate(Type concreteType, params object[] args);
        
        TComponent AddComponent<TComponent>(GameObject componentHolder, params object[] args) where TComponent : Component;
        TObject InstantiatePrototype<TObject>(Object prototype, Transform parent = null, params object[] args);
        TObject InstantiatePrototype<TObject>(Object prototype, Transform parent = null);
        GameObject InstantiatePrototype(Object prototype, Transform parent = null);
    }
}