using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Events;

namespace Shared.Game.Events.Context.Queue
{
    public class GameQueueReleaseEvent
    {
        public List<IGameEvent> Queue { get; }

        public GameQueueReleaseEvent(List<IGameEvent> queue)
        {
            Queue = queue;
        }
    }
}