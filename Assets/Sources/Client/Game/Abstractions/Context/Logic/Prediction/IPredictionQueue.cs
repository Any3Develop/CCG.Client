using Client.Game.Abstractions.Collections;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.Logic.Prediction
{
    public interface IPredictionQueue : IQueue<IGameEvent> {}
}