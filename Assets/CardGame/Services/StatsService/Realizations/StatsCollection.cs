using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Services.StorageService;
using CardGame.Services.TypeRegistryService;
using UnityEngine;
using Zenject;

namespace CardGame.Services.StatsService
{
    public class StatsCollection : Storage<Stat>, IStorageItem, IDisposable
    {
        public string Id => _statsCollectionDto.Guid;
        private StatsCollectionDto _statsCollectionDto;
        
        [Inject]
        private void Inject(StatsCollectionDto statsCollectionDto, 
                            IEnumerable<StatModel> statModels,
                            IInstantiator instantiator,
                            TypeRegistry typeStorage)
        {
            _statsCollectionDto = statsCollectionDto;
            var models = statModels.ToDictionary(x => x.StatID);
            foreach (var dto in statsCollectionDto.Stats)
            {
                var model = !models.ContainsKey(dto.StatId) ? default : models[dto.StatId];
                var typeItem = typeStorage.Get(dto.StatType);
                Add((Stat)instantiator.Instantiate(typeItem.Type, new object[]{dto, model.BaseValue}));
            }

            OnInstalled();
        }
        
        public void Dispose()
        {
            foreach (var stat in Dict.Values)
            {
                if (stat is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            Clear();
        }
        
        public float GetValue(string statId)
        {
            return Get(statId).Value;
        }

        public IEnumerable<Stat> GetAllStats()
        {
            return Dict.Values;
        }

        public T Get<T>(string statId) where T : Stat
        {
            if (HasEntity(statId))
            {
                if (Get(statId) is T castable)
                {
                    return castable;
                }
                Debug.LogError($"{this} : Stat id : {statId}, does not inherit target type :{typeof(T).Name}"); 
                return default;
            }

            Debug.LogError($"{this} : Stat id : {statId}, does not represented in collection");
            return default;
        }
        
        public void AddModifier(string statId, IStatModifier modifier)
        {
            Get<StatModifiable>(statId).AddModifier(modifier);
        }
        
        public void RemoveModifier(string statId, IStatModifier modifier)
        {
            Get<StatModifiable>(statId).RemoveModifier(modifier);
        }
        
        public void RemoveAllModifiers(string statId)
        {
            Get<StatModifiable>(statId).ClearModifiers();
        }

        protected virtual void OnInstalled()
        {
            
        }
    }
}