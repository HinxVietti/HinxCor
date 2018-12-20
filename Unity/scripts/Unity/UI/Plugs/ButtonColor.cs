using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ButtonColor : MonoBehaviour
{
    MaskableGraphic graphic;
    [SerializeField] ColorBlock color = default(ColorBlock);

    public void Handler(ButtonStates states)
    {
        if (!graphic) graphic = GetComponent<MaskableGraphic>();
        switch (states)
        {
            case ButtonStates.Normal:
                graphic.color = color.normalColor;
                break;
            case ButtonStates.HightLighted:
                graphic.color = color.highlightedColor;
                break;
            case ButtonStates.Pressed:
                graphic.color = color.pressedColor;
                break;
            case ButtonStates.Disabled:
                graphic.color = color.disabledColor;
                break;
            default:
                Debug.LogWarning(states + " Has error on:" + name);
                break;
        }
    }

}
