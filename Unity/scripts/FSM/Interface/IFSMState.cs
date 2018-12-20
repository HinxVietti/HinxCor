using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HinxCor.FSM
{
    public interface IFSMState
    {
        List<IFSMTransition> Transitions { get; set; }
        void AddTransition(IFSMTransition transition);
        void Enter();
        void Doing();
        void Exit();
    }

    /*说明：
     * 单位：动画状态机状态
     * 动作：
     * Stage；Acting
     * 注册行为（依赖Transition）
     */
}