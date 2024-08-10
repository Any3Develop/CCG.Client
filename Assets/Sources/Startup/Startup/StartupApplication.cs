using Cysharp.Threading.Tasks;
using Services.SceneService;
using Services.SceneService.Abstractions;
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
            await sceneService.LoadAsync("Server");
            await UniTask.Delay(2000);
            await sceneService.LoadAsync("Client", LoadSceneMode.Additive);
        }
    }
}