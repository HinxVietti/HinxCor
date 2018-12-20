using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonSprites : MonoBehaviour
{
    Image graphic;
    Sprite normal;
    [SerializeField] private SpriteState spriteState;

    public void Handler(ButtonStates state)
    {
        if (!graphic)
        {
            graphic = GetComponent<Image>();
            normal = graphic.sprite;
        }
        switch (state)
        {
            case ButtonStates.Normal:
                graphic.sprite = normal;
                break;
            case ButtonStates.HightLighted:
                if (spriteState.highlightedSprite)
                    graphic.sprite = spriteState.highlightedSprite;
                break;
            case ButtonStates.Pressed:
                if (spriteState.pressedSprite)
                    graphic.sprite = spriteState.pressedSprite;
                break;
            case ButtonStates.Disabled:
                if (spriteState.disabledSprite)
                    graphic.sprite = spriteState.disabledSprite;
                break;
            default:
                break;
        }
    }

}
