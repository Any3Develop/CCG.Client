using System;

namespace CardGame.Services.StatsService
{
    public abstract class StatLinker : IStatLinker
    {
        public event EventHandler<float> OnValueChanged;
        public string LinkerId { get; private set; }
        public Stat Stat { get; private set; }
        public virtual float Value => Stat.Value * _value;

        private readonly float _value;

        public StatLinker(string linkerId, Stat stat, float value = 0f)
        {
            LinkerId = linkerId;
            _value = value;
            Stat = stat;
            if (Stat is IStatValueChanged changableStat)
            {
                changableStat.OnValueChanged += OnStatValueChanged;
            }
        }

        public void Dispose()
        {
            if (Stat is IStatValueChanged changableStat)
            {
                changableStat.OnValueChanged -= OnStatValueChanged;
            }

            OnValueChanged = null;
            Stat = null;
        }
        protected virtual void OnStatValueChanged(object sender, float value)
        {
            OnValueChanged?.Invoke(this, Value);
        }
    }
}