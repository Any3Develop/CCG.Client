using Demo.Core.Game.Commands.Base;

namespace Demo.Core.Game.Commands.Models
{
    public class PlayCardModel : CommandModelBase
    {
        public int? Position { get; set; }
    }
}