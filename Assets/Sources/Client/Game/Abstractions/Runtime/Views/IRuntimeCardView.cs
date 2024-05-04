using Client.Game.Abstractions.Runtime.Models;

namespace Client.Game.Abstractions.Runtime.Views
{
    public interface IRuntimeCardView : IRuntimeObjectView
    {
        new IRuntimeCardModel Model { get; }
    }
}