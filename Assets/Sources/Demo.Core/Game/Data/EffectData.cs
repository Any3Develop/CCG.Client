using Demo.Core.Abstractions.Game.Data;

namespace Demo.Core.Game.Data
{
    public class EffectData : IData
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
    }
}