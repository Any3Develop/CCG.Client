using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Shared.Common.Logger;
using UnityEngine.SceneManagement;

namespace Client.Common.Services.SceneService
{
	public class SceneService : ISceneService, IDisposable
	{
		public event Action<SceneId> OnSceneLoaded;

		public SceneId Current { get; private set; }

		public async UniTask LoadAsync(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single)
		{
			SharedLogger.Log($"Previous scene : {Current.ToString()}");
			Current = mode == LoadSceneMode.Additive ? Current | sceneId : sceneId;
			SharedLogger.Log($"Scene loading : {sceneId}");
			var operation = SceneManager.LoadSceneAsync((int)sceneId, mode);
			while (!operation.isDone)
				await Task.Yield();
			
			OnSceneLoaded?.Invoke(sceneId);
		}

		public void Dispose()
		{
			OnSceneLoaded = null;
		}
	}
}