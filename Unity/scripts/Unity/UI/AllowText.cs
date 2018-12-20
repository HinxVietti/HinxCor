using HinxCor.UI;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 结构为Rect Transform + ./Text 的自动修正大小的Text Label。 rect 为文字背景
/// </summary>
[System.Obsolete("Using Auto size text install")]
public class AllowText : HBehaviour
{
    [Header("需要子物体'allowtxt' t: Text")]
    [TextArea(2, 5)]
    [SerializeField] private string str;
    [SerializeField] private Vector2 offset;
    private Text _txt;
    private Text txt
    {
        get
        {
            if (!_txt)
                //_txt = transform.Find("allowtxt").GetComponent<Text>();
                _txt = GetComponentInChildren<Text>();
            return _txt;
        }
    }
    RectTransform rect;

    private void Update()
    {
        if (!rect) rect = this.transform.GetRect();
        rect.sizeDelta = txt.rectTransform.sizeDelta + offset;
    }

    public void SetText(string value)
    {
        this.str = value;
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (!rect) rect = this.transform.GetRect();
        txt.text = str;
        txt.transform.localPosition = Vector3.zero;
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        UpdateValue();
    }
#endif
}
