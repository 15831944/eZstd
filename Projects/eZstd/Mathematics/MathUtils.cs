using System;
using System.Collections.Generic;
using System.Linq;

namespace eZstd.Mathematics
{
    /// <summary>
    /// 一些基础的算法
    /// </summary>
    public class MathUtils
    {
        #region 角度相关

        /// <summary> 根据二维矢量返回其相对于正X轴沿逆时针的角度值，其值的范围为[0,2*π) </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public static double GetAngleR(double dx, double dy)
        {
            var baseAng = Math.Atan(dy / dx); // -π/2 ≤ θ ≤ π/2
            if (dx < 0)
            {
                baseAng += Math.PI;
            }
            else if (dx >= 0 && dy < 0)
            {
                baseAng += 2 * Math.PI;
            }
            return baseAng;
        }

        /// <summary> 根据二维矢量返回其相对于正X轴沿逆时针的角度值，其值的范围为[0,360度) </summary>
        public static double GetAngleD(double dx, double dy)
        {
            return GetAngleR(dx, dy) / 2 / Math.PI * 360; ;
        }

        #endregion

    }
}