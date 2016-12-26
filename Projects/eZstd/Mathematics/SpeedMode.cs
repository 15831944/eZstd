using System;
using System.Collections.Generic;
using System.Linq;
using eZstd.Enumerable;
using eZstd.Geometry;

namespace eZstd.Mathematics
{
    /// <summary>
    /// 根据不同的算法进行密集点位曲线的数据点收缩
    /// </summary>
    public class SpeedMode
    {
        #region ---   Fields

        /// <summary> X轴序列值 </summary>
        private double[] _xValues;

        /// <summary> Y轴序列值 </summary>
        private double[] _yValues;

        #endregion

        #region ---   Properties

        /// <summary> 原始曲线的数据点个数，其值至少为2 </summary>
        public int Length { get; }

        /// <summary> 缩减后的结果 </summary>
        public ShrinkResult Result { get; private set; }

        /// <summary> 缩减出错的报错信息 </summary>
        public string ErrorMessage { get; private set; }

        /// <summary> 在执行Shrink后，剩下来的点在初始数组中的下标 </summary>
        public int[] FilteredIds { get; set; }

        #endregion

        /// <summary> 构造函数 </summary>
        /// <param name="xValues">数组中的元素个数至少为2个</param>
        /// <param name="yValues">数组中的元素个数必须与<paramref name="xValues"/>中的元素相等 </param>
        public SpeedMode(double[] xValues, double[] yValues)
        {
            Length = xValues.Length;

            if (Length < 2 || yValues.Length != Length)
            {
                throw new ArgumentException(
                    "the count of elements in vector X and Y must be the same and larger than 1.");
            }
            _xValues = xValues;
            _yValues = yValues;
            //
            FilteredIds = ArrayConstructor.FromRangeAri(0, Length - 1, 1);
            Result = ShrinkResult.NotStarted;
        }

        #region ---   缩减后的结果提取

        /// <summary> 在执行Shrink后，剩下来的点的X坐标 </summary>
        public double[] GetX()
        {
            return GetResults(_xValues);
        }

        /// <summary> 在执行Shrink后，剩下来的点的Y坐标 </summary>
        public double[] GetY()
        {
            return GetResults(_yValues);
        }

        private double[] GetResults(double[] XorY)
        {
            double[] ans = new double[FilteredIds.Length];
            for (int i = 0; i < FilteredIds.Length; i++)
            {
                ans[i] = XorY[FilteredIds[i]];
            }
            return ans;
        }

        #endregion

        #region ---   Shrink 操作1：根据指定的元素个数进行均一化缩减，而不考虑具体的元素值

        /// <summary> 根据指定的元素个数进行均一化缩减，而不考虑具体的元素值 </summary>
        /// <param name="count">计划要缩减到的目标个数</param>
        /// <remarks>不考虑数组中元素的值，只根据每个元素的下标进行平均化的缩减</remarks>
        public ShrinkResult ShrinkByIdAverage(int count)
        {
            if (CheckCount(count) == ShrinkResult.NotFinished)
            {
                // 此时待计算的count值的范围为 [3,Length)，而 Length 的值大于3，也就 说，要缩减的个数至少为1。
                var ids = new int[count]; //ArrayConstructor.FromRangeAri(0, count - 1, 1);
                ids[0] = 0; // 保证第一个元素被选中
                ids[count - 1] = Length - 1; // 保证最后一个元素被选中
                //
                double unit = (double) (Length - 1)/(count - 1); // 将原始曲线分成 (count - 1) 段
                double node = unit; // 每一次要取的点大致就在这个节点附近
                int newId = 1; // 取到的节点在新数组中所对应的下标

                // 开始为缩减后数组中的每一个元素赋值
                for (int originId = 0; originId < Length; originId++)
                {
                    if (originId >= node)
                    {
                        ids[newId] = originId;
                        newId += 1;
                        node = unit*newId;
                    }
                }
                //
                FilteredIds = ids;
                Result = ShrinkResult.Succeed;
            }
            return Result;
        }

        #endregion

        #region ---   Shrink 操作2：考虑X坐标值的分布，根据所占X轴的区间的分段数来进行缩减，缩减后的数组中的元素不大于指定的段数值segments+1

        /// <summary> 考虑X坐标的分布，根据所占X轴的区间的分段数来进行缩减，缩减后的数组中的元素个数不大于 <paramref name="segments" />+1。 </summary>
        /// <param name="segments"> 将整条曲线所占的X轴区间分割为 segments 段，所以其值至少为1，且无上限。 </param>
        /// <remarks>
        /// 此收缩方法适用于数据对象随X轴增加的变化不大的情况。
        /// 比如基坑监测中每一天的位移。
        /// </remarks>
        public ShrinkResult ShrinkByXAxis(int segments)
        {
            if (segments > 0)
            {
                CurveShape trend = CheckXTrend(_xValues);
                if (trend == CurveShape.XAscend)
                {
                    return ShrinkByAscendXAxis(segments);
                }
                else if (trend == CurveShape.XDescend)
                {
                    // 先将源曲线的X、Y序列反过来
                    var originalX = _xValues;
                    try
                    {
                        // 先将源曲线的X、Y序列反过来
                        _xValues = originalX.Reverse().ToArray();
                        // 
                        Result = ShrinkByAscendXAxis(segments);
                    }
                    finally
                    {
                        // 最后务必将源XY序列复原
                        _xValues = originalX;

                        // 也要将结果再反过来
                        FilteredIds = FilteredIds.Select(r => Length - 1 - r).ToArray();
                    }
                }
                else
                {
                    ErrorMessage = @"X序列非单调。";
                    Result = ShrinkResult.XNonMonotonic;
                }
            }
            else
            {
                ErrorMessage = @"指定的分段数至少为1，且小于 总点数-1。";
                Result = ShrinkResult.NotStarted;
            }
            return Result;
        }

        private ShrinkResult ShrinkByAscendXAxis(int segments)
        {
            List<int> ids = new List<int>(); // 最终筛选出来的子集合
            double min = _xValues[0];
            double max = _xValues.Last();
            double unit = (double) (max - min)/segments; // 将原始曲线分成 (count - 1) 段
            //
            ids.Add(0); // 保证第一个元素被选中
            double leftXValue = min, rightXValue = min + unit;

            // 每一个小分段区域中的左边边界点的下标   
            int leftIndex = 1;
            int rightIndex = 0;

            // 开始为缩减后数组中的每一个元素赋值
            for (int originId = 1; originId < Length; originId++)
            {
                double v = _xValues[originId];
                if (v >= leftXValue && v <= rightXValue)
                {
                    rightIndex = originId;
                }
                else // 此时的 v 肯定比 rightXValu 大，有可能也比 rightXValu + n*unit 大。
                {
                    // 将前一个分段中选出来的点添加到总集合中
                    if (rightIndex >= leftIndex)
                    {
                        ids.Add(PickOneByXAxis(leftIndex, rightIndex));
                    }
                    // 进入下一个分段
                    leftIndex = originId;
                    rightIndex = originId;
                    // originId -= 1; // 先将刚才跳过的那个一点加入到下一个分段中

                    leftXValue = min + unit*(Math.Ceiling((v - min)/unit) - 1);
                        // v 左边离它最近的一个节点值，而且，当 v 的值恰好为下一段的终点时，要确保其被下一段捕获。
                    rightXValue = leftXValue + unit;
                }
            }
            // 保证最后一个元素被选中
            if (ids.Last() != Length - 1)
            {
                ids.Add(Length - 1);
            }
            //
            FilteredIds = ids.ToArray();
            Result = ShrinkResult.Succeed;
            return Result;
        }

        /// <summary> 从递增的局部序列中选出一个点来 </summary>
        /// <param name="startIndex">局部序列的起始下标</param>
        /// <param name="endIndex">局部序列的终止下标</param>
        /// <returns>选出来的那一个点在整个曲线集合中的下标</returns>
        private int PickOneByXAxis(int startIndex, int endIndex)
        {
            int[] indice = ArrayConstructor.FromRangeAri(startIndex, endIndex, 1);
            bool ascend = true;
            // 如果 indice 中
            int count = indice.Length;

            switch (count)
            {
                case 0:
                {
                    throw new ArgumentException(@"集合中必须有至少一个元素。");
                }
                case 1:
                {
                    return indice[0];
                }
                case 2:
                {
                    if (ascend)
                    {
                        return indice[1];
                    }
                    else
                    {
                        return indice[0];
                    }
                }
                default: // 此时集合中的元素个数至少为 3 个
                {
                    bool considerDistance = false; // 是否以每两个相邻点之间的距离作为参考指标
                    if (!considerDistance)
                    {
                        if (ascend)
                        {
                            // 直接返回子集合中的最后一个
                            return indice[count - 1];
                        }
                        else
                        {
                            // 直接返回子集合中的第一个
                            return indice[0];
                        }
                    }
                    else
                    {
                        var distances = new List<double>();
                        XY lastPoint = new XY(_xValues[indice[0]], _yValues[indice[0]]);
                        XY nextPoint;

                        // 提取出每两个点之间的距离
                        for (int i = 1; i < count; i++)
                        {
                            nextPoint = new XY(_xValues[indice[i]], _yValues[indice[i]]);
                            distances.Add(nextPoint.DistanceTo(lastPoint));
                            //
                            lastPoint = nextPoint;
                        }

                        // 找出距离最小的两个点的下标{ minLeft,minLeft+1 }
                        double minDist = double.MaxValue;
                        int minLeft = indice[0]; // 距离最小的那一段的两个点中靠左边的那个点在整条曲线中的绝对下标值
                        for (int i = 0; i < distances.Count; i++)
                        {
                            if (distances[i] <= minDist)
                            {
                                minDist = distances[i];
                                minLeft = i + indice[0];
                            }
                        }

                        // 取距离最小的两个点{ minLeft,minLeft+1 }中的一个
                        if (ascend) // 总思路为：在无法比较时，都趋向于选择向右边的点
                        {
                            // 基本原则为： 如果为最前面两个，则取第2个；如果为最后两个，则取最后一个；如果为中间的某一小段，则取向两边各扩展一个点后比较哪个更密。
                            if (minLeft == indice[0])
                            {
                                return indice[1];
                            }
                            else if (minLeft == indice[count - 2])
                            {
                                return indice[count - 1];
                            }
                            else
                            {
                                // indice 中至少有4个点，而 minLeft 为第2个到第 count - 2 个。
                                // 向两边各扩展一个点后比较哪个更密。
                                if (distances[minLeft - indice[0] - 1] >= distances[minLeft - indice[0] + 1])
                                {
                                    // 取靠右边的那个
                                    return minLeft + 1;
                                }
                                else
                                {
                                    // 取靠左边的那个
                                    return minLeft;
                                }
                            }
                        }
                        else // 整条曲线的X序列为降序时，总思路为：在无法比较时，都趋向于选择向左边的点
                        {
                            // 基本原则为： 如果为最前面两个，则取第1个；如果为最后两个，则取倒数第二个；如果为中间的某一小段，则取向两边各扩展一个点后比较哪个更密。
                            if (minLeft == indice[0])
                            {
                                return indice[0];
                            }
                            else if (minLeft == indice[count - 2])
                            {
                                return indice[count - 2];
                            }
                            else
                            {
                                // indice 中至少有4个点，而 minLeft 为第2个到第 count - 2 个。
                                // 向两边各扩展一个点后比较哪个更密。
                                if (distances[minLeft - indice[0] - 1] <= distances[minLeft - indice[0] + 1])
                                {
                                    // 取靠左边的那个
                                    return minLeft;
                                }
                                else
                                {
                                    // 取靠右边的那个
                                    return minLeft + 1;
                                }
                            }
                        }
                    }
                } // 此时集合中的元素个数至少为 3 个
            }
        }

        #endregion

        #region ---   Shrink 操作3：考虑X、Y的整体曲线形态，从曲线的斜率等角度来进行缩减

        #endregion

        #region ---   私有函数

        /// <summary> 对新的点数进行检测，以排除一些特殊的情况。如果返回的值为true，则还要继续缩减，此时的count值的范围为 [3,<see cref="SpeedMode.Length"/>)。 </summary>
        /// <param name="count"></param>
        /// <returns>如果为 false，则表示不用再继续讨论如何缩减了。
        /// 如果为true，则还需要继续计算，此时的count值的范围为 [3,<see cref="SpeedMode.Length"/>) </returns>
        private ShrinkResult CheckCount(int count)
        {
            if (count <= 0)
            {
                ErrorMessage = @"新的点数必须大于0";
                Result = ShrinkResult.NewCountOutOfRange;
                // throw new ArgumentOutOfRangeException(nameof(count), @"the new Length must  be larger than zero.");
                // return false;
            }
            else if (count > Length)
            {
                ErrorMessage = @"新的点数必须小于原点数";
                Result = ShrinkResult.NewCountOutOfRange;
                // throw new ArgumentOutOfRangeException(nameof(count), @"the new Length must not be larger than the length of the original vector.");
                // return false;
            }
            else if (count == 1)
            {
                Result = ShrinkResult.Succeed;
                FilteredIds = new int[] {0};
                // return false;
            }
            else if (count == 2)
            {
                Result = ShrinkResult.Succeed;
                FilteredIds = new int[] {0, Length - 1};
                // return false;
            }
            else if (count == Length)
            {
                Result = ShrinkResult.Succeed;
                FilteredIds = ArrayConstructor.FromRangeAri(0, Length - 1, 1);
                // return false;
            }
            else
            {
                // 此时待计算的count值的范围为 [3,Length)，而 Length 的值大于3。
                Result = ShrinkResult.NotFinished;
                // return true;
            }
            return Result;
        }

        /// <summary> 检查X序列的值的单调性 </summary>
        /// <param name="xValues"></param>
        private static CurveShape CheckXTrend(double[] xValues)
        {
            CurveShape cs = CurveShape.XAscend;
            bool ascend = (xValues[1].CompareTo(xValues[0]) > 0);
            if (ascend)
            {
                cs = CurveShape.XAscend;
                for (int i = 2; i < xValues.Length; i++)
                {
                    // 当相邻的两个个值相等时，认为不妨碍其原来的单调性
                    if (xValues[i] - xValues[i - 1] < 0)
                    {
                        return CurveShape.XNonMonotonic;
                    }
                }
            }
            else
            {
                cs = CurveShape.XDescend;
                for (int i = 2; i < xValues.Length; i++)
                {
                    if (xValues[i] - xValues[i - 1] > 0)
                    {
                        return CurveShape.XNonMonotonic;
                    }
                }
            }
            return cs;
        }

        #endregion

        /// <summary> 进行缩减计算后的结果 </summary>
        public enum ShrinkResult
        {
            /// <summary> 还未开始进行过任何缩减计算 </summary>
            NotStarted = 0,

            /// <summary> 计算还未完成，此时待计算的count值的范围为 [3,<see cref="SpeedMode.Length"/>)，而<see cref="SpeedMode.Length"/>的值大于3。 </summary>
            NotFinished,

            /// <summary> 计算完成 </summary>
            Succeed,

            /// <summary> 缩减计算失败，但是不给出具体的出错原因 </summary>
            Failed,

            /// <summary> 指定的缩减值在[1,<see cref="SpeedMode.Length"/>]之外 </summary>
            NewCountOutOfRange,

            /// <summary> X 序列的值非单调 </summary>
            XNonMonotonic,
        }

        /// <summary> 曲线的大致形状 </summary>
        [Flags]
        public enum CurveShape
        {
            /// <summary> X轴的值不是单调递增或单调递减 </summary>
            XNonMonotonic = 0,

            /// <summary> X轴的值依次增加，也包括相邻的值相等的情况 </summary>
            XAscend = 1,

            /// <summary> X轴的值依次减小，也包括相邻的值相等的情况 </summary>
            XDescend = 2,
        }
    }
}