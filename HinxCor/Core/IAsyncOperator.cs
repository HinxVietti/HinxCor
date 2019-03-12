using System;
using System.Collections.Generic;

namespace HinxCor
{

    public interface IAsyncOperator
    {
        string GetLog();
        FormerLog GetHLog();
        float GetProcess();
        bool GetIsDoneState();
        //AsyncOperate AO { get; }
        //void SetAO(AsyncOperate ao);
        //void OutLog(out string log);
        //void OutHLog(out FormerLog hlog);
        //void OutProcess(out float process);
        //void OutState(out bool state);
    }
}

