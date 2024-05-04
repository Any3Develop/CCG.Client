using System;
using DG.Tweening;
using UnityEngine;

namespace CardGame.Services.UIService
{
    public class UIWindowPopupAnimation : UIBaseAnimation
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

            var settings = _settings;
            
            _t = _body
                .DOScale(_to, settings.durationIn)
                .SetDelay(settings.delay)
                .SetEase(settings.easeIn)
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
            
            var settings = _settings;
            
            _t = _body
                .DOScale(_from, settings.durationOut)
                .SetDelay(settings.delay)
                .SetEase(settings.easeOut)
                .OnComplete(() =>
                {
                    onEnd?.Invoke();
                });
        }

        public override void ResetValues()
        {
            Stop();
            _t?.Kill();
            _body.localScale = _from;
            _t = null;
        }

        private void Stop()
        {
            _t?.Pause();
        }
    }
}