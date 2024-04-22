using Demo.Core.Abstractions.Game.Data;

namespace Demo.Core.Game.Data
{
    public class StatData : IData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Base { get; set; }
        public int Max { get; set; }
    }
}