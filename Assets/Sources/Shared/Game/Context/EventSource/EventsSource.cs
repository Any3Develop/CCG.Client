using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Common.Logger;

namespace Shared.Game.Context.EventSource
{
    public class EventsSource : IEventsSource, IEventPublisher
    {
        protected readonly Dictionary<Type, SubscribersCollection> Subscribers = new();

        public IDisposable Subscribe(Type contract, Action<object> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(contract, callback, true, token, order);

        public IDisposable Subscribe(Action callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(object), callback, false, token, order);

        public IDisposable Subscribe(Action<object> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(object), callback, true, token, order);

        public IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(T), callback, false, token, order);

        public IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(T), callback, true, token, order);
        
        public IDisposable Subscribe(Type contract, Func<object, Task> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(contract, callback, true, token, order);
        
        public IDisposable Subscribe(Func<Task> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(object), callback, false, token, order);

        public IDisposable Subscribe(Func<object, Task> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(object), callback, true, token, order);

        public IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(T), callback, false, token, order);

        public IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null)
            => InternalSubscribe(typeof(T), callback, true, token, order);


        public virtual void Dispose()
        {
            foreach (var collection in Subscribers.Values.ToArray())
                collection.Clear();
            
            Subscribers.Clear();
        }
        
        public virtual void Publish(object value)
        {
            var eventType = value?.GetType();
            if (eventType == null)
                return;
            
            var broadCast = typeof(object);
            var subscribers = eventType == broadCast
                ? GetSubscribers(broadCast)
                : GetSubscribers(eventType).Concat(GetSubscribers(broadCast));
            
            foreach (var subscriber in subscribers)
                DynamicInvoke(subscriber, value);
        }

        public virtual async Task PublishAsync(object value)
        {
            var eventType = value?.GetType();
            if (eventType == null)
                return;
            
            var broadCast = typeof(object);
            var subscribers = eventType == broadCast
                ? GetSubscribers(broadCast)
                : GetSubscribers(eventType).Concat(GetSubscribers(broadCast));
            
            foreach (var subscriber in subscribers)
                if (DynamicInvoke(subscriber, value) is Task task)
                    await task;
        }

        public virtual async Task PublishParallelAsync(object value)
        {
            var eventType = value?.GetType();
            if (eventType == null)
                return;
            
            var broadCast = typeof(object);
            var subscribers = eventType == broadCast
                ? GetSubscribers(broadCast)
                : GetSubscribers(eventType).Concat(GetSubscribers(broadCast));
            
            try
            {
                await Task.WhenAll(subscribers.Select(subscriber => DynamicInvoke(subscriber, value) switch
                {
                    Task task => task,
                    _ => Task.CompletedTask
                }));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
        }

        protected virtual IEnumerable<Subscriber> GetSubscribers(Type eventType)
        {
            if (!Subscribers.TryGetValue(eventType, out var subscriberCollection))
                return Array.Empty<Subscriber>();

            if (subscriberCollection.UnSorted)
            {
                subscriberCollection.Sort();
                subscriberCollection.UnSorted = false;
            }

            return subscriberCollection.ToArray();
        }

        protected virtual object DynamicInvoke(Subscriber subscriber, object value)
        {
            try
            {
                return subscriber.HasParameters 
                    ? subscriber.Callback?.DynamicInvoke(value) 
                    : subscriber.Callback?.DynamicInvoke();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{GetType().Name}] Some exception caused when {nameof(PublishAsync)} event method : {subscriber.Callback?.Method.Name}, with registered type : {value?.GetType().FullName}. Full exception: {e}");
            }

            return null;
        }

        protected virtual IDisposable InternalSubscribe(Type contract, Delegate callback, bool hasParameters, CancellationToken? token, int? order)
        {
            if (!Subscribers.TryGetValue(contract, out var registered))
                Subscribers[contract] = registered = new SubscribersCollection();

            registered.UnSorted |= order.HasValue;
            var subscriber = new Subscriber
            {
                Callback = callback,
                Order = order ?? registered.Count,
                HasParameters = hasParameters,
            };

            var cancellationTokenRegistration = token?.Register(subscriber.Dispose);

            subscriber.OnDisposeAction += () =>
            {
                if (cancellationTokenRegistration is {Token: {IsCancellationRequested: false}})
                    cancellationTokenRegistration.Value.Dispose();

                if (!Subscribers.TryGetValue(contract, out var result))
                    return;

                result.Remove(subscriber);
                if (result.Count == 0)
                    Subscribers.Remove(contract);
            };

            registered.Add(subscriber);
            return subscriber;
        }
    }
}