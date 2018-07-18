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
        /// <param name="listOfRows">第一个元素（一个行向量）中的元素个数必须最小。
        /// 如果某行的元素个数大于第一行的元素个数，则此行的多余元素不考虑；
        /// 如果某行的元素个数小于第一行的元素个数，则此行的空缺元素以默认值填充 </param>
        /// <returns> 返回的二维数组的列数与 List 中的第一个行向量的列数相同。</returns>
        public static T[,] FromList2D<T>(IList<T[]> listOfRows)
        {
            var firstColCount = listOfRows[0].Length;
            var arr2D = new T[listOfRows.Count, firstColCount];
            int rowIndex = 0;
            foreach (var row in listOfRows)
            {
                var cCount = Math.Min(firstColCount, row.Length);
                for (var c = 0; c < cCount; c++)
                {
                    arr2D[rowIndex, c] = row[c];
                }
                rowIndex += 1;
            }
            return arr2D;
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

        #region --- 二维数组的插值

        /// <summary> 将源二维数组中插入多个行或者多个列 </summary>
        /// <typeparam name="Tsrc">源数组的元素类型</typeparam>
        /// <typeparam name="Tins">要插入的数组的元素类型</typeparam>
        /// <typeparam name="Tout">插入后的新数组的元素类型</typeparam>
        /// <param name="sourceArray">源二维数组</param>
        /// <param name="intoRow">true 表示将新数据按行插入源数组中</param>
        /// <param name="insertedVectors">要插入的每一行或者每一列</param>
        /// <param name="indices"> indices 集合中的元素个数必须与 insertedVectors 的列数或行数相同，
        /// 向量中的每一个元素代表要插入到源数组的那一行或那一列之后，
        /// 如果原数组中有n行或n列，则要插入的行或列的指定下标范围为(-2,n)的开区间，超出范围则会报错。如果要插入为数组的首行或者首列，输入下标的区间为(-2,-1]；
        /// 如果 indices 集合中有相同的值，表示多个新列插入同一列的后面，这些多个新列会按输入的顺序排列。比如{2,2}表示将两列按顺序插入到源数组的第3列后面。
        /// </param>
        /// <returns></returns>
        /// <remarks>举例：
        ///    var src = new int[2, 3] {{1, 2, 3}, {4, 5, 6}};
        ///    var ins1 = new[] { 7, 8 };
        ///    var ins2 = new[] { 9, 10 };
        ///    var ss = src.InsertVector<int, int, int>(false, new IList<int>[] { ins1, ins2 }, new[] { -1, 2 });
        /// </remarks>
        public static Tout[,] InsertVector<Tsrc, Tins, Tout>(this Tsrc[,] sourceArray, bool intoRow,
            IList<IList<Tins>> insertedVectors, float[] indices)
        {
            var inserts = new InsertedVector<Tins>[insertedVectors.Count];
            for (int i = 0; i < inserts.Length; i++)
            {
                inserts[i] = new InsertedVector<Tins>(insertedVectors[i], indices[i]);
            }
            //
            int originalCount;
            var rowCount = sourceArray.GetLength(0);
            var colCount = sourceArray.GetLength(1);
            if (intoRow)
            {
                originalCount = rowCount;
                rowCount += insertedVectors.Count;
            }
            else
            {
                originalCount = colCount;
                colCount += insertedVectors.Count;
            }
            // 计算源数组与要插入的数组在新数组中的对应下标
            Dictionary<int, int> newId_srcId;
            GetIndicesAndVectors(originalCount, ref inserts, out newId_srcId);
            //
            var res = new Tout[rowCount, colCount];
            // 最终数组的赋值
            if (intoRow)
            {
                // 要插入的数据
                foreach (var ins in inserts)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        res[ins.RealIndex, c] = ins.Vector[c] as dynamic;
                    }
                }
                // 源数组中的数据
                var oldIds = newId_srcId.Keys.ToArray();
                var newIds = newId_srcId.Values.ToArray();
                for (int i = 0; i < oldIds.Length; i++)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        res[newIds[i], c] = sourceArray[oldIds[i], c] as dynamic;
                    }
                }
            }
            else // 将新数据按列插入源数组中，行数保持不变
            {
                // 要插入的数据
                foreach (var ins in inserts)
                {
                    for (int r = 0; r < rowCount; r++)
                    {
                        res[r, ins.RealIndex] = ins.Vector[r] as dynamic;
                    }
                }
                // 源数组中的数据
                var oldIds = newId_srcId.Keys.ToArray();
                var newIds = newId_srcId.Values.ToArray();
                for (int i = 0; i < oldIds.Length; i++)
                {
                    for (int r = 0; r < rowCount; r++)
                    {
                        res[r, newIds[i]] = sourceArray[r, oldIds[i]] as dynamic;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalCount">原数组中有多少行</param>
        /// <param name="insertedVectors">要插入的行的下标</param>
        /// <param name="newId_srcId">源数组某行所对应的新下标</param>
        /// <remarks>indices 集合中的元素个数必须与 insertedVectors 的列数或行数相同，每一个向量要插入到源数组的那一行或哪一列。
        /// 如果 indices 集合中有相同的值，表示多个新列插入同一列的后面，这些多个新列会按输入的顺序排列。比如{2,2}表示将两列按顺序插入到源数组的第3列后面。
        /// </remarks>
        private static void GetIndicesAndVectors<T>(
            int originalCount,
            ref InsertedVector<T>[] insertedVectors,
            out Dictionary<int, int> newId_srcId)
        {
            // Array.Sort(insertedVectors, comparison: InsertedVector<T>.Comparison);
            Array.Sort(insertedVectors, comparison: InsertedVector<T>.InsertedVectorComparison);
            // 确定要插入的行的最终排序
            int idAdded = 0;
            var insertedNewIds = new int[insertedVectors.Length];
            foreach (var ins in insertedVectors)
            {
                idAdded += 1;
                ins.RealIndex = (int)(ins.WantedIndex) + idAdded;
                insertedNewIds[idAdded - 1] = ins.RealIndex;
            }
            // 确定原数组中的行所对应的新的排序
            int oldId = 0;
            newId_srcId = new Dictionary<int, int>();
            for (int newId = 0; newId < originalCount + insertedVectors.Length; newId++)
            {
                if (!insertedNewIds.Contains(newId))
                {
                    newId_srcId.Add(oldId, newId);
                    oldId += 1;
                }
            }
        }

        private class InsertedVector<T>
        {
            public readonly IList<T> Vector;
            /// <summary> 要插入的行的下标 </summary>
            public readonly float WantedIndex;
            /// <summary> 要插入的某行所对应的真正的下标 </summary>
            public int RealIndex;

            public InsertedVector(IList<T> vector, float wantedIndex)
            {
                Vector = vector;
                WantedIndex = wantedIndex;
            }

            public override string ToString()
            {
                return $"WantedIndex: {WantedIndex},RealIndex: {RealIndex},Vector: {Vector[0]}";
            }

            public static int InsertedVectorComparison(InsertedVector<T> obj1, InsertedVector<T> obj2)
            {
                return obj1.WantedIndex.CompareTo(obj2.WantedIndex);
            }

        }

        #endregion
    }
}
