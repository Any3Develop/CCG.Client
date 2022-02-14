using Zenject;

namespace CardGame.Cameras
{
    public class CamerasInstaller : Installer<CamerasInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CameraSceneObject>()
                .FromComponentInNewPrefabResource("CameraSceneObject")
                .AsSingle()
                .NonLazy();
        }
    }
}