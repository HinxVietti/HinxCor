using System;
using System.Collections.Generic;

namespace HinxCor.Math
{
    using Math = System.Math;

    public struct Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public static Point one { get { return new Point(1, 1); } }
        public static Point zero { get { return new Point(); } }
        public double sqrMagnitude { get { return x * x + y * y; } }
        public double magnitude { get { return Math.Sqrt(sqrMagnitude); } }


        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator -(Point left, Point right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }

        public static Point operator +(Point left, Point right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }

        public static Point operator /(Point left, Point right)
        {
            left.x /= right.x;
            left.y /= right.y;
            return left;
        }
        public static Point operator /(Point left, double right)
        {
            left.x /= right;
            left.y /= right;
            return left;
        }

        public static Point operator *(Point left, Point right)
        {
            left.x *= right.x;
            left.y *= right.y;
            return left;
        }

        public static Point operator *(Point left, float right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }

        public static Point operator *(Point left, double right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }

        public static Point operator *(float right, Point left)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }

        public static Point operator *(double right, Point left)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = 0x00f2d1a;
            hash *= (int)(x * 0xf15ad61f) >> 5;
            hash *= (int)(y * 0xf15ad61f) << 2;
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }

            var point = (Point)obj;
            return x == point.x &&
                   y == point.y;
        }


        public static implicit operator Point(System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }
        public static implicit operator Point(System.Drawing.PointF p)
        {
            return new Point(p.X, p.Y);
        }
    }

}
