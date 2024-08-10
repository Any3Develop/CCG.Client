using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Abstractions.Game.Context.EventSource
{
    public interface IEventsSource : IDisposable
    {
        /// <summary>
        /// Listen for a specific event data type and receive callbacks in the form of an object.
        /// </summary>
        /// <param name="contract">Specific event data type to listen for and receive event data of this type.</param>
        /// <param name="callback">Callback delegate with an object parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Type contract, Action<object> callback, CancellationToken? token = null, int? order = null);
        
        /// <summary>
        /// Listen for all types of events and receive callbacks.
        /// </summary>
        /// <param name="callback">Callback delegate without any parameters.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Action callback, CancellationToken? token = null, int? order = null);
        
        /// <summary>
        /// Listen for all types of events and receive callbacks in the form of an event data object.
        /// </summary>
        /// <param name="callback">Callback delegate with an object parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Action<object> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Action<T> callback, CancellationToken? token = null, int? order = null);
        
        
        /// <summary>
        /// Listen for a specific event data type and receive async callbacks in the form of an object.
        /// </summary>
        /// <param name="contract">Specific event data type to listen for and receive event data of this type.</param>
        /// <param name="callback">Async callback delegate with an object parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Type contract, Func<object, Task> callback, CancellationToken? token = null, int? order = null);
        
        /// <summary>
        /// Listen for all types of events and receive async callbacks.
        /// </summary>
        /// <param name="callback">Callback delegate with an object parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Func<Task> callback, CancellationToken? token = null, int? order = null);
        
        /// <summary>
        /// Listen for all types of events and receive async callbacks in the form of an event data object.
        /// </summary>
        /// <param name="callback">Callback delegate with an object parameter as the event data.</param>
        /// <param name="token">Use it to unsubscribe from an event.</param>
        /// <param name="order">Subscribe to an event with a specified listening order. By default it is the most recent one.</param>
        /// <returns>Subscription object to unsubscribe from an event.</returns>
        IDisposable Subscribe(Func<object, Task> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<Task> callback, CancellationToken? token = null, int? order = null);
        IDisposable Subscribe<T>(Func<T, Task> callback, CancellationToken? token = null, int? order = null);
    }
}