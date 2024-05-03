using Shared.Game.Commands.Base;

namespace Shared.Game.Commands.Models
{
    public class PlayCardModel : CommandModelBase
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}