#if TEXTMESHPRO
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace RR.UIService.AudioSource
{
    public class UITMPInputAudioHandler : UIAudioHandlerBase<UITMPInputAudioConfig>
    {
        protected List<TMP_InputField> ListenComponents = new();
        protected string CurrentInput = string.Empty;
        
        protected override void OnInit() => GetComponents(ref ListenComponents);

        protected override void OnDisposed()
        {
            if (ListenComponents != null)
                OnDisabled();
            
            ListenComponents = null;
            CurrentInput = null;
        }

        protected override void OnEnabled()
        {
            CurrentInput = string.Empty;
            
            foreach (var component in ListenComponents.Where(component => component))
            {
                component.onSelect.AddListener(OnSelected);
                component.onSubmit.AddListener(OnSubmited);
                component.onDeselect.AddListener(OnDeselected);
                component.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected override void OnDisabled()
        {
            foreach (var component in ListenComponents.Where(component => component))
            {
                component.onSelect.RemoveListener(OnSelected);
                component.onSubmit.RemoveListener(OnSubmited);
                component.onDeselect.RemoveListener(OnDeselected);
                component.onValueChanged.RemoveListener(OnValueChanged);
            }
            
            CurrentInput = string.Empty;
        }
        
        private void OnSelected(string value)
        {
            if (!Initialized || !Enabled)
                return;
            
            CurrentInput = value;
            PlayAudioClip(Config.SelectAudio);
        }
        
        private void OnDeselected(string value)
        {
            if (!Initialized || !Enabled)
                return;
            
            CurrentInput = string.Empty;
            PlayAudioClip(Config.DeselectAudio);
        }
        
        protected virtual void OnValueChanged(string value)
        {
            if (!Initialized || !Enabled)
                return;

            PlayAudioClip(CurrentInput.Length > value.Length ? Config.WriteAudio : Config.EraseAudio);
            CurrentInput = value;
        }
        
        protected virtual void OnSubmited(string value)
        {
            if (!Initialized || !Enabled)
                return;

            PlayAudioClip(Config.SubmitAudio);
        }

        protected virtual void GetComponents<T>(ref List<T> components)
        {
            components ??= new List<T>();
            if (Config.IncludeInherited)
            {
                components.AddRange(Window.Content.GetComponentsInChildren<T>(Config.IncludeDisabled));
                return;
            }

            var specifiedType = typeof(T);  // to ensure the components are not inherited.
            components.AddRange(Window.Content
	            .GetComponentsInChildren<T>(Config.IncludeDisabled)
	            .Where(x => x.GetType() == specifiedType));
        }
    }
}
#endif