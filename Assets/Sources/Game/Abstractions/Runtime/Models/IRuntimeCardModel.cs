using Shared.Game.Data;

namespace Client.Game.Abstractions.Runtime.Models
{
    public interface IRuntimeCardModel : IRuntimeObjectModel
    {
        new CardData Data { get; }
        int? Position { get; set; }
    }
}