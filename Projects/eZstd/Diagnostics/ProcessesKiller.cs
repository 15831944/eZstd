using System;
using System.Diagnostics;
using System.Globalization;
using System.Management;

namespace eZstd.Diagnostics
{
    /// <summary>
    /// 关闭进程或者关闭进程树
    /// </summary>
    public static class ProcessesKiller
    {
        public static void FindAndKillProcess(int id)
        {
            killProcess(id);
        }

        public static void FindAndKillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if ((clsProcess.ProcessName.StartsWith(name, StringComparison.CurrentCulture))
                    || (clsProcess.MainWindowTitle.StartsWith(name, StringComparison.CurrentCulture)))
                    killProcess(clsProcess.Id);
            }
        }

        private static bool killProcess(int pid)
        {
            Process[] procs = Process.GetProcesses();
            for (int i = 0; i < procs.Length; i++)
            {
                if (getParentProcess(procs[i].Id) == pid)
                    killProcess(procs[i].Id);
            }

            try
            {
                Process myProc = Process.GetProcessById(pid);
                myProc.Kill();
            }
            // process already quited  
            catch (ArgumentException)
            {
                ;
            }

            return true;
        }

        private static int getParentProcess(int Id)
        {
            int parentPid = 0;
            using (ManagementObject mo = new ManagementObject(path: "win32_process.handle='" + Id.ToString(CultureInfo.InvariantCulture) + "'"))
            {
                try
                {
                    mo.Get();
                }
                catch (ManagementException)
                {
                    return -1;
                }
                parentPid = Convert.ToInt32(mo["ParentProcessId"], CultureInfo.InvariantCulture);
            }
            return parentPid;
        }

        #region ---   结束进程树

        ///<summary>根据父进程id，杀死与之相关的进程树</summary>
        /// <param name="pid">父进程id</param>
        public static void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                Console.WriteLine(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                /* process already exited */
            }
        }

        #endregion
    }
}
