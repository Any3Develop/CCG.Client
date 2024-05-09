using System;
using System.Threading;
using System.Threading.Tasks;
using Client.Game.Abstractions.Context.EventSource;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Context.EventSource
{
    public class GameEventSource : Shared.Game.Context.EventSource.EventSource, IGameEventSource, IGameEventPublisher
    {
        public new IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
        {
            return base.Subscribe(callback, token, order);
        }

        public new IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
        {
            return base.Subscribe<T>(callback, token, order);
        }

        public IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
        {
            return InternalSubscribe<T>(callback, false, token, order);
        }

        public IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
        {
            return InternalSubscribe<T>(callback, true, token, order);
        }

        public async UniTask PublishAsync(IGameEvent value)
        {
            foreach (var subscriber in GetSubscribers(value))
            {
                switch (DynamicInvoke(subscriber, value))
                {
                    case UniTask uniTask : await uniTask; return;
                    case Task task : await task.AsUniTask(); return;
                }
            }
        }
    }
}