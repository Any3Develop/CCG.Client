#if !DOTWEEN
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace RR.UIService
{
    public class UIAppearUnityAnimation : UIAnimationBase<UIAppearAnimationConfig>
    {
        private Coroutine tween;

        protected override void OnPlay(Action onCompleted, CancellationToken token)
        {
            OnReset(() => tween = UIAnimationCoroutineRunner.StartCoroutine(OnPlayAsync(onCompleted, token)), token);
        }

        private IEnumerator OnPlayAsync(Action onCompleted, CancellationToken token)
        {
            if (Config.Delay > 0)
                yield return new WaitForSeconds(Config.Delay);

            var content = Window.Content;

            if (!content || token.IsCancellationRequested)
                EndAnimation();

            var elapsedTime = 0f;
            var duration = Config.Duration;
            var ease = Config.EaseCurve;
            var from = content.anchoredPosition;
            var to = Config.ToPosition.GetAllowed(from);
            while (Application.isPlaying && elapsedTime < duration && content && !token.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;
                var t = ease.Evaluate(elapsedTime / duration);

                if (!content || token.IsCancellationRequested)
                    EndAnimation();

                content.anchoredPosition = Vector3.LerpUnclamped(from, to, t);
                yield return null;
            }

            EndAnimation(to);

            yield break;

            void EndAnimation(Vector3? end = default)
            {
                if (end.HasValue && content)
                    content.anchoredPosition = end.Value;

                OnStop(null, token);
                onCompleted?.Invoke();
            }
        }

        protected override void OnStop(Action onCompleted, CancellationToken token)
        {
            UIAnimationCoroutineRunner.StopCoroutine(tween);
            tween = null;
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

            var destination = Config.FromPosition.GetAllowed(Window.Content.anchoredPosition);
            Window.Content.anchoredPosition = destination;
            onCompleted?.Invoke();
        }

        protected override void OnDisposed() => OnStop(null, CancellationToken.None);
    }
}
#endif