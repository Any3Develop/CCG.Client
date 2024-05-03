using Shared.Abstractions.Game.Data;

namespace Shared.Game.Data
{
    public class StatData : IData
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Max { get; set; }
    }
}