using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZstd.Geometry;

namespace eZstd.Geometry
{
    /// <summary>
    /// 二维平面坐标系中的空间点或空间方向矢量
    /// </summary>
    public class XY
    {
        #region ---   Properties

        /// <summary>
        /// 角度容差，单位为弧度。当两个向量之间的夹角小于此常数时，认为此两个向量方向相同。
        /// 此常数的值可以借鉴 Revit 2016（X64）中的容差值，即 0.00174532925199433 (rad)。
        /// </summary>
        public static double AngleTolerance = 0.00174532925199433;

        /// <summary>
        /// 距离容差，具体单位视整个模型的全局单位而定。在此距离之内的两个点会被认为是重合的。
        /// 此常数的值可以借鉴 Revit 2016（X64）中的容差值，即  0.0005233832795 inch = 0.01329394 mm。
        /// </summary>
        public static double VertexTolerance = 0.0005233832795;

        public readonly double X;
        public readonly double Y;

        #endregion

        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }

        #region ---   空间点 的方法

        /// <summary> 计算空间两个点的距离 </summary>
        /// <returns></returns>
        public static double Distance(XY point1, XY point2)
        {
            return (point1 - point2).GetLength();
        }

        /// <summary> 计算空间两个点的距离 </summary>
        /// <returns></returns>
        public double DistanceTo(XY node2)
        {
            return Distance(this, node2);
        }

        public double GetLength()
        {

            return Math.Abs(Math.Sqrt(X) + Math.Sqrt(Y));
        }
        #endregion

        #region ---   空间矢量 的方法


        /// <summary> 将空间矢量反向，保持其长度不变 </summary>
        public XY Reverse()
        {
            return new XY(-X, -Y);
        }


        /// <summary>
        /// 将空间矢量进行归一化，使其长度变为 1 。
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public XY Normalize()
        {
            return SetLength(newLength: 1.0);
        }

        /// <summary>
        /// 将一个空间矢量缩放到指定的长度（方向不变）
        /// </summary>
        /// <param name="newLength">缩放后的长度</param>
        /// <returns> 缩放后的新矢量 </returns>
        public XY SetLength(double newLength)
        {
            return this * (newLength / GetLength());
        }

        public double AngleTo(XY vector2)
        {
            return Math.Acos(this.DotProduct(vector2) / GetLength() / vector2.GetLength());
        }

        public double DotProduct(XY vector2)
        {
            return X * vector2.X + Y * vector2.Y;
        }


        /// <summary> 两个方向矢量是否共线（方向相同或者相反）。 </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsCollinearWith(XY v2)
        {
            return this.IsAlmostEqualTo(v2, false) || this.IsAlmostEqualTo(v2.Reverse(), false);
        }

        /// <summary> 两个方向矢量是否共线（方向相同或者相反）。 </summary>
        /// <param name="v2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public bool IsCollinearWith(XY v2, double tolerance)
        {
            return this.IsAlmostEqualTo(v2, false, tolerance) || this.IsAlmostEqualTo(v2.Reverse(), false, tolerance);
        }

        #endregion

        #region ---   空间矢量 或者 空间点 的方法

        /// <summary>
        /// 比较两个点是否重合，或者两个方向向量是否共线（容差为整个系统的容差 AngleTolerance 或 VertexTolerance）
        /// </summary>
        /// <param name="node2"></param>
        /// <param name="isPoint">如果为true，则表示是比较两个点是否重合；如果为false，则表示是比较两个方向向量是否方向相同。</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsAlmostEqualTo(XY node2, bool isPoint)
        {
            if (isPoint)
            {
                if (this.DistanceTo(node2) <= VertexTolerance)
                {
                    return true;
                }
            }
            else
            {
                if (this.AngleTo(node2) <= AngleTolerance)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 比较两个点是否重合，或者两个方向向量是否共线
        /// </summary>
        /// <param name="node2"></param>
        /// <param name="isPoint">如果为true，则表示是比较两个点是否重合；如果为false，则表示是比较两个方向向量是否方向相同。</param>
        /// <param name="tolerance"> 用户指定角度或者距离的容差 </param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsAlmostEqualTo(XY node2, bool isPoint, double tolerance)
        {
            if (isPoint)
            {
                if (this.DistanceTo(node2) <= tolerance)
                {
                    return true;
                }
            }
            else
            {
                if (this.AngleTo(node2) <= tolerance)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ---   运算符

        /// <summary>  执行向量运算 V = V1 + V2 </summary>
        public static XY operator +(XY V1, XY V2)
        {
            return new XY(V1.X + V2.X, V1.Y + V2.Y);
        }

        /// <summary>  执行向量运算 V = V1 - V2 </summary>
        public static XY operator -(XY V1, XY V2)
        {
            return new XY(V1.X - V2.X, V1.Y - V2.Y);
        }

        /// <summary>  将向量 V1 中的分量均乘以 s </summary>
        public static XY operator *(XY V1, double s)
        {
            return new XY(V1.X * s, V1.Y * s);
        }

        #endregion
    }
}
