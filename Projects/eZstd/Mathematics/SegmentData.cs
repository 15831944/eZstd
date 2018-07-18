using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Mathematics
{
    /// <summary>
    /// 包含起点，终点，以及此分段中的某些数据。比如一段边坡中的防护面积
    /// </summary>
    /// <typeparam name="TSeg">起止点定位数据的类型</typeparam>
    /// <typeparam name="TValue">分段数据的类型</typeparam>
    public class SegmentData<TSeg, TValue>
    {
        public TSeg Start { get; }
        public TSeg End { get; }
        /// <summary> 此分段中所对应的数据 </summary>
        public TValue Data { get; private set; }

        public SegmentData(TSeg startMileage, TSeg endMileage, TValue slopeArea)
        {
            Start = startMileage;
            End = endMileage;
            Data = slopeArea;
        }

        public override string ToString()
        {
            return $"{Start},\t{End},\t{Data}";
        }


        /// <summary> 将 边坡横断面集合转换为二维数组，以用来写入 Excel </summary>
        /// <param name="segments"> <seealso cref="TValue"/>类必须为标量 </param>
        /// <returns></returns>
        public static object[,] ConvertToArr(IList<SegmentData<TSeg, TValue>> segments)
        {
            var res = new object[segments.Count(), 3];
            var r = 0;
            foreach (var seg in segments)
            {
                res[r, 0] = seg.Start;
                res[r, 1] = seg.End;
                res[r, 2] = seg.Data;
                r += 1;
            }
            return res;
        }
    }
}
