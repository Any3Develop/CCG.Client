using System;
using System.Collections.Generic;
using System.Linq;
using Services.UIService.Abstractions;

namespace Services.UIService
{
	public class UIFullFadePresenter : IUIFullFadePresenter, IDisposable
	{
		private readonly List<IUIWindow> showed = new();
		private IUIService uiService;
		private IUIWindow current;
		
		public void Init(IUIService service)
		{
			uiService = service;
		}
		
		public void Dispose()
		{
			uiService = null;
			current = null;
			showed.Clear();
		}

		public void OnDeleted(IUIWindow window)
		{
			if (window is not IFullFadeTarget)
				return;

			showed.Remove(window);
			TryShowLast();
		}

		public void OnShow(IUIWindow window)
		{
			if (window is not IFullFadeTarget)
				return;

			if (!showed.Contains(window))
				showed.Add(window);
			
			Show(window);
		}

		public void OnHidden(IUIWindow window)
		{
			if (window is not IFullFadeTarget)
				return;

			showed.Remove(window);
			TryShowLast();
		}

		private bool TryHide()
		{
			showed.RemoveAll(x => x == null);
			if (showed.Count != 0)
				return false;

			Hide();
			current = null;
			return true;
		}

		private void TryShowLast()
		{
			if (TryHide())
				return;

			current = showed.Last();
			Show(current);
		}

		private void Show(IUIWindow window)
		{
			var fadeTarget = (IFullFadeTarget) window;
			var fadeWindow = uiService.Get<UIFullFadeWindow>();
			fadeWindow.SetColor(fadeTarget.FadeColor);
			fadeWindow.SetCloseAction(fadeTarget.OnFadeClick);

			uiService.Show<UIFullFadeWindow>()
				.WithParent(window.Parent)
				.WithOrder(0)
				.Execute();
		}

		private void Hide() => uiService.Hide<UIFullFadeWindow>().Execute();
	}
}