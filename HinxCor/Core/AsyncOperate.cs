using System;
using System.Collections.Generic;
using System.Threading;

namespace HinxCor
{

    /// <summary>
    /// 请在构造AO后手动Start启动
    /// </summary>
    public class AsyncOperate
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public AsyncOperate(IAsyncOperator @operator, int timerout = 100)
        {
            this.timerout = timerout;

            //@operator.SetAO(this);
            LogUpdate = () =>
            {
                isDone = @operator.GetIsDoneState();
                progress = @operator.GetProcess();
                progress = HMath.Clamp01(progress);
                Log = @operator.GetLog();
                HLog = @operator.GetHLog();
                _logs.Add(HLog);
            };
        }

        public void Start()
        {
            if (hasStart) return;
            THR = new Thread(() =>
            {
                while (isDone == false)
                {
                    LogUpdate();
                }
                if (completed != null)
                {
                    completed.Invoke(this);
                    completed = null;
                }
                Thread.Sleep(timerout);
            })
            {
                IsBackground = true
            };
            THR.Start();
        }


        /// <summary>
        /// 析构函数
        /// </summary>
        ~AsyncOperate()
        {
            if (THR.IsAlive == true)
                THR.Abort();
            if (completed != null)
            {
                completed.Invoke(this);
                completed = null;
            }
            _logs = null;
            LogUpdate = null;
            THR = null;
            HLog = null;
            completed = null;
            Log = this + " is release.";
        }

        protected Thread THR;
        protected Action LogUpdate;
        private bool hasStart = false;
        private int timerout = 100;
        public bool isDone { get; protected set; }
        public float progress { get; protected set; }
        public string Log { get; protected set; }

        protected List<FormerLog> _logs = new List<FormerLog>();
        public FormerLog[] Logs { get { return _logs.ToArray(); } }
        public FormerLog HLog { get; protected set; }

        /// <summary>
        /// 任务完成时执行,无论是失败还是成功, 可以通过读取传入的AO 对象读取状态和日志
        /// </summary>
        public event Action<AsyncOperate> completed;
    }
}

