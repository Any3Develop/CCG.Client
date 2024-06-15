using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public class UIPopupAnimation : UIAnimationBase
    {
        [SerializeField] protected Vector3 fromSize;
        [SerializeField] protected Vector3 toSize;
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
                .SetAutoKill(false)
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