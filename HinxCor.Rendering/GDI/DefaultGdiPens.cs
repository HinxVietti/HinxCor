using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// 画笔集合
/// </summary>
public class DefaultGdiPens
{
    /// <summary>
    /// 多彩色样本
    /// </summary>
    public Pen ColorfulSimple
    {
        get
        {
            return new Pen(
                new LinearGradientBrush(new PointF(0, 0), new PointF(300, 300), Color.Red, Color.Cyan));

        }
    }
    //public Pen Red { get; set; }
}

/// <summary>
/// 默认画笔颜色
/// </summary>
public class DefaultGdiColors
{
    /// <summary>
    /// 红
    /// </summary>
    public static Color Red { get { return Color.FromArgb(255, 243, 83, 83); } }
    /// <summary>
    /// 橙
    /// </summary>
    public static Color Orange { get { return Color.FromArgb(255, 255, 155, 60); } }
    /// <summary>
    /// 黄
    /// </summary>
    public static Color Yellow { get { return Color.FromArgb(255, 255, 211, 53); } }
    /// <summary>
    /// 绿
    /// </summary>
    public static Color Green { get { return Color.FromArgb(255, 38, 194, 129); } }
    /// <summary>
    /// 蓝
    /// </summary>
    public static Color Blue { get { return Color.FromArgb(255, 43, 144, 239); } }
    /// <summary>
    /// 殿
    /// </summary>
    public static Color Indigo { get { return Color.FromArgb(255, 106, 70, 250); } }
    /// <summary>
    /// 紫
    /// </summary>
    public static Color Purple { get { return Color.FromArgb(255, 176, 93, 217); } }
}

