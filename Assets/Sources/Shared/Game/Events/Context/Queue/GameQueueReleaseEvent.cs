using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Events;

namespace Shared.Game.Events.Context.Queue
{
    public class GameQueueReleaseEvent
    {
        public List<IGameEvent> Queue { get; }

        public GameQueueReleaseEvent(IEnumerable<IGameEvent> queue)
        {
            Queue = queue.ToList();
        }
    }
}