using System;
using CardGame.Services.StorageService;
using Zenject;

namespace CardGame.Services.StatsService
{
    public class Stat : IStorageItem, IDisposable
    {
        public string Id => StatDto.StatId;
        public virtual float Value => BaseValue;
        public float BaseValue { get; private set; }

        protected StatDto StatDto;
        
        [Inject]
        private void Inject(StatDto statDto, 
                            float baseValue)
        {
            StatDto = statDto;
            BaseValue = baseValue;
        }
        void IDisposable.Dispose()
        {
            OnDispose();
        }
        protected virtual void OnDispose()
        {
        }
    }
}