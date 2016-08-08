using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;

namespace eZstd.Drawing
{
	
	/// <summary>
	/// 对颜色进行处理
	/// </summary>
	/// <remarks></remarks>
	public class ColorUtils
	{
		
		/// <summary>
		/// RGB 颜色插值
		/// </summary>
		/// <param name="c1">颜色1所对应的数值</param>
		/// <param name="c2">颜色2所对应的数值</param>
		/// <param name="x">x的值位于[0,1]的闭区间内，当其值为0时，它代表颜色c1，当其值为1时，它代表颜色c2</param>
		/// <returns>插值后的RGB颜色所对应的数值</returns>
		/// <remarks>返回的整数值与RGB参量的换算关系为：Color属性值=R + 256*G + 256^2*B</remarks>
		public static double ColorInterp(dynamic c1, dynamic c2, dynamic x)
		{
			
			byte R0 = 0;
			byte G0 = 0;
			byte B0 = 0;
			byte R1 = 0;
			byte G1 = 0;
			byte B1 = 0;
			
			// c0 的 各分量
			R0 = System.Convert.ToByte(System.Convert.ToInt32(c1) % 256);
			G0 = System.Convert.ToByte(c1 / 256 % 256);
			B0 = System.Convert.ToByte(c1 / 256 / 256);
			
			// c1 的 各分量
			R1 = System.Convert.ToByte(System.Convert.ToInt32(c2) % 256);
			G1 = System.Convert.ToByte(c2 / 256 % 256);
			B1 = System.Convert.ToByte(c2 / 256 / 256);
			
			//
			byte R = 0;
			byte G = 0;
			byte B = 0;
			R = R0 + (R1 - R0) * x;
			G = G0 + (G1 - G0) * x;
			B = B0 + (B1 - B0) * x;
			//
			return R + 256.0D * G + 256.0D * 256.0D * B;
		}
		
	}
}
