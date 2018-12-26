using UnityEngine;

namespace HinxCor.Unity.UI.InfinityScrollList
{
    public abstract class DataBinder : UIComponent
    {
        protected object data;

        public T GetBinderData<T>() where T : class
        {
            return data as T;
        }

        public virtual void BindData<T>(ItemData<T> obj)
        {
            if (obj == null) return;
            data = obj;
            var p = obj.position;
            p.y += obj
                .Primer
                .PyOffset;
            //this.transform.GetRect().anchoredPosition = p; //in package monoHelper.
            (transform as RectTransform).anchoredPosition = p;
        }

        private RectTransform prect, rect;

        /// <summary>
        /// 是否在显示范围内
        /// </summary>
        /// <param name="itemy"></param>
        /// <returns></returns>
        public virtual bool IsInVisible(float itemy)
        {
            if (!prect) prect = GetComponentInParent<RectTransform>();
            if (!rect)
                //rect = transform.GetRect();
                rect = transform as RectTransform;
            var sy = prect.sizeDelta.y;
            return rect.anchoredPosition.y > itemy || rect.anchoredPosition.y < -itemy - sy;
        }

    }
}
