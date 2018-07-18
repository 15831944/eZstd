using System;
using System.Collections.Generic;
using System.Linq;

namespace eZstd.Mathematics
{
    /// <summary>
    /// 样条插值算法
    /// </summary>
    public class Interpolation
    {
        #region 二维曲线的线性插值

        /// <summary>
        /// 进行二维曲线的线性插值，对于超出源数据范围的区域，采用直线放射
        /// </summary>
        /// <param name="srcX">插值数据源中的X，X序列中的值不一定必须要递增或者递减排列</param>
        /// <param name="srcY">插值数据源中的Y</param>
        /// <param name="interpX">想要进行插值的x序列</param>
        /// <returns>插值后得到的与 <paramref name="interpX"/> 相对应的y值序列</returns>
        public static double[] LinearInterpolation(double[] srcX, double[] srcY, double[] interpX)
        {
            // 构造插值数据源
            var src = new SortedList<double, double>();
            for (int i = 0; i < srcX.Length; i++)
            {
                var x = srcX[i];
                if (!src.ContainsKey(x))
                {
                    src.Add(x, srcY[i]);
                }
            }

            var srcCount = src.Count;
            if (srcCount >= 2)
            {
                // 进行线性插值
                var interpY = new double[interpX.Length];

                var sortedX = src.Keys; // 排序后的X序列，其第一个值为最小值，最后一个值为最大值
                var bindedY = src.Values; // 排序后的X序列，其第一个值为最小值，最后一个值为最大值


                var min = sortedX[0];
                var max = sortedX[srcCount - 1];
                for (int i = 0; i < interpX.Length; i++)
                {
                    //
                    var vx = interpX[i];
                    double vy = 0;
                    if (vx <= min)
                    {
                        vy = LinearInterp(x1: min, y1: bindedY[0], x2: sortedX[1], y2: bindedY[1], interpX: vx);
                    }
                    else if (vx >= max)
                    {
                        vy = LinearInterp(x1: max, y1: bindedY[srcCount - 1], x2: sortedX[srcCount - 2],
                            y2: bindedY[srcCount - 2], interpX: vx);
                    }
                    else
                    {
                        // 正常插值
                        int ind;
                        for (ind = 0; ind < srcCount; ind++)
                        {
                            if (sortedX[ind] > vx)
                            {
                                // 找到插值位置，位于 ind-1 与 ind 之间
                                vy = LinearInterp(x1: sortedX[ind - 1], y1: bindedY[ind - 1], x2: sortedX[ind],
                                    y2: bindedY[ind], interpX: vx);
                                break;
                            }
                        }
                    }
                    interpY[i] = vy;
                }
                return interpY;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 两个点之间的线性插值 </summary>
        /// <param name="interpX">要进行插值的位置，此位移可以在[x1, x2]之外</param>
        /// <returns></returns>
        private static double LinearInterp(double x1, double y1, double x2, double y2, double interpX)
        {
            double k = (y1 - y2)/(x1 - x2);
            return y1 + k*(interpX - x1);
        }

        #endregion

        #region 二维曲线的样条插值

        /// <summary>
        /// 进行二维曲线的样条插值
        /// </summary>
        /// <param name="srcX">插值数据源中的X，X序列中的值不一定必须要递增或者递减排列</param>
        /// <param name="srcY">插值数据源中的Y</param>
        /// <param name="interpX">想要进行插值的x序列</param>
        /// <returns>插值后得到的与 <paramref name="interpX"/> 相对应的y值序列</returns>
        public static double[] SplineInterpolation(double[] srcX, double[] srcY, double[] interpX)
        {
            var count = srcX.Length;
            if (count <= 0 || srcY.Length != count)
            {
                throw new ArgumentException("the source arrays Xs and Ys must have the same length.");
            }

            point[] points = new point[count];
            for (var i = 0; i < count; i++)
            {
                points[i] = new point(srcX[i], srcY[i]);
            }

            point.DeSortX(points);
            if ((interpX.Min() < points.Min().x) || (interpX.Max() > points.Max().x))
            {
                throw new ArgumentException("the x range to be interpolated must be within the source x range.");
            }
            var y = splineInsertPoint(points, interpX);

            return y;
        }

        private static double[] splineInsertPoint(point[] points, double[] xs)
        {
            int plength = points.Length;
            double[] h = new double[plength];
            double[] f = new double[plength];
            double[] l = new double[plength];
            double[] v = new double[plength];
            double[] g = new double[plength];

            for (int i = 0; i < plength - 1; i++)
            {
                h[i] = points[i + 1].x - points[i].x;
                f[i] = (points[i + 1].y - points[i].y)/h[i];
            }

            for (int i = 1; i < plength - 1; i++)
            {
                l[i] = h[i]/(h[i - 1] + h[i]);
                v[i] = h[i - 1]/(h[i - 1] + h[i]);
                g[i] = 3*(l[i]*f[i - 1] + v[i]*f[i]);
            }

            double[] b = new double[plength];
            double[] tem = new double[plength];
            double[] m = new double[plength];
            double f0 = (points[0].y - points[1].y)/(points[0].x - points[1].x);
            double fn = (points[plength - 1].y - points[plength - 2].y)/(points[plength - 1].x - points[plength - 2].x);

            b[1] = v[1]/2;
            for (int i = 2; i < plength - 2; i++)
            {
                // Console.Write(" " + i);
                b[i] = v[i]/(2 - b[i - 1]*l[i]);
            }
            tem[1] = g[1]/2;
            for (int i = 2; i < plength - 1; i++)
            {
                //Console.Write(" " + i);
                tem[i] = (g[i] - l[i]*tem[i - 1])/(2 - l[i]*b[i - 1]);
            }
            m[plength - 2] = tem[plength - 2];
            for (int i = plength - 3; i > 0; i--)
            {
                //Console.Write(" " + i);
                m[i] = tem[i] - b[i]*m[i + 1];
            }
            m[0] = 3*f[0]/2.0;
            m[plength - 1] = fn;
            int xlength = xs.Length;
            double[] insertRes = new double[xlength];
            for (int i = 0; i < xlength; i++)
            {
                int j = 0;
                for (j = 0; j < plength; j++)
                {
                    if (xs[i] < points[j].x)
                        break;
                }
                j = j - 1;
                if (j == -1 || j == points.Length - 1)
                {
                    if (j == -1)
                        throw new Exception("插值下边界超出");
                    if (j == points.Length - 1 && xs[i] == points[j].x)
                        insertRes[i] = points[j].y;
                    else
                        throw new Exception("插值下边界超出");
                }
                else
                {
                    double p1;
                    p1 = (xs[i] - points[j + 1].x)/(points[j].x - points[j + 1].x);
                    p1 = p1*p1;
                    double p2;
                    p2 = (xs[i] - points[j].x)/(points[j + 1].x - points[j].x);
                    p2 = p2*p2;
                    double p3;
                    p3 = p1*(1 + 2*(xs[i] - points[j].x)/(points[j + 1].x - points[j].x))*points[j].y +
                         p2*(1 + 2*(xs[i] - points[j + 1].x)/(points[j].x - points[j + 1].x))*points[j + 1].y;

                    double p4;
                    p4 = p1*(xs[i] - points[j].x)*m[j] + p2*(xs[i] - points[j + 1].x)*m[j + 1];
                    //         Console.WriteLine(m[j] + " " + m[j + 1] + " " + j);
                    p4 = p4 + p3;
                    insertRes[i] = p4;
                }
            }
            return insertRes;
        }

        private class point : IComparable
        {
            public double x = 0;
            public double y = 0;

            public point(double x = 0, double y = 0)
            {
                this.x = x;
                this.y = y;
            }

            //-------写一个排序函数，使得输入的点按顺序排列，是因为插值算法的要求是，x轴递增有序的---------
            public static point[] DeSortX(point[] points)
            {
                int length = points.Length;
                double temx, temy;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                        if (points[j].x > points[j + 1].x)
                        {
                            temx = points[j + 1].x;
                            points[j + 1].x = points[j].x;
                            points[j].x = temx;
                            temy = points[j + 1].y;
                            points[j + 1].y = points[j].y;
                            points[j].y = temy;
                        }
                }
                return points;
            }

            public int CompareTo(object obj)
            {
                var p1 = obj as point;
                return x.CompareTo(p1.x);
            }
        }

        #endregion
    }
}