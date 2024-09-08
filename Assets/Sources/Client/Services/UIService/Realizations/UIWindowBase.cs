using System;
using System.Linq;
using UnityEngine;

namespace Client.Services.UIService
{
    public abstract class UIWindowBase : MonoBehaviour, IUIWindow
    {
        [SerializeField] protected RectTransform parent;
        [SerializeField] protected RectTransform content;
        protected bool Initialzied { get; private set; }

        public string Id { get; private set; }
        public event Action OnChanged;
        public IUIAnimationSource AnimationSource { get; private set; }
        public IUIAudioSource AudioSource { get; private set; }
        public RectTransform Parent => parent;
        public RectTransform Content => content;

        public UIWindowBase Init(
	        string id,
            IUIAnimationSource animationSource,
            IUIAudioSource audioSource)
        {
	        Id = id;
            AnimationSource = animationSource;
            AudioSource = audioSource;
            Initialzied = true;

            OnFixContainers();
            OnInit();
            return this;
        }

        public virtual void Show() {}
        
        public virtual void Showed() {}

        public virtual void Hide() {}

        public virtual void Hidden() {}

        protected virtual void OnInit() {}

        protected virtual void OnDisposed() {}

        protected virtual void OnFixContainers()
        {
            if (!parent)
                parent = GetComponent<RectTransform>();

            if (!content && parent)
                content = parent.GetComponentsInChildren<RectTransform>(true)
                    .FirstOrDefault(x => x && x.name.ToLower().Contains("content")
                                         || x.name.ToLower().Contains("container")) ?? parent;
        }

        protected void OnWindowChanged() => OnChanged?.Invoke();

        private void OnValidate() => OnFixContainers();

        protected void OnDestroy()
        {
            if (!Initialzied)
                return;

            OnDisposed();
            Initialzied = false;
            AnimationSource = null;
            AudioSource = null;
            content = null;
            OnChanged = null;
            parent = null;
        }

        /// <summary>
        /// Instead, use <see cref="OnInit()"/>
        /// </summary>
        protected void Awake() {}

        /// <summary>
        /// Instead, use <see cref="OnInit()"/>
        /// </summary>
        protected void Start() {}
    }
}