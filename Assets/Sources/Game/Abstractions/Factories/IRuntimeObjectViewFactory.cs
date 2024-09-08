using Client.Game.Abstractions.Runtime.Models;
using Client.Game.Abstractions.Runtime.Views;

namespace Client.Game.Abstractions.Factories
{
    public interface IRuntimeObjectViewFactory : IRuntimeViewFactory<IRuntimeObjectView, IRuntimeObjectModel>{}
}