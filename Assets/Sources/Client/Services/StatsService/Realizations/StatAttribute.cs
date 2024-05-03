using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Services.TypeRegistryService;
using UnityEngine;
using Zenject;

namespace CardGame.Services.StatsService
{
    public class StatAttribute : StatModifiable, IStatScalable, IStatLinkable
    {
        public override float Value => Mathf.Max(0, base.Value + ScaleValue + LinkerValue);
        
        public float ScaleValue => _statDto.ScaleValue;
        public float LinkerValue { get; private set; }
        
        private StatAttributeDto _statDto;
        private List<IStatLinker> _linkers;
        private StatsCollectionStorage _statCollectionStorage;
        private IInstantiator _instantiator;
        private TypeRegistry _typeStorage;
        
        [Inject]
        private void Inject(IInstantiator instantiator,
                            StatsCollectionStorage statCollectionStorage,
                            TypeRegistry typeStorage)
        {
            _statDto = (StatAttributeDto)StatDto;
            _linkers = new List<IStatLinker>();
            _instantiator = instantiator;
            _statCollectionStorage = statCollectionStorage;
            _typeStorage = typeStorage;
            
            foreach (var linkerData in _statDto.Linkers.ToArray())
            {
                if (!_statCollectionStorage.HasEntity(linkerData.LinkerId))
                {
                    _statDto.Linkers.Remove(linkerData);
                    continue;
                }

                var collection = _statCollectionStorage.Get(linkerData.LinkerId);
                if (!collection.HasEntity(linkerData.StatId))
                {
                    _statDto.Linkers.Remove(linkerData);
                    continue;
                }

                _linkers.Add(CreateLinker(linkerData.TypeName, collection.Get(linkerData.StatId), linkerData.Value));
            }

            UpdateLinkers();
        }

        public virtual void ScaleStat(float value)
        {
            _statDto.ScaleValue = value;
            TriggerValueChanged();
        }

        public void AddLinker(IStatLinker linker)
        {
            if (_linkers.Contains(linker))
            {
                throw new ArgumentException($"Linker with Id : {linker.LinkerId}, StatId : {linker.Stat.Id}, alredy exist");
            }
            _statDto.Linkers.Add(new StatLinkerData
            {
                LinkerId = linker.LinkerId,
                StatId = linker.Stat.Id,
                Value = linker.Value
            });
            _linkers.Add(linker);
            linker.OnValueChanged += OnLinkerValueChange;
            UpdateLinkers();
        }
        
        public void RemoveLinker(IStatLinker linker)
        {
            if (!_linkers.Contains(linker))
            {
                return;
            }
            
            linker.Dispose();
            linker.OnValueChanged -= OnLinkerValueChange;

            _linkers.Remove(linker);
            _statDto.Linkers.RemoveAll(data => data.LinkerId == linker.LinkerId && 
                                               data.StatId == linker.Stat.Id);
            UpdateLinkers();
        }
        
        public void ClearLinkers()
        {
            foreach (var linker in _linkers)
            {
                linker.OnValueChanged -= OnLinkerValueChange;
                linker.Dispose();
            }
            _linkers.Clear();
            _statDto.Linkers.Clear();
            UpdateLinkers();
        }

        public IEnumerable<IStatLinker> GetLinkers()
        {
            return _linkers;
        }

        public IEnumerable<IStatLinker> GetLinkers(string linkerId)
        {
            return _linkers.Where(x => x.LinkerId == linkerId);
        }

        public void UpdateLinkers()
        {
            var newValue = _linkers.Sum(x => x.Value);
            if (Math.Abs(newValue - LinkerValue) > 0)
            {
                LinkerValue = newValue;
                TriggerValueChanged();
            }
        }
        
        private IStatLinker CreateLinker(string typeName, params object[] args)
        {
            return (IStatLinker)_instantiator.Instantiate(_typeStorage.Get(typeName).Type, args);
        }
        
        private void OnLinkerValueChange(object sender, float value)
        {
            UpdateLinkers();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            LinkerValue = 0;
            _linkers.Clear();
            _statDto = null;
            _linkers = null;
            _statCollectionStorage = null;
        }
    }
}