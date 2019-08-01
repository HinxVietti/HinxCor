using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class TimeLine
{
    public int FramePerSecond { get; set; }
    public List<Action> this[int FrameIndex]
    {
        get
        {
            maxReq = maxReq > FrameIndex ? maxReq : FrameIndex;
            return Data[FrameIndex];
        }
    }

    int maxReq = 0;
    LuaTable<List<Action>> Data = new LuaTable<List<Action>>();


    public void Run(Action onfinished)
    {
        Task.Run(() =>
        {
            FramePerSecond = FramePerSecond <= 0 ? 1 : FramePerSecond;
            var tspan = 1000f / FramePerSecond;
            DateTime t = DateTime.MinValue;

            for (int i = 0; i <= maxReq;)
            {
                t = DateTime.Now;
                var actions = this[i];
                if (actions != null)
                    for (int j = 0; j < actions.Count; j++)
                        actions[j]?.Invoke();
                var cost = (DateTime.Now - t).TotalMilliseconds;
                var left = (int)(tspan - cost);
                if (left > 0) Thread.Sleep(left);
                i++;
            }
            onfinished?.Invoke();
            return 0;
        });
    }

}

