using System;
using System.Threading.Tasks;
using CardGame.Layouts;
using CardGame.Services.CommandService;
using CardGame.Session;
using UniRx;
using UnityEngine;

namespace CardGame.Cards
{
    public class PlaceCardCommand : ICommand<PlaceCardProtocol>
    {
        private readonly CardDtoStorage _cardDtoStorage;
        private readonly CardSceneObjectStorage _cardSceneObjectStorage;
        private readonly SessionSceneObjectStorage _sessionSceneObjectStorage;

        public PlaceCardCommand(CardDtoStorage cardDtoStorage, 
                                      CardSceneObjectStorage cardSceneObjectStorage,
                                      SessionSceneObjectStorage sessionSceneObjectStorage)
        {
            _cardDtoStorage = cardDtoStorage;
            _cardSceneObjectStorage = cardSceneObjectStorage;
            _sessionSceneObjectStorage = sessionSceneObjectStorage;
        }

        public Task Execute(PlaceCardProtocol protocol)
        {
            if (!_sessionSceneObjectStorage.HasEntity(protocol.SessionId))
            {
                throw new ArgumentException($"Session with id {protocol.SessionId}, does not exits");
            }

            if (!_cardDtoStorage.HasEntity(protocol.CardId))
            {
                throw new ArgumentException($"CardDto with id {protocol.CardId}, does not exits");
            }

            var cardDto = _cardDtoStorage.Get(protocol.CardId);
            protocol.BeforePlace = cardDto.CurrentField;
            cardDto.CurrentField = protocol.ToPlace;
            cardDto.Place = protocol.Place;

            var sessionSceneObject = _sessionSceneObjectStorage.Get(protocol.SessionId);
            var cardSceneObjcet = _cardSceneObjectStorage.Get(protocol.CardId);
            var layout = GetLayout(protocol.ToPlace, protocol.SessionId);
            if (!layout)
            {
                // offscreen
                cardSceneObjcet.transform.SetParent(sessionSceneObject.OffScreenContainer);
                cardSceneObjcet.transform.position = Vector3.zero;
                cardSceneObjcet.transform.rotation = Quaternion.identity; 
            }
            else
            {
                if (cardDto.Place < 0)
                {
                    cardDto.Place = layout.CellCount;
                    protocol.Place = cardDto.Place;
                }
                layout.InsertAt(cardSceneObjcet.transform, protocol.Place);
            }
            MessageBroker.Default.Publish(protocol);
            return Task.CompletedTask;
        }
        
        private HorizontalCellLayout GetLayout(FieldType fieldType, string sessionId)
        {
            if (!_sessionSceneObjectStorage.HasEntity(sessionId))
            {
                return default;
            }

            var sessionSceneObject = _sessionSceneObjectStorage.Get(sessionId);
            switch (fieldType)
            {
                case FieldType.Hands: return sessionSceneObject.HandsLayout;
                case FieldType.Table: return sessionSceneObject.TableLayout;
                default:              return default;
            }
        }
    }
}