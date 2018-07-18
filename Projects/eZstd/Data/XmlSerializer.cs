using System;
using System.IO;
using System.Text;
using System.Xml;

namespace eZstd.Data
{
    /// <summary> XML 文件的序列化与反序列化 </summary>
    public static class XmlSerializer
    {
        #region --- 从 XML 文件中导入

        /// <summary>
        ///     在 xml 文档的根节点中去匹配指定的基类型的下一级派生类型，或者匹配指定的基类型本身。
        ///     匹配原则为类型自身的名称，所以不要为根节点所对应的类定义中添加 XmlRoot(elementName: "Myclass") 这样的Attribute。
        /// </summary>
        /// <param name="xmlFilePath">xml 文件的绝对路径</param>
        /// <param name="baseForRoot">根节点对应的类型要与此基类型的下一级派生类进行匹配</param>
        /// <param name="baseIncluded">根节点所匹配的对象中是否包含基类本身。所以如果指定的基类型为抽象类，则此参数的值要赋为 false。</param>
        /// <returns>如果未匹配到，则返回<paramref name="baseForRoot" /> 对象</returns>
        public static Type GetXmlRootType(string xmlFilePath, Type baseForRoot, bool baseIncluded)
        {
            var rootType = baseForRoot;
            string rootTypeName = null;
            using (var xr = new XmlTextReader(xmlFilePath))
            {
                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        rootTypeName = xr.Name;
                        break;
                    }
                }
            }
            if (rootTypeName != null)
            {
                var ass = baseForRoot.Assembly;
                if (baseIncluded)
                {
                    foreach (var tp in ass.GetTypes())
                    {
                        if (((tp == baseForRoot) || (tp.BaseType == baseForRoot)) && (tp.Name == rootTypeName))
                        {
                            rootType = tp;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var tp in ass.GetTypes())
                    {
                        if ((tp.BaseType == baseForRoot) && (tp.Name == rootTypeName))
                        {
                            rootType = tp;
                            break;
                        }
                    }
                }
            }
            return rootType;
        }

        /// <summary>从 xml 文件中导入对象 </summary>
        /// <param name="filePath">此路径必须为一个有效的路径</param>
        /// <param name="exactRootType">
        ///     xml文件的根节点所对应的类型，也就是要导入的类型。
        ///     注意，此类型必须与根节点类型完全匹配，而不能是其基类。
        /// </param>
        /// <param name="succeeded">  </param>
        /// <param name="errorMessage">要导入的类型</param>
        public static object ImportFromXml(string filePath, Type exactRootType, out bool succeeded,
            ref StringBuilder errorMessage)
        {
            FileStream reader = null;
            object obj = null;
            try
            {
                // 
                reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var xmlS = new System.Xml.Serialization.XmlSerializer(exactRootType);
                obj = xmlS.Deserialize(reader);
                //
                errorMessage.AppendLine("成功将 xml 文件中的对象进行导入");
                succeeded = true;
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine("将 xml 文件中的对象进行导入时出错" + "\r\n" + ex.Message);
                succeeded = false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return obj;
        }
        #endregion

        #region ---   将数据导出到 XML 文件

        /// <summary>
        ///     将C#中的可序列化对象写入到 xml 文件中
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="src">要导出的数据源</param>
        /// <param name="errorMessage"></param>
        /// <returns>如果成功写入，则返回 true，如果失败则返回 false。</returns>
        public static bool ExportToXmlFile(string xmlFilePath, object src, ref StringBuilder errorMessage)
        {
            StreamWriter fs = null;
            try
            {
                var tp = src.GetType();

                fs = new StreamWriter(xmlFilePath, false);
                var s = new System.Xml.Serialization.XmlSerializer(tp);
                s.Serialize(fs, src);
                //
                errorMessage.AppendLine("成功将数据导出为 xml 文件");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage.AppendLine("数据写入 xml 文件失败" + "\r\n" + ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        #endregion

    }
}