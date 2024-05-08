using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Abstractions.Game.Context;
using Shared.Abstractions.Game.Events;
using Shared.Game.Events.Context.Commands;
using Shared.Game.Events.Context.Queue;

namespace Shared.Game.Context
{
    public class GameQueueCollector : IGameQueueCollector
    {
        private readonly IContext context;
        private readonly Queue<IGameEvent> queue;
        private IDisposable commandExecutionListener;
        private string predictionId;

        public GameQueueCollector(IContext context)
        {
            this.context = context;
            queue = new Queue<IGameEvent>();
            commandExecutionListener = context.EventSource
                .Subscribe<BeforeCommandExecuteEvent>(ev => predictionId = ev.Command.Model.PredictionId);
        }

        public void Register(IGameEvent value)
        {
            value.Order = context.RuntimeOrderProvider.Next();
            value.PredictionId = predictionId;
            queue.Enqueue(value);
        }

        public void Release()
        {
            if (queue.Count == 0)
                return;

            var releaseEvent = new GameQueueReleaseEvent(queue.ToList());
            predictionId = null;
            queue.Clear();
            context.EventSource.Publish(releaseEvent);
        }

        public void Dispose()
        {
            commandExecutionListener?.Dispose();
            commandExecutionListener = null;
            queue.Clear();
        }
    }
}