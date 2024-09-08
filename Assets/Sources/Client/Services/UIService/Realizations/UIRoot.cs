using UnityEngine;

namespace Client.Services.UIService
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Canvas uiCanvas;
        [SerializeField] private RectTransform safeArea;
        [SerializeField] private RectTransform poolContainer;
        [SerializeField] private RectTransform deactivatedContainer;
        [SerializeField] private RectTransform topContainer;
        [SerializeField] private RectTransform middleContainer;
        [SerializeField] private RectTransform buttomContainer;
        
        public Camera UICamera => uiCamera;
        public Canvas UICanvas => uiCanvas;
        public RectTransform SafeArea => safeArea;
        public RectTransform PoolContainer => poolContainer;
        public RectTransform DeactivatedContainer => deactivatedContainer;
        public RectTransform TopContainer => topContainer;
        public RectTransform MiddleContainer => middleContainer;
        public RectTransform ButtomContainer => buttomContainer;
    }
}