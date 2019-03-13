using SWF = System.Windows.Forms;
using DialogResult = System.Windows.Forms.DialogResult;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using MessageBoxDefaultButton = System.Windows.Forms.MessageBoxDefaultButton;
using MessageBoxOptions = System.Windows.Forms.MessageBoxOptions;
using HelpNavigator = System.Windows.Forms.HelpNavigator;

namespace HinxCor.WindowsForm
{
    /// <summary>
    /// 特殊环境下无法使用Windows自带的MessageBox,特此替代
    /// </summary>
    public class MessageBox
    {
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text)
        {
            return SWF.MessageBox.Show(text);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption)
        {
            return SWF.MessageBox.Show(text, caption);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            return SWF.MessageBox.Show(text, caption, buttons);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="displayHelpButton">是否显示帮助按钮</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options, displayHelpButton);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="keyword">关键字</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="navigator">导航方式</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator);
        }
        /// <summary>
        /// 显示信息窗口(对话框)
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="navigator">导航方式</param>
        /// <param name="param">用户单击帮助按钮时要显示的帮助主题的数字ID</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
        {
            return SWF.MessageBox.Show(text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton, options);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton, options, helpFilePath);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="keyword">关键字</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="navigator">导航方式</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator);
            topf.Hide();
            return result;
        }
        /// <summary>
        /// 显示信息窗口(对话框) 在对顶层
        /// </summary>
        /// <param name="text">信息文本(主要的信息内容)</param>
        /// <param name="caption">提示框标题</param>
        /// <param name="buttons">可选按钮</param>
        /// <param name="icon">窗口图标</param>
        /// <param name="defaultButton">默认选择的按钮(从左到右第几个)</param>
        /// <param name="options">选项</param>
        /// <param name="helpFilePath">帮助文件路劲</param>
        /// <param name="navigator">导航方式</param>
        /// <param name="param">用户单击帮助按钮时要显示的帮助主题的数字ID</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowOnTopMost(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
        {
            var topf = GetShowedTopMostForm();
            var result = SWF.MessageBox.Show(topf, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);
            topf.Hide();
            return result;
        }

        private static SWF.Form singleTopf;
        /// <summary>
        /// 获取一个空的win在虚拟空间显示
        /// </summary>
        /// <returns>Form</returns>
        private static SWF.Form GetShowedTopMostForm()
        {
            if (singleTopf == null)
                //singleTopf = new SWF.Form();
                singleTopf = new EmptyForm();

            singleTopf.Size = new System.Drawing.Size(1, 1);
            singleTopf.StartPosition = SWF.FormStartPosition.Manual;
            System.Drawing.Rectangle rect = SWF.SystemInformation.VirtualScreen;
            singleTopf.Location = new System.Drawing.Point(rect.Bottom + 10, rect.Height + 10);
            singleTopf.Show();
            singleTopf.Focus();
            singleTopf.BringToFront();
            singleTopf.TopMost = true;
            return singleTopf;
        }

    }
}

