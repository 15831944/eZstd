using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using eZstd.Geometry;
using eZstd.MatrixPack;
using eZstd.Windows;

namespace eZstd
{
    public class ExeTest
    {
        [STAThread]
        public static void Main()
        {
            m4();
        }


        public static void m1()
        {
            double[][] dataA = new double[][]
     {
                new double[] {8.1,2.3,-1.5},
                new double[] {0.5,-6.23,0.87},
                new double[] {2.5,1.5,10.2},
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
                new double[] {0.03,58.9},
                new double[] {5.31,-6.10},};

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
    }
}
