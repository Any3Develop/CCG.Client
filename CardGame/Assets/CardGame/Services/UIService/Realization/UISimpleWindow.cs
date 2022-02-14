using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CardGame.Services.UIService
{
    public abstract class UISimpleWindow : UIWindow
    {
        [SerializeField] protected UIBaseAnimation _animation;
        [SerializeField] private UnityEvent _onShow;
        [SerializeField] private UnityEvent _onHide;
        public event EventHandler CloseEvent;
        
        protected virtual void Awake()
        {
            gameObject.SetActive(false);

            if (_animation != null)
            {
                _animation.ResetValues();
            }
        }

        public override void Show()
        {
            if (_animation != null)
                _animation.ResetValues();
            
            gameObject.SetActive(true);

            if (_animation != null)
                _animation.Play();

            DOVirtual.DelayedCall(0.5f, () => _onShow?.Invoke()); // a bit pause, need refactoring
        }

        public override void Hide()
        {
            if (_animation != null)
            {
                _animation.Backward(OnHided);
            }
            else
            {
                OnHided();
            }
            _onHide?.Invoke();
        }

        public virtual void Close()
        {
            CloseEvent?.Invoke(this, EventArgs.Empty);
            CloseEvent = null;
        }

        protected override void OnHided()
        {
            gameObject.SetActive(false);
            base.OnHided();
        }
    }
}