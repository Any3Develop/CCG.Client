using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Services.UIService.Abstractions
{
    public interface IDisplayOption : IDisposable
    {
        /// <summary>
        /// The method moves to the specified parent at runtime.
        /// </summary>
        IDisplayOption WithParent(Transform value);

        /// <summary>
        /// Cancels this asynchronous action with an external token.
        /// </summary>
        IDisplayOption WithToken(CancellationToken value);
        
        /// <summary>
        /// Do not interrupt previous actions. By default interrupts.
        /// </summary>
        IDisplayOption WithNoInterrupt();
        
        /// <summary>
        /// Custom display order. It has the highest priority by default.
        /// </summary>
        IDisplayOption WithOrder(int value);
        
        /// <summary>
        /// Turn off playing all animations?
        /// </summary>
        /// <param name="value">Callback when service actions are completed.</param>
        IDisplayOption WithNoAnimation(bool value = true);
        
        /// <summary>
        /// Starts an asynchronous action with the passed arguments.
        /// </summary>
        Task ExecuteAsync();
        
        /// <summary>
        /// Starts an asynchronous action with the passed arguments.
        /// </summary>
        /// <param name="onComplete">Callback when service actions are completed.</param>
        void Execute(Action onComplete = null);
    }
}