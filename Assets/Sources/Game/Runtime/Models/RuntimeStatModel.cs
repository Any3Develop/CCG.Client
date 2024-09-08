using Client.Game.Abstractions.Runtime.Models;

namespace Client.Game.Runtime.Models
{
    public class RuntimeStatModel : RuntimeModel, IRuntimeStatModel
    {
        public string DataId { get; set; }
        public int RuntimeOwnerId { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }
    }
}