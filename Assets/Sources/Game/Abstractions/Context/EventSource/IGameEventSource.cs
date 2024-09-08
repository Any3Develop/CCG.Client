using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Abstractions.Game.Events;

namespace Client.Game.Abstractions.Context.EventSource
{
    public interface IGameEventSource
    {
        /// <summary>
        /// Listen for a specific event data type and receive callbacks in the form of a <see cref="IGameEvent"/>
        /// </summary>
        /// <param name="contract">Specific event data type to listen for and receive event data of this type. Only inherited from <see cref="IGameEvent"/></param>
        /// <param name="callback">Callback delegate with a <see cref="IGameEvent"/> parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Type contract, Action<IGameEvent> callback, CancellationToken? token = null, int? order = null);
        /// <summary>
        /// Listen for all types of events and receive callbacks with <see cref="IGameEvent"/> as event data.
        /// </summary>
        /// <param name="callback">Callback delegate with a <see cref="IGameEvent"/> parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Action<IGameEvent> callback, CancellationToken? token = null, int? order = null);
        /// <summary>
        /// Listen for all types of events and receive callbacks.
        /// </summary>
        /// <param name="callback">Callback delegate without any parameters.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        /// <summary>
        /// Listen for a specific event data type and receive async callbacks in the form of a <see cref="IGameEvent"/>.
        /// </summary>
        /// <param name="contract">Specific event data type to listen for and receive event data of this type. Only inherited from <see cref="IGameEvent"/>.</param>
        /// <param name="callback">Async callback delegate with a <see cref="IGameEvent"/> parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Type contract, Func<IGameEvent, UniTask> callback, CancellationToken? token = null, int? order = null);
        /// <summary>
        /// Listen for all types of events and receive async callbacks.
        /// </summary>
        /// <param name="callback">Async callback delegate without any parameters.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Func<UniTask> callback, CancellationToken? token = null, int? order = null);
        /// <summary>
        /// Listen for all types of events and receive async callbacks in the form of a <see cref="IGameEvent"/>.
        /// </summary>
        /// <param name="callback">Async callback delegate with a <see cref="IGameEvent"/> parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Func<IGameEvent, UniTask> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
        IDisposable Subscribe<T>(Func<T, UniTask> callback, CancellationToken? token = null, int? order = null) where T : IGameEvent;
    }
}