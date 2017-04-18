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
            int v1 = 1;
            Action<int> myMethod1 = input1 =>
            {
                var v2 = 20;
                v1 = input1 + v2;
            };

            myMethod1(4); // returns false of course   
            Console.WriteLine(v1); // 返回 24 
            Console.Read();
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