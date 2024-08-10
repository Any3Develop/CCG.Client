using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Services.SceneService.Abstractions
{
	public interface ISceneService
	{
		event Action<string> OnSceneLoaded;
		string ActiveScene { get; }
		
		Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
	}
}