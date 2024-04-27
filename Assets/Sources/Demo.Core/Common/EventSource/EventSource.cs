using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Common.Logger;

namespace Demo.Core.Common.EventSource
{
    public class EventSource : IEventsSource
    {
        private readonly Dictionary<Type, SubscriberCollection> subscribers = new();
        public IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, true, order, token);
        }

        public IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, false, order, token);
        }

        public IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, false, order, token);
        }

        public IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalSubscribe<T>(callback, true, order, token);
        }

        public void Dispose()
        {
            var disposables = subscribers.Values.OfType<IDisposable>().ToArray();
            subscribers.Clear();
            
            foreach (var disposable in disposables)
                disposable?.Dispose();
        }

        public void Publish<T>(T value)
        {
            if (!subscribers.TryGetValue(typeof(T), out var subscriberCollection))
                return;

            if (subscriberCollection.UnSorted)
            {
                subscriberCollection.Sort();
                subscriberCollection.UnSorted = false;
            }

            foreach (var subscriber in subscriberCollection.ToArray())
            {
                try
                {
                    if (subscriber.HasParameters)
                    {
                        subscriber.Callback?.DynamicInvoke(value);
                        continue;
                    }

                    subscriber.Callback?.DynamicInvoke();
                }
                catch (Exception e)
                {
                    SharedLogger.Error($"[{GetType().Name}] Some exception caused when {nameof(Publish)} event method : {subscriber.Callback?.Method.Name}, with registered type : {typeof(T).FullName}. Full exception: {e}");
                }
            }
        }

        private IDisposable InternalSubscribe<T>(Delegate callback, bool hasParameters, int? order, CancellationToken? token)
        {
            var registerType = typeof(T);
            if (!subscribers.TryGetValue(registerType, out var registered))
                subscribers[registerType] = registered = new SubscriberCollection();

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

                if (!subscribers.TryGetValue(registerType, out var result))
                    return;

                result.Remove(subscriber);
                if (result.Count == 0)
                    subscribers.Remove(registerType);
            };

            registered.Add(subscriber);
            return subscriber;
        }
    }
}