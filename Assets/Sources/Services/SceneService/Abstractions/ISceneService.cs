using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Client.Services.SceneService
{
	public interface ISceneService
	{
		event Action<string> OnSceneLoaded;
		string ActiveScene { get; }
		
		Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
	}
}