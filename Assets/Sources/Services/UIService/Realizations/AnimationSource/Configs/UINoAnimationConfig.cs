using Services.UIService.Abstractions;
using UnityEngine;

namespace Services.UIService
{
    public sealed class UINoAnimationConfig : IUIAnimationConfig
    {
        public bool EnabledByDefault { get; }
        public bool ReInitWhenModified { get; }
        
        public AnimationCurve EaseCurve { get; } = AnimationCurve.Linear(0,0,1,1);

        public bool HasPlayEvent(object id) => false;

        public bool HasStopEvent(object id) => false;

        public bool HasResetEvent(object id) => false;
    }
}