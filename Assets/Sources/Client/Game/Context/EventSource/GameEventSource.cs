using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Game.Abstractions.Context.EventSource;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;
using Shared.Common.Logger;
using Shared.Game.Context.EventSource;

namespace Client.Game.Context.EventSource
{
    public class GameEventSource : EventsSource, IGameEventSource, IGameEventPublisher
    {
        private static Type Validate(Type other)
            => !other.IsAssignableFrom(typeof(IGameEvent)) 
                ? throw new InvalidOperationException($"Can't register type which not inherited from {nameof(IGameEvent)} type.")
                : other;
        public IDisposable Subscribe(Type contract, Action<IGameEvent> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(Validate(contract), callback, true, token, order);

        public IDisposable Subscribe(Action<IGameEvent> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(IGameEvent), callback, true, token, order);

        public new IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
            => base.Subscribe<T>(callback, token, order);

        public new IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
            => base.Subscribe(callback, token, order);

        public IDisposable Subscribe(Type contract, Func<IGameEvent, UniTask> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(Validate(contract), callback, true, token, order);

        public IDisposable Subscribe(Func<UniTask> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(IGameEvent), callback, false, token, order);

        public IDisposable Subscribe(Func<IGameEvent, UniTask> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(IGameEvent), callback, true, token, order);

        public IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
            => InternalSubscribe(typeof(T), callback, false, token, order);

        public IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent
            => InternalSubscribe(typeof(T), callback, false, token, order);

        public void Publish(IGameEvent value)
            => base.Publish(value);

        public async UniTask PublishAsync(IGameEvent value)
        {
            var eventType = value?.GetType();
            if (eventType == null)
                return;
            
            var broadCast = typeof(object);
            var subscribers = eventType == broadCast
                ? GetSubscribers(broadCast)
                : GetSubscribers(eventType).Concat(GetSubscribers(broadCast));

            foreach (var subscriber in subscribers)
            {
                try
                {
                    await (DynamicInvoke(subscriber, value) switch
                    {
                        UniTask uniTask => uniTask,
                        Task task => task.AsUniTask(),
                        _ => UniTask.CompletedTask
                    });
                }
                catch (Exception e)
                {
                    SharedLogger.Error(e);
                }
            }
        }

        public async UniTask PublishParallelAsync(IGameEvent value)
        {
            try
            {
                var eventType = value?.GetType();
                if (eventType == null)
                    return;
            
                var broadCast = typeof(object);
                var subscribers = eventType == broadCast
                    ? GetSubscribers(broadCast)
                    : GetSubscribers(eventType).Concat(GetSubscribers(broadCast));
                
                await UniTask.WhenAll(subscribers.Select(subscriber => DynamicInvoke(subscriber, value) switch
                {
                    UniTask uniTask => uniTask,
                    Task task => task.AsUniTask(),
                    _ => UniTask.CompletedTask
                }));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }
    }
}