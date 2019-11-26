using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

public static class StartUpUtility
{
    static Thread wfThread;
    static HinxCor.WindowsForm.ApplicationStartPage startPage;
    private static bool finished = false;

    public static void Start()
    {
        wfThread = new Thread(windowsFormThread)
        {
            IsBackground = true
        };
        wfThread.Start();
    }


    public static void End()
    {
        finished = false;
        if (wfThread != null)
        {
            if (startPage != null)
            {
                startPage.ClosePanel();
            }
            if (wfThread.IsAlive)
                wfThread.Abort();
        }
        wfThread = null;
    }


    private static void windowsFormThread()
    {
        startPage = new HinxCor.WindowsForm.ApplicationStartPage();
        startPage.SetImage();
        finished = true;
        startPage.ShowDialog();
    }

    public static void SetColor(Color color)
    {
        if (finished) startPage?.ChangeTextColor(color);
    }

    public static void SetMessage(Point point, string message)
    {
        if (finished) startPage?.DrawLog(point, message);
    }




}

