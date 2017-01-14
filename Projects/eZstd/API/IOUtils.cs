using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZstd.API;

namespace eZstd.API
{
    public class IOUtils
    {
        /// <summary>
        /// 通过生成.bat文件的方式来删除指定的文件，包括某正在执行的.exe或.dll文件。
        /// </summary>
        /// <param name="executableFile">要删除的文件的绝对路径。
        /// 如果要删除当前正在运行的程序文件，可以将 System.Reflection.Assembly.GetExecutingAssembly().Location 作为参数。</param>
        /// <returns> 返回生成的那个.bat文件的绝对路径。但是当此函数执行完成时，此.bat文件应该已经被删除了。 </returns>
        /// <remarks>此函数是通过先在同文件夹下生成一个.bat命令文件，然后再通过.bat中的命令来删除.exe文件的。 </remarks>
        public static string KillFileByBat(string executableFile)
        {
            FileInfo assemblyFile = new FileInfo(executableFile);
            var directory = assemblyFile.Directory;
            string batFileName = "DeleteMyself-" + Guid.NewGuid().ToString() + ".bat";
            string batFilePath = Path.Combine(directory.FullName, batFileName);

            // 创建一个bat 文件，用来先删除此运行的 .exe， 再删除 .bat 文件本身
            FileStream fs = new FileStream(batFilePath, mode: FileMode.CreateNew);

            // 在一般情况下，.bat文件中不能识别中文字符，此时此文件所在的文件夹路径可以有中文，但是此文件本身的名称不能有中文存在。
            // 比如“F:\软件学习\Runner.exe”是可行的，但是“F:\跑步机.exe”是不可行的。
            // 所以下面通过  Encoding.ASCII 来解决了这一问题。
            StreamWriter sw = new StreamWriter(fs, encoding: Encoding.ASCII);
            sw.WriteLine("echo off");
            sw.WriteLine("del " + assemblyFile.Name);
            sw.WriteLine("del " + batFileName);
            sw.Close();
            fs.Close();

            // 执行 .bat 文件，以删除当前运行的.exe。
            API.IO.ShellExecute(IntPtr.Zero, "open", batFileName, "", "", ShowCommands.SW_HIDE);

            return batFilePath;
        }

    }
}
