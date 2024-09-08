using System.Collections.Generic;
using System.Linq;
using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.EventProcessors;
using Client.Game.Abstractions.Context.EventSource;
using Client.Game.Events;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;
using Shared.Common.Logger;
using Shared.Game.Events.Output;
using Shared.Game.Utils;
using UnityEngine;

namespace Client.Game.Context.EventProcessors
{
    public class GameEventQueueRemoteProcessor : IGameEventQueueRemoteProcessor
    {
        private readonly IGameEventRemoteQueue remoteQueue;
        private readonly IGameEventPredictedQueue predictedQueue;
        private readonly IGameEventRollbackQueue rollbackQueue;
        private readonly IGameEventQueueLocalProcessor queueLocalProcessor;
        private readonly IGameEventPublisher gameEventPublisher;
        private bool initialized;
        private bool rollBackTask;

        public GameEventQueueRemoteProcessor(
            IGameEventRemoteQueue remoteQueue,
            IGameEventPredictedQueue predictedQueue,
            IGameEventRollbackQueue rollbackQueue,
            IGameEventQueueLocalProcessor queueLocalProcessor,
            IGameEventPublisher gameEventPublisher)
        {
            this.remoteQueue = remoteQueue;
            this.predictedQueue = predictedQueue;
            this.rollbackQueue = rollbackQueue;
            this.queueLocalProcessor = queueLocalProcessor;
            this.gameEventPublisher = gameEventPublisher;
        }
        
        public void Process(IEnumerable<IGameEvent> queue)
        {
            if (!initialized)
            {
                // remove old game events before initialize
                var remaind = queue.SkipWhile(x => x is not Inititalize).ToArray();
                if (remaind.Length == 0)
                    return;

                initialized = true;
                queue = remaind;
            }
            
            if (rollBackTask)
            {
                remoteQueue.Enqueue(queue);
                return;
            }
            
            if (predictedQueue.Count == 0)
            {
                rollbackQueue.Clear();
                queueLocalProcessor.ProcessAsync(queue).Forget(SharedLogger.Error);
                return;
            }
            
            remoteQueue.Enqueue(queue);
            Reconciliation();
        }

        private void Reconciliation()
        {
            while (Application.isPlaying 
                   && remoteQueue.TryPeek(out var remoteEvent) 
                   && predictedQueue.TryPeek(out var predictedEvent))
            {

                if (!remoteEvent.CompareValues(predictedEvent))
                {
                    RollBackAsync().Forget(SharedLogger.Error);
                    return;
                }

                remoteQueue.Dequeue();
                predictedQueue.Dequeue();
                
                if (rollbackQueue.Count > 0)
                    rollbackQueue.Dequeue();
            }
        }

        private async UniTask RollBackAsync()
        {
            if (rollBackTask)
                return;

            try
            {
                rollBackTask = true;
                await gameEventPublisher.PublishAsync(new RollBackStart());
                await queueLocalProcessor.InterruptAsync();
                await queueLocalProcessor.ProcessAsync(rollbackQueue);
                
                var memRemoteQueue = remoteQueue.ToArray();
                predictedQueue.Clear();
                rollbackQueue.Clear();
                remoteQueue.Clear();
                await queueLocalProcessor.ProcessAsync(memRemoteQueue);
            }
            finally
            {
                rollBackTask = false;
                await gameEventPublisher.PublishAsync(new RollBackEnd());
            }
        }
    }
}