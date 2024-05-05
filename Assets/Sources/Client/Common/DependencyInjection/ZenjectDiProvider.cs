using System;
using Client.Common.Abstractions.DependencyInjection;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Client.Common.DependencyInjection
{
    public class ZenjectDiProvider : IAbstractFactory
    {
        private readonly IInstantiator instantiator;

        public ZenjectDiProvider(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public T Instantiate<T>(params object[] args)
        {
            return instantiator.Instantiate<T>(args);
        }

        public object Instantiate(Type concreteType, params object[] args)
        {
            return instantiator.Instantiate(concreteType, args);
        }

        public TComponent AddComponent<TComponent>(GameObject placeHolder, params object[] args) where TComponent : Component
        {
            return instantiator.InstantiateComponent<TComponent>(placeHolder, args);
        }

        public TObject Instantiate<TObject>(TObject prototype, Transform parent = null, params object[] args) where TObject : Object
        {
            return instantiator.InstantiatePrefabForComponent<TObject>(prototype, parent, args);
        }
    }
}