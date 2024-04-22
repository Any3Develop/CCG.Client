using System.Threading.Tasks;
using CardGame.Services.CommandService;
using Core.PictureStorage;

namespace CardGame.Cards
{
    public class RemoveCardSceneObjectCommand : ICommand<RemoveCardSceneObjectProtocol>
    {
        private readonly PictureStorage pictureStorage;
        private readonly CardSceneObjectStorage _sceneObjectStorage;

        public RemoveCardSceneObjectCommand(PictureStorage pictureStorage,
                                            CardSceneObjectStorage sceneObjectStorage)
        {
            this.pictureStorage = pictureStorage;
            _sceneObjectStorage = sceneObjectStorage;
        }

        public Task Execute(RemoveCardSceneObjectProtocol protocol)
        {
            var sceneObject = _sceneObjectStorage.Get(protocol.Id);
            _sceneObjectStorage.Remove(protocol.Id);
            pictureStorage.Remove(protocol.Id);
            sceneObject.Relese();
            return Task.CompletedTask;
        }
    }
}