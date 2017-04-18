using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;

namespace eZstd.Enumerable
{
    /// <summary>
    /// 一些二维数组所特有的操作。
    /// 注意，有很多集合的算法都可以通过 LINQ 语句来实现，如果可以，则推荐用 Linq。但是对于一些特殊的情况，比如二维数组的运算，则在此类中实现。
    /// </summary>
    public class Array2D : ICloneable
    {
        #region ---   Properties

        /// <summary> 如果<see cref="IsElementsInterger"/>，则此字段的值为对应的源二维数组。否则为 null </summary>
        private int[,] _arrayInt;
        /// <summary> 如果<see cref="IsElementsInterger"/>为false，则此字段的值为对应的源二维数组。否则为 null </summary>
        private double[,] _arraydbl;
        /// <summary> 二维数组对象，即 double[,] 或 int[,] 对象。在外部可通过 as 进行类型转换，如 double[,] v = Value as double[,]; </summary>
        public Array Value
        {
            get
            {
                if (IsElementsInterger)
                {
                    return _arrayInt;
                }
                else
                {
                    return _arraydbl;
                }
            }
        }

        /// <summary> 二维数组对象，即 double[,]。如果数据源为 int[,]，则会从其中提取出 double[,] ; </summary>
        public int[,] ValueI
        {
            get
            {
                if (IsElementsInterger)
                {
                    return _arrayInt;
                }
                else
                {
                    return Double2Int();
                }
            }
        }

        /// <summary> 二维数组对象，即 int[,]。如果数据源为 double[,]，则会从其中提取出 int[,] ; </summary>
        public double[,] ValueD
        {
            get
            {
                if (IsElementsInterger)
                {
                    return Int2Double();
                }
                else
                {
                    return _arraydbl;
                }
            }
        }

        /// <summary> 二维数组中的元素类型是否为整数。如果为true，则 Value 为 int[,]类型，否则 Value 为 double[,]类型。 </summary>
        public bool IsElementsInterger { get; private set; }


        /// <summary> 二维数组中的行数 </summary>
        public int Rows { get; private set; }
        /// <summary> 二维数组中的列数 </summary>
        public int Columns { get; private set; }

        #endregion

        #region ---   构造函数

        public Array2D(int[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            IsElementsInterger = true;
            _arrayInt = array;
        }

        public Array2D(double[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);
            IsElementsInterger = false;
            _arraydbl = array;
        }

        #endregion

        #region ---   加减乘除 + - * / 

        #region ---   集合与标量的 Add 加法(即减法)

        public void Add(double valueAdded)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Add(_arrayInt, valueAdded);
            }
            else
            {
                _arraydbl = Add(_arrayInt, valueAdded);
            }
            IsElementsInterger = false;
        }
        public void Add(int valueAdded)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Add(_arrayInt, valueAdded);
            }
            else
            {
                _arraydbl = Add(_arraydbl, valueAdded);
            }

        }

        public void Substract(double valueSubstracted)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Add(_arrayInt, -valueSubstracted);
            }
            else
            {
                _arraydbl = Add(_arrayInt, -valueSubstracted);
            }
            IsElementsInterger = false;

        }
        public void Substract(int valueSubstracted)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Add(_arrayInt, -valueSubstracted);
            }
            else
            {
                _arraydbl = Add(_arraydbl, -valueSubstracted);
            }
        }

        // -------------------------------------------------------------------------------------------------

        public void Add(double[,] arrayAdded)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Add(arrayAdded, _arrayInt);
            }
            else
            {
                _arraydbl = Add(_arraydbl, arrayAdded);
            }
            IsElementsInterger = false;
        }
        public void Add(int[,] arrayAdded)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Add(_arrayInt, arrayAdded);
            }
            else
            {
                _arraydbl = Add(_arraydbl, arrayAdded);
            }
        }
        public void Add(Array2D arrayAdded)
        {
            if (IsElementsInterger)
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arrayInt = Add(_arrayInt, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Add(_arrayInt, arrayAdded.ValueD);
                    IsElementsInterger = false;
                }
            }
            else
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arraydbl = Add(_arraydbl, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Add(_arraydbl, arrayAdded.ValueD);
                }
            }

        }

        public void Substract(double[,] arraySubstracted)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Substract(_arrayInt, arraySubstracted);
            }
            else
            {
                _arraydbl = Substract(_arraydbl, arraySubstracted);
            }
            IsElementsInterger = false;

        }
        public void Substract(int[,] arraySubstracted)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Substract(_arrayInt, arraySubstracted);
            }
            else
            {
                _arraydbl = Substract(_arraydbl, arraySubstracted);
            }

        }
        public void Substract(Array2D arrayAdded)
        {
            if (IsElementsInterger)
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arrayInt = Substract(_arrayInt, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Substract(_arrayInt, arrayAdded.ValueD);
                    IsElementsInterger = false;
                }
            }
            else
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arraydbl = Substract(_arraydbl, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Substract(_arraydbl, arrayAdded.ValueD);
                }
            }

        }

        #endregion

        #region ---   集合与标量的 Multiply 乘法(即除法)

        public void Multiply(double valueMultiplied)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Multiply(_arrayInt, valueMultiplied);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, valueMultiplied);
            }
            IsElementsInterger = false;

        }
        public void Multiply(int valueMultiplied)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Multiply(_arrayInt, valueMultiplied);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, valueMultiplied);
            }

        }

        public void Divide(int valueDivided)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Divide(_arrayInt, valueDivided);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, 1 / (double)valueDivided);
            }
            IsElementsInterger = true;
        }
        public void Divide(double valueDivided)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Multiply(_arrayInt, 1 / valueDivided);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, 1 / valueDivided);
            }
            IsElementsInterger = true;

        }

        // -------------------------------------------------------------------------------------------------

        public void Multiply(double[,] arrayMultiplied)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Multiply(arrayMultiplied, _arrayInt);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, arrayMultiplied);
            }
            IsElementsInterger = false;
        }
        public void Multiply(int[,] arrayMultiplied)
        {
            if (IsElementsInterger)
            {
                _arrayInt = Multiply(_arrayInt, arrayMultiplied);
            }
            else
            {
                _arraydbl = Multiply(_arraydbl, arrayMultiplied);
            }

        }
        public void Multiply(Array2D arrayAdded)
        {
            if (IsElementsInterger)
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arrayInt = Multiply(_arrayInt, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Multiply(_arrayInt, arrayAdded.ValueD);
                    IsElementsInterger = false;
                }
            }
            else
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arraydbl = Multiply(_arraydbl, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Multiply(_arraydbl, arrayAdded.ValueD);
                }
            }

        }

        public void Divide(double[,] arrayDivided)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Divide(_arrayInt, arrayDivided);
            }
            else
            {
                _arraydbl = Divide(_arraydbl, arrayDivided);
            }
            IsElementsInterger = false;
        }
        public void Divide(int[,] arrayDivided)
        {
            if (IsElementsInterger)
            {
                _arraydbl = Divide(_arrayInt, arrayDivided);
            }
            else
            {
                _arraydbl = Divide(_arraydbl, arrayDivided);
            }
            IsElementsInterger = false;
        }
        public void Divide(Array2D arrayAdded)
        {
            if (IsElementsInterger)
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arraydbl = Divide(_arrayInt, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Divide(_arrayInt, arrayAdded.ValueD);
                }
            }
            else
            {
                if (arrayAdded.IsElementsInterger)
                {
                    _arraydbl = Divide(_arraydbl, arrayAdded.ValueI);
                }
                else
                {
                    _arraydbl = Divide(_arraydbl, arrayAdded.ValueD);
                }
            }
            IsElementsInterger = false;
        }

        #endregion

        #region ---   算术操作符 + - * /

        //public static Array2D operator +(Array2D left, double right)
        //{
        //    return left.Add(right);
        //}
        //public static Array2D operator +(Array2D left, int right)
        //{
        //    return left.Add(right);
        //}
        //public static Array2D operator +(Array2D left, Array2D right)
        //{
        //    if (right.IsElementsInterger)
        //    {
        //        return left.Add(right.ValueI);
        //    }
        //    else
        //    {
        //        return left.Add(right.ValueD);
        //    }
        //}

        //public static Array2D operator -(Array2D left, double right)
        //{
        //    return left.Substract(right);
        //}
        //public static Array2D operator -(Array2D left, int right)
        //{
        //    return left.Substract(right);
        //}
        //public static Array2D operator -(Array2D left, Array2D right)
        //{
        //    if (right.IsElementsInterger)
        //    {
        //        return left.Substract(right.ValueI);
        //    }
        //    else
        //    {
        //        return left.Substract(right.ValueD);
        //    }
        //}

        //public static Array2D operator *(Array2D left, double right)
        //{
        //    return left.Multiply(right);
        //}
        //public static Array2D operator *(Array2D left, int right)
        //{
        //    return left.Multiply(right);
        //}
        //public static Array2D operator *(Array2D left, Array2D right)
        //{
        //    if (right.IsElementsInterger)
        //    {
        //        return left.Multiply(right.ValueI);
        //    }
        //    else
        //    {
        //        return left.Multiply(right.ValueD);
        //    }
        //}

        //public static Array2D operator /(Array2D left, double right)
        //{
        //    return left.Divide(right);
        //}
        //public static Array2D operator /(Array2D left, int right)
        //{
        //    return left.Divide(right);
        //}
        //public static Array2D operator /(Array2D left, Array2D right)
        //{
        //    if (right.IsElementsInterger)
        //    {
        //        return left.Divide(right.ValueI);
        //    }
        //    else
        //    {
        //        return left.Divide(right.ValueD);
        //    }
        //}

        #endregion

        #region ---   私有方法 + - * / 

        #region ---   集合与标量的 Add 加法(即减法)

        private double[,] Add(double[,] source, double valueAdded)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    source[r, c] = source[r, c] + valueAdded;
                }
            }
            return source;
        }
        private double[,] Add(int[,] source, double valueAdded)
        {
            _arraydbl = _arraydbl ?? new double[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    _arraydbl[r, c] = source[r, c] + valueAdded;
                }
            }
            return _arraydbl;
        }
        private int[,] Add(int[,] source, int valueAdded)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    source[r, c] = source[r, c] + valueAdded;
                }
            }
            return source;
        }

        // -------------------------------------------------------------------------------------------------

        private double[,] Add(double[,] sourceArray, double[,] arrayAdded)
        {
            if (CheckArray(ref arrayAdded))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] + arrayAdded[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Add(double[,] sourceArray, int[,] arrayAdded)
        {
            if (CheckArray(ref arrayAdded))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] + arrayAdded[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Add(int[,] sourceArray, double[,] arrayAdded)
        {
            return Add(arrayAdded, sourceArray);
        }
        private int[,] Add(int[,] sourceArray, int[,] arrayAdded)
        {
            if (CheckArray(ref arrayAdded))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] + arrayAdded[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }


        private double[,] Substract(double[,] sourceArray, double[,] arraySubstracted)
        {
            if (CheckArray(ref arraySubstracted))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] - arraySubstracted[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private int[,] Substract(int[,] sourceArray, int[,] arraySubstracted)
        {

            if (CheckArray(ref arraySubstracted))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] - arraySubstracted[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Substract(double[,] sourceArray, int[,] arraySubstracted)
        {

            if (CheckArray(ref arraySubstracted))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] - arraySubstracted[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Substract(int[,] sourceArray, double[,] arraySubstracted)
        {
            if (CheckArray(ref arraySubstracted))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        arraySubstracted[r, c] = sourceArray[r, c] - arraySubstracted[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return arraySubstracted;
        }

        #endregion

        #region ---   集合与标量的 Multiply 乘法(即除法)

        private double[,] Multiply(double[,] source, double valueMultiplied)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    source[r, c] = source[r, c] * valueMultiplied;
                }
            }
            return source;
        }
        private int[,] Multiply(int[,] source, int valueMultiplied)
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    source[r, c] = source[r, c] * valueMultiplied;
                }
            }
            return source;
        }
        private double[,] Multiply(int[,] source, double valueMultiplied)
        {
            _arraydbl = _arraydbl ?? new double[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    _arraydbl[r, c] = source[r, c] * valueMultiplied;
                }
            }
            return _arraydbl;
        }

        private double[,] Divide(int[,] source, int valueDivided)
        {
            _arraydbl = _arraydbl ?? new double[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    _arraydbl[r, c] = source[r, c] / valueDivided;
                }
            }
            return _arraydbl;
        }


        // -------------------------------------------------------------------------------------------------

        private double[,] Multiply(double[,] sourceArray, double[,] arrayMultiplied)
        {
            if (CheckArray(ref arrayMultiplied))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] * arrayMultiplied[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Multiply(double[,] sourceArray, int[,] arrayMultiplied)
        {

            if (CheckArray(ref arrayMultiplied))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] * arrayMultiplied[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Multiply(int[,] sourceArray, double[,] arrayMultiplied)
        {
            return Multiply(arrayMultiplied, sourceArray);
        }
        private int[,] Multiply(int[,] sourceArray, int[,] arrayMultiplied)
        {

            if (CheckArray(ref arrayMultiplied))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] * arrayMultiplied[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }

        private double[,] Divide(double[,] sourceArray, double[,] arrayDivided)
        {

            if (CheckArray(ref arrayDivided))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] / arrayDivided[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Divide(int[,] sourceArray, int[,] arrayDivided)
        {
            if (CheckArray(ref arrayDivided))
            {
                _arraydbl = _arraydbl ?? new double[Rows, Columns];
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // ReSharper disable once PossibleLossOfFraction
                        _arraydbl[r, c] = sourceArray[r, c] / arrayDivided[r, c];
                    }
                }
                return _arraydbl;
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
        }
        private double[,] Divide(double[,] sourceArray, int[,] arrayDivided)
        {
            if (CheckArray(ref arrayDivided))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        sourceArray[r, c] = sourceArray[r, c] / arrayDivided[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return sourceArray;
        }
        private double[,] Divide(int[,] sourceArray, double[,] arrayDivided)
        {
            if (CheckArray(ref arrayDivided))
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        arrayDivided[r, c] = sourceArray[r, c] / arrayDivided[r, c];
                    }
                }
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return arrayDivided;
        }

        #endregion

        #endregion

        #endregion

        #region ---   Max & Min

        /// <summary> 求二维数组中每一行或者每一列的最大值 </summary>
        /// <param name="rank"> true 表示输出每一行的最大值，false 表示输出每一列的最大值 </param>
        /// <param name="maxValue">每一行或者每一列的最大值</param>
        /// <returns>每一个最大值所在的位置下标</returns>
        public int[] Max(bool rank, out int[] maxValue)
        {
            int[] maxIndex = null;
            if (rank)  // 输出每一行的最大值，以及每一个最大值在此行中对应的列号
            {
                maxValue = new int[Rows];
                maxIndex = new int[Rows];
                if (IsElementsInterger) // 元素值都是整数
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        int maxV = int.MinValue;
                        int maxI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arrayInt[r, c] > maxV)
                            {
                                maxV = _arrayInt[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[r] = maxV;  // 不用进行数据类型转换
                        maxIndex[r] = maxI;
                    }
                }
                else
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        double maxV = double.MinValue;
                        int maxI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arraydbl[r, c] > maxV)
                            {
                                maxV = _arraydbl[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[r] = (int)maxV;  // 进行数据类型转换
                        maxIndex[r] = maxI;
                    }
                }
            }
            else   // 输出每一列的最大值，以及每一个最大值在此列中对应的行号
            {
                maxIndex = new int[Columns];
                maxValue = new int[Columns];
                if (IsElementsInterger)  // 元素值都是整数
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        int maxV = int.MinValue;
                        int maxI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arrayInt[r, c] > maxV)
                            {
                                maxV = _arrayInt[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[c] = maxV;  // 不用进行数据类型转换
                        maxIndex[c] = maxI;
                    }
                }
                else
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        double maxV = double.MinValue;
                        int maxI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arraydbl[r, c] > maxV)
                            {
                                maxV = _arraydbl[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[c] = (int)maxV;  // 进行数据类型转换
                        maxIndex[c] = maxI;
                    }
                }
            }
            return maxIndex;
        }

        /// <summary> 求二维数组中每一行或者每一列的最大值 </summary>
        /// <param name="rank"> true 表示输出每一行的最大值，false 表示输出每一列的最大值 </param>
        /// <param name="maxValue">每一行或者每一列的最大值</param>
        /// <returns>每一个最大值所在的位置下标</returns>
        public int[] Max(bool rank, out double[] maxValue)
        {
            int[] maxIndex = null;
            if (rank)  // 输出每一行的最大值，以及每一个最大值在此行中对应的列号
            {
                maxValue = new double[Rows];
                maxIndex = new int[Rows];
                if (IsElementsInterger) // 元素值都是整数
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        int maxV = int.MinValue;
                        int maxI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arrayInt[r, c] > maxV)
                            {
                                maxV = _arrayInt[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[r] = (double)maxV;  // 不用进行数据类型转换
                        maxIndex[r] = maxI;
                    }
                }
                else
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        double maxV = double.MinValue;
                        int maxI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arraydbl[r, c] > maxV)
                            {
                                maxV = _arraydbl[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[r] = maxV;  // 不用进行数据类型转换
                        maxIndex[r] = maxI;
                    }
                }
            }
            else   // 输出每一列的最大值，以及每一个最大值在此列中对应的行号
            {
                maxIndex = new int[Columns];
                maxValue = new double[Columns];
                if (IsElementsInterger)  // 元素值都是整数
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        int maxV = int.MinValue;
                        int maxI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arrayInt[r, c] > maxV)
                            {
                                maxV = _arrayInt[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[c] = (double)maxV;  // 进行数据类型转换
                        maxIndex[c] = maxI;
                    }
                }
                else
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        double maxV = double.MinValue;
                        int maxI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arraydbl[r, c] > maxV)
                            {
                                maxV = _arraydbl[r, c];
                                maxI = c;
                            }
                        }
                        maxValue[c] = maxV; // 不用进行数据类型转换
                        maxIndex[c] = maxI;
                    }
                }
            }
            return maxIndex;
        }

        /// <summary> 求二维数组中每一行或者每一列的最小值 </summary>
        /// <param name="rank"> true 表示输出每一行的最小值，false 表示输出每一列的最小值 </param>
        /// <param name="minValue">每一行或者每一列的最小值</param>
        /// <returns>每一个最小值所在的位置下标</returns>
        public int[] Min(bool rank, out int[] minValue)
        {
            int[] minIndex = null;
            if (rank)  // 输出每一行的最大值，以及每一个最大值在此行中对应的列号
            {
                minValue = new int[Rows];
                minIndex = new int[Rows];
                if (IsElementsInterger) // 元素值都是整数
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        int minV = int.MaxValue;
                        int minI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arrayInt[r, c] < minV)
                            {
                                minV = _arrayInt[r, c];
                                minI = c;
                            }
                        }
                        minValue[r] = minV;  // 不用进行数据类型转换
                        minIndex[r] = minI;
                    }
                }
                else
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        double minV = double.MaxValue;
                        int minI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arraydbl[r, c] < minV)
                            {
                                minV = _arraydbl[r, c];
                                minI = c;
                            }
                        }
                        minValue[r] = (int)minV;  // 进行数据类型转换
                        minIndex[r] = minI;
                    }
                }
            }
            else   // 输出每一列的最大值，以及每一个最大值在此列中对应的行号
            {
                minIndex = new int[Columns];
                minValue = new int[Columns];
                if (IsElementsInterger)  // 元素值都是整数
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        int minV = int.MaxValue;
                        int minI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arrayInt[r, c] < minV)
                            {
                                minV = _arrayInt[r, c];
                                minI = c;
                            }
                        }
                        minValue[c] = minV;  // 不用进行数据类型转换
                        minIndex[c] = minI;
                    }
                }
                else
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        double minV = double.MaxValue;
                        int minI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arraydbl[r, c] < minV)
                            {
                                minV = _arraydbl[r, c];
                                minI = c;
                            }
                        }
                        minValue[c] = (int)minV;  // 进行数据类型转换
                        minIndex[c] = minI;
                    }
                }
            }
            return minIndex;
        }

        /// <summary> 求二维数组中每一行或者每一列的最小值 </summary>
        /// <param name="rank"> true 表示输出每一行的最小值，false 表示输出每一列的最小值 </param>
        /// <param name="minValue">每一行或者每一列的最小值</param>
        /// <returns>每一个最小值所在的位置下标</returns>
        public int[] Min(bool rank, out double[] minValue)
        {
            int[] minIndex = null;
            if (rank)  // 输出每一行的最大值，以及每一个最大值在此行中对应的列号
            {
                minValue = new double[Rows];
                minIndex = new int[Rows];
                if (IsElementsInterger) // 元素值都是整数
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        int minV = int.MaxValue;
                        int minI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arrayInt[r, c] < minV)
                            {
                                minV = _arrayInt[r, c];
                                minI = c;
                            }
                        }
                        minValue[r] = (double)minV;  // 进行数据类型转换
                        minIndex[r] = minI;
                    }
                }
                else
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        // 对每一行进行操作
                        double minV = double.MaxValue;
                        int minI = 0;
                        for (int c = 0; c < Columns; c++)
                        {
                            if (_arraydbl[r, c] < minV)
                            {
                                minV = _arraydbl[r, c];
                                minI = c;
                            }
                        }
                        minValue[r] = minV;  // 不用进行数据类型转换
                        minIndex[r] = minI;
                    }
                }
            }
            else   // 输出每一列的最大值，以及每一个最大值在此列中对应的行号
            {
                minIndex = new int[Columns];
                minValue = new double[Columns];
                if (IsElementsInterger)  // 元素值都是整数
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        int minV = int.MaxValue;
                        int minI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arrayInt[r, c] < minV)
                            {
                                minV = _arrayInt[r, c];
                                minI = c;
                            }
                        }
                        minValue[c] = (double)minV;  // 进行数据类型转换
                        minIndex[c] = minI;
                    }
                }
                else
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        // 对每一列进行操作
                        double minV = double.MaxValue;
                        int minI = 0;
                        for (int r = 0; r < Rows; r++)
                        {
                            if (_arraydbl[r, c] < minV)
                            {
                                minV = _arraydbl[r, c];
                                minI = c;
                            }
                        }
                        minValue[c] = minV;  // 不用进行数据类型转换
                        minIndex[c] = minI;
                    }
                }
            }
            return minIndex;
        }

        #endregion

        #region ---   其他公共方法

        /// <summary> 将对象创建一个副本。返回值为一个<see cref="Array2D"/>对象 </summary>
        /// <returns> 返回值为一个<see cref="Array2D"/>对象 </returns>
        public object Clone()
        {
            if (IsElementsInterger)
            {
                int[,] v = _arrayInt.Clone() as int[,];
                return new Array2D(v);
            }
            else
            {
                double[,] v = _arraydbl.Clone() as double[,];
                return new Array2D(v);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Columns > 0)
            {
                // 添加数据
                if (IsElementsInterger)
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        sb.Append(@"( ");
                        for (int c = 0; c < Columns - 1; c++)
                        {
                            sb.Append(_arrayInt[r, c] + ",\t");
                        }
                        sb.AppendLine(_arrayInt[r, Columns - 1] + " )");
                    }
                }
                else
                {
                    for (int r = 0; r < Rows; r++)
                    {
                        sb.Append(@"( ");
                        for (int c = 0; c < Columns - 1; c++)
                        {
                            sb.Append(_arraydbl[r, c] + ",\t");
                        }
                        sb.AppendLine(_arraydbl[r, Columns - 1] + " )");
                    }
                }
            }
            else
            {
                for (int r = 0; r < Rows; r++)
                {
                    sb.AppendLine(@"( )");
                }
            }
            return sb.ToString();
        }

        public string[,] ToString2D()
        {
            string[,] s = new string[Rows, Columns];
            if (IsElementsInterger)
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        s[r, c] = ValueI[r, c].ToString();
                    }
                }
            }
            else
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        s[r, c] = ValueD[r, c].ToString();
                    }
                }
            }
            return s;
        }

        #endregion

        #region ---   多个二维数组的复合计算

        #region ---   浮点数计算

        /// <summary>
        /// 多个数值之间的计算过程
        /// </summary>
        /// <param name="values"></param>
        /// <returns>多个数值之间算术计算的结果值</returns>
        public delegate double NumericalOperation(params double[] values);

        /// <summary>
        /// 多个二维数组的复合计算
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="arrays">集合中二维数组的个数可以多于 <paramref name="operation"/> 中进行数值计算的元素个数；
        /// 但是每一个二维数组的大小必须相同。</param>
        /// <returns></returns>
        public static Array2D CompositeCalculation(NumericalOperation operation, params Array2D[] arrays)
        {
            int rows;
            int columns;
            int arraysCount = arrays.Length;
            double[][,] arraysD = new double[arraysCount][,];
            for (int i = 0; i < arraysCount; i++)
            {
                arraysD[i] = arrays[i].ValueD;
            }

            if (CheckArrays(ref arrays, out rows, out columns))
            {
                double[,] result = CompositeCalculation(operation, rows, columns, arraysCount, arraysD);
                return new Array2D(result);
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return null;
        }

        /// <summary> 核心计算过程 </summary>
        /// <param name="operation">多个数组之间的计算过程</param>
        /// <param name="rows">每个数组的行数</param>
        /// <param name="columns">每个数组的列数</param>
        /// <param name="arraysCount">数组的个数</param>
        /// <param name="arrays">所有要参与计算的数组，每个数组都是一个二维数组double[,]或int[,]，而且数组的大小一定要相同</param>
        /// <returns></returns>
        private static double[,] CompositeCalculation(NumericalOperation operation, int rows, int columns, int arraysCount,
            params double[][,] arrays)
        {
            double[,] res = new double[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    double[] pa = new double[arraysCount];
                    for (int i = 0; i < arraysCount; i++)
                    {
                        pa[i] = arrays[i][r, c];
                    }
                    res[r, c] = operation(pa);
                }
            }
            return res;
        }

        #endregion

        #region ---   整数计算

        /// <summary>
        /// 多个数值之间的计算过程
        /// </summary>
        /// <param name="values"></param>
        /// <returns>多个数值之间算术计算的结果值</returns>
        public delegate int NumericalOperationI(params int[] values);

        /// <summary>
        /// 多个整数型二维数组的复合计算
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="arrays">集合中二维数组的个数可以多于 <paramref name="operation"/> 中进行数值计算的元素个数；
        /// 但是每一个二维数组的大小必须相同。 每个二维数组最好都是整数类型的。</param>
        /// <returns></returns>
        public static Array2D CompositeCalculation(NumericalOperationI operation, params Array2D[] arrays)
        {
            int rows;
            int columns;
            int arraysCount = arrays.Length;
            var arraysI = new int[arraysCount][,];
            for (int i = 0; i < arraysCount; i++)
            {
                arraysI[i] = arrays[i].ValueI;
            }

            if (CheckArrays(ref arrays, out rows, out columns))
            {
                int[,] result = CompositeCalculation(operation, rows, columns, arraysCount, arraysI);
                return new Array2D(result);
            }
            else
            {
                throw new ArgumentException(@"The Dimensions of the two 2D arrays must match.");
            }
            return null;
        }

        /// <summary> 核心计算过程 </summary>
        /// <param name="operation">多个数组之间的计算过程</param>
        /// <param name="rows">每个数组的行数</param>
        /// <param name="columns">每个数组的列数</param>
        /// <param name="arraysCount">数组的个数</param>
        /// <param name="arrays">所有要参与计算的数组，每个数组都是一个二维数组double[,]或int[,]，而且数组的大小一定要相同</param>
        /// <returns></returns>
        private static int[,] CompositeCalculation(NumericalOperationI operation, int rows, int columns, int arraysCount,
            params int[][,] arrays)
        {
            var res = new int[rows, columns];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var pa = new int[arraysCount];
                    for (int i = 0; i < arraysCount; i++)
                    {
                        pa[i] = arrays[i][r, c];
                    }
                    res[r, c] = operation(pa);
                }
            }
            return res;
        }

        #endregion

        /// <summary> 比较多个二维数组的大小是否相同 </summary>
        /// <param name="array2ds"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns>如果所有二维数组的大小都相同，则返回true</returns>
        private static bool CheckArrays(ref Array2D[] array2ds, out int rows, out int columns)
        {
            rows = array2ds[0].Rows;
            columns = array2ds[0].Columns;

            for (int i = 1; i < array2ds.Length; i++)
            {
                if ((array2ds[i].Rows != rows) || (array2ds[i].Columns != columns))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region ---   私有方法



        #endregion

        #region ---   私有方法

        /// <summary> 检查两个二维数组的元素数量是否一致 </summary>
        /// <param name="array2d2">必须为二维数组，如 int[,] </param>
        private bool CheckArray(ref int[,] array2d2)
        {
            if (Value == array2d2)
            {
                // array2d2 = array2d2.Clone() as int[,];
                return true;
            }
            else
            {
                return (Rows == array2d2.GetLength(0)) && (Columns == array2d2.GetLength(1));
            }
        }
        /// <summary> 检查两个二维数组的元素数量是否一致 </summary>
        /// <param name="array2d2">必须为二维数组，如 int[,] </param>
        private bool CheckArray(ref double[,] array2d2)
        {
            if (Value == array2d2)
            {
                // array2d2 = array2d2.Clone() as double[,];
                return true;
            }
            else
            {
                return (Rows == array2d2.GetLength(0)) && (Columns == array2d2.GetLength(1));
            }
        }

        /// <summary> 将整数类型的二维数组转换为双精度的二维数组 </summary>
        private double[,] Int2Double()
        {
            double[,] res = new double[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    res[r, c] = _arrayInt[r, c];
                }
            }
            // 不改变 IsElementsInterger 的属性
            return res;
        }

        /// <summary> 将整数类型的二维数组转换为双精度的二维数组 </summary>
        private int[,] Double2Int()
        {
            int[,] res = new int[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    res[r, c] = (int)_arraydbl[r, c];
                }
            }
            // 不改变 IsElementsInterger 的属性
            return res;
        }

        #endregion
    }
}