using System.Collections.Generic;

namespace CardGame.Services.StorageService
{
    public interface IStorage<T> where T : IStorageItem
    {
        int Count { get; }
        void Add(T value);
        void Remove(string key);
        void Remove(T value);
        bool HasEntity(string id);
        bool Any();
        IEnumerable<T> Get();
        T Get(string key);
        void Clear();
    }
}