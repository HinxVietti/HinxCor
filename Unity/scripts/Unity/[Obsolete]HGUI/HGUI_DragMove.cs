
//[UnityEngine.RequireComponent(typeof(UnityEngine.UI.Image))]
using UnityEngine.EventSystems;

public class HGUI_DragMove : UnityEngine.UI.Selectable
{
    [UnityEngine.Header("拖拽移动的目标，注意：使用者自身必须是继承自Graphic的")]
    [UnityEngine.SerializeField]
    private UnityEngine.Transform m_target;

    /// <summary>
    /// 是否是被按下的状态
    /// </summary>
    public bool IsActiveInSprite { get { return currentSelectionState == SelectionState.Pressed; } }

    protected override void Start()
    {
        base.Start();
        transition = Transition.None;
    }

    private bool domove = false;
    private UnityEngine.Vector3 oldpos;
    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0) && currentSelectionState == SelectionState.Pressed)
        {
            oldpos = UnityEngine.Input.mousePosition;
            domove = true;
        }
        if (m_target != null)
            if (domove)
            {
                var m_posi = UnityEngine.Input.mousePosition;
                var d_change = m_posi - oldpos;
                oldpos = m_posi;
                m_target.localPosition += d_change;
            }
        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            domove = false;
        }
    }


}
