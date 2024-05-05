using Shared.Abstractions.Game.Data;
using Shared.Game.Data.Enums;

namespace Shared.Game.Data
{
    public class EffectData : IData
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
        public LogicId LogicId { get; set; }
        public VisualId VisualId { get; set; }
    }
}