using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eZstd.Enumerable
{
    /// <summary> 通过不同的方式构造一维或多维数组 </summary>
    public static class ArrayConstructor
    {
        #region --- FromRange:指定[start,end]

        /// <summary>
        /// 返回均匀分布的等差序列（Arithmetical series），数组中的元素值不会跳出[start,end]闭区间。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count">返回的数组中的元素个数，其值至少为1</param>
        /// <returns></returns>
        public static double[] FromRangeAri(double start, double end, uint count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("The count of elements in an array must be larger than 0.");
            }
            else if (count == 1)
            {
                return new double[1] { start };
            }
            else if (count == 2)
            {
                return new double[2] { start, end };
            }
            else
            {
                double[] arr = new double[count];
                double step = (end - start) / (count - 1);
                double v = start;
                for (int i = 0; i < count; i++)
                {
                    arr[i] = v;
                    v += step;
                }
                // 如果最后一个元素的值略大于 end 的值，则作出调整。
                if (arr[count - 1] > end)
                {
                    arr[count - 1] = end;
                }
                return arr;
            }
        }

        /// <summary>
        /// 返回均匀分布的等差序列（Arithmetical series），数组中的元素值不会跳出[start,end]闭区间。
        /// </summary>
        /// <param name="start">返回的数组中必定包含这个值</param>
        /// <param name="end">返回的数组中的最后一个元素的值必定小于等于此参数的值</param>
        /// <param name="step"> step的值不能太小，避免导致数组元素过多</param>
        /// <returns>返回的数组中的最后一个元素的值必定小于等于<paramref name="end" />的值</returns>
        public static double[] FromRangeAri(double start, double end, double step)
        {
            // 首先确定数组中一共有多少个元素
            int count;
            // 首先控制step的值不能太小，避免导致数组元素过多
            if ((end - start) * step < 0 || (end - start) / step + 1 >= int.MaxValue)
            {
                throw new ArgumentException("step must be large enough to avoid IndexOutOfRange Exception.");
            }
            else
            {
                count = (int)Math.Floor((end - start) / step) + 1;
            }
            double[] arr = new double[count];
            int index = 0;
            if (step > 0)
            {
                for (double v = start; v <= end; v += step)
                {
                    arr[index] = v;
                    index += 1;
                }
            }
            else
            {
                for (double v = start; v >= end; v += step)
                {
                    arr[index] = v;
                    index += 1;
                }
            }
            return arr;
        }

        /// <summary>
        /// 返回均匀分布的“整数”等差序列（Arithmetical series），数组中的元素值不会跳出[start,end]闭区间。
        /// </summary>
        /// <param name="start">返回的数组中必定包含这个值</param>
        /// <param name="end">返回的数组中的最后一个元素的值必定小于等于此参数的值</param>
        /// <param name="step"> step的值不能太小，避免导致数组元素过多</param>
        /// <returns>返回的数组中的最后一个元素的值必定小于等于<paramref name="end" />的值</returns>
        public static int[] FromRangeAri(int start, int end, int step)
        {
            // 首先确定数组中一共有多少个元素
            int count;
            // 首先控制step的值不能太小，避免导致数组元素过多，其次保证
            if ((end - start) * step < 0 || (end - start) / step + 1 >= int.MaxValue)
            {
                throw new ArgumentException("step must be large enough to avoid IndexOutOfRange Exception.");
            }
            else
            {
                count = (int)Math.Floor((double)(end - start) / step) + 1;
            }
            int[] arr = new int[count];
            int index = 0;
            if (step > 0)
            {
                for (int v = start; v <= end; v += step)
                {
                    arr[index] = v;
                    index += 1;
                }
            }
            else
            {
                for (int v = start; v >= end; v += step)
                {
                    arr[index] = v;
                    index += 1;
                }
            }
            return arr;
        }

        #endregion

        #region --- FromStep:仅指定start

        /// <summary>
        /// 返回均匀分布的等差序列（Arithmetical series）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="step"> </param>
        /// <param name="count">数组中的元素个数</param>
        /// <returns></returns>
        public static double[] FromStepAri(double start, uint count, double step)
        {
            double[] arr = new double[count];
            double v = start;
            for (int index = 0; index < count; index++)
            {
                arr[index] = v;
                v = start + step;
            }
            return arr;
        }

        #endregion

        /// <summary>
        /// 返回等比序列（Geometric series），数组中的元素值不会跳出[start,end]闭区间。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"> </param>
        /// <param name="bias">每一个元素值是上一个元素值的 bias 倍</param>
        /// <returns></returns>
        /// <remarks>等比数列的通项公式是：An=A1*q^(n-1)</remarks>
        public static double[] FromStepGeo(double start, uint count, double bias)
        {
            double[] arr = new double[count];
            double v = start;
            for (int index = 0; index < count; index++)
            {
                arr[index] = v;
                v = start * bias;
            }
            return arr;
        }


        #region --- 大数组中提取小数组

        /// <summary> 从二维数组中提取一行向量 </summary>
        /// <param name="source"></param>
        /// <param name="rowIndex">第一行的下标为0</param>
        /// <typeparam name="T"></typeparam>
        public static T[] GetRow<T>(T[,] source, int rowIndex)
        {
            int colsCount = source.GetLength(1);
            T[] result = new T[colsCount];
            for (int i = 0; i < colsCount; i++)
            {
                result[i] = source[rowIndex, i];
            }
            return result;
        }

        /// <summary> 从二维数组中提取一列向量 </summary>
        /// <param name="source"></param>
        /// <param name="colIndex">第一列的下标为0</param>
        /// <typeparam name="T"></typeparam>
        public static T[] GetColumn<T>(T[,] source, int colIndex)
        {
            int rowsCount = source.GetLength(0);
            T[] result = new T[rowsCount];
            for (int i = 0; i < rowsCount; i++)
            {
                result[i] = source[i, colIndex];
            }
            return result;
        }

        #endregion


        #region --- 从List等集合中提取二维数组

        /// <summary>
        /// 从多个一维行向量的List集合中构造出对应的二维数组
        /// </summary>
        /// <param name="numList"></param>
        /// <returns> 返回的二维数组的列数与 List 中的第一个行向量的列数相同。</returns>
        public static double[,] FromList2D(List<double[]> numList)
        {
            int columnCount = numList[0].Length;
            double[,] arr = new double[numList.Count, columnCount];
            for (int r = 0; r < numList.Count; r++)
            {
                for (int c = 0; c < columnCount; c++)
                {
                    arr[r, c] = numList[r][c];
                }
            }
            return arr;
        }
        #endregion

        public enum DistributeMode
        {
            /// <summary>
            /// 线性均匀分布
            /// </summary>
            Linear = 0,
            /// <summary>
            /// 指数分布
            /// </summary>
            Exponential = 1,
        }
    }
}
