using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Services.UIService.Options
{
	public interface IUIOptions<out T> : IDisposable where T : IUIWindow
	{
		/// <summary>
		/// At runtime, moves to the specified parent and index.
		/// </summary>
		/// <param name="parent">By default it has a parent, it depends on the action: <see cref="Show"/> + <see cref="IUIRoot.MiddleContainer"/>, <see cref="Hide"/> + <see cref="IUIRoot.DeactivatedContainer"/>.</param>
		/// <param name="index">Display order or sibling index. It has the highest priority by default.</param>
		IUIOptions<T> WithMove(Transform parent = null, int? index = null);
        
		/// <summary>
		/// Do not interrupt previous actions. By default interrupts.
		/// </summary>
		IUIOptions<T> WithNoInterrupt();
        
		/// <summary>
		/// Turn off playing all animations?
		/// </summary>
		IUIOptions<T> WithNoAnimation(bool value = true);
		
		/// <summary>
		/// Callback when a window ready to be initialized.
		/// </summary>
		IUIOptions<T> WithInitAsync(Func<T, Task> callBack);
		
		/// <summary>
		/// Callback when a window ready to be initialized.
		/// </summary>
		IUIOptions<T> WithInit(Action<T> callBack);
		
		/// <summary>
		/// Specify in which group to find a window. By default whole the service.
		/// </summary>
		IUIOptions<T> WithGroup(object value);
		
		/// <summary>
		/// With a unique window identifier, you can separate duplicate types and refer to a specific instance.
		/// </summary>
		IUIOptions<T> WithId(string value);
		
		/// <summary>
		/// Finalising the option to show the window asynchronously.
		/// </summary>
		Task ShowAsync(CancellationToken token = default);
		
		/// <summary>
		/// Finalising the option to hide the window asynchronously.
		/// </summary>
		Task HideAsync(CancellationToken token = default);
		
		/// <summary>
		/// Finalising the option to show the window asynchronously.
		/// </summary>
		void Show(Action onComplete = null);
		
		/// <summary>
		/// Finalising the option to hide the window asynchronously.
		/// </summary>
		void Hide(Action onComplete = null);
	}
}