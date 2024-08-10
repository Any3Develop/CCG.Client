#if DOTWEEN
using System;
using System.Threading;
using DG.Tweening;

namespace Services.UIService
{
    public class UIPopupDoTweenAnimation : UIAnimationBase<UIPopupAnimationConfig>
    {
        private Tween tween;

        protected override void OnPlay(Action onCompleted, CancellationToken token)
        {
            OnReset(null, token);
            tween = Window.Content
                .DOScale(Config.ToSize.GetAllowed(Window.Content.localScale), Config.Duration)
                .SetDelay(Config.Delay);

            tween = Config.UseEase
                ? tween.SetEase(Config.Ease)
                : tween.SetEase(Config.EaseCurve);

            tween.OnComplete(EndAnimation)
                .OnKill(EndAnimation)
                .Play();

            if (token.CanBeCanceled)
                token.Register(() => OnStop(null, token));

            return;

            void EndAnimation()
            {
                if (onCompleted == null)
                    return;

                var mem = onCompleted;
                onCompleted = null;
                
                OnStop(null, token);
                mem?.Invoke();
            }
        }

        protected override void OnStop(Action onCompleted, CancellationToken token)
        {
            if (tween == null)
            {
                onCompleted?.Invoke();
                return;
            }

            var mem = tween;
            tween = null;
            mem.Kill();
            onCompleted?.Invoke();
        }

        protected override void OnReset(Action onCompleted, CancellationToken token)
        {
            OnStop(null, token);
            if (!Window?.Content)
            {
                onCompleted?.Invoke();
                return;
            }

            var destination = Config.FromSize.GetAllowed(Window.Content.localScale);
            Window.Content.localScale = destination;
            onCompleted?.Invoke();
        }

        protected override void OnDisposed() => OnStop(null, CancellationToken.None);
    }
}
#endif