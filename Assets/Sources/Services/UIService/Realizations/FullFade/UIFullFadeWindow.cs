using System;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Services.UIService.FullFade
{
	public class UIFullFadeWindow : UIWindowBase
	{
		[SerializeField] protected Button closeButton;
		[SerializeField] protected Image fadeImage;
		private Color defaultColor;

		protected override void OnInit()
		{
			if (fadeImage)
				defaultColor = fadeImage.color;
		}

		public void SetCloseAction(Action value)
		{
			if (!closeButton)
				return;
			
			closeButton.onClick.RemoveAllListeners();
			closeButton.onClick.AddListener(() => value?.Invoke());
		}

		public void SetColor(Color? value)
		{
			if (!fadeImage)
				return;

			fadeImage.color = value ?? defaultColor;
		}

		public void Clear()
		{
			if (closeButton)
				closeButton.onClick.RemoveAllListeners();
			
			if (fadeImage)
				fadeImage.color = defaultColor;
		}

		public override void Hidden()
		{
			Clear();
		}
	}
}