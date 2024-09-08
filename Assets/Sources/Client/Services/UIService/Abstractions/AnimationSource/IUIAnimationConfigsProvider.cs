using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services.UIService
{
    public interface IUIAnimationConfigsProvider
    {
        IEnumerable<IUIAnimationConfig> LoadAll();
        Task<IEnumerable<IUIAnimationConfig>> LoadAllAsync();
    }
}