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
#endif
        
        protected override async UniTask OnPlayAsync()
        {
            await ResetAsync();
            var destination = animationData.Reversed ? fromPosition : toPosition;
            appearTween = Window.Container
                .DOAnchorPos(destination, animationData.Duration)
                .SetDelay(animationData.Delay)
                .SetEase(animationData.Ease)
                .SetAutoKill(false);

            await appearTween.Play().ToUniTask();
        }

        protected override UniTask OnResetAsync()
        {
            appearTween?.Kill();
            appearTween = null;
            if (!beginFromCurrent)
            {
                var from = animationData.Reversed ? toPosition : fromPosition;
                Window.Container.anchoredPosition = from;
            }
            
            return UniTask.CompletedTask;
        }
        
        protected override void OnDisposed()
        {
            appearTween?.Kill();
            appearTween = null;
        }
    }
}