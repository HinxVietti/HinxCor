using Common;
using UnityEngine;
using UnityEngine.UI;


#pragma warning disable 649
public class HGUI_ColorPicker : MonoSingleton<HGUI_ColorPicker>
{
    [SerializeField] private Image m_picker, m_locater, m_result;
    [SerializeField] private Slider ColorBand, AlphaBand, AlphaValue;
    [SerializeField] private InputField AlphaTxt, HexColorTxt;
    [SerializeField] private Vector3 offset;

    private Vector3 locaterposition;

    CallBack OnClosePanelCallBack;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowColorPicker(CallBack call)
    {
        transform.localPosition = Vector3.zero;//位置归零
        gameObject.SetActive(true);
        OnClosePanelCallBack = call;
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
        if (OnClosePanelCallBack != null)
        {
            OnClosePanelCallBack();
        }
        OnClosePanelCallBack = null;
    }

    public void DestroyPanel()
    {
        Destroy(gameObject);
    }

    private bool codpiccolor = false;

    // [SerializeField] private Vector3 pick_local, pick_trans, canvas_trans, mouse_onsc, d_change;
    private void Update()
    {
        float ain = 0, sin = 0;
        sin = ColorBand.value;
        ain = AlphaBand.value;

        UpdateColorBand(1, sin);
        AlphaTxt.text = ((int)(ain * 255)).ToString();


        if (Input.GetMouseButtonDown(0))
        {
            var downp = Input.mousePosition.ScreenSpaceToCanvasSpace() - (transform.localPosition + m_picker.transform.parent.localPosition) + offset;
            if (downp.x >= -75 && downp.x <= 75 && downp.y >= -75 && downp.y <= 75) codpiccolor = true;
            else codpiccolor = false;
        }
        if (Input.GetMouseButton(0) && codpiccolor)
        {
            locaterposition = Input.mousePosition.ScreenSpaceToCanvasSpace() - (transform.localPosition + m_picker.transform.parent.localPosition) + offset;
            m_locater.transform.localPosition = locaterposition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            codpiccolor = false;
        }

        var tmp = m_locater.transform.localPosition;
        tmp.x = Mathf.Clamp(tmp.x, -75, 75);
        tmp.y = Mathf.Clamp(tmp.y, -75, 75);

        m_locater.transform.localPosition = tmp;
        UpdateColor(tmp, ain);
        var tmp_color = Color;
        tmp_color.a = 1;
        m_result.color = tmp_color;
        AlphaValue.value = ain;

        HexColorTxt.text = Color.ToHexString();
    }

    private void UpdateColor(Vector2 position, float ain)
    {

        position += Vector2.one * 75;
        var r_x = position.x / 150f;
        var r_y = position.y / 150f;
        r_x = 1 - r_x;//取反

        var bandc = m_picker.color;
        var d_bandc = bandc.D_COLOR();

        d_bandc = d_bandc * r_x;
        bandc += d_bandc;
        Color = bandc * r_y;
        Color.a = ain;
    }

    private void UpdateColorBand(float ain, float sin)
    {
        Color color = Color.white;
        //混色器计算
        //rgb

        color.a = ain;

        var vu = sin / (1f / 3f);
        if (vu >= 0 && vu <= 1)
        {
            //先绿增，后红减少；
            //红绿混色
            color.g = 2 * vu;
            color.r = 2 - 2 * vu;
            color.b = 0;
        }

        if (vu > 1 && vu <= 2)
        {
            //绿蓝混色
            vu -= 1;
            color.g = 2 - 2 * vu;
            color.b = 2 * vu;
            color.r = 0;
        }

        if (vu > 2 && vu <= 3)
        {
            vu -= 2;
            color.b = 2 - 2 * vu;
            color.r = 2 * vu;
            color.g = 0;
        }

        color = color.Clamp();
        m_picker.color = color;
    }

    public static Color Color = Color.white;

}
