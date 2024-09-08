using System;
using Client.Services.DIService;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Startup.Infrastructure.Client.Common.DependencyInjection
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

        public TComponent AddComponent<TComponent>(GameObject componentHolder, params object[] args) where TComponent : Component
        {
            return instantiator.InstantiateComponent<TComponent>(componentHolder, args);
        }

        public TObject InstantiatePrototype<TObject>(Object prototype, Transform parent = null, params object[] args)
        {
            return instantiator.InstantiatePrefabForComponent<TObject>(prototype, parent, args);
        }

        public TObject InstantiatePrototype<TObject>(Object prototype, Transform parent = null)
        {
            return instantiator.InstantiatePrefab(prototype, parent).GetComponent<TObject>();
        }

        public GameObject InstantiatePrototype(Object prototype, Transform parent = null)
        {
            return instantiator.InstantiatePrefab(prototype, parent);
        }
    }
}