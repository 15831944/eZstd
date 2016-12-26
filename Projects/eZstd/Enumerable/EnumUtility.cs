using System;
using System.Collections.Generic;
using System.Linq;

namespace eZstd.Enumerable
{
    /// <summary>
    /// 与集合相关的一些基本操作函数，比如数组的加减乘除等。
    /// 注意，有很多集合的算法都可以通过 LINQ 语句来实现，如果可以，则推荐用 Linq。但是对于一些特殊的情况，比如二维数组的运算，则在此类中实现。
    /// </summary>
    public static class EnumUtility
    {
        #region ---   集合与标量的 Add 加法(即减法)

        public static double[] Add(double[] source, double valueAdded)
        {
            return source.Select(r => r + valueAdded).ToArray();
        }

        public static double[] Add(int[] source, double valueAdded)
        {
            return source.Select(r => r + valueAdded).ToArray();
        }

        public static int[] Add(int[] source, int valueAdded)
        {
            return source.Select(r => r + valueAdded).ToArray();
        }

        public static IList<double> Add(IList<double> source, double valueAdded)
        {
            return source.Select(r => r + valueAdded).ToArray();
        }

        public static IList<int> Add(IList<int> source, int valueAdded)
        {
            return source.Select(t => t + valueAdded).ToList();
        }

        public static IList<double> Add(IList<int> source, double valueAdded)
        {
            return source.Select(t => t + valueAdded).ToList();
        }

        #endregion

        #region ---   集合与标量的 Multiply 乘法(即除法)

        public static double[] Multiply(double[] source, double valueAdded)
        {
            return source.Select(r => r * valueAdded).ToArray();
        }

        public static double[] Multiply(int[] source, double valueAdded)
        {
            return source.Select(r => r * valueAdded).ToArray();
        }

        public static int[] Multiply(int[] source, int valueAdded)
        {
            return source.Select(r => r * valueAdded).ToArray();
        }


        public static IList<double> Multiply(IList<double> source, double valueAdded)
        {
            return source.Select(r => r * valueAdded).ToArray();
        }

        public static IList<int> Multiply(IList<int> source, int valueAdded)
        {
            return source.Select(t => t * valueAdded).ToList();
        }

        public static IList<double> Multiply(IList<int> source, double valueAdded)
        {
            return source.Select(t => t * valueAdded).ToList();
        }

        #endregion

        #region ---   求集合中的最大值 Max 或最小值 Min

        public static double Max(double arg1, double arg2, params double[] args)
        {
            double m = Math.Max(arg1, arg2);
            if (args.Length > 0)
            {
                return Math.Max(m, args.Max());
            }
            return m;
        }

        public static int Max(int arg1, int arg2, params int[] args)
        {
            int m = Math.Max(arg1, arg2);
            if (args.Length > 0)
            {
                return Math.Max(m, args.Max());
            }
            return m;
        }

        public static double Min(double arg1, double arg2, params double[] args)
        {
            double m = Math.Min(arg1, arg2);
            if (args.Length > 0)
            {
                return Math.Min(m, args.Min());
            }
            return m;
        }

        public static int Min(int arg1, int arg2, params int[] args)
        {
            int m = Math.Min(arg1, arg2);
            if (args.Length > 0)
            {
                return Math.Min(m, args.Min());
            }
            return m;
        }
        #endregion
    }
}