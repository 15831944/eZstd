using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using win = eZstd.API.Windows;


namespace eZstd.APIUtils
{

    /// <summary>
    /// 在windows API中，已经有大量的函数可以直接调用，但是在某些特殊的情况下，还是要通过一些复杂的操作流程才能对窗口进行处理。
    /// 此类对于这些复杂操作进行进一步封装。
    /// </summary>
    public static class Windows
    {
        #region --- 搜索父窗口（或控件）中指定类名与窗口文本的子窗口

        /// <summary>
        /// 搜索父窗口（或控件）中指定类名与窗口文本的子窗口。如果没有匹配到指定类名与文本的子窗口，则返回 new IntPtr(0)
        /// </summary>
        /// <param name="hwndParent">父窗口（或控件）的句柄</param>
        /// <param name="className">要匹配的窗口类名，如果不需要匹配，则输入null</param>
        /// <param name="windowText">要匹配的窗口文本，如果不需要匹配，则输入null</param>
        /// <returns>如果没有匹配到指定类名与文本的子窗口，则返回 new IntPtr(0)</returns>
        public static IntPtr FindChildWindow(IntPtr hwndParent, string className, string windowText)
        {
            ChildWindowFinder cwf = new ChildWindowFinder();
            return cwf.FindChild(hwndParent, className, windowText);
        }
        
        /// <summary>
        /// 搜索父窗口（或控件）中指定类名与窗口文本的子窗口
        /// </summary>
        private class ChildWindowFinder
        {
            private IntPtr _childHandle;

            /// <summary>
            /// 搜索父窗口（或控件）中指定类名与窗口文本的子窗口
            /// </summary>
            /// <param name="hwndParent">父窗口（或控件）的句柄 </param>
            /// <param name="className">要匹配的窗口类名，如果不需要匹配，则输入null</param>
            /// <param name="windowText">要匹配的窗口文本，如果不需要匹配，则输入null</param>
            /// <returns> 如果没有匹配到指定类名与文本的子窗口，则返回 new IntPtr(0) </returns>
            public IntPtr FindChild(IntPtr hwndParent, string className, string windowText)
            {
                _childHandle = new IntPtr();
                //
                this._matchingClassName = className;
                this._matchingWindowText = windowText;
                //
                win.EnumWindowsProc proc = FindGraphicalViewProc;
                win.EnumChildWindows(hwndParent, proc, new IntPtr(0));
                return _childHandle;
            }

            private string _matchingClassName;
            private string _matchingWindowText;

            /// <summary>
            /// 对 Revit 主窗口下的所有子窗口进行遍历，以找到 绘图View的那个子窗口
            /// </summary>
            /// <param name="hwnd"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            private bool FindGraphicalViewProc(IntPtr hwnd, IntPtr lParam)
            {
                // 匹配类名
                if (_matchingClassName != null)
                {
                    StringBuilder className = new StringBuilder(255);
                    win.GetClassName(hwnd, className, className.Capacity);

                    if (!string.Equals(className.ToString(), _matchingClassName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                // 匹配窗口文本
                if (_matchingWindowText != null)
                {
                    StringBuilder windowText = new StringBuilder(255);
                    win.GetWindowText(hwnd, windowText, windowText.Capacity);

                    if (!string.Equals(windowText.ToString(), _matchingWindowText, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                _childHandle = hwnd;
                return false;
            }

        }

        #endregion
    }
}
