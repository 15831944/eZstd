using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Geometry
{
    /// <summary>
    /// 由空间共面的四个点所形成的平面
    /// </summary>
    public class Plane4
    {
        /// <summary>
        /// 最初始的四个点，此四个点在后面的操作中可能会经历重排、缩放、旋转等操作，这些操作会体现在 Nodes 属性中，但是 _initialNodes 中的值是不会变化的。
        /// </summary>
        private readonly XYZ[] _initialNodes;

        /// <summary>
        /// 构成平面的四个点。此数组中共有四个元素
        /// </summary>
        public XYZ[] Nodes;

        /// <summary> Nodes 数组中的四个点是否可以按顺序排成一个边界环路。如果其值为null，表示还未检测过。 </summary>
        public bool? IsCycle;

        #region ---   构造函数

        /// <summary> 默认三角形：XY平面中的第一象限的正方形（边长为1，从原点开始逆时针旋转一圈） </summary>
        public Plane4()
        {
            XYZ[] nodes = new XYZ[4]
            {
              new XYZ(0, 0, 0),  // 原点
              new XYZ(1, 0, 0),  // x 轴
              new XYZ(1, 1, 0),  // y 轴
              new XYZ(0, 1, 0),  // y 轴
            };

            Nodes = nodes;
            IsCycle = true;
            _initialNodes = Nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        public Plane4(XYZ[] nodes)
        {
            Initialize(nodes);
            _initialNodes = Nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        public Plane4(XYZ node1, XYZ node2, XYZ node3, XYZ node4)
        {
            XYZ[] nodes = new XYZ[] { node1, node2, node3, node4 };

            // 

            Initialize(nodes);
            _initialNodes = Nodes;
        }

        private void Initialize(XYZ[] nodes)
        {
            if (nodes == null || nodes.Length != 4)
            {
                throw new ArgumentException("The input array must have four nodes.");
            }

            // 检测此四个点是否在同一个平面上
            Ray3D r1 = new Ray3D(Nodes[0], nodes[1] - Nodes[0], false);
            Ray3D r2 = new Ray3D(Nodes[2], nodes[3] - Nodes[2], false);

            if (!r1.IsCoplanarWith(r2))
            {
                throw new ArgumentException("the input nodes must be in the same plane");
            }

            //
            Nodes = nodes;
            IsCycle = null;
        }

        #endregion
        /// <summary> 将集合中的四个点还原为最初始的状态 </summary>
        public void Reset()
        {
            Nodes = _initialNodes;
        }

        #region ---   四个点依次连成边界环路

        /// <summary>
        /// 检查代表平面的集合中的四个点是否能依次连成一个边界环路
        /// </summary>
        /// <returns></returns>
        public bool CheckCycle()
        {
            bool isCycle = false;

            // 进行检测
            Line3D l01 = new Line3D(Nodes[0], Nodes[1] - Nodes[0]);
            Line3D l12 = new Line3D(Nodes[1], Nodes[2] - Nodes[1]);
            Line3D l23 = new Line3D(Nodes[2], Nodes[3] - Nodes[2]);
            Line3D l30 = new Line3D(Nodes[3], Nodes[0] - Nodes[3]);

            //
            isCycle = (l01.GetIntersectPointWith(l23) == null)
                && (l12.GetIntersectPointWith(l30) == null);

            // 检测完成
            IsCycle = isCycle;
            return isCycle;
        }

        /// <summary>
        /// 将代表平面的集合中的四个点进行重新排列，以使其可以依次连成一个边界环路
        /// </summary>
        /// <returns></returns>
        public void ArrangeCycle()
        {
            // 进行检测
            Line3D l01 = new Line3D(Nodes[0], Nodes[1] - Nodes[0]);
            Line3D l12 = new Line3D(Nodes[1], Nodes[2] - Nodes[1]);
            Line3D l23 = new Line3D(Nodes[2], Nodes[3] - Nodes[2]);
            Line3D l30 = new Line3D(Nodes[3], Nodes[0] - Nodes[3]);

            // 对于一个四边形，其只四个节点不能构成边界环线的情况只有两种
            if (l01.GetIntersectPointWith(l23) != null)
            {
                Nodes = new XYZ[4] { Nodes[0], Nodes[3], Nodes[1], Nodes[2] };
            }
            else if (l12.GetIntersectPointWith(l30) != null)
            {
                Nodes = new XYZ[4] { Nodes[0], Nodes[1], Nodes[3], Nodes[2] };
            }
            // 检测完成
            IsCycle = true;
        }



        #endregion

        /// <summary>
        /// 寻找空间四个点所形成的共面的空间四边形角形的形心，如果四个点不共面，则会报错。
        /// 在计算形心之前，必须确保此平面四边形的节点集合可以依次形成一个边界环路，即其<see cref="IsCycle"/>要为true。
        /// </summary>
        /// <returns> 四边形的形心点的坐标 </returns>
        /// <remarks>在hypermesh导出的 inp文件中，输入的四个节点的顺序一定是可以形成一个边界环路的，即使此S4单元的网格形状为有凹角的异型错误网格。</remarks>
        public XYZ FindCentroid()
        {
            XYZ node1 = Nodes[0];
            XYZ node2 = Nodes[1];
            XYZ node3 = Nodes[2];
            XYZ node4 = Nodes[3];

            // 以两个对角点中距离较短的那个作为两个三角形的分割边
            XYZ[] nodes;
            if (node1.DistanceTo(node3) < node2.DistanceTo(node4))
            {
                nodes = new XYZ[] { node2, node1, node3, node4 };
            }
            else
            {
                nodes = new XYZ[] { node1, node2, node4, node3 };
            }

            // 先计算第一个三角形的形心位置与面积
            Triangle tria1 = new Triangle(nodes[0], nodes[1], nodes[2]);
            XYZ c1 = tria1.FindCentroid();

            // 再计算第二个三角形的形心位置与面积
            Triangle tria2 = new Triangle(nodes[1], nodes[2], nodes[3]);
            XYZ c2 = tria2.FindCentroid();

            double centDis = c1.DistanceTo(c2);

            // 利用杠杆原理计算两个三角形的组合四边形形心位置：area1 * x=area2 * (centDis-x)
            double x = centDis / (tria1.get_Area() / tria2.get_Area() + 1);  // 四边形的形心点处在两个三角形的形心连线上，x 为四边形的形心点到第1个三角形形心的距离。

            return c1.Move(c1.VectorTo(c2).SetLength(x));

        }
    }
}
