using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Services.TypeRegistryService;
using UnityEngine;
using Zenject;

namespace CardGame.Services.StatsService
{
    public class StatModifiable : Stat, IStatModifiable, IStatValueChanged
    {
        public event EventHandler<float> OnValueChanged;
        public override float Value => Mathf.Max(0,BaseValue + StatModifierValue);
        public float StatModifierValue { get; private set; }
        
        private List<IStatModifier> _statMods;
        private StatModifiableDto _statDto;
        private IInstantiator _instantiator;
        private TypeRegistry _typeStorage;

        [Inject]
        private void Inject(IInstantiator instantiator,
                            TypeRegistry typeStorage)
        {
            _statDto = (StatModifiableDto)StatDto;
            _statMods = new List<IStatModifier>();
            _instantiator = instantiator;
            _typeStorage = typeStorage;

            foreach (var modifierData in _statDto.Modifiers)
            {
                _statMods.Add(CreateModifier(modifierData.TypeName,
                                             modifierData.Value,
                                             modifierData.Stacks,
                                             modifierData.Id));
            }
            UpdateModifiers();
        }

        public void AddModifier(IStatModifier modifier)
        {
            _statMods.Add(modifier);
            _statDto.Modifiers.Add(new StatModifierData
            {
                Id = modifier.Id,
                TypeName = modifier.GetType().Name,
                Stacks = modifier.Stacks,
                Value = modifier.Value
            });
            modifier.OnValueChanged += OnModValueChanged;
            UpdateModifiers();
        }

        public void RemoveModifier(IStatModifier modifier)
        {
            if (!_statMods.Contains(modifier))
            {
                return;
            }
            _statMods.Remove(modifier);
            var index = _statDto.Modifiers.FindIndex(x=>x.Id == modifier.Id);
            _statDto.Modifiers.RemoveAt(index);
            modifier.OnValueChanged -= OnModValueChanged;
            UpdateModifiers();
        }

        public IEnumerable<IStatModifier> GetModifiers()
        {
            return _statMods;
        }

        public void ClearModifiers()
        {
            foreach (var modifier in _statMods)
            {
                modifier.OnValueChanged -= OnModValueChanged;
            }
            _statDto.Modifiers.Clear();
            _statMods.Clear();
            UpdateModifiers();
        }

        public void UpdateModifiers()
        {
            StatModifierValue = 0;
            var orderGroups = _statMods.OrderBy(x => x.Order).GroupBy(x => x.Order);
            foreach (var group in orderGroups)
            {
                var sum = 0f;
                var max = 0f;

                foreach (var modifier in group)
                {
                    if (modifier.Stacks == false)
                    {
                        max = Mathf.Max(max, modifier.Value);
                    }
                    else
                    {
                        sum += modifier.Value;
                    }
                }

                StatModifierValue += group.First().ApplyModifier(this, sum > max ? sum : max);
            }

            TriggerValueChanged();
        }
        
        protected void TriggerValueChanged()
        {
            OnValueChanged?.Invoke(this, Value);
        }
        
        private void OnModValueChanged(object sender, float value)
        {
            if (sender is IStatModifier modifier)
            {
                var index = _statDto.Modifiers.FindIndex(x => x.Id == modifier.Id);
                var statModifierData = new StatModifierData
                {
                    Id = modifier.Id,
                    TypeName = modifier.GetType().Name,
                    Stacks = modifier.Stacks,
                    Value = modifier.Value
                };
                if (index > 0)
                {
                    _statDto.Modifiers[index] = statModifierData;
                }
                else
                {
                    _statDto.Modifiers.Add(statModifierData);
                }
            }
            UpdateModifiers();
        }

        private IStatModifier CreateModifier(string typeName, params  object[] args)
        {
            return (IStatModifier)_instantiator.Instantiate(_typeStorage.Get(typeName).Type, args);
        }

        protected override void OnDispose()
        {
            OnValueChanged = null;
            StatModifierValue = 0;
            _statMods.Clear();
            _statMods = null;
            _statDto = null;
        }
    }
}