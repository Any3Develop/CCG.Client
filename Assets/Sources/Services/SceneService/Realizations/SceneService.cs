using System;
using System.Threading.Tasks;
using Services.SceneService.Abstractions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneService
{
	public class SceneService : ISceneService, IDisposable
	{
		public event Action<string> OnSceneLoaded;
		public string ActiveScene { get; private set; }

		public async Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
		{
			Debug.Log($"Previous scene : {ActiveScene}");
			var operation = SceneManager.LoadSceneAsync(sceneName, mode);
			while (!operation.isDone)
				await Task.Yield();

			ActiveScene = SceneManager.GetActiveScene().name;
			OnSceneLoaded?.Invoke(sceneName);
			Debug.Log($"Scene loaded : {sceneName}");
		}

		public void Dispose()
		{
			OnSceneLoaded = null;
		}
	}
}