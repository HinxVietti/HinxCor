using System;
using System.Collections.Generic;
using System.Drawing;
using HinxCor.Math;

public class PCmdData_Arrow : IPainterCmd
{
    public PaintType Type { get { return PaintType.Arrow; } }

    public Pen Pen { get; set; }

    public PointF Start { get; set; }
    public PointF End { get; set; }

    public SizeF RA { get; set; }
    public SizeF RB { get; set; }
    public SizeF LA { get; set; }
    public SizeF LB { get; set; }




    const double dangle = 1.5707963267949;
    public const double angle1 = 1.06369782240256;
    public const double angle2 = 1.20362249297668;
    public const double angle3 = -1.06369782240256;
    public const double angle4 = -1.20362249297668;

    public const double dA = 20.591260281974;
    public const double dB = 13.9283882771841;

    public PCmdData_Arrow(PointF start, PointF end)
    {
        Start = start;
        End = end;
        double theta = Math.PI / 2 + Math.Atan((end.Y - start.X) / (end.X - start.X));
        //double theta = 0;
        RA = new SizeF((float)(Math.Cos(theta + angle2) * dB), (float)(Math.Sin(theta + angle2) * dB));
        RB = new SizeF((float)(Math.Cos(theta + angle1) * dA), (float)(Math.Sin(theta + angle1) * dA));
        LA = new SizeF((float)(Math.Cos(theta + angle4) * dB), (float)(Math.Sin(theta + angle4) * dB));
        LB = new SizeF((float)(Math.Cos(theta + angle3) * dA), (float)(Math.Sin(theta + angle3) * dA));

        //var angle_1 = theta + angle1;
        //var angle_2 = theta + angle2;
        //RA = new SizeF((float)(Math.Sin(angle2) * dB), (float)(Math.Cos(angle2) * dB));
        //RB = new SizeF((float)(Math.Sin(angle1) * dA), (float)(Math.Cos(angle1) * dA));
        //angle_1 = theta + angle3;
        //angle_2 = theta + angle4;
        //LA = new SizeF((float)(Math.Sin(angle2) * dB), (float)(Math.Cos(angle2) * dB));
        //LB = new SizeF((float)(Math.Sin(angle1) * dA), (float)(Math.Cos(angle1) * dA));

    }

    public PointF[] GetDrawData()
    {
        //return new[] { new PointF(1000, 800), new PointF(1005, 313), new PointF(1010, 318), new PointF(1000, 300), new PointF(990, 318), new PointF(995, 313) };
        return new[] { Start, End + RA, End + RB, End, End - LB, End - LA };
    }

}

