using Client.Game.Abstractions.Runtime.Models;

namespace Client.Game.Runtime.Models
{
    public class RuntimeModel : IRuntimeModel
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
    }
}