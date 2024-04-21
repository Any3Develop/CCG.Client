using UnityEngine;
using Zenject;

namespace CardGame.Cameras
{
    public class RenderingInstaller : Installer<RenderingInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<Camera>()
                .FromComponentInNewPrefabResource("Common/GameCamera")
                .AsSingle()
                .NonLazy();
        }
    }
}