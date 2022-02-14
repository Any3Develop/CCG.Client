using System;
using System.Threading.Tasks;
using CardGame.Services.CommandService;
using CardGame.Services.SceneEntity;
using CardGame.Session.SceneObject;
using Zenject;

namespace CardGame.Session
{
    public class CreateSessionCommand : ICommand<CreateSessionProtocol>
    {
        private readonly IInstantiator _instantiator;
        private readonly SceneEntityStorage _sceneEntityStorage;
        private readonly SessionSceneObjectStorage _sessionSceneObjectStorage;

        public CreateSessionCommand(IInstantiator instantiator, 
                                    SceneEntityStorage sceneEntityStorage,
                                    SessionSceneObjectStorage sessionSceneObjectStorage)
        {
            _instantiator = instantiator;
            _sceneEntityStorage = sceneEntityStorage;
            _sessionSceneObjectStorage = sessionSceneObjectStorage;
        }

        public Task Execute(CreateSessionProtocol protocol)
        {
            var args = new object[] {protocol.Id};
            var sceneObject = _instantiator
                .InstantiatePrefabResourceForComponent<SessionSceneObject>("Prefabs/SessionSceneObject", args);
            _sessionSceneObjectStorage.Add(sceneObject);
            
            var sceneController = 
                _instantiator.Instantiate<SessionController>(args);
            sceneController.Initialize();
            _sceneEntityStorage.Add(sceneController);
            return Task.CompletedTask;
        }
    }
}