using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Services.StorageService
{
    public class Storage<T> : IStorage<T>  where T : IStorageItem
    {
        public event Action<string> OnStorageChanged;
        protected readonly Dictionary<string, T> Dict = new Dictionary<string, T>();

        public int Count => Dict.Count;

        public virtual void Add(T value)
        {
            if (value == null)
            {
                Debug.LogError("value is null");
                return;
            }

            if (value.Id == null)
            {
                Debug.LogError("value.id is null");
                return;
            }

            if (HasEntity(value.Id))
            {
                Debug.LogError($"value id: {value.Id} already exists for {value.GetType().Name}, ID : {value.Id}");
                return;
            }
            
            Dict.Add(value.Id, value);
            OnStorageChanged?.Invoke(value.Id);
        }

        public virtual void Remove(string key)
        {
            if (!HasEntity(key))
            {
                throw new ArgumentException($"Key not presented : {key}");
            }
            Dict.Remove(key);
            OnStorageChanged?.Invoke(key);
        }
        
        public virtual void Remove(T value)
        {
            if (value == null)
            {
                throw new ArgumentException($"Value is missing");
            }
            Remove(value.Id);
        }

        public bool HasEntity(string id)
        {
            return Dict.ContainsKey(id);
        }

        public bool Any()
        {
            return Count > 0;
        }

        public IEnumerable<T> Get()
        {
            return Dict.Values;
        }

        public T Get(string key)
        {
            if (!HasEntity(key))
            {
                throw new ArgumentException($"Key not presented : {key}");
            }
            return Dict[key];
        }

        public virtual void Clear()
        {
            Dict?.Clear();
            OnStorageChanged?.Invoke("empty");
        }
    }
}