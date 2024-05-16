using System.Collections.Generic;
using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.EventProcessors;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;
using Shared.Common.Logger;
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
        private bool rollBackTask;


        public GameEventQueueRemoteProcessor(
            IGameEventRemoteQueue remoteQueue,
            IGameEventPredictedQueue predictedQueue,
            IGameEventRollbackQueue rollbackQueue,
            IGameEventQueueLocalProcessor queueLocalProcessor)
        {
            this.remoteQueue = remoteQueue;
            this.predictedQueue = predictedQueue;
            this.rollbackQueue = rollbackQueue;
            this.queueLocalProcessor = queueLocalProcessor;
        }
        
        public void Process(IEnumerable<IGameEvent> queue)
        {
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

                if (!remoteEvent.ValueEquals(predictedEvent))
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
                await queueLocalProcessor.InterruptAsync();
                await queueLocalProcessor.ProcessAsync(rollbackQueue);
                queueLocalProcessor.ProcessAsync(remoteQueue).Forget(SharedLogger.Error);
            }
            finally
            {
                predictedQueue.Clear();
                rollbackQueue.Clear();
                remoteQueue.Clear();
                rollBackTask = false;
            }
        }
    }
}