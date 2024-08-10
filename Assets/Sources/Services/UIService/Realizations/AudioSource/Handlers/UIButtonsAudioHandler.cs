using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Services.UIService
{
    public class UIButtonsAudioHandler : UIAudioHandlerBase<UIButtonsAudioConfig>
    {
        protected List<Button> ListenButtons = new();

        protected override void OnInit() => GetComponents(ref ListenButtons);

        protected override void OnDisposed()
        {
            if (ListenButtons != null)
                OnDisabled();

            ListenButtons = null;
        }

        protected override void OnEnabled()
        {
            foreach (var component in ListenButtons.Where(component => component))
                component.onClick.AddListener(OnClicked);
        }

        protected override void OnDisabled()
        {
            foreach (var component in ListenButtons.Where(component => component))
                component.onClick.RemoveListener(OnClicked);
        }

        protected virtual void OnClicked()
        {
            if (!Initialized || !Enabled)
                return;

            PlayAudioClip(Config.ClickAudio);
        }

        protected virtual void GetComponents<T>(ref List<T> components)
        {
            components ??= new List<T>();
            if (Config.IncludeInherited)
            {
                components.AddRange(Window.Content.GetComponentsInChildren<T>(Config.IncludeDisabled));
                return;
            }

            components.AddRange(Window.Content
                .GetComponentsInChildren<T>(Config.IncludeDisabled)
                .Where(x => x.GetType() == typeof(T))); // to ensure the buttons are not inherited.
        }
    }
}