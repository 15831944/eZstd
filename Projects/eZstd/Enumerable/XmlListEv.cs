using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace eZstd.Enumerable
{
    /// <summary>
    /// 添加了 ItemChanged 事件的 List对象，同时支持 xml 文件的序列化与反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable()]
    public class XmlListEv<T> : ListEv<T>, IXmlSerializable
    {
        #region IXmlSerializable 接口的方法实现  

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary> 从 xml 文档中提取对象 </summary>
        /// <param name="reader"></param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            // :warning: ReadXml1 与 ReadXml2 这两个方法，有时第一个好用，有时第二个好用，暂时不明白它们的区别在哪里。
            ReadXml1(reader);
            // ReadXml2(reader);
        }

        private void ReadXml1(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false)
            {
                return;
            }

            XmlSerializer inner = new XmlSerializer(typeof(T));
            try
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {

                    T e = (T)inner.Deserialize(reader);
                    this.Add(e);
                }
            }
            catch (Exception ex)
            {
                // 对于 T 为值类型的情况，在EndElement之前的那一次只能读到一个null，而null元素转换为值类型。
            }
            reader.ReadEndElement();
        }
        private void ReadXml2(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false)
            {
                return;
            }

            XmlSerializer inner = new XmlSerializer(typeof(T));
            try
            {
                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    // EndElement之前还会有一次多余的读取，其会反序列化为null，但是这个值在集合中并不存在。
                    // 不要增减下面用于判断的字符后面的空格数量（2个）

                    if (reader.Value == "\n" + new string(' ', reader.Depth * 2))
                    {
                        T e = (T)inner.Deserialize(reader);
                        this.Add(e);
                    }
                    else
                    {
                        reader.Read();
                    }
                }
            }
            catch (Exception ex)
            {
                // 对于 T 为值类型的情况，在EndElement之前的那一次只能读到一个null，而null元素转换为值类型。
            }
            reader.ReadEndElement();
        }

        /// <summary> 将可序列化对象写入到 xml 文档中 </summary>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (this.Count == 0)
                return;
            XmlSerializer inner = new XmlSerializer(typeof(T));
            for (int i = 0; i < this.Count; i++)
            {
                inner.Serialize(writer, this[i]);
            }
        }

        #endregion
    }
}
