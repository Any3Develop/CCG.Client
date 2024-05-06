using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Context.EventProcessors;
using Shared.Game.Events.Context.Queue;
using Shared.Game.Events.Output;
using Shared.Game.Utils;

namespace Shared.Game.Context.EventProcessors
{
    public class ContextEventProcessor : IContextEventProcessor
    {
        private readonly IContext context;
        private bool initialized;

        public ContextEventProcessor(IContext context)
        {
            this.context = context;
        }

        public virtual void Subscribe()
        {
            if (initialized)
                return;

            initialized = true;
            OnSubscribed();
        }

        protected virtual void OnSubscribed()
        {
            context.EventSource.Subscribe<GameQueueReleaseEvent>(data =>
            {
                data.Queue.Add(new SyncRuntimeOrder().Map(context.RuntimeOrderProvider));
                data.Queue.Add(new SyncRuntimeId().Map(context.RuntimeIdProvider));
            }, order: int.MinValue);
        }
    }
}