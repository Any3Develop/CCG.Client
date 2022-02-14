using System.Threading.Tasks;
using CardGame.ImageRepository;
using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public class RemoveCardSceneObjectCommand : ICommand<RemoveCardSceneObjectProtocol>
    {
        private readonly ImageStorage _imageStorage;
        private readonly CardSceneObjectStorage _sceneObjectStorage;

        public RemoveCardSceneObjectCommand(ImageStorage imageStorage,
                                            CardSceneObjectStorage sceneObjectStorage)
        {
            _imageStorage = imageStorage;
            _sceneObjectStorage = sceneObjectStorage;
        }

        public Task Execute(RemoveCardSceneObjectProtocol protocol)
        {
            var sceneObject = _sceneObjectStorage.Get(protocol.Id);
            _sceneObjectStorage.Remove(protocol.Id);
            _imageStorage.Remove(protocol.Id);
            sceneObject.Relese();
            return Task.CompletedTask;
        }
    }
}