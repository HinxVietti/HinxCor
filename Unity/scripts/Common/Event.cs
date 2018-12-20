using System;
using System.Collections.Generic;
using System.Text;

namespace HinxCor.Common
{
    public class Event<T>
    {
        private Action<T> action;

        public Event()
        {
            //action = ()=> { };
        }

    }
}

