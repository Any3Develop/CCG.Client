using UnityEngine;

namespace Client.Services.UIService.FullFade
{
	public interface IFullFadeTarget
	{
		RectTransform Parent { get; }
		Color? FadeColor { get; }
		void OnFadeClick();
	}
}