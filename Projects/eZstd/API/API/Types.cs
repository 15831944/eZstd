using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace eZstd.API
{
#region   ---  Windows消息
	
	/// <summary>
	/// The MSG structure contains message information from a thread's message queue.
	/// Alternative Managed API: Use the <see cref="System.Windows.Forms.Message"/> or System.Windows.Interop.MSG struct.
	/// </summary>
	/// <remarks></remarks>
	[StructLayout(LayoutKind.Sequential)]public struct MSG
	{
		public IntPtr hwnd;
		public int message;
		public IntPtr wParam;
		public IntPtr lParam;
		public int time;
		public Point pt;
	}
	
	/// <summary>
	/// This is a different data structure from <see cref="System.Windows.Forms.Message"/>
	/// </summary>
	/// <remarks></remarks>
	[StructLayout(LayoutKind.Sequential)]public struct NativeMessage
	{
		public IntPtr handle;
		public UInt32 msg;
		public IntPtr wParam;
		public IntPtr lParam;
		public UInt32 time;
		public System.Drawing.Point pt;
	}
	
#endregion
	
	/// <summary>
	/// 这是windows广泛采用的一种数据结构，通常作为参数传递给许多api函数。
	/// RECT结构表示一个矩形区域，left和top字段描叙了矩形第一个角（通常是左上角），
	/// right和bottom字段描叙了矩形的第二个角（通常是右下角）。这两个位置决定了矩形的大小与位置。
	/// 这些字段采用的单位及坐标系统取决于当前的有效缩放比例、准备表示的对象以及准备调用的api函数。
	/// 并不要求bottom字段的绝对值大于top字段，而且也可以为负数。
	/// </summary>
	/// <remarks>由right及bottom字段指定的点通常不是矩形的一部分；矩形对象描叙的是个空矩形（其中不包含像素）；
	/// RECT结构要求按引用传递给windows函数，不要试图使用ByVal </remarks>
	[StructLayout(LayoutKind.Sequential)]public struct RECT
		{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;

	    public override string ToString()
	    {
            return string.Format("( Top = {0}, Left = {1}, Bottom = {2}, Right = {3} )", Top, Left, Bottom, Right);
	    }
		}
	
#region   ---  钩子Hook
	
	[StructLayout(LayoutKind.Sequential)]public struct KBDLLHOOKSTRUCT
	{
		public UInt32 vkCode;
		public UInt32 scanCode;
		public KBDLLHOOKSTRUCTFlags flags;
		public UInt32 time;
		public UIntPtr dwExtraInfo;
	}
	
	[StructLayout(LayoutKind.Sequential)]public struct MSLLHOOKSTRUCT
	{
		public Point pt;
		public int mouseData;
		public MSLLHOOKSTRUCTFlags flags;
		public int time;
		public UIntPtr dwExtraInfo;
	}
	
#endregion
	
}
