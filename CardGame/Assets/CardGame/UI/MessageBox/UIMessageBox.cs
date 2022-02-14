using System;
using CardGame.Services.UIService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.UI
{
    public class UIMessageBox : UISimpleWindow
    {
        public event EventHandler AcceptEvent;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _acceptButtonText;

        public override void Show()
        {
            base.Show();
            _acceptButton.onClick.AddListener(OnAcceptEvent);
        }

        public override void Hide()
        {
            base.Hide();
            _acceptButton.onClick.RemoveListener(OnAcceptEvent);

            AcceptEvent = null;
        }

        public void SetAcceptButtonText(string value)
        {
            _acceptButtonText.text = value;
        }
        
        public void SetHeaderText(string value)
        {
            _headerText.text = value;
        }
        
        public void SetMessageText(string value)
        {
            _messageText.text = value;
        }

        private void OnAcceptEvent()
        {
            AcceptEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
