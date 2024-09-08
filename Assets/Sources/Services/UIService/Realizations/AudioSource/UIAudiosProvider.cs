using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Services.UIService
{
    public class UIAudiosProvider : MonoBehaviour, IUIAudioConfigsProvider
    {
        [SerializeField] protected List<UIAudioBaseConfig> configs = new();
        
        public IEnumerable<IUIAudioConfig> LoadAll()
            => configs;

        public Task<IEnumerable<IUIAudioConfig>> LoadAllAsync()
            => Task.FromResult<IEnumerable<IUIAudioConfig>>(configs);

        [ContextMenu("Load All Resources")]
        private void Reset()
        {
            configs = Resources.LoadAll<UIAudioBaseConfig>(string.Empty).ToList();
        }
    }
}