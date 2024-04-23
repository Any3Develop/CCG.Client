using System;
using Demo.Core.Abstractions.Common.EventSource;

namespace Demo.Core.Game.Utils
{
    public static class EventSourceExtensions
    {
        public static void Publish<T>(this IEventsSource source, bool isAllowed, params object[] args)
        {
            if (isAllowed && source != null)
                source?.Publish(Activator.CreateInstance(typeof(T), args));
        }
    }
}