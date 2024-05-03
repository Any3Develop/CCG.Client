using System.Collections.Generic;
using System.Linq;
using CardGame.Services.MonoPoolService;
using CardGame.Services.SceneEntity;
using CardGame.Services.StatsService;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace CardGame.Cards
{
    public class CardController : ISceneEntity, IInitializable
    {
        public string Id { get; }
        private readonly string _sessionId;
        private readonly IMonoPool _monoPool;
        private readonly IInstantiator _instantiator;
        private readonly CardDtoStorage _cardDtoStorage;
        private readonly CardSceneObjectStorage _cardSceneObjectStorage;
        private readonly StatsCollectionStorage _statsCollectionStorage;
        private const string _healthStatKey = "stat_health";
        private const string _mannaStatKey  = "stat_manna";
        private const string _attackStatKey = "stat_attack";
        private const string _headerTextKey = "CardHeader_";
        private const string _descriptionTextKey = "CardDescription_";
        private readonly Dictionary<string, UICardLabel> _labels;
        private readonly CompositeDisposable _events;
        private StatsCollection _statsCollection;
        private CardSceneObject _sceneObject;
        private CardDto _cardDto;


        public CardController(string id, 
                              string sessionId,
                              IMonoPool monoPool,
                              IInstantiator instantiator,
                              CardSceneObjectStorage cardSceneObjectStorage,
                              StatsCollectionStorage statsCollectionStorage,
                              CardDtoStorage cardDtoStorage)
        {
            Id = id;
            _sessionId = sessionId;
            _monoPool = monoPool;
            _instantiator = instantiator;
            _cardSceneObjectStorage = cardSceneObjectStorage;
            _statsCollectionStorage = statsCollectionStorage;
            _cardDtoStorage = cardDtoStorage;
            _labels = new Dictionary<string, UICardLabel>();
            _events = new CompositeDisposable();
        }
        
        public void Initialize()
        {
            _cardDto = _cardDtoStorage.Get(Id);
            _statsCollection = _statsCollectionStorage.Get(Id);
            _sceneObject = _cardSceneObjectStorage.Get(Id);

            InitModifiableLabel(_healthStatKey,"Health");
            InitModifiableLabel(_mannaStatKey, "Manna");
            InitModifiableLabel(_attackStatKey,"Attack");
            
            _labels.Add("header", GetLabel<UICardHeader>("Header", _headerTextKey + _cardDto.ModelId));
            _labels.Add("descrition",GetLabel<UICardDescription>("Description", _descriptionTextKey + _cardDto.ModelId));
            MessageBroker.Default.Receive<PlaceCardProtocol>().Subscribe(OnPlaceSceneObjectEventHandler).AddTo(_events);
        }

        private void OnPlaceSceneObjectEventHandler(PlaceCardProtocol protocol)
        {
            if (_cardDto == null || 
                _sceneObject == null)
            {
                return;
            }
            if (_cardDto.CurrentField == protocol.ToPlace ||
                _cardDto.CurrentField == protocol.BeforePlace)
            {
                if (_sceneObject)
                {
                    var placeIndex = _sceneObject.transform.GetSiblingIndex();
                    _sceneObject.SetLayerOrder(placeIndex * 2);
                }  
            }

        }

        public void Dispose()
        {
            _events.Dispose();
            _events.Clear();
            foreach (var cardLabel in _labels.ToArray())
            {
                DisposeLabel(cardLabel.Key);
            }
            _labels.Clear();
        }

        private void InitModifiableLabel(string statId, string key)
        {
            if (!_statsCollection.HasEntity(statId))
            {
                return;
            }

            var stat = _statsCollection.Get<StatVital>(statId);
            stat.OnCurrentValueChanged += OnStatValueChangedEventHandler;

            var cardLabel = GetLabel<UICardStatLabel>(key, ((int) stat.Value).ToString());
            cardLabel.SetIcon(Resources.Load<Sprite>($"Art/{key}"));
            _labels.Add(statId, cardLabel);
        }

        private void DisposeLabel(string key)
        {
            if (_statsCollection.HasEntity(key))
            {
                if (_statsCollection.Get(_healthStatKey) is StatVital statVital)
                {
                    statVital.OnCurrentValueChanged -= OnStatValueChangedEventHandler;
                }
            }

            if (!_labels.ContainsKey(key))
            {
                return;
            }
            _labels[key]?.Relese();
            _labels.Remove(key);
        }

        private T GetLabel<T>(string key, string value = "") where T : UICardLabel
        {
            var target = _sceneObject.transform.Find(key);
            if (!target)
            {
                return default;
            }
            
            var label = _monoPool.Spawn<T>(_sceneObject.UIContainer);
            label.transform.position   = target.position;
            label.transform.rotation   = target.rotation;
            label.transform.localScale = target.localScale;
            label.SetText(value);
            return label;
        }

        private void OnStatValueChangedEventHandler(object sender, float value)
        {
            if (!(sender is Stat stat))
            {
                return;
            }

            if (_labels.ContainsKey(stat.Id))
            {
                var label = _labels[stat.Id];
                label.SetText(((int)value).ToString());
                label.transform.DOShakePosition(0.5f,Vector3.one /10, 100);
            }

            if (stat.Id == _healthStatKey)
            {
                if (value <= 0)
                {
                    _instantiator.Instantiate<RemoveCardCommand>()
                        .Execute(new RemoveCardProtocol {CardId = Id, SessionId = _sessionId});
                }
            }
        }
    }
}