using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Geometry
{
    /// <summary>
    /// 一个空间的坐标点或者空间的矢量
    /// </summary>
    public class XYZ
    {
        #region ---   Properties

        /// <summary>
        /// 角度容差，单位为弧度。当两个向量之间的夹角小于此常数时，认为此两个向量方向相同。
        /// </summary>
        /// <remarks>此常数的值借鉴于 Revit 2016（X64）中的容差值。</remarks>
        public static double AngleTolerance = 0.00174532925199433;

        /// <summary>
        /// 距离容差，具体单位视整个模型的全局单位而定。在此距离之内的两个点会被认为是重合的。
        /// 此常数的值可以借鉴 Revit 2016（X64）中的容差值，即  0.0005233832795 inch = 0.01329394 mm。
        /// </summary>
        /// <remarks> </remarks>
        public static double VertexTolerance = 0.0005233832795;

        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        #endregion

        #region ---   构造函数

        /// <summary> 默认点 (0, 0, 0) </summary>
        public XYZ()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public XYZ(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #endregion

        public override string ToString()
        {
            return "( " + X.ToString() + "," + "\t" + Y.ToString() + "," + "\t" + Z.ToString() + " )";
        }

        #region ---   空间点的方法

        /// <summary> 计算空间两个点的距离 </summary>
        /// <returns></returns>
        public static double Distance(XYZ point1, XYZ point2)
        {
            return Substract(point2, point1).GetLength();
        }

        /// <summary> 计算空间两个点的距离 </summary>
        /// <returns></returns>
        public double DistanceTo(XYZ point2)
        {
            return Distance(this, point2);
        }

        /// <summary> 一个空间点沿空间的位移矢量移动后的新位置 </summary>
        public XYZ Move(XYZ vector)
        {
            return new XYZ(X + vector.X, Y + Y, Z + vector.Z);
        }

        /// <summary>
        /// 在三维空间中，将一个点 point 沿指定的方向 direction 延伸指定的距离 length
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public XYZ Move(XYZ direction, double length)
        {
            return this + direction.SetLength(length);
        }

        /// <summary> 从本坐标点指向输入的 node2 的位移矢量 </summary>
        /// <param name="point2"> 矢量的终点 </param>
        /// <returns> 一个空间矢量，起始点为 node1，终点为 node2 </returns>
        public XYZ VectorTo(XYZ point2)
        {
            return Substract(point2, this);
        }

        /// <summary>
        /// 比较两个点是否重合（容差为整个系统的容差 AngleTolerance）
        /// </summary>
        /// <param name="point2"></param>
        public bool IsAlmostEqualTo(XYZ point2)
        {
            return this.DistanceTo(point2) <= VertexTolerance;
        }

        /// <summary>
        /// 比较两个点是否重合
        /// </summary>
        /// <param name="point2"></param>
        /// <param name="tolerance"> 用户指定的距离容差 </param>
        public bool IsAlmostEqualTo(XYZ point2, double tolerance)
        {
            return this.DistanceTo(point2) <= tolerance;
        }
        #endregion

        #region ---   空间矢量的方法

        /// <summary> 将空间矢量反向，保持其长度不变 </summary>
        public XYZ Reverse()
        {
            return new XYZ(-X, -Y, -Z);
        }

        /// <summary>
        /// 将空间矢量进行归一化，使其长度变为 1 。
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public XYZ Normalize()
        {
            return SetLength(newLength: 1.0);
        }

        /// <summary>
        /// 将一个空间矢量缩放到指定的长度（方向不变）
        /// </summary>
        /// <param name="newLength">缩放后的长度</param>
        /// <returns> 缩放后的新矢量 </returns>
        public XYZ SetLength(double newLength)
        {
            return this * (newLength / GetLength());
        }

        /// <summary> 空间矢量的长度 </summary>
        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// 向量的点乘（内积）：D = V1 . V2
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        /// <remarks>此标量值的几何意义为向量a在向量b上的投影长度a'与向量b的长度的乘积</remarks>
        public double DotProduct(XYZ vector2)
        {
            return this.X * vector2.X + this.Y * vector2.Y + this.Z * vector2.Z;
        }

        /// <summary>
        /// 向量的叉乘（外积）：V = V1 X V2
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        /// <remarks> 两个向量的外积矢量V，其模（长度）是由这两个向量所形成的平行四边形的面积；
        /// 其方向为以V1、V2 分别作为X轴、Y轴，在进行右手法则后所得到的Z轴的方向。 </remarks>
        public XYZ CrossProduct(XYZ vector2)
        {
            return new XYZ(
                this.Y * vector2.Z - this.Z * vector2.Y,
                this.Z * vector2.X - this.X * vector2.Z,
                this.X * vector2.Y - this.Y * vector2.X);
        }

        /// <summary>
        /// 计算两个空间向量之间的夹角，单位为弧度，范围为[0,pi]
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        /// <remarks>如果要考查大于180度的向量夹角，则要进行附加的判断。</remarks>
        public double AngleTo(XYZ v2)
        {
            return Math.Acos(this.DotProduct(v2) / this.GetLength() / v2.GetLength());
        }


        /// <summary> 两个方向矢量是否共线（方向相同或者相反）。容差为整个系统的容差 AngleTolerance。</summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public bool IsCollinearWith(XYZ v2)
        {
            if ((this.AngleTo(v2) <= AngleTolerance) || (this.AngleTo(v2.Reverse()) <= AngleTolerance))
            {
                return true;
            }
            return false;
        }

        /// <summary> 两个方向矢量是否共线（方向相同或者相反）。。容差为用户指定的角度容差。 </summary>
        /// <param name="v2"></param>
        /// <param name="tolerance">用户指定的角度容差</param>
        /// <returns></returns>
        public bool IsCollinearWith(XYZ v2, double tolerance)
        {
            if ((this.AngleTo(v2) <= tolerance) || (this.AngleTo(v2.Reverse()) <= tolerance))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region ---   私有方法

        /// <summary>  执行向量运算 V = V1 + V2 </summary>
        private XYZ Add(XYZ V2)
        {
            return XYZ.Add(this, V2);
        }

        /// <summary>  执行向量运算 V = V1 - V2 </summary>
        private XYZ Substract(XYZ V2)
        {
            return this - V2;
        }

        /// <summary>  执行向量运算 V = V1 + V2 </summary>
        private static XYZ Add(XYZ V1, XYZ V2)
        {
            return V1 + V2;
        }

        /// <summary>  执行向量运算 V = V1 - V2 </summary>
        private static XYZ Substract(XYZ V1, XYZ V2)
        {
            return V1 - V2;
        }

        #endregion

        #region ---   运算符

        /// <summary>  执行向量运算 V = V1 + V2 </summary>
        public static XYZ operator +(XYZ V1, XYZ V2)
        {
            return new XYZ(V1.X + V2.X, V1.Y + V2.Y, V1.Z + V2.Z);
        }

        /// <summary>  执行向量运算 V = V1 - V2 </summary>
        public static XYZ operator -(XYZ V1, XYZ V2)
        {
            return new XYZ(V1.X - V2.X, V1.Y - V2.Y, V1.Z - V2.Z);
        }

        /// <summary>  将向量 V1 中的分量均乘以 s </summary>
        public static XYZ operator *(XYZ V1, double s)
        {
            return new XYZ(V1.X * s, V1.Y * s, V1.Z * s);
        }

        #endregion
    }
}