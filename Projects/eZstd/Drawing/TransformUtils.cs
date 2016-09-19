using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZstd.Drawing
{
   public static class TransformUtils
    {

        /// <summary>
        /// 在单一轴上进行模型与屏幕之间的变换
        /// </summary>
        /// <param name="s1">屏幕上的端点1</param>
        /// <param name="s2">屏幕上的端点2</param>
        /// <param name="m1">s1 在模型中对应的端点1</param>
        /// <param name="m2">s2 在模型中对应的端点2</param>
        /// <param name="mp">模型中的任意一个点</param>
        /// <param name="sp">模型中的点 mp 在屏幕中对应的位置点</param>
        /// <param name="ratio">从模型到屏幕的缩放比例</param>
        /// <remarks>
        /// 应用举例：
        /// // 转换坐标系：对 Y 方向进行变换
        ///   float sp; float ratio;
        ///   GetAxisTransform(
        ///   s1: _daShaft.Top, s2: _daShaft.Top + _daShaft.Height,
        ///   m1: elevations.Max, m2: elevations.Min,
        ///   mp: 0f, sp: out sp, ratio: out ratio);
        /// 
        /// // 将得到的结果应用到仿射矩阵中
        ///   Matrix mY = new Matrix();
        ///   mY.Translate(_daShaft.Left, sp);
        ///   mY.Scale(1, ratio);
        ///   gr.Transform = mY;  // 将变换应用到绘图面板中
        /// </remarks>
        public static void GetAxisTransform(float s1, float s2, float m1, float m2, float mp,
            out float sp, out float ratio)
        {
            ratio = (s1 - s2) / (m1 - m2);
            sp = (mp - m1) * ratio + s1;
        }

    }
}
