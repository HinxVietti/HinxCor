using UnityEngine;
using UnityEngine.UI;

namespace HinxCor.Unity.UI.InfinityScrollList
{

    [RequireComponent(typeof(Mask))]
    public class InfinityScrollList<T> : UIComponent
    {
#pragma warning disable
        protected ArrayItemData<T> BindingData;
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
        private float PyOffset;
        private float maxLength { get { return (BindingData.Array.Length * (ItemSize.y + Spacing) - rectSize.y); } }
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
            for (int i = 1; i < maxContain; i++)
                Binders[i] = Instantiate(prefab.gameObject, transform).GetComponent<DataBinder>();
            for (int i = 0; i < data.Array.Length; i++)
                data.Array[i].position = -((i + 0.5f) * (new Vector2(0, ItemSize.y + Spacing)));

            for (int i = 1; i < maxContain; i++)
            {
                Binders[i].transform.localPosition =
                    Binders[i - 1].transform.localPosition -
                    new Vector3(0, ItemSize.y + Spacing, 0);
            }
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
                    var y = Binders[i].transform.localPosition.y;
                    if (y > 0.5f * Spacing || y < rect.sizeDelta.y + 0.5f * Spacing)
                        Binders[i].gameObject.SetActive(false);
                }
                does = false;
                for (int i = 0; i < BindingData.Array.Length; i++)
                    if (inDisplay(BindingData.Array[i]))
                        doDisplay(BindingData.Array[i]);

            }
            else does = true;
        }

        private void doDisplay(ItemData<T> itemData)
        {
            var binder = GetBinder();
            if (!binder) return;
            binder.gameObject.SetActive(true);
            binder.BindData(itemData, PyOffset);
        }

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