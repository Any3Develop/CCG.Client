using System;
using DG.Tweening;
using UnityEngine;

namespace CardGame.Services.UIService
{
    public class UIWindowAppearAnimation : UIBaseAnimation
    {
        private Tween _t;
        // todo: temp solution for check  animation 
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Play();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Backward();
            }
        }

        public override void Play(Action onEnd = null)
        {
            Stop();
            
            _t = _body
                .DOAnchorPos(_to, _settings.durationIn)
                .SetDelay(_settings.delay)
                .SetEase(_settings.easeIn)
                .OnComplete(() =>
                {
                    onEnd?.Invoke();
                });
            
            _t.SetAutoKill(false);
        }

        public override void Backward(Action onEnd = null)
        {
            if (_t == null)
            {
                return;
            }
            
            Stop();
            
            _t = _body
                .DOAnchorPos(_from, _settings.durationOut)
                .SetDelay(_settings.delay)
                .SetEase(_settings.easeOut)
                .OnComplete(() =>
                {
                    onEnd?.Invoke();
                });
        }

        public override void ResetValues()
        {
            Stop();

            _t?.Kill();
            _body.anchoredPosition = _from;
            _t = null;
        }

        private void Stop()
        {
            _t?.Pause();
        }
    }
}