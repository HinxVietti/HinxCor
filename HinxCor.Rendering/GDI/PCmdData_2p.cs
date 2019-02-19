using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// 所有两个点的绘制对象的类型
/// </summary>
public class PCmdData_2p : IPainterCmd
{
    /// <summary>
    /// construct
    /// </summary>
    /// <param name="type"></param>
    /// <param name="startp"></param>
    public PCmdData_2p(PaintType type, PointF startp = default(PointF))
    {
        this.Start = startp;
        ColorAlpha = 255;
        Type = type;
        Pen = new Pen(DefaultGdiColors.Orange, 5);
        switch (Type)
        {
            case PaintType.Rectangle:
                Pen.DashStyle = DashStyle.Solid;
                break;
            case PaintType.Ellipse:
                Pen.DashStyle = DashStyle.Solid;
                break;
            case PaintType.Arrow:
                Pen = getArrowPen(Pen);
                break;
            case PaintType.StraightLine:
                Pen.DashStyle = DashStyle.Solid;
                break;
            case PaintType.Dashed:
                Pen = getDashPen(Pen);
                break;
        }

    }

    private Pen getDashPen(Pen pend)
    {
        var pen = new Pen(Color.FromArgb(ColorAlpha, pend.Color), pend.Width);
        pen.DashStyle = DashStyle.DashDot;
        return pen;
    }


    private Pen getArrowPen(Pen pend)
    {
        var pen = new Pen(Color.FromArgb(ColorAlpha, pend.Color), 10);
        pen.DashStyle = DashStyle.Solid;
        pen.DashCap = DashCap.Round;
        AdjustableArrowCap bigArrow = new AdjustableArrowCap(2, 2);
        pen.CustomEndCap = bigArrow;
        return pen;
    }

    /// <summary>
    /// 起点
    /// </summary>
    public PointF Start { get; set; }

    /// <summary>
    /// 终点
    /// </summary>
    public PointF End { get; set; }
    /// <summary>
    /// 是否可以绘制
    /// </summary>
    public bool CouldHandle { get { return true; } }
    /// <summary>
    /// 绘制对象的类型
    /// </summary>
    public PaintType Type { get; set; }
    /// <summary>
    /// 绘制的画笔
    /// </summary>
    public Pen Pen { get; set; }
    /// <summary>
    /// 绘制的颜色
    /// </summary>
    public int ColorAlpha { get; set; }
}

