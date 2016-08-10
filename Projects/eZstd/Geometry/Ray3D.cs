using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZstd.MatrixPack;

namespace eZstd.Geometry
{

    /// <summary>
    /// 三维直角坐标系中，有限长度或无限长度的射线指向器，它有两个基本属性：原点位置，以及指定的方向矢量
    /// </summary>
    public class Ray3D
    {
        #region ---   Properties

        /// <summary>
        /// 射线的起始原点
        /// </summary>
        public readonly XYZ Origin;

        /// <summary>
        /// 射线的方向矢量。矢量的模代表射线的长度
        /// </summary>
        public readonly XYZ Direction;

        /// <summary>
        /// 有限长度的射线的终点坐标
        /// </summary>
        public readonly XYZ EndPoint;

        /// <summary>
        /// 如果为true，则此射线为无限长的，如果为false，则此射线是有限长度的。 
        /// 对于有限长度的射线，其长度由属性 Direction 矢量来确定。
        /// </summary>
        public readonly bool InfiniteLength;

        /// <summary> 此射线所对应的无限长的直线 </summary>
        public readonly Line3D Line;

        #endregion
        #region ---   构造函数

        /// <summary>
        /// 创建一条位于空间直角坐标系的坐标轴上的单位长度的射线
        /// </summary>
        /// <param name="axis">射线所在的坐标轴。其值可取1、2、3，分别代表x、y、z轴</param>
        /// <param name="infiniteLength">如果为true，则此射线为无限长的，如果为false，则此射线是有限长度的，而且长度为1。</param>
        public Ray3D(byte axis, bool infiniteLength)
        {
            Origin = new XYZ();
            switch (axis)
            {
                case 1: EndPoint = new XYZ(1, 0, 0); break;
                case 2: EndPoint = new XYZ(0, 1, 0); break;
                case 3: EndPoint = new XYZ(0, 0, 1); break;
            }
            Direction = EndPoint - Origin;
            InfiniteLength = infiniteLength;

            // 此射线所对应的无限长的直线
            Line = new Line3D(Origin, Direction);
        }

        /// <summary>
        /// 一条从起点指向终点的有限长射线
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        public Ray3D(XYZ startPoint, XYZ endPoint)
        {
            Origin = startPoint;
            Direction = endPoint - startPoint;
            EndPoint = endPoint;
            InfiniteLength = false;

            // 此射线所对应的无限长的直线
            Line = new Line3D(Origin, Direction);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="infiniteLength">如果为true，则此射线为无限长的，如果为false，则此射线是有限长度的。
        /// 对于有限长度的射线，其长度由 direction 矢量来确定。</param>
        public Ray3D(XYZ origin, XYZ direction, bool infiniteLength)
        {
            Origin = origin;
            Direction = direction;
            EndPoint = origin + direction;
            InfiniteLength = infiniteLength;

            // 此射线所对应的无限长的直线
            Line = new Line3D(origin, direction);
        }
        #endregion

        /// <summary> 两条射线是否在同一个三维平面上 </summary>
        /// <param name="ray2"></param>
        /// <returns></returns>
        public bool IsCoplanarWith(Ray3D ray2)
        {
            return Line.IsCoplanarWith(ray2.Line);
        }

        #region ---   两条空间射线的交点

        /// <summary>
        /// 两个指向器在平面上是否能够相交，如果能，则返回其交点坐标；如果两射线平行，则不能相交，此时返回 null
        /// </summary>
        /// <param name="ray2">用来判断相交的另一条射线</param>
        /// <returns></returns>
        public XYZ IntersectWith(Ray3D ray2)
        {
            // 先判断两条射线所对应的无限长的直线在三维平面中的交点
            if (Direction.IsCollinearWith(ray2.Direction))
            {
                // 平行向量不可能相交
                return null;
            }
            // 两直线的交点
            XYZ intersectPoint;
            intersectPoint = Line.GetIntersectPointWith(ray2.Line);
            if (intersectPoint == null)
            {
                return null;
            }
            // 再判断这两条射线的起止范围是否包含了此交点
            return (Contains(intersectPoint) && ray2.Contains(intersectPoint)) ? intersectPoint : null;
        }

        /// <summary>
        /// 无限长或者有限长的射线所代表的三维矩形方框是否能够框住指定的点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(XYZ point)
        {
            if (InfiniteLength)
            {
                return InfiniteRayContains(point);
            }
            else
            {
                Ray3D reversedRay = new Ray3D(EndPoint, Direction.Reverse(), InfiniteLength);
                return reversedRay.InfiniteRayContains(point);
            }
        }

        /// <summary>
        /// 无限长射线所代表的三维矩形方框是否能够框住指定的点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool InfiniteRayContains(XYZ point)
        {

            if (!((point.X >= Origin.X && Direction.X >= 0) || (point.X <= Origin.X && Direction.X <= 0)))
                return false;
            if (!((point.Y >= Origin.Y && Direction.Y >= 0) || (point.Y <= Origin.Y && Direction.Y <= 0)))
                return false;
            if (!((point.Z >= Origin.Z && Direction.Z >= 0) || (point.Z <= Origin.Z && Direction.Z <= 0)))
                return false;

            return true;
        }

        #endregion
    }
}