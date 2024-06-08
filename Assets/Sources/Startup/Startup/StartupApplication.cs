using Client.Common.Services.SceneService;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Startup.Startup
{
    public class StartupApplication : IInitializable
    {
        private readonly ISceneService sceneService;
        public StartupApplication(ISceneService sceneService)
        {
            this.sceneService = sceneService;
        }

        public async void Initialize()
        {
            await sceneService.LoadAsync(SceneId.Server);
            await UniTask.Delay(2000);
            await sceneService.LoadAsync(SceneId.Client, LoadSceneMode.Additive);
        }
    }
}