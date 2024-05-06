using System.Collections.Generic;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Events;
using Shared.Game.Events.Context.Queue;

namespace Shared.Game.Context
{
    public class GameQueueCollector : IGameQueueCollector
    {
        private readonly IContext context;
        private readonly Queue<IGameEvent> queue;

        public GameQueueCollector(IContext context)
        {
            this.context = context;
            queue = new Queue<IGameEvent>();
        }

        public void Register(IGameEvent value)
        {
            value.Order = context.RuntimeOrderProvider.Next();
            queue.Enqueue(value);
        }

        public void Release()
        {
            if (queue.Count == 0)
                return;

            var releaseEvent = new GameQueueReleaseEvent(queue);
            queue.Clear();
            context.EventSource.Publish(releaseEvent);
        }

        public void Dispose()
        {
            queue.Clear();
        }
    }
}