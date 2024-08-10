using System;
using Shared.Abstractions.Game.Context.EventSource;

namespace Shared.Game.Utils
{
    public static class EventSourceExtensions
    {
        public static void Publish<T>(this IEventPublisher source, bool isAllowed, params object[] args)
        {
            if (!isAllowed || source == null)
                return;

            source.Publish(Activator.CreateInstance(typeof(T), args));
        }
    }
}