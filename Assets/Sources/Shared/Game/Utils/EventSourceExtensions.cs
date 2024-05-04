using System;
using Shared.Abstractions.Game.Context.Logic;

namespace Shared.Game.Utils
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