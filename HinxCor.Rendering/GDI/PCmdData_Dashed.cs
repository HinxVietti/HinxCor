using System.Drawing;



/// <summary>
/// 虚线
/// </summary>
public class PCmdData_Dashed : IPainterCmd
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="pen"></param>
    public PCmdData_Dashed(PointF start, PointF end, Pen pen)
    {
        Start = start;
        End = end;
        Pen = pen;
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
    /// 画笔
    /// </summary>
    public Pen Pen { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public PaintType Type { get { return PaintType.Dashed; } }

    /// <summary>
    /// 长度
    /// </summary>
    public float Length { get { return (float)new HinxCor.Math.Point(Start.X - End.X, Start.Y - End.Y).magnitude; } }
}




