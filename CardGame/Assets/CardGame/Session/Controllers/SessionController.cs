using CardGame.Cards;
using CardGame.Services.InputService;
using CardGame.Services.SceneEntity;
using CardGame.Services.UIService;
using CardGame.Session.SceneObject;
using CardGame.UI;
using UniRx;
using Zenject;
using Random = UnityEngine.Random;

namespace CardGame.Session
{
    public class SessionController : ISceneEntity, IInitializable
    {
        public string Id { get; }
        private readonly IUIService _uiService;
        private readonly IInstantiator _instantiator;
        private readonly IInputController<MainLayer> _inputController;
        private readonly SessionSceneObjectStorage _sessionSceneObjectStorage;
        private readonly CardModelStorage _cardModelStorage;
        private readonly CompositeDisposable _events;
        private SessionSceneObject _sceneObject;
        private CardsInteractor _cardsInteractor;
        private CardsInputInteractor _cardsInputInteractor;

        public SessionController(string id, 
                                 IUIService uiService,
                                 IInstantiator instantiator,
                                 IInputController<MainLayer> inputController,
                                 SessionSceneObjectStorage sessionSceneObjectStorage,
                                 CardModelStorage cardModelStorage)
        {
            Id = id;
            _uiService = uiService;
            _instantiator = instantiator;
            _inputController = inputController;
            _sessionSceneObjectStorage = sessionSceneObjectStorage;
            _cardModelStorage = cardModelStorage;
            _events = new CompositeDisposable();
        }
        
        public async void Initialize()
        {
            _inputController.Lock();
            _sceneObject = _sessionSceneObjectStorage.Get(Id);
            var window = _uiService.Show<UIUpdate>();
            var randomCardCount = Random.Range(4, 6);

            for (var i = 0; i < randomCardCount; i++)
            {
                window.SetProgressText((((float)i / randomCardCount) * 100f) + "<size=50%>%</size>");
                var cardModel = _cardModelStorage.Get(Random.Range(0, _cardModelStorage.Count).ToString());
                var cardProtocol = new AddCardProtocol(Id, cardModel.ModelId);
                await _instantiator.Instantiate<AddCardCommand>().Execute(cardProtocol);
                await _instantiator.Instantiate<PlaceCardCommand>().Execute(new PlaceCardProtocol
                {
                    CardId = cardProtocol.CardId,
                    ToPlace = FieldType.Hands,
                    Place = i,
                    SessionId = Id
                });
            }
            _uiService.Hide<UIUpdate>();
            _cardsInteractor = _instantiator.Instantiate<CardsInteractor>(new []{Id});
            _cardsInputInteractor = _instantiator.Instantiate<CardsInputInteractor>(new []{Id});
            _cardsInputInteractor.Initialize();
            _cardsInteractor.Initialize();

            MessageBroker.Default
                .Receive<PlaceCardProtocol>()
                .Subscribe(p =>
                {
                    if (p.BeforePlace == p.ToPlace)
                    {
                        UpdateLayout(p.SessionId, p.ToPlace);
                    }
                    else
                    {
                        UpdateLayout(p.SessionId, p.BeforePlace);
                        UpdateLayout(p.SessionId, p.ToPlace);
                    }
                })
                .AddTo(_events);
            
            MessageBroker.Default
                .Receive<RemoveCardProtocol>()
                .Subscribe(p => UpdateLayout(p.SessionId,p.FieldType))
                .AddTo(_events);
            
            _inputController.Unlock();
        }
        private void UpdateLayout(string sessionId, 
                                  FieldType fieldType)
        {
            if (sessionId != Id)
            {
                return;
            }
            
            switch (fieldType)
            {
                case FieldType.Hands:
                    _sceneObject.HandsLayout.Rebuild();
                    break;
                case FieldType.Table:
                    _sceneObject.TableLayout.Rebuild();
                    break;
            }
        }

        public void Dispose()
        {
            _cardsInputInteractor?.Dispose();
            _cardsInputInteractor = null;
            _cardsInteractor?.Dispose();
            _cardsInteractor = null;
            _events?.Dispose();
            _events?.Clear();
            _sceneObject = null;
        }
    }
}