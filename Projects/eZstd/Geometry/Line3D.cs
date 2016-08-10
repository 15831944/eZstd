using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZstd.MatrixPack;

namespace eZstd.Geometry
{
    /// <summary>
    /// 三维直角坐标系中，无限长度的直线，它有两个基本属性：原点位置及方向矢量
    /// </summary>
    public class Line3D
    {

        #region ---   Properties

        /// <summary>
        /// 空间直线所经过的某一个点
        /// </summary>
        public readonly XYZ Origin;

        /// <summary>
        /// 空间直线的方向矢量，其矢量长度并无意义
        /// </summary>
        public readonly XYZ Direction;

        #endregion

        #region ---   构造函数

        /// <summary>
        /// 创建一条位于空间直角坐标系的坐标轴上的单位长度的直线
        /// </summary>
        /// <param name="axis">直线所在的坐标轴。其值可取1、2、3，分别代表x、y、z轴</param>
        public Line3D(byte axis)
        {
            Origin = new XYZ();
            switch (axis)
            {
                case 1: Direction = new XYZ(1, 0, 0); break;
                case 2: Direction = new XYZ(0, 1, 0); break;
                case 3: Direction = new XYZ(0, 0, 1); break;
            }
        }

        /// <summary>
        /// 通过节点与方向来构造直线
        /// </summary>
        /// <param name="origin">空间直线所经过的某一个点</param>
        /// <param name="direction">空间直线的方向矢量，其矢量长度并无意义</param>
        public Line3D(XYZ origin, XYZ direction)
        {
            Origin = origin;
            Direction = direction;
        }
        #endregion

        #region ---   直线交点

        /// <summary>
        /// 三维空间中两直线的交点，如果没有交点，则返回 null
        /// </summary>
        /// <returns>如果这两条射线所对应的空间直线能够相交，则返回其交点，如果不能相交，则返回 null。</returns>
        /// <remarks>
        /// 空间三维直线的参数方程为：
        ///    x=x0 + m*t; 
        ///    y=y0 + n*t;
        ///    z=z0 + p*t; 
        /// 其中{x0,y0,z0} 为直线中的某个点，{m,n,p}为直线的方向向量。</remarks>
        public XYZ GetIntersectPointWith(Line3D line2)
        {
            return GetIntersectPoint(this, line2);
        }

        /// <summary>
        /// 三维空间中两直线的交点
        /// </summary>
        /// <returns>如果这两条射线所对应的空间直线能够相交，则返回其交点，如果不能相交，则返回 null。</returns>
        /// <remarks>
        /// 空间三维直线的参数方程为：
        ///    x=x0 + m*t; 
        ///    y=y0 + n*t;
        ///    z=z0 + p*t; 
        /// 其中{x0,y0,z0} 为直线中的某个点，{m,n,p}为直线的方向向量。</remarks>
        private static XYZ GetIntersectPoint(Line3D line1, Line3D line2)
        {
            // 构造一个六个方程、五个未知数x,y,z,t,k的线性方程组

            // 先用前五个方程解出对应的五个未知数
            double[][] dataA = new double[][]
            {
                new double[] {1,0,0,-line1.Direction.X,0},
                new double[] {0,1,0,-line1.Direction.Y,0},
                new double[] {0,0,1,-line1.Direction.Z,0},
                new double[] {1,0,0,0,-line2.Direction.X},
                new double[] {0,1,0,0,-line2.Direction.Y},
            };

            double[][] dataB = new double[][]
            {
                new double[] {line1.Origin.X},
                new double[] {line1.Origin.Y},
                new double[] {line1.Origin.Z},
                new double[] {line2.Origin.X},
                new double[] {line2.Origin.Y},
            };

            Matrix A = new Matrix(dataA);
            Matrix b = new Matrix(dataB);

            // 通过LU上下三角分解法 求解线性方程组 Ax = b
            LuDecomposition d = new LuDecomposition(A);

            Matrix x = d.Solve(b);

            // 五个未知量 x,y,z,t,k 的值
            var col = x.GetColumn(0);

            // 将这五个变量代入第六个方程 z = z2 + p2 * k 中，
            // 如果此方程左边与右边相等，则表示两条直线有交点，否则两条直线不相交。
            var left = col[2];
            var right = line2.Origin.Z + line2.Direction.Z * col[4];

            // 计算左右两边的相对误差
            double relative = Math.Abs((left - right) / Math.Max(left, right));

            //
            if (relative > 0.0001)
            {
                return null;
            }
            return relative < 0.0001 ? new XYZ(col[0], col[1], col[2]) : null;
        }

        #endregion

        /// <summary> 两条直线是否在同一个三维平面上 </summary>
        /// <param name="line2"></param>
        /// <returns></returns>
        public bool IsCoplanarWith(Line3D line2)
        {
            XYZ v_r1r2 = line2.Origin - Origin;
            XYZ v_d1 = v_r1r2.CrossProduct(Direction);
            XYZ v_d2 = v_r1r2.CrossProduct(line2.Direction);

            // 如果 v_d1 与 v_d2 的方向共线，则此两条射线共面
            return v_d1.IsCollinearWith(v_d2);
        }
    }
}
