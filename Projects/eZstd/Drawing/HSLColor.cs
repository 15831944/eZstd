using System;
using System.Drawing;

namespace eZstd.Drawing
{
    /// <summary>
    /// RGB 与 HSL 颜色的相互转换。
    /// 转换算法参考： http://en.wikipedia.org/wiki/HSL_color_space。
    /// </summary>
    public class HSLColor
    {
        #region ---   Properties

        /// <summary> 透明度 Alpha  </summary>
        private int _alpha = 255;

        /// <summary> 色相，其值的范围为[0,360]   </summary>
        private int _hue = 0;
        /// <summary> 色相，其值的范围为[0,360]  </summary>
        public int Hue
        {
            get { return _hue; }
            set
            {
                if (value < 0)
                {
                    _hue = value + 360;
                }
                else if (_hue > 360)
                {
                    _hue = value % 360;
                }
                else
                {
                    _hue = value;
                }
            }
        }

        /// <summary> 饱和度，其值的范围为[0,1]   </summary>
        private double _saturation = 1d;
        /// <summary> 饱和度，其值的范围为[0,1]  </summary>
        public double Saturation
        {
            get { return _saturation; }
            set
            {
                if (_saturation < 0)
                {
                    _saturation = 0;
                }
                else
                {
                    _saturation = Math.Min(value, 1d);
                }
            }
        }

        /// <summary> 亮度或明度，也译为 Lightness/Brightness，其值的范围为[0,1]   </summary>
        private double _luminosity = 1d;
        /// <summary> 亮度或明度，也译为 Lightness/Brightness，其值的范围为[0,1]   </summary>
        public double Luminosity
        {
            get { return _luminosity; }
            set
            {
                if (_luminosity < 0)
                {
                    _luminosity = 0;
                }
                else
                {
                    _luminosity = Math.Min(value, 1d);
                }
            }
        }

        /// <summary> 对应的 RGB 颜色 </summary>
        public Color Color
        {
            get { return ToRGB(); }
            set { FromRGB(value); }
        }
        #endregion

        #region ---   构造函数

        public HSLColor()
        {
        }

        /// <summary>
        /// 用一个RGB颜色构造HSLColor。
        /// </summary>
        /// <param name="color"></param>
        public HSLColor(Color color)
        {
            _alpha = color.A;
            FromRGB(color);
        }

        /// <summary>
        /// 用色彩、饱和度、亮度 构造HSLColor。并检查每一个分量值的范围是否超出允许值。
        /// </summary>
        /// <param name="hue">色彩。</param>
        /// <param name="saturation">饱和度。</param>
        /// <param name="lightness">亮度。</param>
        public HSLColor(int hue, double saturation, double lightness)
        {
            Hue = hue;
            Saturation = saturation;
            Luminosity = lightness;
        }

        /// <summary>
        /// 用色彩、饱和度、亮度 构造HSLColor。但不检查每一个分量值的范围是否超出允许值。
        /// </summary>
        /// <param name="hue">色彩。</param>
        /// <param name="saturation">饱和度。</param>
        /// <param name="lightness">亮度。</param>
        /// <param name="checkRange">不论此参数的值如何，都不会检查每一个分量值的范围是否超出允许值，如要检查，请用另一个构造函数</param>
        public HSLColor(int hue, double saturation, double lightness, bool checkRange)
        {
            _hue = hue;
            _saturation = saturation;
            _luminosity = lightness;
        }

        #endregion

        #region ---   HSL 与 RGB 的相互转换

        /// <summary> 将 RGB 颜色转换为 HSL 颜色 </summary>
        /// <param name="color"></param>
        private void FromRGB(Color color)
        {
            double r = ((double)color.R) / 255;
            double g = ((double)color.G) / 255;
            double b = ((double)color.B) / 255;

            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            _luminosity = (max + min) / 2;  // 亮度

            double distance = max - min;
            if (distance == 0)
            {
                _hue = 0;
                _saturation = 0;
            }
            else
            {
                double hueTmp;
                _saturation = (_luminosity < 0.5) ? (distance / (max + min)) : (distance / ((2 - max) - min));   // 饱和度
                double tempR = (((max - r) / 6) + (distance / 2)) / distance;
                double tempG = (((max - g) / 6) + (distance / 2)) / distance;
                double tempB = (((max - b) / 6) + (distance / 2)) / distance;
                if (r == max)
                {
                    hueTmp = tempB - tempG;
                }
                else if (g == max)
                {
                    hueTmp = (0.33333333333333331 + tempR) - tempB;
                }
                else
                {
                    hueTmp = (0.66666666666666663 + tempG) - tempR;
                }
                if (hueTmp < 0)
                {
                    hueTmp += 1;
                }
                if (hueTmp > 1)
                {
                    hueTmp -= 1;
                }
                _hue = (int)(hueTmp * 360);   // 色相
            }
        }

        /// <summary> 将 HSL 颜色转换为 RGB 颜色 </summary>
        private Color ToRGB()
        {
            byte r;
            byte g;
            byte b;

            if (_saturation == 0)
            {
                r = (byte)(_luminosity * 255);
                g = r;
                b = r;
            }
            else
            {
                double vH = ((double)_hue) / 360;
                double v2 = (_luminosity < 0.5)
                        ? (_luminosity * (1 + _saturation))
                        : ((_luminosity + _saturation) - (_luminosity * _saturation));

                double v1 = (2 * _luminosity) - v2;

                r = (byte)(255 * HueToRGB(v1, v2, vH + 0.33333333333333331));
                g = (byte)(255 * HueToRGB(v1, v2, vH));
                b = (byte)(255 * HueToRGB(v1, v2, vH - 0.33333333333333331));
            }
            return Color.FromArgb(r, g, b);
        }

        private double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
            {
                vH += 1;
            }

            if (vH > 1)
            {
                vH -= 1;
            }

            if ((6 * vH) < 1)
            {
                return v1 + (((v2 - v1) * 6) * vH);
            }

            if ((2 * vH) < 1)
            {
                return v2;
            }

            if ((3 * vH) < 2)
            {
                return v1 + (((v2 - v1) * (0.66666666666666663 - vH)) * 6);
            }
            return v1;
        }
        #endregion

        public static bool operator ==(HSLColor left, HSLColor right)
        {
            return (left.Hue == right.Hue &&
                    left.Luminosity == right.Luminosity &&
                    left.Saturation == right.Saturation);
        }

        public static bool operator !=(HSLColor left, HSLColor right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null && !(obj is HSLColor))
            {
                return false;
            }

            HSLColor color = (HSLColor)obj;
            return this == color;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("HSL({0:f2}, {1:f2}, {2:f2})", Hue, Saturation, Luminosity);
        }
    }
}