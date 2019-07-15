using HinxCor.WindowsForm;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

public static class MessageUtility
{
    private static PureMessage message;
    private static bool isStart = false;
    private static Thread messageThread, progressThread;

    private static void Start()
    {
        if (isStart) return;
        isStart = true;
        message = new PureMessage();
        messageThread = new Thread(() =>
        {
            IWin32Window w = Control.FromHandle(HinxCor.Win32.User32.GetForegroundWindow());
            //message.Show(w);
            message.ShowDialog(w);
        })
        {
            IsBackground = true
        };

        messageThread.Start();
    }

    public static void ShowString(string title, string msg)
    {
        if (!isStart) Start();
        while (message == null || !message.shown) Thread.Sleep(10);
        message.Set(title, msg);
        message.MidCenter();
    }


    public static void ShowString(string msg) => ShowString(string.Empty, msg);

    /// <summary>
    /// 关闭信息盒子
    /// </summary>
    public static void Clear()
    {
        message.Clear();
        if (messageThread.IsAlive) messageThread.Abort();
    }




    private static bool ProgressBarShowed;
    private static Progressbar Progressbar;

    public static void ShowProgressbar(string message, float value)
    {
        if (!ProgressBarShowed)
        {
            progressThread = new Thread(() =>
            {
                Progressbar = new Progressbar();
                Progressbar.ShowDialog();
            })
            {
                IsBackground = true
            };

            ProgressBarShowed = true;
            progressThread.Start();
        }
        while (Progressbar == null || !Progressbar.isReady) Thread.Sleep(100);
        Progressbar.setMessage(message, value);
    }


    public static void ClearProgressBar()
    {
        if (Progressbar != null)
            Progressbar._EX_CLOSE();
        if (progressThread.IsAlive)
            progressThread.Abort();
        ProgressBarShowed = false;
    }

}

