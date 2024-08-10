using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.UIService.Abstractions
{
    public interface IUIAnimationConfigsProvider
    {
        IEnumerable<IUIAnimationConfig> LoadAll();
        Task<IEnumerable<IUIAnimationConfig>> LoadAllAsync();
    }
}