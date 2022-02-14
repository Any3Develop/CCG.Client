using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Services.StatsService;
using CardGame.Services.UIService;
using CardGame.UI;
using UniRx;
using Random = UnityEngine.Random;

namespace CardGame.Cards
{
    public class CardsInteractor
    {
        private readonly string _sessionId;
        private readonly IUIService _uiService;
        private readonly CardDtoStorage _cardDtoStorage;
        private readonly StatsCollectionStorage _statsCollectionStorage;
        private readonly string[] _statsToChange = {"stat_manna", "stat_attack", "stat_health"};
        private readonly CompositeDisposable _events;
        private readonly Dictionary<FieldType, List<CardDto>> _fields;
        private int _lastCardHandPlace = -1;

        public CardsInteractor(string sessionId,
                               IUIService uiService,
                               CardDtoStorage cardDtoStorage,
                               StatsCollectionStorage statsCollectionStorage)
        {
            _sessionId = sessionId;
            _uiService = uiService;
            _cardDtoStorage = cardDtoStorage;
            _statsCollectionStorage = statsCollectionStorage;
            _events = new CompositeDisposable();
            _fields = new Dictionary<FieldType, List<CardDto>>();
        }

        public void Initialize()
        {
            var windowAllStats = _uiService.Show<UIChangeCardButton>();
            var windowhealthStat = _uiService.Show<UIChangeHealthCardButton>();
            windowAllStats.SetButtonText("Обновить 1 из (HP/MP/AD)");
            windowhealthStat.SetButtonText("Обновить (HP)");
            windowAllStats.ClickEvent += OnRandomStatChangeEventHandler;
            windowhealthStat.ClickEvent += OnHealthStatChangeEventHandler;
            UpdateFieldCards(FieldType.Hands);
            UpdateFieldCards(FieldType.Table);
            MessageBroker.Default
                .Receive<RemoveCardProtocol>()
                .Subscribe(OnRemoveCardEventHandler)
                .AddTo(_events);
            
            MessageBroker.Default
                .Receive<PlaceCardProtocol>()
                .Subscribe(OnPlaceCardEventHandler)
                .AddTo(_events);
        }

        public void Dispose()
        {
            _fields.Clear();
            _events?.Dispose();
            _events?.Clear();
            _uiService.Hide<UIChangeCardButton>();
            _uiService.Hide<UIChangeHealthCardButton>();
        }
        
        private string GetCardByOrder()
        {
            _lastCardHandPlace++;
            var cardsDto = _fields[FieldType.Hands];
            if (cardsDto.Count == 0)
            {
                return string.Empty;
            }

            if (_lastCardHandPlace >= cardsDto.Count)
            {
                _lastCardHandPlace = 0;
            }

            return cardsDto[_lastCardHandPlace].Id;
        }

        private void ChangeStat(string statId, string cardId)
        {
            if (!_statsCollectionStorage.HasEntity(cardId))
            {
                return;
            }
            var statCollection = _statsCollectionStorage.Get(cardId);
            if (statCollection.HasEntity(statId))
            {
                var stat = statCollection.Get<StatVital>(statId);
                stat.CurrentValue = Random.Range(-2, 9);
            }
        }

        private void UpdateFieldCards(FieldType fieldType)
        {
            if (fieldType == FieldType.OffScreen)
            {
                return;
            }
            
            if (!_fields.ContainsKey(fieldType))
            {
                _fields.Add(fieldType,new List<CardDto>());
            }
            _fields[fieldType].Clear();
            _fields[fieldType].AddRange(_cardDtoStorage.Get()
                                            .Where(x=>x.CurrentField == fieldType)
                                            .OrderBy(x=>x.Place));
        }

        private void OnRandomStatChangeEventHandler(object sender, EventArgs e)
        {
            var cardId = GetCardByOrder();
            if (string.IsNullOrEmpty(cardId))
            {
                return;
            }
            var randomStatKey = _statsToChange[Random.Range(0, _statsToChange.Length)];
            ChangeStat(randomStatKey, cardId);
        }
        
        private void OnHealthStatChangeEventHandler(object sender, EventArgs e)
        {
            var cardId = GetCardByOrder();
            if (string.IsNullOrEmpty(cardId))
            {
                return;
            }
            ChangeStat(_statsToChange[2], cardId);
        }

        private void OnRemoveCardEventHandler(RemoveCardProtocol protocol)
        {
            if (protocol.SessionId != _sessionId || 
                protocol.FieldType == FieldType.OffScreen)
            {
                return;
            }

            UpdateFieldCards(protocol.FieldType);
            switch (protocol.FieldType)
            {
                case FieldType.Hands:
                    // move back index if index greater than removed card
                    if (_lastCardHandPlace >= protocol.Place)
                    {
                        _lastCardHandPlace--;
                    }
                    // reset index if alredy wrap list
                    if (_lastCardHandPlace >= _fields[protocol.FieldType].Count)
                    {
                        _lastCardHandPlace = -1;
                    }
                    break;
                
                case FieldType.Table:
                    break;
            }
        }
        
        private void OnPlaceCardEventHandler(PlaceCardProtocol protocol)
        {
            if (protocol.SessionId != _sessionId)
            {
                return;
            }

            if (protocol.BeforePlace == FieldType.OffScreen ||
                protocol.BeforePlace == protocol.ToPlace)
            {
                UpdateFieldCards(protocol.ToPlace);
                return;
            }
            UpdateFieldCards(protocol.BeforePlace);
            UpdateFieldCards(protocol.ToPlace);
        }
    }
}