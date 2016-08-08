using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion

        /// <summary>
        /// 
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
        }
        
        /// <summary> 两条射线是否在同一个三维平面上 </summary>
        /// <param name="ray2"></param>
        /// <returns></returns>
        public bool IsCoplanarWith(Ray3D ray2)
        {
            XYZ v_r1r2 = ray2.Origin - Origin;
            XYZ v_d1 = v_r1r2.CrossProduct(Direction);
            XYZ v_d2 = v_r1r2.CrossProduct(ray2.Direction);

            // 如果 v_d1 与 v_d2 的方向共线，则此两条射线共面
            return v_d1.IsCollinearWith(v_d2);
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
    }
}