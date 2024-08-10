using UnityEngine;

namespace Services.UIService.Abstractions
{
	public interface IFullFadeTarget
	{
		Color? FadeColor { get; }
		void OnFadeClick();
	}
}