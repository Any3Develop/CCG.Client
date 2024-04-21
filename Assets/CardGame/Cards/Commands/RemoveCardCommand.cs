using System.Threading.Tasks;
using CardGame.Services.CommandService;
using CardGame.Services.SceneEntity;
using CardGame.Services.StatsService;
using UniRx;
using Zenject;

namespace CardGame.Cards
{
    public class RemoveCardCommand : ICommand<RemoveCardProtocol>
    {
        private readonly IInstantiator _instantiator;
        private readonly SceneEntityStorage _sceneEntityStorage;
        private readonly CardDtoStorage _cardDtoStorage;

        public RemoveCardCommand(IInstantiator instantiator,
                                 SceneEntityStorage sceneEntityStorage,
                                 CardDtoStorage cardDtoStorage)
        {
            _instantiator = instantiator;
            _sceneEntityStorage = sceneEntityStorage;
            _cardDtoStorage = cardDtoStorage;
        }
        
        public Task Execute(RemoveCardProtocol protocol)
        {
            var controller =_sceneEntityStorage.Get(protocol.CardId);
            controller.Dispose();
            _sceneEntityStorage.Remove(controller);
            
            _instantiator
                .Instantiate<RemoveStatsCollectionCommand>()
                .Execute(new RemoveStatsCollectionProtocol{Id = protocol.CardId});
            
            _instantiator.Instantiate<RemoveCardSceneObjectCommand>()
                .Execute(new RemoveCardSceneObjectProtocol {Id = protocol.CardId});

            var cardDto = _cardDtoStorage.Get(protocol.CardId);
            protocol.FieldType = cardDto.CurrentField;
            protocol.Place = cardDto.Place;
            _cardDtoStorage.Remove(protocol.CardId);
            MessageBroker.Default.Publish(protocol);
            return Task.CompletedTask;
        }
    }
}