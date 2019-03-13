using System;
using System.Collections.Generic;

namespace HinxCor.Math
{
    using Math = System.Math;
    /// <summary>
    /// 点
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// x
        /// </summary>
        public double x { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// 1
        /// </summary>
        public static Point one { get { return new Point(1, 1); } }
        /// <summary>
        /// 0
        /// </summary>
        public static Point zero { get { return new Point(); } }
        /// <summary>
        /// length²
        /// </summary>
        public double sqrMagnitude { get { return x * x + y * y; } }
        /// <summary>
        /// length
        /// </summary>
        public double magnitude { get { return Math.Sqrt(sqrMagnitude); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// -
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator -(Point left, Point right)
        {
            left.x -= right.x;
            left.y -= right.y;
            return left;
        }
        /// <summary>
        /// +
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator +(Point left, Point right)
        {
            left.x += right.x;
            left.y += right.y;
            return left;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator /(Point left, Point right)
        {
            left.x /= right.x;
            left.y /= right.y;
            return left;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator /(Point left, double right)
        {
            left.x /= right;
            left.y /= right;
            return left;
        }
        /// <summary>
        /// *
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator *(Point left, Point right)
        {
            left.x *= right.x;
            left.y *= right.y;
            return left;
        }
        /// <summary>
        /// *
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator *(Point left, float right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        /// <summary>
        /// *
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Point operator *(Point left, double right)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        /// <summary>
        /// *
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Point operator *(float right, Point left)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        /// <summary>
        /// *
        /// </summary>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Point operator *(double right, Point left)
        {
            left.x *= right;
            left.y *= right;
            return left;
        }
        /// <summary>
        /// equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// not equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// get hash code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 0x00f2d1a;
            hash *= (int)(x * 0xf15ad61f) >> 5;
            hash *= (int)(y * 0xf15ad61f) << 2;
            return hash;
        }
        /// <summary>
        /// equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Point(System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }
        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="p"></param>
        public static implicit operator Point(System.Drawing.PointF p)
        {
            return new Point(p.X, p.Y);
        }
    }

}
