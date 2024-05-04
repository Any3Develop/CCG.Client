using UnityEngine;
using Zenject;

namespace Startup.Infrastructure.Client.Common
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