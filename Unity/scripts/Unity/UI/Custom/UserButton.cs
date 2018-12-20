using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HinxCor.Unity.UI
{



    public class UserButton :Button
    {
        /**********************************User Function************************************************/


        public ButtonStates buttonState { get { return ToButtonState(this.currentSelectionState); } }

        protected Color HighlightedColor;
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            this.HighlightedColor = colors.highlightedColor;
            var copies = colors;
            copies.highlightedColor = colors.normalColor;
            colors = copies;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (HighlightedColor.a == 0) return;
            var copies = colors;
            copies.highlightedColor = this.HighlightedColor;
            colors = copies;
        }

        private ButtonStates ToButtonState(SelectionState currentSelectionState)
        {
            switch (currentSelectionState)
            {
                case SelectionState.Normal:
                    return ButtonStates.Normal;
                case SelectionState.Highlighted:
                    return ButtonStates.HightLighted;
                case SelectionState.Pressed:
                    return ButtonStates.Pressed;
                case SelectionState.Disabled:
                    return ButtonStates.Disabled;
                default:
                    break;
            }
            return ButtonStates.Normal;
        }
    }

}

public enum ButtonStates
{
    Normal,
    HightLighted,
    Pressed,
    Disabled
}