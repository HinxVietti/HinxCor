using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HinxCor.UI
{
    public class Selectable : HBehaviour,
        IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
#pragma warning disable 414
       protected static Selectable s_Hovered;
       protected static Selectable s_SelectObj;
       protected bool m_IsMouseStay;
       protected bool m_IsPressed;
#pragma warning restore 414

        protected UnityAction OnMouseEnter;
        protected UnityAction OnMouseExit;
        protected UnityAction OnMouseDown;
        protected UnityAction OnMouseUp;
        protected UnityAction OnMouseClick;

        protected Selectable()
        {
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (m_IsMouseStay)
                s_SelectObj = this;
            OnMouseClick.TryInvoke();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            m_IsPressed = true;
            OnMouseDown.TryInvoke();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            m_IsMouseStay = true;
            s_Hovered = this;
            OnMouseEnter.TryInvoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            m_IsMouseStay = false;
            if (s_Hovered == this)
                s_Hovered = null;
            OnMouseExit.TryInvoke();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            m_IsPressed = false;
            OnMouseUp.TryInvoke();
        }
    }
}
