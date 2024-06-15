using System;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Client.Common.Services.UIService.Animations
{
    [Serializable]
    public class UIAnimationData
    {
        public bool Reversed;
        public float Delay;
        public float Duration = 1f;
        public Ease Ease = Ease.Linear;
        public UIAnimationTrigger PlayTrigger = UIAnimationTrigger.Show;
        public UIAnimationTrigger StopTrigger = UIAnimationTrigger.Hided;
        public UIAnimationTrigger ResetTrigger = UIAnimationTrigger.Show | UIAnimationTrigger.Hided;
    }
}