using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Events;

namespace Shared.Game.Events.Context.Queue
{
    public class AfterGameQueueReleasedEvent
    {
        public List<IGameEvent> Queue { get; }

        public AfterGameQueueReleasedEvent(List<IGameEvent> queue)
        {
            Queue = queue;
        }
    }
}