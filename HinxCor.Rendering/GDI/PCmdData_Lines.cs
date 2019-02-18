using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// 绘制线条的数据
/// </summary>
public class PCmdData_Lines : IPainterCmd
{
    public PaintType Type { get { return PaintType.Lines; } }
    public Pen Pen { get; set; }
    public Pen[] Pens { get; set; }
    public PointF[] Points { get; set; }
}

