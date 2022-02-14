using System.Threading.Tasks;
using CardGame.ImageRepository;
using CardGame.Services.CommandService;
using CardGame.Services.MonoPoolService;

namespace CardGame.Cards
{
    public class CreateCardSceneObjectCommand : ICommand<CreateCardSceneObjectProtocol>
    {
        private readonly IMonoPool _monoPool;
        private readonly IImageRepository _imageRepository;
        private readonly ImageStorage _imageStorage;
        private readonly CardSceneObjectStorage _cardSceneObjectStorage;
        public CreateCardSceneObjectCommand(IMonoPool monoPool,
                                            IImageRepository imageRepository,
                                            ImageStorage imageStorage,
                                            CardSceneObjectStorage cardSceneObjectStorage)
        {
            _monoPool = monoPool;
            _imageRepository = imageRepository;
            _imageStorage = imageStorage;
            _cardSceneObjectStorage = cardSceneObjectStorage;
        }

        public async Task Execute(CreateCardSceneObjectProtocol protocol)
        {
            var sprite = await _imageRepository.Get();
            if (!sprite)
            {
                return;
            }
            _imageStorage.Add(new ImageDto{Id = protocol.Id, Image = sprite});
            var sceneObject = _monoPool.Spawn<CardSceneObject>();
            sceneObject.Init(protocol.Id);
            sceneObject.SetImage(sprite);
            _cardSceneObjectStorage.Add(sceneObject);
        }
    }
}