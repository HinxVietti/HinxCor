using UnityEngine.UI;
using UnityEngine;

public class ToggleButtons : MonoBehaviour
{
    public Button Current { get; set; }
    [SerializeField] Button[] Buttons;
#pragma warning disable
    [SerializeField] Color Actived, Disabled;
#pragma warning restore
    private void Start()
    {
        foreach (var item in Buttons)
        {
            item.onClick.AddListener(() =>
            {
                BtnOnClick(item);
            });
        }
        SetIndex(0);
    }

    public void SetString(string buttonName)
    {
        Button btn = Buttons[0];
        foreach (var _btn in Buttons)
        {
            if (_btn.name == buttonName)
            {
                btn = _btn;
                break;
            }
        }
        BtnOnClick(btn);
    }

    public void SetIndex(int index)
    {
        if (index < 0) index = 0;
        if (index >= Buttons.Length) index = Buttons.Length - 1;
        BtnOnClick(Buttons[index]);
    }

    private void BtnOnClick(Button btn)
    {
        Current = btn;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Buttons[i] == Current)
                Active(Current);
            else Disable(Buttons[i]);
        }
    }

    private void Disable(Button button)
    {
        button.image.color = Disabled;
    }

    private void Active(Button current)
    {
        current.image.color = Actived;
    }
}
