using UnityEngine;

namespace HinxCor.Unity.UI.InfinityScrollList
{
    public abstract class DataBinder : UIComponent
    {
        public virtual void BindData<T>(ItemData<T> obj, float offset)
        {
            var p = obj.position;
            p.y += offset;
            //this.transform.GetRect().anchoredPosition = p; //in package monoHelper.
            (transform as RectTransform).anchoredPosition = p;
        }
        public virtual bool IsInVisible() { return false; }

    }
}
