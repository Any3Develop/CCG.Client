using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Client.Common.Services.SceneService
{
	public interface ISceneService
	{
		event Action<SceneId> OnSceneLoaded;
		SceneId Current { get; }
		UniTask LoadAsync(SceneId sceneId, LoadSceneMode mode = LoadSceneMode.Single);
	}
}