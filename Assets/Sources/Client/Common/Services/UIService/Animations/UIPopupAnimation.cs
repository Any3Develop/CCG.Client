using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public class UIPopupAnimation : UIAnimationBase
    {
        [SerializeField] protected Vector3 fromSize = Vector3.zero;
        [SerializeField] protected Vector3 toSize = Vector3.one;
        [SerializeField] protected bool beginFromCurrent = true;
        private Tween popupTween;
        
#if UNITY_EDITOR
        [ContextMenu("Swap Size")]
        public void SwapSize()
        {
            (fromSize, toSize) = (toSize, fromSize);
        }
#endif
        
        protected override async UniTask OnPlayAsync()
        {
            await Window.Container
                .DOScale(toSize, animationData.Duration)
                .SetDelay(animationData.Delay)
                .SetEase(animationData.Ease)
                .Play()
                .ToUniTask();
        }

        protected override UniTask OnResetAsync()
        {
            popupTween?.Kill();
            popupTween = null;
            if (!beginFromCurrent && Window?.Container)
                Window.Container.localScale = fromSize;
            
            return UniTask.CompletedTask;
        }

        protected override void OnDisposed()
        {
            popupTween?.Kill();
            popupTween = null;
        }
    }
}