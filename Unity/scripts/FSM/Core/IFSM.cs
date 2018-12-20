using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HinxCor.FSM
{

    /// <summary>
    /// 请继承者自行设置默认状态和当前状态属性;
    /// 使用者可以使用MakeFSM定制转换
    /// </summary>
    public interface IFSM
    {
        IFSMState Current { get; }
        List<IFSMState> States { get; set; }
        void RegisterState(IFSMState State);
        void DeleteState(IFSMState state);
        void PerformTransition(IFSMState state);
        void Reset();
        void Run();
        void Quit();
    }

    /*说明
     * 单位：有限状态机
     * 动作：
     * *注册状态；
     * *持有状态；
     * *切换状态；
     */
}
