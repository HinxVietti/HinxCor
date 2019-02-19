using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// 
/// </summary>
public interface IPainterCmd
{
    /// <summary>
    /// 是否满足绘制条件
    /// </summary>
    bool CouldHandle { get; }
    /// <summary>
    /// 绘制对象的类型
    /// </summary>
    PaintType Type { get; }
    /// <summary>
    /// 绘制的画笔
    /// </summary>
    Pen Pen { get; set; }
    /// <summary>
    /// 画笔的透明度
    /// </summary>
    int ColorAlpha { get; set; }
}

