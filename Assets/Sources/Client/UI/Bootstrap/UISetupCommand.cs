using CardGame.Services.BootstrapService;
using CardGame.Services.UIService;
using UnityEngine;

namespace CardGame.UI
{
    public class UISetupCommand : Command
    {
        private readonly Camera _cameraSceneObject;
        private readonly UIRoot _uiRoot;

        public UISetupCommand(Camera cameraSceneObject,
                              UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _cameraSceneObject = cameraSceneObject;
        }

        public override void Do()
        {
            // TODO : setup canvases, safeArea, scalable etc..
            OnDone();
        }
    }
}