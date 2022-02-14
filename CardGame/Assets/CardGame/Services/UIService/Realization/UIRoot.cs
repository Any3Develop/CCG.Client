using UnityEngine;

namespace CardGame.Services.UIService
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Camera _uiCamera;
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private RectTransform _poolContainer;
        [SerializeField] private RectTransform _deactivatedContainer;
        [SerializeField] private RectTransform _buttomContainer;
        [SerializeField] private RectTransform _middleContainer;
        [SerializeField] private RectTransform _topContainer;
        [SerializeField] private RectTransform _safeArea;

        public Camera UICamera => _uiCamera;
        public Canvas UICanvas => _uiCanvas;

        public RectTransform SafeArea => _safeArea;
        
        public RectTransform DeactivatedContainer => _deactivatedContainer;
        public RectTransform PoolContainer => _poolContainer;

        public RectTransform ButtomContainer => _buttomContainer;
        public RectTransform MiddleContainer => _middleContainer;

        public RectTransform TopContainer => _topContainer;
    }
}