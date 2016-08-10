using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Geometry
{

    /// <summary> 空间中三个点形成的平面三角形 </summary>
    public class Triangle
    {
        /// <summary>
        /// 最初始的三个点，此四个点在后面的操作中可能会经历重排、缩放、旋转等操作，这些操作会体现在 Nodes 属性中，但是 _initialNodes 中的值是不会变化的。
        /// </summary>
        private readonly XYZ[] _initialNodes;

        /// <summary>
        /// 构成平面的四个点。此数组中共有四个元素
        /// </summary>
        public XYZ[] Nodes;

        #region ---   构造函数

        /// <summary> 默认三角形：XY平面中的第一象限的直角三角形（两直角边长为1） </summary>
        public Triangle()
        {
            XYZ[] nodes = new XYZ[3]
            {
              new XYZ(0, 0, 0),  // 原点
              new XYZ(1, 0, 0),  // x 轴
              new XYZ(0, 1, 0),  // y 轴
            };

            Nodes = nodes;
            _initialNodes = Nodes;
        }

        /// <summary> 构造函数 </summary>
        public Triangle(XYZ[] nodes)
        {
            if (nodes == null || nodes.Length != 3)
            {
                throw new ArgumentException("The input array must have four nodes.");
            }
            //
            Nodes = nodes;
            _initialNodes = Nodes;
        }

        /// <summary> 构造函数 </summary>
        public Triangle(XYZ node1, XYZ node2, XYZ node3)
        {
            XYZ[] nodes = new XYZ[] { node1, node2, node3 };

            // 
            if (nodes == null || nodes.Length != 3)
            {
                throw new ArgumentException("The input array must have four nodes.");
            }
            //
            Nodes = nodes;
            _initialNodes = Nodes;
        }

        #endregion

        /// <summary> 将集合中的四个点还原为最初始的状态 </summary>
        public void Reset()
        {
            Nodes = _initialNodes;
        }

        /// <summary> 寻找三个点所形成的空间三角形的形心 </summary>
        /// <returns></returns>
        public XYZ FindCentroid()
        {
            return FindCentroid(Nodes[0], Nodes[1], Nodes[2]);
        }

        /// <summary>
        /// 寻找三个点所形成的空间三角形的形心
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="node3"></param>
        /// <returns></returns>
        public static XYZ FindCentroid(XYZ node1, XYZ node2, XYZ node3)
        {
            return new XYZ((node1.X + node2.X + node3.X) / 3, (node1.Y + node2.Y + node3.Y) / 3, (node1.Z + node2.Z + node3.Z) / 3);
        }

        /// <summary> 计算空间三角形的面积  </summary>
        /// <returns></returns>
        public double get_Area()
        {
            return Area(Nodes[0], Nodes[1], Nodes[2]);
        }

        /// <summary> 计算空间三角形的面积  </summary>
        /// <returns></returns>
        private static double Area(XYZ node1, XYZ node2, XYZ node3)
        {
            // 三条边长
            double a = XYZ.Distance(node1, node2);
            double b = XYZ.Distance(node2, node3);
            double c = XYZ.Distance(node3, node1);

            double p = (a + b + c) / 2;
            return p * (p - a) * (p - b) * (p - c); // 海伦公式
        }
    }
}