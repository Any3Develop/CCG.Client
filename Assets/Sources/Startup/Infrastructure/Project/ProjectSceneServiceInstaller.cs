using Client.Common.Services.SceneService;
using Zenject;

namespace Startup.Infrastructure.Project
{
	public class ProjectSceneServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<SceneService>()
				.AsSingle()
				.NonLazy();
		}
	}
}