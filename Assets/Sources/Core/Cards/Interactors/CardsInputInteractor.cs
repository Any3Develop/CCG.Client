using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Cameras;
using CardGame.Layouts;
using CardGame.Services.InputService;
using CardGame.Session;
using CardGame.Session.SceneObject;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CardGame.Cards
{
    public class CardsInputInteractor
    {
        private readonly string _sessionId;
        private readonly IInstantiator _instantiator;
        private readonly IInputController<HandFieldLayer> _inputControllerHand;
        private readonly IInputController<TableFieldLayer> _inputControllerTable;
        private readonly SessionSceneObjectStorage _sessionSceneObjectStorage;
        private readonly CardSceneObjectStorage _cardSceneObjectStorage;
        private readonly Camera _camera;
        private readonly CardDtoStorage _cardDtoStorage;
        private readonly List<string> _registred;
        private readonly CompositeDisposable _events;
        private SessionSceneObject _sessionSceneObject;
        private Vector3 _initPosDarg;
        private Quaternion _initRotDarg;
        private CardSceneObject _current;
        private int _initSortOrder;
        private FieldType _from;
        private bool _dragged;

        public CardsInputInteractor(string sessionId,
                                    IInstantiator instantiator,
                                    IInputController<HandFieldLayer> inputControllerHand,
                                    IInputController<TableFieldLayer> inputControllerTable,
                                    SessionSceneObjectStorage sessionSceneObjectStorage,
                                    CardSceneObjectStorage cardSceneObjectStorage,
                                    Camera camera,
                                    CardDtoStorage cardDtoStorage)
        {
            _sessionId = sessionId;
            _instantiator = instantiator;
            _inputControllerHand = inputControllerHand;
            _inputControllerTable = inputControllerTable;
            _sessionSceneObjectStorage = sessionSceneObjectStorage;
            _cardSceneObjectStorage = cardSceneObjectStorage;
            _camera = camera;
            _cardDtoStorage = cardDtoStorage;
            _registred = new List<string>();
            _events = new CompositeDisposable();
        }

        public void Initialize()
        {
            _sessionSceneObject = _sessionSceneObjectStorage.Get(_sessionId);
            MessageBroker.Default
                .Receive<RemoveCardProtocol>().Subscribe(_ => UpdateTriggers())
                .AddTo(_events);
            
            MessageBroker.Default
                .Receive<AddCardProtocol>().Subscribe(_ => UpdateTriggers())
                .AddTo(_events);
            
            UpdateTriggers();
        }
        
        public void Dispose()
        {
            foreach (var id in _registred.ToArray())
            {
                if (!_cardSceneObjectStorage.HasEntity(id))
                {
                    continue;
                }

                var sceneObject = _cardSceneObjectStorage.Get(id);
                if (sceneObject && sceneObject.TryGetComponent(out Selectable selectable))
                {
                    selectable.EnterEvent -= SelectableOnEnterEvent;
                    selectable.ExitEvent -= SelectableOnExitEvent;
                    selectable.DragBeginEvent -= SelectableOnDragBeginEvent;
                    selectable.DragEvent -= SelectableOnDragEvent;
                    selectable.DragEndEvent -= SelectableOnDragEndEvent;
                }
            }
            _registred.Clear();
        }

        private void UpdateTriggers()
        {
            foreach (var id in _registred.ToArray())
            {
                if (!_cardSceneObjectStorage.HasEntity(id))
                {
                    _registred.Remove(id);
                }
            }

            foreach (var sceneObject in _cardSceneObjectStorage.Get().ToArray())
            {
                if (_registred.Contains(sceneObject.Id))
                {
                    continue;
                }

                if (sceneObject.TryGetComponent(out Selectable selectable))
                {
                    selectable.EnterEvent += SelectableOnEnterEvent;
                    selectable.ExitEvent += SelectableOnExitEvent;
                    selectable.DragBeginEvent += SelectableOnDragBeginEvent;
                    selectable.DragEvent += SelectableOnDragEvent;
                    selectable.DragEndEvent += SelectableOnDragEndEvent;
                    _registred.Add(sceneObject.Id);
                }
            }
        }

        private bool Select(object target)
        {
            if (target is Selectable selected)
            {
                if (!_current)
                {
                    return selected.TryGetComponent(out _current);
                }
                
                return !SameSelection(target) && selected.TryGetComponent(out _current);
            }
            return _current = null;
        }

        private bool SameSelection(object target)
        {
            return _current &&
                   target is Selectable selected &&
                   _current.GameObject == selected.gameObject;
        }
        
        private void SelectableOnEnterEvent(object sender, EventArgs e)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (_inputControllerHand.Locked)
            {
                return;
            }
            
            if (!Select(sender))
            {
                _inputControllerHand.Unlock();
                return;
            }

            if (_dragged)
            {
                return;
            }
            
            _inputControllerHand.Lock();
            _current.SetOutlineActive(true);
        }
        
        private void SelectableOnExitEvent(object sender, EventArgs e)
        {
            if (!SameSelection(sender))
            {
                return;
            }

            if (_dragged)
            {
                return;
            }
            _inputControllerHand.Unlock();
            _current.SetOutlineActive(false);
            _dragged = false;
            _current = null;
        }
        
        private void SelectableOnDragBeginEvent(object sender, EventArgs e)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (!SameSelection(sender))
            {
                return;
            }

            if (_dragged)
            {
                return;
            }

            _initPosDarg = _current.transform.position;
            _initRotDarg = _current.transform.rotation;
            _initSortOrder = _current.SortOrder;
            _current.SetLayerOrder(20);
            _from = _cardDtoStorage.Get(_current.Id).CurrentField;
            _dragged = true;
        }
        
        private void SelectableOnDragEvent(object sender, EventArgs e)
        {
            if (!_current)
            {
                return;
            }
            if (!SameSelection(sender))
            {
                return;
            }
            
            var cardPos = _camera.WorldToScreenPoint(_current.transform.position);
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cardPos.z);
            _current.transform.position = _camera.ScreenToWorldPoint(mousePosition);
            _current.transform.rotation = Quaternion.identity;
        }
        
        private void SelectableOnDragEndEvent(object sender, EventArgs e)
        {
            if (!_current)
            {
                return;
            }
            
            if (!SameSelection(sender))
            {
                return;
            }


            for (var i = Enum.GetValues(typeof(FieldType)).Length; i > 0; i--)
            {
                if (i-1 == 0)
                {
                    Place(_current.Id, _from, _current.transform.GetSiblingIndex());
                    break;
                }

                var field = (FieldType) i - 1;
                if (LayoutContains(field, _current.transform.position) && _from != field)
                {
                    Place(_current.Id, field);
                    break;
                }
            }
            
            _initSortOrder = 0;
            _dragged = false;
            _from = FieldType.OffScreen;
            SelectableOnExitEvent(sender, e);
        }

        private void Place(string cardId, FieldType fieldType, int place = -1)
        {
            _instantiator.Instantiate<PlaceCardCommand>().Execute(new PlaceCardProtocol
            {
                CardId = cardId,
                Place = place,
                ToPlace = fieldType,
                SessionId = _sessionId
            });
        }

        private bool LayoutContains(FieldType fieldType, Vector3 position)
        {
            return GetLayout(fieldType)?.Rect.Contains(position) ?? false;
        }

        private HorizontalCellLayout GetLayout(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.Hands: return _sessionSceneObject.HandsLayout;
                case FieldType.Table: return _sessionSceneObject.TableLayout;
                default:              return default;
            }
        }
    }
}