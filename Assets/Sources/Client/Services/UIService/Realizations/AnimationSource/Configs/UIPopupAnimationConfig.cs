using Client.Services.UIService.Data;
using DG.Tweening;
using UnityEngine;

namespace Client.Services.UIService
{
    [CreateAssetMenu(fileName = "UIPopupAnimation", menuName = "UIService/Animations/UIPopupAnimation")]
    public class UIPopupAnimationConfig : UIAnimationBaseConfig
    {
#if DOTWEEN
        [Header("===> DoTween <===")]
        [Space]
        [SerializeField] private Ease ease = Ease.Linear;
        [SerializeField] private bool useEase = true;        
        [Header("===> DoTween <===")]
        [Space]
#endif
        [SerializeField] protected float delay;
        [SerializeField] protected float duration = 1f;
        [SerializeField] protected Vector3WithFlags fromSize = new() {vector = Vector3.one};
        [SerializeField] protected Vector3WithFlags toSize = new() {vector = Vector3.one};

#if DOTWEEN
        public bool UseEase => useEase;
        public Ease Ease => ease;
#endif
            
        public float Delay => delay;
        public float Duration => duration;
        public Vector3WithFlags FromSize => fromSize;
        public Vector3WithFlags ToSize => toSize;

#if UNITY_EDITOR
        [ContextMenu("Swap Size")]
        public void SwapSize()
        {
            (fromSize.vector, toSize.vector) = (toSize.vector, fromSize.vector);
        }
#endif
    }
}