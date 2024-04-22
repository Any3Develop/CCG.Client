using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Game.RuntimeData
{
    public class RuntimeStatData : IRuntimeStatData
    {
        public int Id { get; set; }
        public string DataId { get; set; }
        public string Name { get; set; }

        public int Max { get; set; }
        public int Base { get; set; }
        public int Value { get; set; }
    }
}