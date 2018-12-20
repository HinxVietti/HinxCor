using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HinxCor.Unity.UI
{
    public class Selectable : Interactivable
    {
        public static Selectable Selected;

        public Action<Selectable> OnSelected
        {
            get
            {
                if (m_OnSelected == null)
                    m_OnSelected = o => { };
                return m_OnSelected;
            }
            set
            {
                m_OnSelected = value;
            }
        }
        public Action<Selectable> OnCancelSelected
        {
            get
            {
                if (m_OnCancelSelected == null) m_OnCancelSelected = o => { };
                return m_OnCancelSelected;
            }
            set
            {
                m_OnCancelSelected = value;
            }
        }
        private Action<Selectable> m_OnSelected;
        private Action<Selectable> m_OnCancelSelected;

        protected override void Awake()
        {
            OnMouseClicked += e =>
            {
                if (Selected == this && !Hovered != this)
                {
                    Selected = null;
                    OnCancelSelected(this);
                }
                else if (Hovered == this)
                {
                    Selected = this;
                    OnSelected(this);
                }
            };
        }
    }
}
