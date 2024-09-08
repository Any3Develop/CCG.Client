using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Client.Services.UIService
{
    internal class UILegacyInputListener : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public event Action<InputField> OnSelected;
        public event Action<InputField> OnDeselected;
        private InputField component;

        public void Init()
        {
            if (component)
                return;

            component ??= GetComponent<InputField>();
        }

        public void Dispose()
        {
            component = null;
            OnSelected = null;
            OnDeselected = null;
        }

        private void OnDestroy()
            => Dispose();

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            if (!component)
                return;

            OnSelected?.Invoke(component);
        }

        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            if (!component)
                return;

            OnDeselected?.Invoke(component);
        }
    }

    public class UILeagcyInputAudioHandler : UIAudioHandlerBase<UILegacyInputAudioConfig>
    {
        protected List<InputField> ListenComponents = new();
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
                if (!component.gameObject.TryGetComponent(out UILegacyInputListener listener))
                    listener = component.gameObject.AddComponent<UILegacyInputListener>();

                listener.Init();
                listener.OnSelected += OnSelected;
                listener.OnDeselected += OnDeselected;
                component.onSubmit.AddListener(OnSubmited);
                component.onValueChanged.AddListener(OnValueChanged);
            }
        }

        protected override void OnDisabled()
        {
            foreach (var component in ListenComponents.Where(component => component))
            {
                if (component.gameObject.TryGetComponent(out UILegacyInputListener listener))
                {
                    listener.Dispose();
                    Object.Destroy(listener);
                }

                component.onSubmit.RemoveListener(OnSubmited);
                component.onValueChanged.RemoveListener(OnValueChanged);
            }

            CurrentInput = string.Empty;
        }

        private void OnSelected(InputField inputField)
        {
            if (!Initialized || !Enabled || !inputField)
                return;

            CurrentInput = inputField.text;
            PlayAudioClip(Config.SelectAudio);
        }


        private void OnDeselected(InputField inputField)
        {
            if (!Initialized || !Enabled || !inputField)
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