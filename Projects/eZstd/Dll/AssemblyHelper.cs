using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eZstd.Dll
{

    public static class AssemblyHelper
    {

        /// <summary>
        /// 将一个程序集插件进行动态加载，并可以在不关闭主程序的情况下进行调试。
        /// 类似于Autodesk Revit中的插件 AddinManager 所起的作用。
        /// </summary>
        /// <param name="dllPath">dll或者exe文件的绝对路径</param>
        /// <param name="classFullName">实例对象所属的类的完全限定名</param>
        /// <param name="constructorArgs">构造函数的输入实参</param>
        /// <returns></returns>
        /// <remarks>将此程序集作为动态插件加载到某程序中去后（比如将某Excel或者CAD的插件功能加载到对应的程序进程中），
        /// 可以在不关闭对应的主程序的情况下，对插件dll进行调试，然后在主程序中重新调用调试更新后的插件功能。</remarks>
        public static object DynamicDebugClass(string dllPath, string classFullName, object[] constructorArgs)
        {
            //先将插件拷贝到内存缓冲。一般情况下，当加载的文件大小大于2^32 byte (即4.2 GB），就会出现OutOfMemoryException，在实际测试中的极限值为630MB。
            byte[] buff = File.ReadAllBytes(dllPath);

            //不能直接通过LoadFrom或者LoadFile，而必须先将插件拷贝到内存，然后再从内存中Load
            Assembly asm = Assembly.Load(buff);

            // Type tp = asm.GetType(dynamicDebug);

            // 构造一个实例，在此构造函数中即可以设计对应的插件功能了
            object instance = asm.CreateInstance(
                typeName: classFullName,
                ignoreCase: false,
                bindingAttr: BindingFlags.CreateInstance,
                binder: null,
                args: constructorArgs,
                culture: CultureInfo.CurrentCulture,
                activationAttributes: null);

            return instance;
        }
    }
}
