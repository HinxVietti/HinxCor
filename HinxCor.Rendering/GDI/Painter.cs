using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// 
/// </summary>
public static class Painter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="cmd"></param>
    public static void Draw(this Graphics graphics, IPainterCmd cmd)
    {

        switch (cmd.Type)
        {
            case PaintType.Rectangle:
                //var rectData = cmd as 
                break;
            case PaintType.Ellipse:
                break;
            case PaintType.Arrow:
                break;
            case PaintType.StraightLine:
                break;
            case PaintType.Dashed:
                var dashedData = cmd as PCmdData_Dashed;
                //var length = dashedData.Length;
                //int caps = (int)(length / 20) + 1;
                //var delta = getDelta(dashedData.Start, dashedData.End, length);
                //var deltaSizex15 = new SizeF(delta.X * 15, delta.Y * 15);
                //var deltaSizex20 = new SizeF(delta.X * 20, delta.Y * 20);
                //for (int i = 0; i < caps; i++)
                //    graphics.DrawLine(dashedData.Pen, dashedData.Start, dashedData.Start + deltaSizex15, deltaSizex20.Multiple(i));

                var pen = dashedData.Pen;
                pen.DashOffset = 15;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                graphics.DrawLine(pen, dashedData.Start, dashedData.End);
                break;
            case PaintType.Lines:
                var linesData = cmd as PCmdData_Lines;
                graphics.DrawLines(cmd.Pen, linesData.Points);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 偏移绘制线段
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="pen"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="offset"></param>
    public static void DrawLine(this Graphics graphics, Pen pen, PointF start, PointF end, SizeF offset)
    {
        graphics.DrawLine(pen, start + offset, end + offset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="size"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SizeF Multiple(this SizeF size, float value)
    {
        size.Width *= value;
        size.Height *= value;
        return size;
    }

    private static PointF getDelta(PointF start, PointF end, float length)
    {
        return new PointF((end.X - start.X) / length, (end.Y - start.Y) / length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static PointF Multiple(this PointF p, float value)
    {
        return new PointF(p.X * value, p.Y * value);
    }
}

