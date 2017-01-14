using System;
using System.Runtime.InteropServices;

namespace eZstd.API
{
	public class Hook
	{
		
#region SetWindowsHookEx 安装钩子
		
		/// <summary>
		/// Represents the method called when a hook catches a monitored event.
		/// </summary>
		public delegate int  HookProc(int code, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll", SetLastError = true)]
        public static  extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, UInt32 dwThreadId);
		
#endregion
		
		/// <summary>
		/// 在安装钩子后要记得卸载钩子
		/// </summary>
		/// <param name="hhk"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[DllImport("user32.dll", SetLastError = true)][return: MarshalAs(UnmanagedType.Bool)]
		public static  extern bool UnhookWindowsHookEx(IntPtr hhk);
		
#region   --- 传递到下一个Hook
		
		/// <summary>
		/// 将hook信息传递到链表中下一个hook处理过程
		/// </summary>
		/// <param name="hhk">注：hhk is ignored, so you can use IntPtr.Zero</param>
		/// <param name="nCode"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[DllImport("user32.dll")]public static  extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr 
		wParam, IntPtr lParam);
		
		
		/// <summary>
		/// CallNextHookEx 鼠标 将hook信息传递到链表中下一个hook处理过程。
		/// overload for use with LowLevelMouseProc
		/// </summary>
		/// <param name="hhk"></param>
		/// <param name="nCode"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[DllImport("user32.dll")]public static  extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, 
		WindowsMessages wParam, 
		[In()]MSLLHOOKSTRUCT lParam);
		
		/// <summary>
		/// CallNextHookEx 键盘 将hook信息传递到链表中下一个hook处理过程。
		/// overload for use with LowLevelKeyboardProc
		/// </summary>
		/// <param name="hhk"></param>
		/// <param name="nCode"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		[DllImport("user32.dll")]public static  extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, 
		WindowsMessages wParam, 
		[In()]KBDLLHOOKSTRUCT lParam);
		
#endregion
		
	}
	
}
