using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZstd.Geometry;

namespace eZstd.Geometry
{
    /// <summary>
    /// 二维直角坐标系中，有限长度或无限长度的射线指向器，它有两个基本属性：原点位置，以及指定的方向矢量
    /// </summary>
    public class Ray2D
    {
        #region ---   Properties

        /// <summary>
        /// 射线的起始原点
        /// </summary>
        public readonly XY Origin;

        /// <summary>
        /// 射线的方向矢量。矢量的模代表射线的长度
        /// </summary>
        public readonly XY Direction;

        /// <summary> 直线方程 y = kx + b 中的斜率参数k </summary>
        public readonly double k;
        /// <summary> 直线方程 y = kx + b 中的截距参数b </summary>
        public readonly double b;

        /// <summary>
        /// 有限长度的射线的终点坐标
        /// </summary>
        public readonly XY EndPoint;

        /// <summary>
        /// 如果为true，则此射线为无限长的，如果为false，则此射线是有限长度的。 
        /// 对于有限长度的射线，其长度由属性 Direction 矢量来确定。
        /// </summary>
        public readonly bool InfiniteLength;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="infiniteLength">如果为true，则此射线为无限长的，如果为false，则此射线是有限长度的。
        /// 对于有限长度的射线，其长度由 direction 矢量来确定。</param>
        public Ray2D(XY origin, XY direction, bool infiniteLength)
        {
            Origin = origin;
            Direction = direction;
            EndPoint = origin + direction;
            InfiniteLength = infiniteLength;

            // 直线方程 y = kx + b
            k = direction.Y / direction.X;
            b = origin.Y - k * origin.X;
        }

        /// <summary>
        /// 两个指向器在平面上是否能够相交，如果能，则返回其交点坐标
        /// </summary>
        /// <param name="pointer2">用来判断相交的另一条射线</param>
        /// <param name="intersectPoint"> 两条射线所对应的直线的直线的交点，如果两射线平行，则不能相交，此时返回 null </param>
        /// <returns></returns>
        public bool CanIntersectWith(Ray2D pointer2, out XY intersectPoint)
        {
            // 先判断两条射线所对应的无限长的直线在二维平面中的交点
            if (Direction.IsAlmostEqualTo(pointer2.Direction, isPoint: false))
            {
                // 平行向量不可能相交
                intersectPoint = null;
                return false;
            }
            // 两直线的交点
            intersectPoint = GetIntersectPoint(k, b, pointer2.k, pointer2.b);

            // 再判断这两条射线的起止范围是否包含了此交点

            return (Contains(intersectPoint) && pointer2.Contains(intersectPoint));
        }

        /// <summary>
        /// 二维平面内直线 y = k1x + b1 与 y = k2x + b2 的交点
        /// </summary>
        /// <returns></returns>
        private static XY GetIntersectPoint(double k1, double b1, double k2, double b2)
        {
            return new XY(x: (b2 - b1) / (k1 - k2),
                y: (b1 * k2 - b2 * k1) / (k2 - k1));
        }

        /// <summary>
        /// 无限长或者有限长的射线所代表的二维矩形方框是否能够框住指定的点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(XY point)
        {
            if (InfiniteLength)
            {
                return InfiniteRayContains(point);
            }
            else
            {
                Ray2D reversedRay = new Ray2D(EndPoint, Direction.Reverse(), InfiniteLength);
                return reversedRay.InfiniteRayContains(point);
            }
        }

        /// <summary>
        /// 无限长射线所代表的二维矩形方框是否能够框住指定的点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool InfiniteRayContains(XY point)
        {

            if (!((point.X >= Origin.X && Direction.X >= 0) || (point.X <= Origin.X && Direction.X <= 0)))
                return false;
            if (!((point.Y >= Origin.Y && Direction.Y >= 0) || (point.Y <= Origin.Y && Direction.Y <= 0)))
                return false;

            return true;
        }
    }
}
