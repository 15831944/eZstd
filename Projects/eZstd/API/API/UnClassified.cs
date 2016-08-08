using System.Runtime.InteropServices;

namespace eZstd.API
{
	
	public class UnClassified
	{
		
		/// <summary>
		/// 将消息信息传送给指定的窗口过程的函数。
		/// </summary>
		/// <param name="lpPrevWndFunc">指向前一个窗口过程的指针。如果该值是通过调用GetWindowLong函数，
		/// 并将该函数中的nlndex参数设为GWL_WNDPROC或DWL_DLGPROC而得到的，那么它实际上要么是窗口或者对话框的地址，要么就是代表该地址的句柄。</param>
		/// <param name="hWnd">指向接收消息的窗口过程的句柄。</param>
		/// <param name="Msg">指定消息类型。</param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		/// <remarks>使用函数CallWindowsProc可进行窗口子分类。通常来说，同一类的所有窗口共享一个窗口过程。子类是一个窗口或者相同类的一套窗口，在其消息被传送到该类的窗口过程之前，这些消息是由另一个窗口过程进行解释和处理的。
		///SetWindowLong函数通过改变与特定窗口相关的窗口过程，使系统调用新的窗口过程来创建子类，
		/// 新的窗口过程替换了以前的窗口过程。应用程序必须通过调用CallWindowsProc来将新窗口过程没有处理的任何消息传送到以前的窗口过程中，
		/// 这样就允许应用程序创建一系列窗口过程。  It is possible to execute an array of bytes. lpPrevWndFunc is equal to the address of the byte array.</remarks>
		[DllImport("user32",EntryPoint="CallWindowProcA", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
		public static extern int CallWindowProc(int lpPrevWndFunc, int hWnd, int Msg, int wParam, int lParam);
		
	}
	
}
