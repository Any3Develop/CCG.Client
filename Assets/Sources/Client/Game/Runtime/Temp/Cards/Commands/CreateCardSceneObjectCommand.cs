using System.Threading.Tasks;
using CardGame.Services.CommandService;
using CardGame.Services.MonoPoolService;
using Core.ArtRepository;
using Core.PictureStorage;

namespace CardGame.Cards
{
    public class CreateCardSceneObjectCommand : ICommand<CreateCardSceneObjectProtocol>
    {
        private readonly IMonoPool _monoPool;
        private readonly IArtRepository artRepository;
        private readonly PictureStorage pictureStorage;
        private readonly CardSceneObjectStorage _cardSceneObjectStorage;
        public CreateCardSceneObjectCommand(IMonoPool monoPool,
                                            IArtRepository artRepository,
                                            PictureStorage pictureStorage,
                                            CardSceneObjectStorage cardSceneObjectStorage)
        {
            _monoPool = monoPool;
            this.artRepository = artRepository;
            this.pictureStorage = pictureStorage;
            _cardSceneObjectStorage = cardSceneObjectStorage;
        }

        public async Task Execute(CreateCardSceneObjectProtocol protocol)
        {
            var texture = await artRepository.GetAsync(protocol.Id);
            if (!texture)
                return;
            
            var sceneObject = _monoPool.Spawn<CardSceneObject>();
            sceneObject.Init(protocol.Id);
            sceneObject.SetImage(texture);
            _cardSceneObjectStorage.Add(sceneObject);
        }
    }
}