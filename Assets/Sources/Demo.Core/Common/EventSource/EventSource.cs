using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.Core.Abstractions.Common.EventSource;
using Demo.Core.Common.SharedLogger;

namespace Demo.Core.Common.EventSource
{
    public class EventSource : IEventsSource
    {
        private readonly Dictionary<Type, List<Subscriber>> subscribers = new();
        private bool sortRequested;

        public IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalRegisterSubscriber<T>(callback, true, order, token);
        }

        public IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null)
        {
            return InternalRegisterSubscriber<T>(callback, false, order, token);
        }

        public IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalRegisterSubscriber<T>(callback, false, order, token);
        }

        public IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null)
        {
            return InternalRegisterSubscriber<T>(callback, true, order, token);
        }

        public void Dispose()
        {
            foreach (var listeners in subscribers.Values.ToArray())
                listeners.ToList().ForEach(x => x?.Dispose());

            subscribers.Clear();
        }

        public void Publish<T>(T value)
        {
            if (!subscribers.TryGetValue(typeof(T), out var listOfListeners))
                return;

            if (sortRequested)
            {
                listOfListeners.Sort();
                sortRequested = false;
            }

            foreach (var subscriber in listOfListeners.ToArray())
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
                    SharedLogger.SharedLogger.Error($"[{GetType().Name}] Some exception caused when {nameof(Publish)} event method : {subscriber.Callback?.Method.Name}, with registered type : {typeof(T).FullName}. Full exception: {e}");
                }
            }
        }

        private IDisposable InternalRegisterSubscriber<T>(Delegate callback, bool hasParameters, int? order, CancellationToken? token)
        {
            var registerType = typeof(T);
            if (!subscribers.TryGetValue(registerType, out var registered))
                subscribers[registerType] = registered = new List<Subscriber>();

            var subscriber = new Subscriber
            {
                Callback = callback,
                Order = order ?? registered.Count,
                HasParameters = hasParameters,
            };

            var cancellationTokenRegistration = token?.Register(subscriber.Dispose);

            subscriber.OnDisposeAction += () =>
            {
                if (cancellationTokenRegistration is
                    {Token: {IsCancellationRequested: false}})
                    cancellationTokenRegistration.Value.Dispose();

                if (!subscribers.TryGetValue(registerType, out var result))
                    return;

                result.Remove(subscriber);
                if (result.Count == 0)
                    subscribers.Remove(registerType);
            };

            registered.Add(subscriber);
            sortRequested = true;
            return subscriber;
        }
    }
}