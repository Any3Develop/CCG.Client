using Client.Services.UIService.Data;
using DG.Tweening;
using UnityEngine;

namespace Client.Services.UIService
{
    [CreateAssetMenu(fileName = "UIAppearAnimation", menuName = "UIService/Animations/UIAppearAnimation")]
    public class UIAppearAnimationConfig : UIAnimationBaseConfig
    {
#if DOTWEEN
        [Header("===> DoTween <===")]
        [Space]
        [SerializeField] private Ease ease = Ease.Linear;
        [SerializeField] private bool useEase = true;        
        [Header("===> DoTween <===")]
        [Space]
#endif
        [SerializeField] private float delay;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Vector2WithFlags fromPosition;
        [SerializeField] private Vector2WithFlags toPosition;
        
#if DOTWEEN
        public bool UseEase => useEase;
        public Ease Ease => ease;
#endif
            
        public float Delay => delay;
        public float Duration => duration;
        public Vector2WithFlags FromPosition => fromPosition;
        public Vector2WithFlags ToPosition => toPosition;

#if UNITY_EDITOR
        [ContextMenu("Swap Positions")]
        public void SwapSize()
        {
            (fromPosition.vector, toPosition.vector) = (toPosition.vector, fromPosition.vector);
        }
#endif
    }
}