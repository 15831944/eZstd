using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace eZstd.API
{
    public class IO
    {
        /// <summary>
        /// 以不同的显示方式执行指定的文件（异步非阻塞模式，直接返回结果，不等待执行的程序结束）
        /// 如果要等待某执行的程序结束，可以参考<seealso cref="Process"/>类中的“WaitForExit()”的相关帮助，zengfy亲测有效。
        /// </summary>
        /// <param name="hwnd">A handle to the parent window used for displaying a UI or error messages. 
        /// This value can be NULL if the operation is not associated with a window.
        /// </param>
        /// <param name="lpOperation"><see cref="ShellExecuteOperation"/>中的某一个常数。
        /// A pointer to a null-terminated string, referred to in this case as a verb, that specifies the action to be performed. 
        /// The set of available verbs depends on the particular file or folder.
        /// </param>
        /// <param name="lpFile">用于指定要打开的文件名、要执行的可执行程序文件名，或要浏览的文件夹名。
        /// A pointer to a null-terminated string that specifies the file or object on which to execute the specified verb. 
        /// To specify a Shell namespace object, pass the fully qualified parse name. 
        /// Note that not all verbs are supported on all objects. 
        /// For example, not all document types support the "print" verb. If a relative path is used for the lpDirectory parameter do not use a relative path for lpFile.
        /// </param>
        /// <param name="lpParameters"> 如果 <paramref name="lpFile"/> 指向一个可执行程序，则此参数为对应的程序指定输入参数。多个参数之间以空格进行分隔。 
        /// If lpFile specifies an executable file, this parameter is a pointer to a null-terminated string that specifies the parameters to be passed to the application. 
        /// The format of this string is determined by the verb that is to be invoked. If lpFile specifies a document file, lpParameters should be NULL.
        /// </param>
        /// <param name="lpDirectory">指定默认工作文件夹。 A pointer to a null-terminated string that specifies the default (working) directory for the action. If this value is NULL, the current working directory is used. 
        /// If a relative path is provided at lpFile, do not use a relative path for lpDirectory.
        /// </param>
        /// <param name="nShowCmd">The flags that specify how an application is to be displayed when it is opened. 
        /// If lpFile specifies a document file, the flag is simply passed to the associated application. It is up to the application to decide how to handle it. 
        /// These values are defined in Winuser.h.
        /// </param>
        /// <returns> 若ShellExecute函数调用成功，则返回值为被执行程序的实例句柄，其值大于32。若返回值小于32，则表示出现错误。  </returns>
        /// <remarks> 
        /// 举例：
        /// 1. 要以隐藏的方式打开某文件，则可能使用语句：ShellExecute(IntPtr.Zero, "open", FileFullName, "", "", ShowCommands.SW_HIDE);
        /// 2.  
        /// </remarks>
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
              IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);

    }
}
