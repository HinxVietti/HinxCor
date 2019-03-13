using System;
using System.Collections.Generic;

namespace HinxCor.Math
{
    using static System.Math;
    /// <summary>
    /// 直线
    /// </summary>
    public class Line
    {
        /// <summary>
        /// 斜率
        /// </summary>
        public double K { get; set; }

        /// <summary>
        /// 截距
        /// </summary>
        public double b { get; set; }

        /// <summary>
        /// 是否垂直于X轴
        /// </summary>
        public bool ParallelY { get { return K == double.PositiveInfinity || K == double.NegativeInfinity; } }

        /// <summary>
        /// 是否垂直于Y轴
        /// </summary>
        public bool ParallelX { get { return K == 0; } }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="k"></param>
        public Line(double k)
        {
            this.K = k;
            b = 0;
        }

        /// <summary>
        /// 过0 P构造
        /// </summary>
        /// <param name="p"></param>
        public Line(Point p)
        {
            this.K = p.y / p.x;
        }
        /// <summary>
        ///  kb构造
        /// </summary>
        /// <param name="k"></param>
        /// <param name="b"></param>
        public Line(double k, double b)
        {
            this.K = k;
            this.b = b;
        }
        /// <summary>
        /// 过两点构造
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Line(Point a, Point b)
        {
            this.K = (a.y - b.y) / (a.x - b.x);
            if (!ParallelX && !ParallelY)
            {
                this.b = a.y - a.x * K;
            }
            else if (ParallelX)
            {
                this.b = a.y;
            }
            else
            {
                //线段,x = const;
                this.b = -a.x;
            }

        }

        /// <summary>
        /// 获取相交的点
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Point Cross(Line line)
        {
            Point p = new Point();
            if (K == line.K) return p;
            p.x = (line.b - b) / (K - line.K);
            p.y = p.x * K + b;
            return p;
        }

        /// <summary>
        /// 线到点p的距离
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public double DistenceTo(Point p)
        {
            if (ParallelX)
            {
                return Abs(p.y - b);
            }
            else if (ParallelY)
            {
                return Abs(p.x - b);
            }
            return Abs((K * p.x - p.y + b) / (Sqrt(K * K + 1)));
        }

    }

}
