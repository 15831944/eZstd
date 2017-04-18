using System;
using System.Drawing;

namespace eZstd.Drawing
{
    /// <summary>
    /// 对颜色进行处理
    /// </summary>
    /// <remarks></remarks>
    public static class ColorUtils
    {
        #region ---   RGB颜色所对应的数值转换


        /// <summary>
        /// 将一个RGB颜色转换为对应的数值。
        /// 颜色的数值与RGB参量的换算关系为：Color属性值=R + 256*G + 256^2*B
        /// </summary>
        /// <param name="c"></param>
        /// <returns>返回值为[0,16777215]之间的任意整数</returns>
        public static double ColorToValue(Color c)
        {
            return c.R + 256.0D * c.G + 256.0D * 256.0D * c.B;
        }

        /// <summary>
        /// 将一个[0,16777215]之间的数值转换为对应的RGB颜色。
        /// 颜色的数值与RGB参量的换算关系为：Color属性值=R + 256*G + 256^2*B
        /// </summary>
        /// <param name="value">value的范围为[0,16777215]之间的任意数值</param>
        /// <returns></returns>
        public static Color ValueToColor(double value)
        {
            var r = Convert.ToInt32(value % 256);
            var g = Convert.ToInt32(value / 256 % 256);
            var b = Convert.ToInt32(value / 256 / 256);
            return Color.FromArgb(r, g, b);
        }

        #endregion

        #region ---   RGB颜色插值与扩展

        /// <summary>
        /// RGB 颜色插值
        /// </summary>
        /// <param name="c1">颜色1所对应的数值，其值为[0,16777215]之间的任意整数</param>
        /// <param name="c2">颜色2所对应的数值，其值为[0,16777215]之间的任意整数</param>
        /// <param name="x">x的值位于[0,1]的闭区间内，当其值为0时，它代表颜色c1，当其值为1时，它代表颜色c2</param>
        /// <returns>插值后的RGB颜色所对应的数值</returns>
        /// <remarks>颜色的数值与RGB参量的换算关系为：Color属性值=R + 256*G + 256^2*B</remarks>
        public static double ColorInterp(double c1, double c2, double x)
        {
            Color color1 = ValueToColor(c1);
            Color color2 = ValueToColor(c2);
            Color c3 = ColorInterp(color1, color2, x);
            return ColorToValue(c3);
        }

        /// <summary>
        /// RGB 颜色插值
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="x">x的值位于[0,1]的闭区间内，当其值为0时，它代表颜色c1，当其值为1时，它代表颜色c2</param>
        /// <returns>插值后的RGB颜色</returns>
        public static Color ColorInterp(Color c1, Color c2, double x)
        {
            // 插值
            var r = (byte)(c1.R + (c2.R - c1.R) * x);
            var g = (byte)(c1.G + (c2.G - c1.G) * x);
            var b = (byte)(c1.B + (c2.B - c1.B) * x);
            //
            return Color.FromArgb(r, g, b);
        }

        /// <summary> 根据经典的有限元插值色系作为基准来进行颜色的插值扩展 </summary>
        /// <param name="colorsCount">目标颜色集的个数</param>
        /// <returns>数组中元素的个数为colorsCount </returns>
        public static Color[] ClassicalExpand(int colorsCount)
        {
            if (colorsCount <= 0) throw new ArgumentException("进行颜色插值的目标个数至少为1");

            // 定义6种经典的过渡颜色用于插值
            Color[] baseColors = new Color[6]
            {
                Color.FromArgb(255, 0,  0   ),
                Color.FromArgb(255 ,192,0   ),
                Color.FromArgb(255 ,255,0   ),
                Color.FromArgb(0,   176,80  ),
                Color.FromArgb(0,   112,192 ),
                Color.FromArgb(0,   32, 96  ),
            };

            return ColorExpand(baseColors, colorsCount);
        }

        /// <summary> 根据多个基准色平均插值出指定数量的渐变颜色集 </summary>
        /// <param name="baseColors">用来进行插值的基准色</param>
        /// <param name="colorsCount">目标颜色集的数量</param>
        /// <returns>数组中元素的个数为colorsCount</returns>
        public static Color[] ColorExpand(Color[] baseColors, int colorsCount)
        {
            if (colorsCount <= 0) return new Color[0]; //new ArgumentException("进行颜色插值的目标个数至少为1");

            if (baseColors == null || baseColors.Length == 0) throw new ArgumentException("进行颜色插值的基准色至少要有一个");

            //
            int baseCount = baseColors.Length;  // 基准色的数量
            Color[] colors = new Color[colorsCount];  // 最后插值完成后的颜色集
            if (baseCount == 1)
            {
                for (int i = 0; i < colorsCount; i++)
                {
                    colors[i] = baseColors[0];
                }
            }
            else  // 基准色不只一个
            {
                // 开始插值
                if (colorsCount == 1)
                {
                    colors[0] = baseColors[0];
                    return colors;
                }
                // 当要插值的颜色多于1个时，即 colorsCount 至少为 2
                double interval = 1D / (colorsCount - 1);  // 全局中每个目标色之间的间隔
                for (int i = 0; i < colorsCount; i++)
                {
                    double interpRatio = interval * i; // interpRatio 的值处于[0,1]之间，0代表第一个基准色，1代表最后一个基准色

                    // 由 interpRatio 的值确定要在哪两个基准色之间进行插值
                    if (interpRatio == 0)
                    {
                        colors[i] = baseColors[0];
                        continue;
                    }
                    else if (interpRatio == 1)
                    {
                        colors[i] = baseColors[baseCount - 1];
                        continue;
                    }
                    else if (interpRatio > 0 && interpRatio < 1)
                    {
                        double baseInterval = 1D / (baseCount - 1);

                        int baseColor1 = (int)Math.Ceiling(interpRatio / baseInterval); // 第一个基准色
                        int baseColor2 = baseColor1 + 1;  // 第二个基准色
                                                          // 换算新的插值比例
                        var x0 = baseInterval * (baseColor1 - 1);
                        var x1 = baseInterval * (baseColor1);
                        var localInterpRatio = (interpRatio - x0) / (x1 - x0);

                        // 在两个基准颜色之间进行插值
                        colors[i] = ColorInterp(baseColors[baseColor1 - 1], baseColors[baseColor2 - 1], localInterpRatio);
                    }
                }
            }
            return colors;
        }

        #endregion
    }
}