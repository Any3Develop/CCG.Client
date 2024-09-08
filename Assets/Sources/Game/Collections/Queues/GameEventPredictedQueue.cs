using Client.Game.Abstractions.Collections.Queues;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Collections.Queues
{
    public class GameEventPredictedQueue : QueueCollection<IGameEvent>, IGameEventPredictedQueue {}
}