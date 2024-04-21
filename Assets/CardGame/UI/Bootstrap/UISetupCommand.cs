using CardGame.Cameras;
using CardGame.Services.BootstrapService;
using CardGame.Services.UIService;

namespace CardGame.UI
{
    public class UISetupCommand : Command
    {
        private readonly CameraSceneObject _cameraSceneObject;
        private readonly UIRoot _uiRoot;

        public UISetupCommand(CameraSceneObject cameraSceneObject,
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