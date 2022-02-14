using CardGame.Services.MonoPoolService;
using TMPro;
using UnityEngine;
using Zenject;

namespace CardGame.Cards
{
    public class UICardLabel : MonoBehaviour, IMonoPoolable
    {
        public GameObject GameObject => gameObject;

        [SerializeField] private TextMeshProUGUI _labelText;
        private IMonoPool _parentPool;

        [Inject]
        private void Inject(IMonoPool parent)
        {
            _parentPool = parent;
        }

        public void SetText(string value)
        {
            _labelText.text = value;
        }

        public void Relese()
        {
            _parentPool?.Despawn(this);
        }
        
        void IPoolable.OnDespawned()
        {
            _labelText.text = string.Empty;
            gameObject.SetActive(false);
        }

        void IPoolable.OnSpawned()
        {
            gameObject.SetActive(true);
        }
    }
}