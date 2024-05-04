using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CardGame.Services.MonoPoolService
{
    public class MonoPool : IMonoPool, IDisposable, IInitializable
    {
        public Transform PoolContainer { get; private set; }
        private const string _path = "Prefabs/Poolables/";
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Type, List<IMonoPoolable>> _pool;
        private readonly List<IMonoPoolable> _prefabsPool;

        public MonoPool(IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _pool = new Dictionary<Type, List<IMonoPoolable>>();
            _prefabsPool = new List<IMonoPoolable>();
            var loadedObjects = Resources.LoadAll<GameObject>(_path);
            if (loadedObjects == null || loadedObjects.Length == 0)
            {
                return;
            }
            _prefabsPool.AddRange(loadedObjects.Select(x=>x.GetComponent<IMonoPoolable>()));
        }

        public void Initialize()
        {
            var poolObject = _instantiator.CreateEmptyGameObject("PoolableContainer");
            PoolContainer = poolObject.transform;
            poolObject.SetActive(false);
        }

        public void Dispose()
        {
            Object.Destroy(PoolContainer.gameObject);
            _prefabsPool.Clear();
            _pool.Clear();
        }

        public T Spawn<T>(Transform parent = null, params object[] args) where T : IMonoPoolable
        {
            var itemType = typeof(T);
            parent = parent ? parent : PoolContainer;
            if (_pool.ContainsKey(itemType))
            {
                var item = _pool[itemType][0];
                _pool[itemType].Remove(item);
                if (_pool[itemType].Count == 0)
                {
                    _pool.Remove(itemType);
                }

                item.GameObject.transform.SetParent(parent);
                item.OnSpawned();
                return (T) item;
            }

            var prefab = _prefabsPool.FirstOrDefault(x => x.GetType() == itemType);
            if (prefab == null)
            {
                Debug.LogError("Poolable prefab does not exist in pool.");
                return default;
            }

            var poolable = SpawnInternal<T>(prefab.GameObject, parent, args);
            poolable.OnSpawned();
            return poolable;
        }

        public void Despawn(IMonoPoolable item)
        {
            if (item == null)
            {
                return;
            }

            if (!_pool.ContainsKey(item.GetType()))
            {
                _pool.Add(item.GetType(), new List<IMonoPoolable>());
            }

            _pool[item.GetType()].Add(item);
            item.OnDespawned();
        }

        public void Destroy(IMonoPoolable item)
        {
            if (item == null) return;

            if (_pool.ContainsKey(item.GetType()) && _pool[item.GetType()].Contains(item))
            {
                _pool[item.GetType()].Remove(item);
                if (!_pool[item.GetType()].Any())
                {
                    _pool.Remove(item.GetType());
                }
            }
        }

        private T SpawnInternal<T>(Object prefab, Transform parent = null, params object[] args)
        {
            if (prefab != null)
            {
                return _instantiator.InstantiatePrefabForComponent<T>(prefab, parent, args);
            }

            Debug.LogError($"{this} : Object prefab is missing.");
            return default;
        }
    }
}