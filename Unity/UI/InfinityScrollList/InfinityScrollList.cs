using System;
using UnityEngine;
using UnityEngine.UI;

namespace HinxCor.Unity.UI.InfinityScrollList
{

    [RequireComponent(typeof(Mask))]
    public class InfinityScrollList<T> : UIComponent
    {
#pragma warning disable
        protected ArrayItemData<T> m_BindingData;
        protected DataBinder[] Binders;
        protected RectTransform rect;
        protected Vector2 rectSize
        {
            get
            {
                if (!rect) rect = transform as RectTransform;
                return
                    rect.sizeDelta + sizeOffset;
            }
        }
        protected bool isMouseOn
        {
            get
            {
                Vector2 d = Input.mousePosition - transform.position;
                //d = d.abs(); // in packeage monohelper
                d.x = Mathf.Abs(d.x);
                d.y = Mathf.Abs(d.y);
                return d.x < rectSize.x / 2 && d.y < rectSize.y / 2;
            }
        }
        [SerializeField] private DataBinder prefab;
        //[SerializeField] private Vector2 ItemSize;
        [SerializeField] private Vector2 ItemSize { get { return (prefab.transform as RectTransform).sizeDelta; } }
        [SerializeField] private float Spacing;
        [SerializeField] private Vector2 sizeOffset;
        [SerializeField] private float MouseSensitive = 25;
        private int maxContain;
        public float PyOffset { get; protected set; }
        public ArrayItemData<T> BindingData { get { return m_BindingData; } protected set { m_BindingData = value; } }
        private float maxLength
        {
            get
            {
                return Mathf.Clamp((BindingData.Array.Length * (ItemSize.y + Spacing) - rectSize.y), 1, 9999);
            }
        }
        //[SerializeField] private Vector4 Padding; //mid center
#pragma warning restore
        [SerializeField] private MovementType m_MovementType;
#if true
        [Tooltip("Only work while MovementType = elastic.")]
        [SerializeField] private float ElasticSensitive = 2.5f;
#endif

        public void Init(ArrayItemData<T> data)
        {
            BindingData = data;
            rect = transform as RectTransform;
            var size = rect.sizeDelta;
            maxContain = (int)(size.y / (ItemSize.y + Spacing)) + 2;
            Binders = new DataBinder[maxContain];
            Binders[0] = prefab;
            prefab.gameObject.SetActive(false);
            for (int i = 1; i < maxContain; i++)
                Binders[i] = Instantiate(prefab.gameObject, transform).GetComponent<DataBinder>();
            for (int i = 0; i < data.Array.Length; i++)
            {
                data.Array[i].position = -((i + 0.5f) * (new Vector2(0, ItemSize.y + Spacing)));
                data.Array[i].Primer = this;
            }

            for (int i = 1; i < maxContain; i++)
            {
                Binders[i].transform.localPosition =
                    Binders[i - 1].transform.localPosition -
                    new Vector3(0, ItemSize.y + Spacing, 0);
            }
        }


        public void AppendItem(ItemData<T> item)
        {
            var copy = BindingData.Array;
            int count = BindingData.Array.Length;
            BindingData.Array = new ItemData<T>[count + 1];
            for (int i = 0; i < count; i++)
                BindingData.Array[i] = copy[i];
            BindingData.Array[count] = item;
            NewInstanceItem(item);
        }

        private void NewInstanceItem(ItemData<T> item)
        {
            item.position = -(BindingData.Array.Length - 1 + 0.5f) * (new Vector2(0, ItemSize.y + Spacing));
            item.Primer = this;
        }

        private void Update()
        {
            if (BindingData == null) return;
            if (isMouseOn)
                PyOffset += Input.GetAxis("Mouse ScrollWheel") * MouseSensitive;
            switch (m_MovementType)
            {
                case MovementType.Unrestricted:
                    break;
                case MovementType.Clamped:
                    PyOffset = Mathf.Clamp(PyOffset, 0, maxLength);
                    break;
                case MovementType.Elastic:
                    var cmp = Mathf.Clamp(PyOffset, 0, maxLength);
                    var d = cmp - PyOffset;
                    PyOffset += d * ElasticSensitive * Time.deltaTime;
                    break;
            }
        }

        private bool does;

        private void FixedUpdate()
        {
            if (BindingData == null) return;
            if (does)
            {
                for (int i = 0; i < Binders.Length; i++)
                {
                    if (Binders[i].gameObject.activeInHierarchy)
                    {
                        var blan = Binders[i].IsInVisible(ItemSize.y);
                        if (blan)
                        {
                            Binders[i].gameObject.SetActive(false);

                            Binders[i].GetBinderData<ItemData<T>>().HasDisplay = false;
                        }
                    }
                    //if (Binders[i].gameObject.activeInHierarchy
                    //    && !Binders[i].IsInVisible())
                    //    Binders[i].gameObject.SetActive(false);
                    //var y = Binders[i].transform.GetRect().anchoredPosition.y;
                    //if ((y > 0.5f * Spacing || y < -rect.sizeDelta.y - 0.5f * Spacing))//范围之外设为false 没毛病
                    //{
                    //    Binders[i].gameObject.SetActive(false);
                    //}
                }
                does = false;
                for (int i = 0; i < BindingData.Array.Length; i++)
                    if (inDisplay(BindingData.Array[i]))
                        doDisplay(BindingData.Array[i]);
                    else BindingData.Array[i].HasDisplay = false;

            }
            else does = true;
            //-- for test interrupt.
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    int a = 0;
            //}
        }

        private void doDisplay(ItemData<T> itemData)
        {
            var binder = GetBinder();
            if (!binder) return;
            if (itemData.HasDisplay) return;
            binder.gameObject.SetActive(true);
            binder.BindData(itemData);
            itemData.HasDisplay = true;
        }

        //判定
        private bool inDisplay(ItemData<T> itemData)
        {
            var py = itemData.position.y + PyOffset;
            var res = (py > -(rect.sizeDelta.y + 0.5f * (Spacing + ItemSize.y)))
                && (py < 0.5f * (Spacing + ItemSize.y));
            return res;
        }


        private DataBinder GetBinder()
        {
            foreach (var binder in Binders)
            {
                if (!binder.gameObject.activeInHierarchy) return binder;
            }
            return null;
        }

        private enum MovementType
        {
            Unrestricted,
            Clamped,
            Elastic
        }
    }
}


//private void Test()
//{
//    string str = "text";
//    var array = str.ToArray();

//    Init(new charData(array));
//}

//private class charData : ListItemData<char>
//{
//    public charData(char[] c)
//    {
//        data = c;
//    }

//    public static implicit operator charData(char[] c)
//    {
//        return new charData(c);
//    }
//}