using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using eZstd.Drawing;
using eZstd.Geometry;
using eZstd.Mathematics;
using eZstd.MatrixPack;
using eZstd.UserControls;
using eZstd.Windows;

namespace eZstd.Test
{
    public class ExeTest
    {
        [STAThread]
        public static void Main()
        {
            DateTime[] Xdate = new DateTime[]
            {
                new DateTime(2013,10,17 ), new DateTime(2013,10,18 ), new DateTime(2013,10,19 ), new DateTime(2013,10,20 ), new DateTime(2013,10,21 ), new DateTime(2013,10,22 ), new DateTime(2013,10,23 ), new DateTime(2013,10,24 ), new DateTime(2013,10,25 ), new DateTime(2013,10,26 ), new DateTime(2013,10,27 ), new DateTime(2013,10,28 ), new DateTime(2013,10,29 ), new DateTime(2013,10,30 ), new DateTime(2013,10,31 ), new DateTime(2013,11,1 ), new DateTime(2013,11,2 ), new DateTime(2013,11,3 ), new DateTime(2013,11,4 ), new DateTime(2013,11,4 ), new DateTime(2013,11,5 ), new DateTime(2013,11,6 ), new DateTime(2013,11,7 ), new DateTime(2013,11,7 ), new DateTime(2013,11,8 ), new DateTime(2013,11,9 ), new DateTime(2013,11,10 ), new DateTime(2013,11,11 ), new DateTime(2013,11,12 ), new DateTime(2013,11,13 ), new DateTime(2013,11,14 ), new DateTime(2013,11,15 ), new DateTime(2013,11,16 ), new DateTime(2013,11,17 ), new DateTime(2013,11,18 ), new DateTime(2013,11,19 ), new DateTime(2013,11,20 ), new DateTime(2013,11,21 ), new DateTime(2013,11,22 ), new DateTime(2013,11,23 ), new DateTime(2013,11,24 ), new DateTime(2013,11,25 ), new DateTime(2013,11,26 ), new DateTime(2013,11,27 ), new DateTime(2013,11,28 ), new DateTime(2013,11,29 ), new DateTime(2013,11,30 ), new DateTime(2013,12,1 ), new DateTime(2013,12,2 ), new DateTime(2013,12,3 ), new DateTime(2013,12,4 ), new DateTime(2013,12,5 ), new DateTime(2013,12,5 ), new DateTime(2013,12,5 ), new DateTime(2013,12,6 ), new DateTime(2013,12,7 ), new DateTime(2013,12,8 ), new DateTime(2013,12,9 ), new DateTime(2013,12,10 ), new DateTime(2013,12,11 ), new DateTime(2013,12,12 ), new DateTime(2013,12,13 ), new DateTime(2013,12,14 ), new DateTime(2013,12,15 ), new DateTime(2013,12,16 ), new DateTime(2013,12,17 ), new DateTime(2013,12,18 ), new DateTime(2013,12,19 ), new DateTime(2013,12,20 ), new DateTime(2013,12,21 ), new DateTime(2013,12,22 ), new DateTime(2013,12,23 ), new DateTime(2013,12,24 ), new DateTime(2013,12,25 ), new DateTime(2013,12,26 ), new DateTime(2013,12,27 ), new DateTime(2013,12,28 ), new DateTime(2013,12,29 ), new DateTime(2013,12,30 ), new DateTime(2013,12,31 ), new DateTime(2014,1,1 ), new DateTime(2014,1,2 ), new DateTime(2014,1,3 ), new DateTime(2014,1,4 ), new DateTime(2014,1,5 ), new DateTime(2014,1,6 ), new DateTime(2014,1,7 ), new DateTime(2014,1,8 ), new DateTime(2014,1,9 ), new DateTime(2014,1,10 ), new DateTime(2014,1,11 ), new DateTime(2014,1,12 ), new DateTime(2014,1,13 ), new DateTime(2014,1,14 ), new DateTime(2014,1,15 ), new DateTime(2014,1,16 ), new DateTime(2014,1,23 ), new DateTime(2014,2,11 ), new DateTime(2014,2,20 ), new DateTime(2014,2,27 ), new DateTime(2014,3,6 ), new DateTime(2014,3,13 ), new DateTime(2014,3,15 ), new DateTime(2014,3,16 ), new DateTime(2014,3,17 ), new DateTime(2014,3,18 ), new DateTime(2014,3,18 ), new DateTime(2014,3,19 ), new DateTime(2014,3,19 ), new DateTime(2014,3,20 ), new DateTime(2014,3,21 ), new DateTime(2014,3,22 ), new DateTime(2014,3,23 ), new DateTime(2014,3,31 ), new DateTime(2014,4,7 ), new DateTime(2014,4,10 ), new DateTime(2014,4,11 ),
            };

            int[] XdateNum = new int[]
            {
                41564 ,41565 ,41566 ,41567 ,41568 ,41569 ,41570 ,41571 ,41572 ,41573 ,41574 ,41575 ,41576 ,41577 ,41578 ,41579 ,41580 ,41581 ,41582 ,41582 ,41583 ,41584 ,41585 ,41585 ,41586 ,41587 ,41588 ,41589 ,41590 ,41591 ,41592 ,41593 ,41594 ,41595 ,41596 ,41597 ,41598 ,41599 ,41600 ,41601 ,41602 ,41603 ,41604 ,41605 ,41606 ,41607 ,41608 ,41609 ,41610 ,41611 ,41612 ,41613 ,41613 ,41613 ,41614 ,41615 ,41616 ,41617 ,41618 ,41619 ,41620 ,41621 ,41622 ,41623 ,41624 ,41625 ,41626 ,41627 ,41628 ,41629 ,41630 ,41631 ,41632 ,41633 ,41634 ,41635 ,41636 ,41637 ,41638 ,41639 ,41640 ,41641 ,41642 ,41643 ,41644 ,41645 ,41646 ,41647 ,41648 ,41649 ,41650 ,41651 ,41652 ,41653 ,41654 ,41655 ,41662 ,41681 ,41690 ,41697 ,41704 ,41711 ,41713 ,41714 ,41715 ,41716 ,41716 ,41717 ,41717 ,41718 ,41719 ,41720 ,41721 ,41729 ,41736 ,41739 ,41740
            };


            return;
            ShowForm();
        }

        // 以窗口的形式打开程序
        public static void ShowForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            Application.Run(new Form1());
        }

        #region ---   各种测试

        public static void m1()
        {
            double[][] dataA = new double[][]
            {
                new double[] {8.1, 2.3, -1.5},
                new double[] {0.5, -6.23, 0.87},
                new double[] {2.5, 1.5, 10.2},
            };

            double[][] dataB = new double[][]
            {
                new double[] {6.1},
                new double[] {2.3},
                new double[] {1.8},
            };

            Matrix A = new Matrix(dataA);
            Matrix b = new Matrix(dataB);

            // 通过LU上下三角分解法 求解线性方程组 Ax = b
            QrDecomposition d = new QrDecomposition(A);
            Matrix x = d.Solve(b);
        }

        public static void m2()
        {
            double[][] dataA = new double[][]
            {
                new double[] {0.03, 58.9},
                new double[] {5.31, -6.10},
            };

            double[][] dataB = new double[][]
            {
                new double[] {59.2},
                new double[] {47.0},
            };

            Matrix A = new Matrix(dataA);
            Matrix b = new Matrix(dataB);
            // 通过LU上下三角分解法 求解线性方程组 Ax = b
            LuDecomposition d = new LuDecomposition(A);
            Matrix x = d.Solve(b);
        }

        public static void m3()
        {
            Ray3D r1 = new Ray3D(startPoint: new XYZ(), endPoint: new XYZ(1, 1, 1));
            Ray3D r2 = new Ray3D(startPoint: new XYZ(1, 0, 0), endPoint: new XYZ(0, 0.45, 0.4501));
            //
            var a1 = r1.IsCoplanarWith(r2);

            var a2 = r1.IntersectWith(r2);
        }

        public static void m4()
        {
            ShowDialogFormTemplate f = new ShowDialogFormTemplate();
            var a = f.ShowDialog();

            ShowDialogWinTemplate t = new ShowDialogWinTemplate();
            var b = t.ShowDialog();
        }

        public static void m5()
        {
            DrawShapesTest dst = new DrawShapesTest();
            dst.ShowDialog();
        }

        #endregion
    }
}