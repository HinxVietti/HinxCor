using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace HinxCor.Unity.UI
{
    public class Interactivable : UIComponent,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler
    {
        public static Interactivable Hovered;
        internal Action<PointerEventData> _nor = (e => { });
        public Action<PointerEventData> OnMouseClicked
        {
            get
            {
                if (m_OnMouseClicked == null)
                    m_OnMouseClicked = _nor;
                return m_OnMouseClicked;
            }
            set
            {
                m_OnMouseClicked = value;
            }
        }
        public Action<PointerEventData> OnMouseEnter
        {
            get
            {
                if (m_OnMouseEnter == null)
                    m_OnMouseEnter = _nor;
                return m_OnMouseEnter;
            }
            set
            {
                m_OnMouseEnter = value;
            }
        }
        public Action<PointerEventData> OnMouseExit
        {
            get
            {
                if (m_OnMouseExit == null)
                    m_OnMouseExit = _nor;
                return m_OnMouseExit;
            }
            set
            {
                m_OnMouseExit = value;
            }
        }
        public Action<PointerEventData> OnMouseDown
        {
            get
            {
                if (m_OnMouseDown == null)
                    m_OnMouseDown = _nor;
                return m_OnMouseDown;
            }
            set
            {
                m_OnMouseDown = value;
            }
        }
        public Action<PointerEventData> OnMouseUp
        {
            get
            {
                if (m_OnMouseUp == null)
                    m_OnMouseUp = _nor;
                return m_OnMouseUp;
            }
            set
            {
                m_OnMouseUp = value;
            }
        }

        private Action<PointerEventData> m_OnMouseClicked;
        private Action<PointerEventData> m_OnMouseEnter;
        private Action<PointerEventData> m_OnMouseExit;
        private Action<PointerEventData> m_OnMouseDown;
        private Action<PointerEventData> m_OnMouseUp;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnMouseClicked(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnMouseDown(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Hovered = this;
            OnMouseEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Hovered == this) Hovered = null;
            OnMouseExit(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnMouseUp(eventData);
        }

        public static implicit operator UnityEngine.GameObject(Interactivable interactivable)
        {
            return interactivable.gameObject;
        }
    }
}
