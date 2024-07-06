using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public class UIAppearAnimation : UIAnimationBase
    {
        [SerializeField] protected Vector3 fromPosition;
        [SerializeField] protected Vector3 toPosition;
        [SerializeField] protected bool beginFromCurrent = true;
        private Tween appearTween;
        
#if UNITY_EDITOR
        [ContextMenu("BindFromPosition")]
        public void BindFromPosition()
        {
            fromPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        [ContextMenu("BindToPosition")]
        public void BindToPosition()
        {
            toPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        
        [ContextMenu("Swap Positions")]
        public void SwapPositions()
        {
            (fromPosition, toPosition) = (toPosition, fromPosition);
        }
#endif
        
        protected override async UniTask OnPlayAsync()
        {
            await ResetAsync();
            appearTween = Window.Container
                .DOAnchorPos(toPosition, animationData.Duration)
                .SetDelay(animationData.Delay)
                .SetEase(animationData.Ease);

            await appearTween.Play().ToUniTask();
        }

        protected override UniTask OnResetAsync()
        {
            appearTween?.Kill();
            appearTween = null;
            if (!beginFromCurrent && Window?.Container)
                Window.Container.anchoredPosition = fromPosition;
            
            return UniTask.CompletedTask;
        }
        
        protected override void OnDisposed()
        {
            appearTween?.Kill();
            appearTween = null;
        }
    }
}