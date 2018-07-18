using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace eZstd.MarshalReflection
{
    public class RegistryHandler
    {
        /// <summary>
        /// 用于32位程序访问64位注册表
        /// </summary>
        /// <param name="hive">根级别的名称</param>
        /// <param name="view">注册表视图，比如要访问64位注册表，请输入<seealso cref="RegistryView.Registry64"/></param>
        /// <returns>值</returns>
        /// <remarks> 
        /// 1. 微软为了让32位程序不做任何修改就能运行在64的操作系统上，添加了一个十分重要的WOW64子系统来实现这个功能，WOW64是Windows-32-on-Windows-64的简称，从总体上来说，WOW64是一套基于用户模式的动态链接库，它可以把32位应用程序的发出的命令翻译成64位系统可以接受的格式，即：WOW 层处理诸如在 32 位和 64 位模式之间切换处理器以及模拟 32 位系统的事务。
        /// 2. 为了防止注册表键冲突，64位机器注册表信息分成了两个部分。一部分是专门给64位系统（即：64位程序）访问的，另一部分是专门给32位系统（即：32位程序）访问的，放在Wow6432Node下面。（Wow6432Node这个节 点存在于HKEY_LOCAL_MACHINE和HKEY_CURRENT_USER下面）
        /// 3. 目标平台是32位的程序运行在64位的系统上，去访问部分注册表的时候系统自动重定向到win32node节点对应的项去了。但是做过安装程序开发人员可能遇到过“需要去掉重定向”的问题，即直接访问64位程序的注册表。
        /// </remarks>
        public static RegistryKey GetRegistryKeyWithRegView(RegistryHive hive, RegistryView? view)
        {
            SafeRegistryHandle handle = new SafeRegistryHandle(GetHiveHandle(hive), true);//获得根节点的安全句柄
            if (view == null)
            {
                return RegistryKey.FromHandle(handle);
                // 
                // return RegistryKey.FromHandle(handle, RegistryView.Registry64);
            }
            else
            {
                return RegistryKey.FromHandle(handle, view.Value);
            }
           
        }

        private static IntPtr GetHiveHandle(RegistryHive hive)
        {
            IntPtr preexistingHandle = IntPtr.Zero;

            IntPtr HKEY_CLASSES_ROOT = new IntPtr(-2147483648);
            IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);
            IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);
            IntPtr HKEY_USERS = new IntPtr(-2147483645);
            IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(-2147483644);
            IntPtr HKEY_CURRENT_CONFIG = new IntPtr(-2147483643);
            IntPtr HKEY_DYN_DATA = new IntPtr(-2147483642);
            switch (hive)
            {
                case RegistryHive.ClassesRoot: preexistingHandle = HKEY_CLASSES_ROOT; break;
                case RegistryHive.CurrentUser: preexistingHandle = HKEY_CURRENT_USER; break;
                case RegistryHive.LocalMachine: preexistingHandle = HKEY_LOCAL_MACHINE; break;
                case RegistryHive.Users: preexistingHandle = HKEY_USERS; break;
                case RegistryHive.PerformanceData: preexistingHandle = HKEY_PERFORMANCE_DATA; break;
                case RegistryHive.CurrentConfig: preexistingHandle = HKEY_CURRENT_CONFIG; break;
                case RegistryHive.DynData: preexistingHandle = HKEY_DYN_DATA; break;
            }
            return preexistingHandle;
        }

        #region ---   案例

        /// <summary>
        /// 用于32位程序访问64位注册表
        /// </summary>
        /// <param name="hive">根级别的名称</param>
        /// <param name="keyName">不包括根级别的名称</param>
        /// <param name="valueName">项名称</param>
        /// <param name="view">注册表视图</param>
        /// <returns>值</returns>
        private static object GetValueWithRegView(RegistryHive hive, string keyName, string valueName, RegistryView view)
        {
            SafeRegistryHandle handle = new SafeRegistryHandle(GetHiveHandle(hive), true);//获得根节点的安全句柄

            RegistryKey subkey = RegistryKey.FromHandle(handle, view).OpenSubKey(keyName);//获得要访问的键

            RegistryKey key = RegistryKey.FromHandle(subkey.Handle, view);//根据键的句柄和视图获得要访问的键
            return key.GetValue(valueName);//获得键下指定项的值
        }

        /// <summary>
        /// 用于32位的程序设置64位的注册表
        /// </summary>
        /// <param name="hive">根级别的名称</param>
        /// <param name="keyName">不包括根级别的名称</param>
        /// <param name="valueName">项名称</param>
        /// <param name="value">值</param>
        /// <param name="kind">值类型</param>
        /// <param name="view">注册表视图</param>
        private static void SetValueWithRegView(RegistryHive hive, string keyName, string valueName, object value, RegistryValueKind kind, RegistryView view)
        {
            SafeRegistryHandle handle = new SafeRegistryHandle(GetHiveHandle(hive), true);

            RegistryKey subkey = RegistryKey.FromHandle(handle, view).OpenSubKey(keyName, true);//需要写的权限,这里的true是关键。0227更新

            RegistryKey key = RegistryKey.FromHandle(subkey.Handle, view);

            key.SetValue(valueName, value, kind);
        }

        #endregion
    }
}
