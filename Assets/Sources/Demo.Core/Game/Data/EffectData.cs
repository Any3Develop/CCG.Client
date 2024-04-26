using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Game.Data.Enums;

namespace Demo.Core.Game.Data
{
    public class EffectData : IData
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
        public Keyword Keyword { get; set; }
    }
}