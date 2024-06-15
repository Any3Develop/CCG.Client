using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public class UIPopupAnimation : UIAnimationBase
    {
        [SerializeField] protected bool beginFromCurrent = true;
        private Tween popupTween;
        
        protected override async UniTask OnPlayAsync()
        {
            var destination = animationData.Reversed ? Vector3.one : Vector3.zero;
            await Window.Container
                .DOScale(destination, animationData.Duration)
                .SetDelay(animationData.Delay)
                .SetEase(animationData.Ease)
                .SetAutoKill(false)
                .Play()
                .ToUniTask();
        }

        protected override UniTask OnResetAsync()
        {
            popupTween?.Kill();
            popupTween = null;
            if (!beginFromCurrent)
            {
                var from = animationData.Reversed ? Vector3.zero : Vector3.one;
                Window.Container.localScale = from;
            }
            
            return UniTask.CompletedTask;
        }

        protected override void OnDisposed()
        {
            popupTween?.Kill();
            popupTween = null;
        }
    }
}