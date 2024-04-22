using System.Threading.Tasks;
using CardGame.Services.CommandService;
using CardGame.Services.SceneEntity;
using CardGame.Services.StatsService;
using CardGame.Services.TypeRegistryService;
using UniRx;
using Zenject;

namespace CardGame.Cards
{
    public class AddCardCommand : ICommand<AddCardProtocol>
    {
        private readonly IInstantiator _instantiator;
        private readonly TypeRegistry _typeStorage;
        private readonly SceneEntityStorage _sceneEntityStorage;
        private readonly CardStatModelStorage _cardStatModelStorage;
        private readonly CardModelStorage _cardModelStorage;
        private readonly CardDtoStorage _cardDtoStorage;

        public AddCardCommand(IInstantiator instantiator,
                                 TypeRegistry typeStorage,
                                 SceneEntityStorage sceneEntityStorage,
                                 CardStatModelStorage cardStatModelStorage,
                                 CardModelStorage cardModelStorage,
                                 CardDtoStorage cardDtoStorage)
        {
            _instantiator = instantiator;
            _typeStorage = typeStorage;
            _sceneEntityStorage = sceneEntityStorage;
            _cardStatModelStorage = cardStatModelStorage;
            _cardModelStorage = cardModelStorage;
            _cardDtoStorage = cardDtoStorage;
        }
        
        public async Task Execute(AddCardProtocol protocol)
        {
            _cardDtoStorage.Add(new CardDto 
            {
                Id = protocol.CardId, 
                ModelId = protocol.ModelId
            });
            
            await _instantiator.Instantiate<CreateCardSceneObjectCommand>()
                .Execute(new CreateCardSceneObjectProtocol {Id = protocol.CardId, SessionId = protocol.SessionId});
            
            var statModel = _cardStatModelStorage.Get(protocol.ModelId);
            await _instantiator
                .Instantiate<CreateStatsCollectionCommand<StatsCollection>>()
                .Execute(new CreateStatsCollectionProtocol(protocol.CardId, statModel.Stats));

            var model = _cardModelStorage.Get(protocol.ModelId);
            var concreteType = _typeStorage.Get(model.TypeName).Type;
            var controller = (ISceneEntity)_instantiator.Instantiate(concreteType,new object[] {protocol.CardId,protocol.SessionId});
            if (controller is IInitializable initializable)
            {
                initializable.Initialize();
            }
            _sceneEntityStorage.Add(controller);
            
            await _instantiator.Instantiate<PlaceCardCommand>().Execute(new PlaceCardProtocol
            {
                SessionId = protocol.SessionId, 
                CardId = protocol.CardId,
                BeforePlace = FieldType.OffScreen,
                ToPlace = FieldType.OffScreen,
                Place = 0,
            });
            MessageBroker.Default.Publish(protocol);
        }
    }
}