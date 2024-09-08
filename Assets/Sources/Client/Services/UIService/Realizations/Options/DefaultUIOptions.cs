using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Client.Services.UIService.Data;
using Client.Services.UIService.FullFade;
using UnityEngine;

namespace Client.Services.UIService.Options
{
	public class DefaultUIOptions<T> : IUIOptions<T> where T : IUIWindow
	{
		private readonly IUIRoot uiRoot;
		private readonly IUIService uiService;
		private readonly IUIFullFadePresenter fullFade;
		private readonly IUIServiceRepository serviceRepository;

		private CancellationTokenSource activeTask;
		private Func<T, Task> onInitCallBack;
		private Transform parent;
		private bool noInterrupts;
		private bool noAnimations;
		private string windowId;
		private string groupId;
		private bool disposed;
		private int? order;

		public DefaultUIOptions(
			IUIRoot uiRoot, 
			IUIService uiService, 
			IUIFullFadePresenter fullFade,
			IUIServiceRepository serviceRepository)
		{
			this.uiRoot = uiRoot;
			this.uiService = uiService;
			this.fullFade = fullFade;
			this.serviceRepository = serviceRepository;
		}
		
		public void Dispose()
		{
			if (disposed)
				return;

			disposed = true;
			activeTask?.Cancel();
			activeTask?.Dispose();
			onInitCallBack = null;
			activeTask = null;
			parent = null;
		}

		public IUIOptions<T> WithMove(Transform container = null, int? index = null)
		{
			if (disposed)
				return this;

			parent = container;
			order = index;
			return this;
		}

		public IUIOptions<T> WithNoInterrupt()
		{
			if (disposed)
				return this;

			noInterrupts = true;
			return this;
		}

		public IUIOptions<T> WithNoAnimation(bool value = true)
		{
			if (disposed)
				return this;

			noAnimations = value;
			return this;
		}

		public IUIOptions<T> WithInitAsync(Func<T, Task> callBack)
		{
			if (disposed)
				return this;
			
			onInitCallBack = window => callBack?.Invoke(window);
			return this;
		}

		public IUIOptions<T> WithInit(Action<T> callBack)
		{
			if (disposed)
				return this;
			
			onInitCallBack = window =>
			{
				callBack?.Invoke(window);
				return Task.CompletedTask;
			};
			return this;
		}

		public IUIOptions<T> WithGroup(object value)
		{
			if (disposed)
				return this;

			groupId = value?.ToString();
			return this;
		}

		public IUIOptions<T> WithId(string value)
		{
			if (disposed)
				return this;

			windowId = value;
			return this;
		}

		public async Task ShowAsync(CancellationToken token = default)
		{
			if (disposed)
				return;
			
			if (!TryGetItems(out var windowItems))
			{
				Finalize(windowItems);
				return;
			}
			
			try
			{
				activeTask = token.CanBeCanceled
					? CancellationTokenSource.CreateLinkedTokenSource(token)
					: new CancellationTokenSource();
		        
				token = activeTask.Token;
				await Task.WhenAll(windowItems.Select(async windowItem =>
				{
					if (!noInterrupts)
						windowItem.ActiveTask?.Dispose();
					
					var window = windowItem.Window;
					windowItem.ActiveTask = this;
					window.AnimationSource.Enable(!noAnimations);
					
					if (onInitCallBack != null)
						await onInitCallBack((T)window);
					
					uiService.Move(window, GetParentOrDefault(uiRoot.MiddleContainer), order);
			        
					// always resize to screen size
					window.Parent.offsetMin = Vector2.zero;
					window.Parent.offsetMax = Vector2.zero;
			        
					window.Show();
					fullFade.OnShow(window);
					await window.AnimationSource.ExecuteAsync(UIAnimationExtensions.SHOW_EVENT, token);

					window.Showed();
					await window.AnimationSource.ExecuteAsync(UIAnimationExtensions.SHOWED_EVENT, token);
				}));
				token.ThrowIfCancellationRequested();
			}
			catch (OperationCanceledException)
			{
				// Nothing to do
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
			finally
			{
				Finalize(windowItems);
			}
		}
        
		public async Task HideAsync(CancellationToken token = default)
		{
			if (disposed)
				return;
			
			if (!TryGetItems(out var windowItems))
			{
				Finalize(windowItems);
				return;
			}
			
			try
			{
				activeTask = token.CanBeCanceled
					? CancellationTokenSource.CreateLinkedTokenSource(token)
					: new CancellationTokenSource();
		        
				token = activeTask.Token;
				await Task.WhenAll(windowItems.Select(async windowItem =>
				{
					if (!noInterrupts)
						windowItem.ActiveTask?.Dispose();
					
					var window = windowItem.Window;
					windowItem.ActiveTask = this;
					window.AnimationSource.Enable(!noAnimations);
					
					if (onInitCallBack != null)
						await onInitCallBack((T)window);
					
					window.Hide();
					await window.AnimationSource.ExecuteAsync(UIAnimationExtensions.HIDE_EVENT, token);
					uiService.Move(window, GetParentOrDefault(uiRoot.DeactivatedContainer), order);

					window.Hidden();
					fullFade.OnHidden(window);
					await window.AnimationSource.ExecuteAsync(UIAnimationExtensions.HIDDEN_EVENT, token);
				}));
				token.ThrowIfCancellationRequested();
			}
			catch (OperationCanceledException)
			{
				// Nothing to do
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
			finally
			{
				Finalize(windowItems);
			}
		}
		
		public void Show(Action onComplete = null)
		{
			if (disposed)
				return;
			
			ShowAsync()
				.ContinueWith(_ => onComplete?.Invoke())
				.GetAwaiter();
		}
        
		public void Hide(Action onComplete = null)
		{
			if (disposed)
				return;
			
			HideAsync()
				.ContinueWith(_ => onComplete?.Invoke())
				.GetAwaiter();
		}
        
		private bool TryGetItems(out WindowItem[] result)
		{
			result = default;
			if (!string.IsNullOrEmpty(windowId))
			{
				var windowItemByid = serviceRepository.Get<T>(windowId, groupId);
				if (windowItemByid == null)
					return false;
				
				result = new[] {windowItemByid};
				return true;
			}
			
			result = serviceRepository.GetAll<T>(groupId).ToArray();
			return result.Length > 0;
		}
		
		private void Finalize(IEnumerable<WindowItem> selected)
		{
			if (selected != null)
			{
				foreach (var windowItem in selected.Where(x => x.Window?.Parent && ReferenceEquals(x.ActiveTask, this)))
					windowItem.ActiveTask = null;
			}
			
			Dispose();
		}
		
		private Transform GetParentOrDefault(Transform defaultParent)
			=> parent ? parent : defaultParent;
	}
}