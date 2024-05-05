using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Common.Abstractions.DependencyInjection
{
    public interface IAbstractFactory
    {
        T Instantiate<T>(params object[] args);
        object Instantiate(Type concreteType, params object[] args);
        
        TComponent AddComponent<TComponent>(GameObject placeHolder, params object[] args) where TComponent : Component;
        TObject Instantiate<TObject>(TObject prototype, Transform parent = null, params object[] args) where TObject : Object;
    }
}