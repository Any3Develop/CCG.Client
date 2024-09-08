using System;
using System.Threading.Tasks;
using Shared.Common.Logger;
using UnityEngine.SceneManagement;

namespace Client.Services.SceneService
{
	public class SceneService : ISceneService, IDisposable
	{
		public event Action<string> OnSceneLoaded;
		public string ActiveScene { get; private set; }

		public async Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			SharedLogger.Log($"Previous scene : {ActiveScene}");
			var operation = SceneManager.LoadSceneAsync(sceneName, mode);
			while (!operation.isDone)
				await Task.Yield();

			ActiveScene = SceneManager.GetActiveScene().name;
			OnSceneLoaded?.Invoke(sceneName);
			SharedLogger.Log($"Scene loaded : {sceneName}");
		}

		public void Dispose()
		{
			OnSceneLoaded = null;
		}
	}
}