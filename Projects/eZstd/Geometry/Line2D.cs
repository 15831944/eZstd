using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Geometry
{
    /// <summary>
    /// 二维直角坐标系中，无限长度的直线，它有两个基本属性：原点位置及方向矢量 
    /// </summary>
    public class Line2D
    {

        #region ---   Properties

        /// <summary> 二维直线所经过的某一个点 </summary>
        public readonly XY Origin;

        /// <summary> 二维直线的方向矢量，其矢量长度并无意义 </summary>
        public readonly XY Direction;

        /// <summary> 直线方程 y = kx + b 中的斜率参数k </summary>
        public readonly double k;
        /// <summary> 直线方程 y = kx + b 中的截距参数b </summary>
        public readonly double b;

        /// <summary>
        /// 有限长度的射线的终点坐标
        /// </summary>
        public readonly XY EndPoint;

        #endregion
        #region ---   构造函数

        /// <summary>
        /// 创建一条位于空间直角坐标系的坐标轴上的单位长度直线
        /// </summary>
        /// <param name="axis">直线所在的坐标轴。其值可取1、2，分别代表x、y轴</param>
        public Line2D(byte axis)
        {
            Origin = new XY();
            switch (axis)
            {
                case 1: Direction = new XY(1, 0); break;
                case 2: Direction = new XY(0, 1); break;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="origin">二维直线所经过的某一个点</param>
        /// <param name="direction">二维直线的方向矢量，其矢量长度并无意义</param>
        public Line2D(XY origin, XY direction)
        {
            Origin = origin;
            Direction = direction;
            EndPoint = origin + direction;

            // 直线方程 y = kx + b
            k = direction.Y / direction.X;
            b = origin.Y - k * origin.X;
        }

        #endregion

        #region ---   直线交点

        /// <summary>
        /// 二维平面内直线的交点，如果没有交点，则返回 null
        /// </summary>
        /// <returns>如果两直线平行或者近似平行，以至找不到交点，则返回 null </returns>
        public XY GetIntersectPointWith(Line2D line2)
        {
            // 先判断两条直线是否平行
            if (Direction.IsCollinearWith(line2.Direction))
            {
                return null;
            }
            else
            {
                return GetIntersectPoint(k, b, line2.k, line2.b);
            }

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

        #endregion
    }
}
