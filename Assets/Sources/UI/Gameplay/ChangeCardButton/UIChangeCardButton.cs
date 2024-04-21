using System;
using CardGame.Services.InputService;
using CardGame.Services.UIService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardGame.UI
{
    public class UIChangeCardButton : UISimpleWindow
    {
        public event EventHandler ClickEvent;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        private IInputController<UIInputLayer> _inputController;
        [Inject]
        private void Inject(IInputController<UIInputLayer> uiInputController)
        {
            _inputController = uiInputController;
        }
        
        public void SetButtonText(string value)
        {
            _buttonText.text = value;
        }

        public override void Show()
        {
            base.Show();
            _button.onClick.AddListener(OnClickEventHandler);
            _inputController.LockChangedEvent += OnInputControllerLockChangedEvent;
        }

        public override void Hide()
        {
            base.Hide();
            _button.onClick.RemoveListener(OnClickEventHandler);
            _inputController.LockChangedEvent -= OnInputControllerLockChangedEvent;
            ClickEvent = null;
        }

        private void OnClickEventHandler()
        {
            ClickEvent?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnInputControllerLockChangedEvent(object sender, EventArgs e)
        {
            _button.interactable = !_inputController.Locked;
        }
    }
}