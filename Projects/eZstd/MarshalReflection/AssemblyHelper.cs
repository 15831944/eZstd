using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eZstd.MarshalReflection
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

        /// <summary> 用新对象中的成员值去替换原对象中的对应成员值。
        /// 其作用是在不改变原变量的内存地址的情况下，对实例对象中的属性值进行修改。
        /// 比如对于引用类型的变量：A=B,当后期修改B的值为C时，如果用B=C，则A的值不会修改，此时只能将C中的成员的值赋值到B的对应成员中，此时A中的对应成员才会同步修改。 </summary>
        /// <param name="originalObject">可以是<paramref name="newObject"/>的派生类 </param>
        /// <param name="newObject">可以是<paramref name="originalObject"/>的基类</param>
        /// <remarks>所以新对象不能是原对象的派生类。</remarks>
        /// <returns>成功则返回 true，反之返回 false </returns>
        public static bool OverrideFields(object originalObject, object newObject)
        {
            try
            {
                FieldInfo[] oldFields = originalObject.GetType().GetFields(BindingFlags.Public);
                FieldInfo[] newFields = newObject.GetType().GetFields(BindingFlags.Public);
                foreach (FieldInfo oldField in oldFields)
                {
                    var f = newFields.FirstOrDefault(r => r == oldField);
                    if (f != null)
                    {
                        f.SetValue(originalObject, f.GetValue(newObject));
                    }
                };
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
