using CardGame.Services.MonoPoolService;
using CardGame.Services.StorageService;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace CardGame.Cards
{
    public class CardSceneObject : MonoBehaviour, IStorageEntity, IMonoPoolable
    {
        public GameObject GameObject => gameObject;
        public Transform UIContainer => _worldCanvas.transform;
        public string Id { get; private set; }
        public int SortOrder => _worldCanvas.sortingOrder;
        
        [SerializeField] private Canvas _worldCanvas;
        [SerializeField] private SortingGroup _sortingGroup;
        [SerializeField] private SpriteRenderer _imagePlaceHolder;
        [SerializeField] private SpriteRenderer _outline;
        private IMonoPool _monoPool;
        
        [Inject]
        private void Inject(IMonoPool monoPool)
        {
            _monoPool = monoPool;
        }
        
        public void Init(string id)
        {
            Id = id;
        }

        public void SetImage(Texture value)
        {
            // _imagePlaceHolder.sprite = value;
        }
        
        public void SetLayerOrder(int value)
        {
            _sortingGroup.sortingOrder = value - 1;
            _worldCanvas.sortingOrder = value;
        }

        public void SetOutlineActive(bool value)
        {
            if (!_outline)
            {
                return;
            }
            _outline.gameObject.SetActive(value);
        }

        public void SetOutlineColor(Color value)
        {
            if (!_outline)
            {
                return;
            }
            _outline.color = value;
        }

        public void Relese()
        {
            _monoPool?.Despawn(this);
        }

        void IPoolable.OnDespawned()
        {
            Id = "";
            _sortingGroup.sortingOrder = 0;
            gameObject.SetActive(false);
        }

        void IPoolable.OnSpawned()
        {
            gameObject.SetActive(true);
        }
    }
}
