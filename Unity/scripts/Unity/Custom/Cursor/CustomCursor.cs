using HinxCor.Unity.UI;
using UnityEngine;

public class CustomCursor : UIComponent
{
    private static CustomCursor I;

    [SerializeField]
    private KeyImage[] cursors;
    private KeyImage current;
    [SerializeField] private float cursorSize;

    protected override void Awake()
    {
        if (I == this) return;
        if (!I) I = this;
        else
        {
            Debug.LogError("you have add multiple custom cursor ,which is not supported. " + transform.name + " & " + I.name);
            DestroyImmediate(this.gameObject);
        }
    }

    protected override void Start()
    {
        current = cursors[0];
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(
            Event.current.mousePosition.x - current.offset.x * cursorSize,//offsetx
            Event.current.mousePosition.y - current.offset.y * cursorSize,//offsety
            cursorSize, cursorSize),
            current.Cursor);
    }

    [System.Serializable]
    public class KeyImage
    {
        public string key;
        public Texture Cursor;
        [Tooltip("0-1")]
        public Vector2 offset = Vector2.one * 0.5f;
    }
}
