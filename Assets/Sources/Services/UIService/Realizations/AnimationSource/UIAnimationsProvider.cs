using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.UIService.Abstractions;
using UnityEngine;

namespace Services.UIService
{
    public class UIAnimationsProvider : MonoBehaviour, IUIAnimationConfigsProvider
    {
        [SerializeField] protected List<UIAnimationBaseConfig> configs = new();
        
        public IEnumerable<IUIAnimationConfig> LoadAll()
            => configs;
        
        public Task<IEnumerable<IUIAnimationConfig>> LoadAllAsync() 
            => Task.FromResult<IEnumerable<IUIAnimationConfig>>(configs);

        [ContextMenu("Load All Resources")]
        private void Reset()
        {
            configs = Resources.LoadAll<UIAnimationBaseConfig>(string.Empty).ToList();
        }
    }
}