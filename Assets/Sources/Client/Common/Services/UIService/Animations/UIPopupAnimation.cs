using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.Common.Services.UIService.Animations
{
    public class UIPopupAnimation : UIAnimationBase
    {
        [SerializeField] protected bool reversed;

        protected override async UniTask OnPlayAsync(bool forceEnd = false)
        {
            var destination = reversed ? Vector3.zero : Vector3.one;
            await Window.Container
                .DOScale(destination, Presset.Duration)
                .SetDelay(Presset.StartDelay)
                .SetEase(Presset.Ease)
                .SetAutoKill(false)
                .Play()
                .ToUniTask();
        }

        protected override async UniTask OnStopAsync(bool forceEnd = false)
        {
            var destination = reversed ? Vector3.zero : Vector3.one;
            await Window.Container
                .DOScale(destination, 0)
                .SetDelay(Presset.StopDelay)
                .SetEase(Presset.Ease)
                .SetAutoKill(false)
                .Play()
                .ToUniTask();
        }

        protected override void OnRestart()
        {
            if (Window?.Container)
                Window.Container.DOKill();
        }
    }
}