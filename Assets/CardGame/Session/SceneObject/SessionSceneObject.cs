using CardGame.Layouts;
using CardGame.Services.StorageService;
using UnityEngine;
using Zenject;

namespace CardGame.Session.SceneObject
{
    public class SessionSceneObject : MonoBehaviour, IStorageItem
    {
        public string Id { get; private set; }
        public Transform OffScreenContainer => _offScreenContainer;
        public HorizontalCellLayout HandsLayout => _handsLayout;
        public HorizontalCellLayout TableLayout => _tableLayout;
        [SerializeField] private HorizontalCellLayout _handsLayout;
        [SerializeField] private HorizontalCellLayout _tableLayout;
        [SerializeField] private Transform _offScreenContainer;
        [Inject]
        private void Inejct(string id)
        {
            Id = id;
        }
    }
}