using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CardGame.Services.StorageService;
using UnityEngine;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public class StatsCollectionDto : IStorageEntity, ISerializationCallbackReceiver
    {
        string IStorageEntity.Id => Guid;
        public string Guid;
        [NonSerialized] public List<StatDto> Stats;
        
        [SerializeField] private List<StatVitalDto> _vitals;
        [SerializeField] private List<StatAttributeDto> _attributes;
        [SerializeField] private List<StatModifiableDto> _modifiables;

    #region SerializationHelper
        
        [OnSerializing]
        public void OnBeforeSerialize()
        {
            _vitals = new List<StatVitalDto>();
            _attributes = new List<StatAttributeDto>();
            _modifiables = new List<StatModifiableDto>();
            foreach (var statDto in Stats) // Static stats (Stat) not savable
            {
                switch (statDto)
                {
                    case StatVitalDto vital: _vitals.Add(vital); break;
                    case StatAttributeDto attribute: _attributes.Add(attribute); break;
                    case StatModifiableDto modifiable: _modifiables.Add(modifiable); break;
                }
            }
        }
        
        [OnDeserialized]
        public void OnAfterDeserialize()
        {
            Stats = new List<StatDto>(_vitals);
            Stats.AddRange(_modifiables);
            Stats.AddRange(_attributes);
        }
    #endregion
    }
}