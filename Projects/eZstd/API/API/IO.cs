using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace eZstd.API
{
    public class IO
    {
        /// <summary>
        /// 以不同的显示方式执行指定的文件。
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpOperation"></param>
        /// <param name="lpFile"></param>
        /// <param name="lpParameters"></param>
        /// <param name="lpDirectory"></param>
        /// <param name="nShowCmd"></param>
        /// <returns> 若ShellExecute函数调用成功，则返回值为被执行程序的实例句柄。若返回值小于32，则表示出现错误。 </returns>
        /// <remarks> 比如要以隐藏的方式打开某文件，则可能使用语句：ShellExecute(IntPtr.Zero, "open", FileFullName, "", "", ShowCommands.SW_HIDE); </remarks>
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
              IntPtr hwnd, string lpOperation, string lpFile,
              string lpParameters, string lpDirectory, ShowCommands nShowCmd);

    }
}
