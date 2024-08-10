#if TEXTMESHPRO
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Services.UIService
{
    public class UITMPDropDownAudioHandler : UIAudioHandlerBase<UITMPDropDownAudioConfig>
    {
        protected List<TMP_Dropdown> ListenComponents = new();
        
        protected override void OnInit() => GetComponents(ref ListenComponents);

        protected override void OnDisposed()
        {
            if (ListenComponents != null)
                OnDisabled();
            
            ListenComponents = null;
        }

        protected override void OnEnabled()
        {
            foreach (var component in ListenComponents.Where(component => component))
                component.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisabled()
        {
            foreach (var component in ListenComponents.Where(component => component))
                component.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(int value)
        {
            if (!Initialized || !Enabled)
                return;

            PlayAudioClip(Config.SelectAudio);
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
#endif