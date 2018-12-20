using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HinxCor.Unity.UI
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.UI.Text))]
    public class HyperText : Interactivable
    {
        private UnityEngine.UI.Text m_Text;
        public UnityEngine.UI.Text Text { get { if (!m_Text) m_Text = GetComponent<UnityEngine.UI.Text>(); return m_Text; } set { m_Text = value; } }

        [SerializeField] private Color normal;
        [SerializeField] private Color hover;

        protected virtual void Update()
        {
            if (this == Hovered)
                SetColor(hover);
            else SetColor(normal);

        }
        void SetColor(Color color)
        {
            Text.color = color;
        }
    }
}
