using System;
using System.Linq;
using UnityEngine;

namespace Client.Services.UIService
{
    public abstract class UIAnimationBaseConfig : ScriptableObject, IUIAnimationConfig
    {
        [SerializeField] private bool enabledByDefault = true;
        [SerializeField] private bool reInitWhenModified;
        [SerializeField] private string[] playAt = Array.Empty<string>();
        [SerializeField] private string[] stopAt = Array.Empty<string>();
        [SerializeField] private string[] resetAt = Array.Empty<string>();
        [SerializeField] private AnimationCurve easeCurve = new();
        
        public bool EnabledByDefault => enabledByDefault;
        public bool ReInitWhenModified => reInitWhenModified;
        public AnimationCurve EaseCurve => easeCurve;

        public bool HasPlayEvent(object id) 
            => HasEventId(id, playAt);

        public bool HasStopEvent(object id)
            => HasEventId(id, stopAt);

        public bool HasResetEvent(object id)
            => HasEventId(id, resetAt);

        private static bool HasEventId(object id, string[] events)
        {
            var eventId = id?.ToString();
            if (string.IsNullOrEmpty(eventId) || events is not {Length: > 0})
                return false;
            
            return events.Contains(eventId);
        }


#if UNITY_EDITOR
        [ContextMenu("Reset Events")]
        private void Reset()
        {
            var defaultEvents =  new[]
            {
                UIAnimationExtensions.SHOW_EVENT,
                UIAnimationExtensions.SHOWED_EVENT,
                UIAnimationExtensions.HIDE_EVENT,
                UIAnimationExtensions.HIDDEN_EVENT,
            };

            stopAt = defaultEvents.ToArray();
            resetAt = defaultEvents.ToArray();
            playAt = defaultEvents;
        }
        
        [ContextMenu("EaseCurve MirrorHorizontally")]
        public void EaseCurveMirrorHorizontally()
        {
            easeCurve ??= new AnimationCurve();
            easeCurve = easeCurve.MirrorHorizontally();
        }

        [ContextMenu("EaseCurve MirrorVertically")]
        public void EaseCurveMirrorVertically()
        {
            easeCurve ??= new AnimationCurve();
            easeCurve = easeCurve.MirrorVertically();
        }

        [ContextMenu("EaseCurve MirrorVerticallyCenter")]
        public void EaseCurveMirrorVerticallyCenter()
        {
            easeCurve ??= new AnimationCurve();
            easeCurve = easeCurve.MirrorVertically(true);
        }
#endif
    }
}