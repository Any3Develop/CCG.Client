using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Services.UIService.FullFade
{
	public class UIFullFadePresenter : IUIFullFadePresenter, IDisposable
	{
		private readonly List<IFullFadeTarget> showed = new();
		private IUIService uiService;
		
		public void Init(IUIService service)
		{
			uiService = service;
		}
		
		public void Dispose()
		{
			uiService = null;
			showed.Clear();
		}

		public void OnDeleted(IUIWindow window)
		{
			if (window is not IFullFadeTarget fadeTarget)
				return;

			showed.Remove(fadeTarget);
			TryShowLast();
		}

		public void OnShow(IUIWindow window)
		{
			if (window is not IFullFadeTarget fadeTarget)
				return;

			if (!showed.Contains(fadeTarget))
				showed.Add(fadeTarget);
			
			Show(fadeTarget);
		}

		public void OnHidden(IUIWindow window)
		{
			if (window is not IFullFadeTarget fadeTarget)
				return;

			showed.Remove(fadeTarget);
			TryShowLast();
		}

		private bool TryHide()
		{
			showed.RemoveAll(x => !x?.Parent);
			if (showed.Count != 0)
				return false;

			uiService.Begin<UIFullFadeWindow>().Hide();
			return true;
		}

		private void TryShowLast()
		{
			if (TryHide())
				return;

			Show(showed.Last());
		}

		private void Show(IFullFadeTarget fadeTarget)
		{
			uiService.Begin<UIFullFadeWindow>()
				.WithMove(fadeTarget.Parent, 0)
				.WithInit(InitWindow)
				.Show();
			
			return;
			void InitWindow(UIFullFadeWindow window)
			{
				window.SetColor(fadeTarget.FadeColor);
				window.SetCloseAction(fadeTarget.OnFadeClick);
			}
		}
	}
}