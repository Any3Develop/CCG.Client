using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Common.Logger;

namespace Shared.Game.Context.EventSource
{
    public class EventSource : IEventsSource
    {
        protected readonly Dictionary<Type, SubscriberCollection> Subscribers = new();
        public IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, true, token, order);
        }

        public IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, false, token, order);
        }

        public IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, false, token, order);
        }

        public IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, true, token, order);
        }

        public void Dispose()
        {
            var disposables = Subscribers.Values.OfType<IDisposable>().ToArray();
            Subscribers.Clear();
            
            foreach (var disposable in disposables)
                disposable?.Dispose();
        }

        public void Publish<T>(T value)
        {
            foreach (var subscriber in GetSubscribers(value))
                DynamicInvoke(subscriber, value);
        }

        public async Task PublishAsync<T>(T value)
        {
            foreach (var subscriber in GetSubscribers(value))
                if (DynamicInvoke(subscriber, value) is Task task)
                    await task;
        }

        protected IEnumerable<Subscriber> GetSubscribers<T>(T value)
        {
            if (!Subscribers.TryGetValue(value?.GetType(), out var subscriberCollection))
                return Array.Empty<Subscriber>();

            if (subscriberCollection.UnSorted)
            {
                subscriberCollection.Sort();
                subscriberCollection.UnSorted = false;
            }

            return subscriberCollection.ToArray();
        }

        protected object DynamicInvoke(Subscriber subscriber, object value)
        {
            try
            {
                return subscriber.HasParameters 
                    ? subscriber.Callback?.DynamicInvoke(value) 
                    : subscriber.Callback?.DynamicInvoke();
            }
            catch (Exception e)
            {
                SharedLogger.Error($"[{GetType().Name}] Some exception caused when {nameof(PublishAsync)} event method : {subscriber.Callback?.Method.Name}, with registered type : {value?.GetType().FullName}. Full exception: {e}");
            }

            return null;
        }

        protected IDisposable InternalSubscribe<T>(Delegate callback, bool hasParameters, CancellationToken? token, int? order)
        {
            var registerType = typeof(T);
            if (!Subscribers.TryGetValue(registerType, out var registered))
                Subscribers[registerType] = registered = new SubscriberCollection();

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

                if (!Subscribers.TryGetValue(registerType, out var result))
                    return;

                result.Remove(subscriber);
                if (result.Count == 0)
                    Subscribers.Remove(registerType);
            };

            registered.Add(subscriber);
            return subscriber;
        }
    }
}