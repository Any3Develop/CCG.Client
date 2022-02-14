using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame.Cards
{
    public class Selectable : MonoBehaviour 
                             ,IPointerEnterHandler
                             ,IPointerExitHandler
                             ,IBeginDragHandler
                             ,IDragHandler
                             ,IEndDragHandler
    {
        public event EventHandler EnterEvent;
        public event EventHandler ExitEvent;
        public event EventHandler DragBeginEvent;
        public event EventHandler DragEvent;
        public event EventHandler DragEndEvent;
        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            EnterEvent?.Invoke(this, EventArgs.Empty);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ExitEvent?.Invoke(this, EventArgs.Empty);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            DragBeginEvent?.Invoke(this, EventArgs.Empty);
        }
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            DragEvent?.Invoke(this, EventArgs.Empty);
        }
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            DragEndEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}