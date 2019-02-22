using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// 绘制线条的数据
/// </summary>
public class PCmdData_Lines : IPainterCmd
{
    /// <summary>
    /// 画笔透明度
    /// </summary>
    public int ColorAlpha { get; set; }
    private List<PointF> _plist = new List<PointF>();
    /// <summary>
    /// 返回Lines
    /// </summary>
    public PaintType Type { get { return PaintType.Lines; } }
    /// <summary>
    /// 画笔
    /// </summary>
    public Pen Pen { get; set; }
    /// <summary>
    /// 多段画笔
    /// </summary>
    public Pen[] Pens { get; set; }
    /// <summary>
    /// 线段
    /// </summary>
    public PointF[] Points { get { return _plist.ToArray(); } }
    /// <summary>
    /// 是否满足绘制条件
    /// </summary>
    public bool CouldHandle { get { return Points != null && Points.Length > 1; } }
    /// <summary>
    /// construct
    /// </summary>
    public PCmdData_Lines(Color pencolor, float pensize)
    {
        this.Pen = new Pen(pencolor, pensize);
    }

    /// <summary>
    /// 添加线段上的点
    /// </summary>
    /// <param name="p"></param>
    public void add(PointF p)
    {
        _plist.Add(p);
        //Points = _plist.ToArray();
    }
}

