using System;
using System.Collections.Generic;

namespace HinxCor.Win32
{
    using LNG = System.Int64;

    /// <summary>
    /// 窗体返回结果
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// abort
        /// </summary>
        IDABORT = 3,
        /// <summary>
        /// cancel
        /// </summary>
        IDCANCEL = 2,
        /// <summary>
        /// continue
        /// </summary>
        IDCONTINUE = 11,
        /// <summary>
        /// ignore
        /// </summary>
        IDIGNORE = 5,
        /// <summary>
        /// no
        /// </summary>
        IDNO = 7,
        /// <summary>
        /// ok
        /// </summary>
        IDOK = 1,
        /// <summary>
        /// retry
        /// </summary>
        IDRETRY = 4,
        /// <summary>
        /// again
        /// </summary>
        IDTRYAGAIN = 10,
        /// <summary>
        /// yes
        /// </summary>
        IDYES = 6
    }

    /// <summary>
    /// 默认按钮
    /// </summary>
    public enum DefaultButtons
    {
        /// <summary>
        /// 有且仅能在HinxCor中调用,请注意其他程序集忽略此项目
        /// </summary>
        INSIDE_ZERO = 0,
        /// <summary>
        /// 显示终止,重试,忽略三个按钮
        /// </summary>
        AbortRetryIgnore = (int)MessageBox.MB_ABORTRETRYIGNORE,
        /// <summary>
        /// 显示取消,重试,继续三个按钮
        /// </summary>
        CancelRetryContinue = (int)MessageBox.MB_CANCELTRYCONTINUE,
        /// <summary>
        /// 单独 OK 按钮
        /// </summary>
        OK = (int)MessageBox.MB_OK,
        /// <summary>
        /// 显示OK和取消两个按钮
        /// </summary>
        OKCancel = (int)MessageBox.MB_OKCANCEL,
        /// <summary>
        /// 显示重试和取消两个按钮
        /// </summary>
        RetryCancel = (int)MessageBox.MB_OKCANCEL,
        /// <summary>
        /// 显示是否两个按钮
        /// </summary>
        YesNo = (int)MessageBox.MB_YESNO,
        /// <summary>
        /// 显示 是 否 和取消 三个按钮
        /// </summary>
        YesNoCancel = (int)MessageBox.MB_YESNOCANCEL
    }

    /// <summary>
    /// 默认显示图标
    /// </summary>
    public enum DefaultICON
    {
        /// <summary>
        /// ! 惊叹号
        /// </summary>
        Warning = (int)MessageBox.MB_ICONWARNING,
        /// <summary>
        /// ! 惊叹号
        /// </summary>
        Exclamation = (int)MessageBox.MB_ICONWARNING,
        /// <summary>
        /// i Info
        /// </summary>
        Infomation = (int)MessageBox.MB_ICONINFORMATION,
        /// <summary>
        /// i Info
        /// </summary>
        Asterisk = (int)MessageBox.MB_ICONINFORMATION,
        /// <summary>
        /// ? 疑问
        /// </summary>
        Question = (int)MessageBox.MB_ICONQUESTION,
        /// <summary>
        /// x 停止
        /// </summary>
        Stop = (int)MessageBox.MB_ICONSTOP,
        /// <summary>
        /// x 错误
        /// </summary>
        Error = (int)MessageBox.MB_ICONSTOP,
        /// <summary>
        /// x 错误
        /// </summary>
        Hand = (int)MessageBox.MB_ICONSTOP
    }

    /// <summary>
    /// 默认选中的图标(建议的默认操作)
    /// </summary>
    public enum DefaultSelectedButton
    {
        /// <summary>
        /// 第一个
        /// </summary>
        Btn1 = (int)MessageBox.MB_DEFBUTTON1,
        /// <summary>
        /// 第二个
        /// </summary>
        Btn2 = (int)MessageBox.MB_DEFBUTTON2,
        /// <summary>
        /// 第三个
        /// </summary>
        Btn3 = (int)MessageBox.MB_DEFBUTTON3,
        /// <summary>
        /// 第四个(注意有没第四个按钮)
        /// </summary>
        Btn4 = (int)MessageBox.MB_DEFBUTTON4
    }

    /// <summary>
    /// 额外选项
    /// </summary>
    public enum MessageBoxOptions
    {
        /// <summary>
        /// 桌面模式(仅仅在激活的桌面上运行)
        /// </summary>
        DefaultDesktopOnly = (int)MessageBox.MB_DEFAULT_DESKTOP_ONLY,
        /// <summary>
        /// 居右对齐
        /// </summary>
        RightAlign = (int)MessageBox.MB_RIGHT,
        /// <summary>
        /// 文本从右边开始(特殊语言文本)
        /// </summary>
        RtlReading = (int)MessageBox.MB_RTLREADING,
        /// <summary>
        /// 调用方是通知用户事件的服务。函数在当前的Active Desktop上显示一个消息框，即使没有用户登录到该计算机。
        /// 终端服务：如果调用线程具有模拟令牌，则该函数将消息框定向到模拟令牌中指定的会话。
        /// 如果设置了此标志，则hwnd参数必须为空。这样，消息框就可以出现在与hwnd相对应的桌面之外的桌面上。
        /// 有关使用此标志的安全注意事项的信息，请参阅交互式服务。
        /// 尤其要注意，此标志可以在锁定的桌面上生成交互式内容，因此应仅用于非常有限的一组场景，如资源耗尽。
        /// </summary>
        ServiceNotification = (int)MessageBox.MB_SERVICE_NOTIFICATION
    }


    /// <summary>
    /// WIN32 打开的 MessageBox;
    /// </summary>
    public static class MessageBox
    {

        #region KEYS
        //KEYS
        /// <summary>
        /// Key Abort Retry IGNORE
        /// </summary>
        public const LNG MB_ABORTRETRYIGNORE = 0x00000002L;
        /// <summary>
        /// Key Cancel Retry Contine
        /// </summary>
        public const LNG MB_CANCELTRYCONTINUE = 0x00000006L;
        /// <summary>
        /// Add Help button to box
        /// </summary>
        public const LNG MB_HELP = 0x00004000L;
        /// <summary>
        /// single OK button
        /// </summary>
        public const LNG MB_OK = 0x00000000L;
        /// <summary>
        /// key cancel OK
        /// </summary>
        public const LNG MB_OKCANCEL = 0x00000001L;
        /// <summary>
        /// key retry cancel
        /// </summary>
        public const LNG MB_RETRYCANCEL = 0x00000005L;
        /// <summary>
        /// key yes no
        /// </summary>
        public const LNG MB_YESNO = 0x00000004L;
        /// <summary>
        /// key Yes No Cancel
        /// </summary>
        public const LNG MB_YESNOCANCEL = 0x00000003L;
        #endregion

        #region ICONS
        //ICONS
        /// <summary>
        /// 惊叹号和三角形 !
        /// </summary>
        public const LNG MB_ICONEXCLAMATION = 0x00000030L;
        /// <summary>
        /// 惊叹号和三角形 !
        /// </summary>
        public const LNG MB_ICONWARNING = 0x00000030L;
        /// <summary>
        /// 提示信息 INFO
        /// </summary>
        public const LNG MB_ICONINFORMATION = 0x00000040L;
        /// <summary>
        /// 提示信息 INFO
        /// </summary>
        public const LNG MB_ICONASTERISK = 0x00000040L;
        /// <summary>
        /// 疑问 ?
        /// </summary>
        public const LNG MB_ICONQUESTION = 0x00000020L;
        /// <summary>
        /// 停止 x
        /// </summary>
        public const LNG MB_ICONSTOP = 0x00000010L;
        /// <summary>
        /// 错误 x
        /// </summary>
        public const LNG MB_ICONERROR = 0x00000010L;
        /// <summary>
        /// 终止 x
        /// </summary>
        public const LNG MB_ICONHAND = 0x00000010L;
        #endregion

        #region DEF BTNS
        //DEF BTN
        /// <summary>
        /// 默认按钮1
        /// </summary>
        public const LNG MB_DEFBUTTON1 = 0x00000000L;
        /// <summary>
        /// 默认按钮2
        /// </summary>
        public const LNG MB_DEFBUTTON2 = 0x000000100L;
        /// <summary>
        /// 默认按钮3
        /// </summary>
        public const LNG MB_DEFBUTTON3 = 0x000000200L;
        /// <summary>
        /// 默认按钮4
        /// </summary>
        public const LNG MB_DEFBUTTON4 = 0x000000300L;
        #endregion

        #region LOAD MOD
        //DIALOG MOD
        /// <summary>
        /// 在hwnd参数标识的窗口中继续工作之前，用户必须响应消息框。但是，用户可以移动到其他线程的窗口并在这些窗口中工作。
        /// 根据应用程序中窗口的层次结构，用户可以移动到线程中的其他窗口。消息框父级的所有子窗口都将被自动禁用，但弹出窗口不会被禁用。
        /// 如果未指定mb_systemmode或mb_taskmode，则mb_applmodal为默认值。
        /// </summary>
        public const LNG MB_APPLMODAL = 0x00000000L;
        /// <summary>
        /// 与mb_applmodal相同，只是消息框具有ws_ex_最顶层的样式。使用系统模式消息框通知用户需要立即关注的严重、潜在的破坏性错误（例如，内存不足）。此标志对用户与除与hwnd关联的窗口以外的其他窗口交互的能力没有影响。
        /// </summary>
        public const LNG MB_SYSTEMMODAL = 0x00001000L;
        /// <summary>
        /// 与mb_applmodal相同，但如果hwnd参数为空，则所有属于当前线程的顶级窗口都将被禁用。当调用的应用程序或库没有可用的窗口句柄，但仍需要阻止输入调用线程中的其他窗口而不挂起其他线程时，请使用此标志。
        /// </summary>
        public const LNG MB_TASKMODAL = 0x00002000L;
        #endregion

        #region SPOPTIONS
        //SP OPTIONS
        /// <summary>
        /// 桌面模式
        /// </summary>
        public const LNG MB_DEFAULT_DESKTOP_ONLY = 0x00002000L;
        /// <summary>
        /// 居右对齐
        /// </summary>
        public const LNG MB_RIGHT = 0x00080000L;
        /// <summary>
        /// 在希伯来语和阿拉伯语系统上，使用从右到左的阅读顺序显示消息和标题文本。
        /// </summary>
        public const LNG MB_RTLREADING = 0x00100000L;
        /// <summary>
        /// 消息框将成为前景窗口。在内部，系统调用消息框的setForegroundWindow函数。 
        /// </summary>
        public const LNG MB_SETFOREGROUND = 0x00010000L;
        /// <summary>
        /// 置顶
        /// </summary>
        public const LNG MB_TOPMOST = 0x00040000L;
        /// <summary>
        /// 调用方是通知用户事件的服务。函数在当前的Active Desktop上显示一个消息框，即使没有用户登录到该计算机。
        /// 终端服务：如果调用线程具有模拟令牌，则该函数将消息框定向到模拟令牌中指定的会话。
        /// 如果设置了此标志，则hwnd参数必须为空。这样，消息框就可以出现在与hwnd相对应的桌面之外的桌面上。
        /// 有关使用此标志的安全注意事项的信息，请参阅交互式服务。
        /// 尤其要注意，此标志可以在锁定的桌面上生成交互式内容，因此应仅用于非常有限的一组场景，如资源耗尽。
        /// </summary>
        public const LNG MB_SERVICE_NOTIFICATION = 0x00200000L;
        #endregion
        //RETURN


        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text)
        {
            return Show(text, "Message");
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption)
        {
            return Show(text, caption, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption, DefaultButtons buttons)
        {
            return Show(text, caption, buttons, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption, DefaultButtons buttons, DefaultICON icon)
        {
            return Show(text, caption, buttons, icon, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton)
        {
            return Show(text, caption, buttons, icon, defaultButton, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options)
        {
            return Show(text, caption, buttons, icon, defaultButton, options, false);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <param name="displayHelpButton">是否显示帮助按钮</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return (DialogResult)User32.MessageBox(User32.GetForegroundWindow(), text, caption, GetCMD((LNG)buttons, (LNG)icon, (LNG)defaultButton, (LNG)options, displayHelpButton ? MB_HELP : 0));
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text)
        {
            return ShowTopMost(text, "Message");
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption)
        {
            return ShowTopMost(text, caption, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption, DefaultButtons buttons)
        {
            return ShowTopMost(text, caption, buttons, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption, DefaultButtons buttons, DefaultICON icon)
        {
            return ShowTopMost(text, caption, buttons, icon, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton)
        {
            return ShowTopMost(text, caption, buttons, icon, defaultButton, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options)
        {
            return ShowTopMost(text, caption, buttons, icon, defaultButton, options, false);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <param name="displayHelpButton">是否显示帮助按钮</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return (DialogResult)User32.MessageBox(User32.GetForegroundWindow(), text, caption, GetCMD((LNG)buttons, (LNG)icon, (LNG)defaultButton, (LNG)options, MB_TOPMOST, displayHelpButton ? MB_HELP : 0));
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text)
        {
            return Show(handle, text, "Message");
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption)
        {
            return Show(handle, text, caption, DefaultButtons.INSIDE_ZERO);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption, DefaultButtons buttons)
        {
            return Show(handle, text, caption, buttons, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon)
        {
            return Show(handle, text, caption, buttons, icon, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton)
        {
            return Show(handle, text, caption, buttons, icon, defaultButton, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options)
        {
            return Show(handle, text, caption, buttons, icon, defaultButton, options, false);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <param name="displayHelpButton">是否显示帮助按钮</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult Show(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return (DialogResult)User32.MessageBox(handle, text, caption, GetCMD((LNG)buttons, (LNG)icon, (LNG)defaultButton, (LNG)options, displayHelpButton ? MB_HELP : 0));
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text)
        {
            return ShowTopMost(handle, text, "Message");
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption)
        {
            return ShowTopMost(handle, text, caption, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption, DefaultButtons buttons)
        {
            return ShowTopMost(handle, text, caption, buttons, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon)
        {
            return ShowTopMost(handle, text, caption, buttons, icon, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton)
        {
            return ShowTopMost(handle, text, caption, buttons, icon, defaultButton, 0);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options)
        {
            return ShowTopMost(handle, text, caption, buttons, icon, defaultButton, options, false);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="buttons">窗体提供的按钮选项,用户选择结果将返回</param>
        /// <param name="icon">信息盒子的提示图标(信息类型)</param>
        /// <param name="defaultButton">默认选择的按钮(注意有无第四项)</param>
        /// <param name="options">信息盒子选项</param>
        /// <param name="displayHelpButton">是否显示帮助按钮</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult ShowTopMost(IntPtr handle, string text, string caption, DefaultButtons buttons, DefaultICON icon, DefaultSelectedButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return (DialogResult)User32.MessageBox(handle, text, caption, GetCMD((LNG)buttons, (LNG)icon, (LNG)defaultButton, (LNG)options, MB_TOPMOST, displayHelpButton ? MB_HELP : 0));
        }

        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="MB_CMDS">窗体命令,0 为空,可以自主选择命令组拼,注意不能重复使用相同组内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static int Show(IntPtr handle, string text, string caption, LNG MB_CMDS)
        {
            return User32.MessageBox(handle, text, caption, (int)MB_CMDS);
        }
        /// <summary>
        /// 显示一个信息盒子, 阻断当前线程
        /// </summary>
        /// <param name="handle">载体窗口句柄</param>
        /// <param name="text">需要显示的Text主体内容</param>
        /// <param name="caption">标题</param>
        /// <param name="MB_CMDS">窗体命令,0 为空,可以自主选择命令组拼,注意不能重复使用相同组内容</param>
        /// <returns>窗体返回结果(用户点击的按钮)</returns>
        public static DialogResult HShow(IntPtr handle, string text, string caption, int MB_CMDS)
        {
            return (DialogResult)Show(handle, text, caption, MB_CMDS);
        }

        private static int GetCMD(params LNG[] cmds)
        {
            int cmd = 0;
            for (int i = 0; i < cmds.Length; i++)
                cmd += (int)cmds[i];
            return cmd;
        }

    }
}

