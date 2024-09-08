using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services.UIService
{
    public interface IUIAudioConfigsProvider
    {
        IEnumerable<IUIAudioConfig> LoadAll();
        Task<IEnumerable<IUIAudioConfig>> LoadAllAsync();
    }
}