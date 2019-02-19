using System.Drawing;

/// <summary>
/// 绘制arrow的命令
/// </summary>
public class PCmdData_Arrow : IPainterCmd
{
    /// <summary>
    /// get set pen alpha
    /// </summary>
    public int ColorAlpha { get; set; }
    /// <summary>
    /// 返回arrow
    /// </summary>
    public PaintType Type { get { return PaintType.Arrow; } }

    /// <summary>
    /// 画笔
    /// </summary>
    public Pen Pen { get; set; }

    /// <summary>
    /// 起始点
    /// </summary>
    public PointF Start { get; set; }
    /// <summary>
    /// 终止点
    /// </summary>
    public PointF End { get; set; }

    private SizeF RA { get; set; }
    private SizeF RB { get; set; }
    private SizeF LA { get; set; }
    private SizeF LB { get; set; }

    /// <summary>
    /// 是否满足绘制条件
    /// </summary>
    public bool CouldHandle { get { return Start != null && End != null; } }

    const double dangle = 1.5707963267949;
    private const double angle1 = 1.06369782240256;
    private const double angle2 = 1.20362249297668;
    private const double angle3 = -1.06369782240256;
    private const double angle4 = -1.20362249297668;
    private const double dA = 20.591260281974;
    private const double dB = 13.9283882771841;

    /// <summary>
    /// construct
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public PCmdData_Arrow(PointF start, PointF end)
    {
        Start = start;
        End = end;


        //double theta = Math.PI / 2 + Math.Atan((end.Y - start.X) / (end.X - start.X));
        ////double theta = 0;
        //RA = new SizeF((float)(Math.Cos(theta + angle2) * dB), (float)(Math.Sin(theta + angle2) * dB));
        //RB = new SizeF((float)(Math.Cos(theta + angle1) * dA), (float)(Math.Sin(theta + angle1) * dA));
        //LA = new SizeF((float)(Math.Cos(theta + angle4) * dB), (float)(Math.Sin(theta + angle4) * dB));
        //LB = new SizeF((float)(Math.Cos(theta + angle3) * dA), (float)(Math.Sin(theta + angle3) * dA));

        //var angle_1 = theta + angle1;
        //var angle_2 = theta + angle2;
        //RA = new SizeF((float)(Math.Sin(angle2) * dB), (float)(Math.Cos(angle2) * dB));
        //RB = new SizeF((float)(Math.Sin(angle1) * dA), (float)(Math.Cos(angle1) * dA));
        //angle_1 = theta + angle3;
        //angle_2 = theta + angle4;
        //LA = new SizeF((float)(Math.Sin(angle2) * dB), (float)(Math.Cos(angle2) * dB));
        //LB = new SizeF((float)(Math.Sin(angle1) * dA), (float)(Math.Cos(angle1) * dA));

    }

    /// <summary>
    /// 获取绘制箭头的数组 [弃用]
    /// </summary>
    /// <returns></returns>
    public PointF[] GetDrawData()
    {
        //return new[] { new PointF(1000, 800), new PointF(1005, 313), new PointF(1010, 318), new PointF(1000, 300), new PointF(990, 318), new PointF(995, 313) };
        return new[] { Start, End + RA, End + RB, End, End - LB, End - LA };
    }

}

