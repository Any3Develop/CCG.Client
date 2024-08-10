using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.UIService.Abstractions
{
    public interface IUIAudioConfigsProvider
    {
        IEnumerable<IUIAudioConfig> LoadAll();
        Task<IEnumerable<IUIAudioConfig>> LoadAllAsync();
    }
}