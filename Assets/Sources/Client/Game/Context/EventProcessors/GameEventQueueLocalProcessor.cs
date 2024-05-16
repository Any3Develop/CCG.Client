using System;
using System.Collections.Generic;
using System.Threading;
using Client.Game.Abstractions.Collections.Queues;
using Client.Game.Abstractions.Context.EventProcessors;
using Client.Game.Abstractions.Context.EventSource;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Context.EventProcessors;
using Shared.Abstractions.Game.Events;
using Shared.Common.Logger;

namespace Client.Game.Context.EventProcessors
{
    public class GameEventQueueLocalProcessor : IGameEventQueueLocalProcessor, IDisposable
    {
        private readonly IGameEventLocalQueue localQueue;
        private readonly IGameEventPublisher gameEventPublisher;
        private readonly IGameEventProcessor gameEventProcessor;
        private CancellationTokenSource unQueueProcess;
        private UniTaskCompletionSource interrupting;
        private bool initialized = true;

        public GameEventQueueLocalProcessor(
            IGameEventLocalQueue localQueue,
            IGameEventPublisher gameEventPublisher,
            IGameEventProcessor gameEventProcessor)
        {
            this.localQueue = localQueue;
            this.gameEventPublisher = gameEventPublisher;
            this.gameEventProcessor = gameEventProcessor;
        }

        public void Dispose()
        {
            if (!initialized)
                return;
            
            initialized = false;
            interrupting?.TrySetResult();
            unQueueProcess?.Cancel();
            unQueueProcess?.Dispose();
            unQueueProcess = null;
            interrupting = null;
            localQueue.Clear();
        }
        
        public async UniTask ProcessAsync(IEnumerable<IGameEvent> queue)
        {
            if (!initialized)
                return;
            
            localQueue.Enqueue(queue);
            await StartUnQueueLoopAsync();
        }

        public async UniTask ProcessAsync(IGameEvent gameEvent)
        {
            if (!initialized)
                return;
            
            gameEventProcessor.Process(gameEvent);
            await gameEventPublisher.PublishAsync(gameEvent);
        }

        public async UniTask InterruptAsync()
        {
            if (interrupting != null)
            {
                await interrupting.Task; // TODO: check if it possible to await twice
                return;
            }

            interrupting = new UniTaskCompletionSource();
            unQueueProcess?.Cancel();
            unQueueProcess?.Dispose();
            unQueueProcess = null;
            await interrupting.Task;
            interrupting = null;
            localQueue.Clear();
        }

        private async UniTask StartUnQueueLoopAsync()
        {
            if (!initialized || localQueue.Count == 0 || unQueueProcess != null || interrupting != null)
                return;

            unQueueProcess = new CancellationTokenSource();
            var token = unQueueProcess.Token;
            while (initialized && !token.IsCancellationRequested && localQueue.Count > 0)
                await ProcessAsync(localQueue.Dequeue());

            unQueueProcess?.Dispose();
            unQueueProcess = null;
            if (interrupting != null)
            {
                interrupting.TrySetResult();
                interrupting = null;
                return;
            }

            StartUnQueueLoopAsync().Forget(SharedLogger.Error);
        }
    }
}