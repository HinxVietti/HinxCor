using System;
using System.Collections.Generic;

namespace HinxCor.Math
{
    using Math = System.Math;

    public struct Point3D
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        /// <summary>
        /// 平方根
        /// </summary>
        public double magnitude { get { return Math.Sqrt(sqrMagnitude); } }
        /// <summary>
        /// 平方
        /// </summary>
        public double sqrMagnitude { get { return x * x + y * y + z * z; } }
        public static Point3D one { get { return new Point3D(); } }
        public static Point3D zero { get { return new Point3D(); } }


        public Point3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Point3D operator +(Point3D left, Point3D right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;
            return left;
        }
        public static Point3D operator -(Point3D left, Point3D right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;
            return left;
        }

        public static Point3D operator *(Point3D left, Point3D right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
            return left;
        }
        public static Point3D operator *(Point3D left, double right)
        {
            left.x *= right;
            left.y *= right;
            left.z *= right;
            return left;
        }
        public static Point3D operator *(double right, Point3D left)
        {
            return left * right;
            //left.x *= right;
            //left.y *= right;
            //left.z *= right;
            //return left;
        }

        public static Point3D operator /(Point3D left, double right)
        {
            left.x /= right;
            left.y /= right;
            left.z /= right;
            return left;
        }

        public static Point3D operator /(Point3D left, Point3D right)
        {
            left.x /= right.x;
            left.y /= right.y;
            left.z /= right.z;
            return left;
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            return !left.Equals(right);
        }
        public static bool operator ==(Point3D left, Point3D right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            int hash = 0xfd2a1F;
            hash *= (int)((hash >> 3) * x);
            hash *= (int)((hash >> 3) * y);
            hash *= (int)((hash >> 3) * z);
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point3D))
            {
                return false;
            }

            var d = (Point3D)obj;
            return x == d.x &&
                   y == d.y &&
                   z == d.z;
        }

        public static implicit operator Point3D(Point p)
        {
            return new Point3D(p.x, p.y, 0);
        }

        public static implicit operator Point(Point3D p)
        {
            return new Point(p.x, p.y);
        }
    }
}
