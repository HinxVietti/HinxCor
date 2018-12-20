using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 如果兄弟层级存在NumberBox则会主动绑定
/// </summary>
public class UserSlider : Slider
{
    private UserNumberBox NumberBox;

    protected override void Start()
    {
        NumberBox = transform.parent.GetComponentInChildren<UserNumberBox>();
        if (NumberBox) NumberBox.OnValueChanged = nvalue =>
        {
            this.value = nvalue;
        };
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying == false)
            return;
#endif
        if (!NumberBox) return;
        //if (NumberBox.NumberType != UserNumberBox.StringNumberType.Percentage) return;
        NumberBox.Value = this.value;
    }

}
