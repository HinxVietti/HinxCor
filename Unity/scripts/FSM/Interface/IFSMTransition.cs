using System;
using System.Collections.Generic;
using UnityEngine;

namespace HinxCor.FSM
{
    public interface IFSMTransition
    {
        IFSMState State { get; set; }
        void Reason(IFSM machine);
    }
}
