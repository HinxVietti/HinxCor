using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace HinxCor.Unity.UI
{
    public class AutoSizeText : UIComponent
    {
        [Header("需要子物体'txt' t: Text,ContainSizeFilter")]
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
}
