using Client.Common.Services.UIService;
using TMPro;
using UnityEngine;

namespace CardGame.UI
{
    public class UIUpdate : UISimpleWindow
    {
        [SerializeField] private TextMeshProUGUI _progressText;

        public void SetProgressText(string value)
        {
            _progressText.text = value;
        }
    }
}
