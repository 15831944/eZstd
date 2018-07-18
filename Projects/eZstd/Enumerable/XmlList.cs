using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using eZstd.Miscellaneous;

namespace eZstd.Enumerable
{
    /// <summary> 专门用来作为 XmlSerializer 序列化与反序列化的 IList 泛型集合，
    /// 以解决 XmlSerializer 不能直接对 IList 泛型进行序列化的问题。 
    /// </summary>
    /// <remarks>
    /// 参考： <see cref="http://www.telerik.com/help/openaccess-classic/openaccess-tasks-using-xmlserializer-with-generic-lists-and-persistent-objects.html"/>
    /// XmlSerializer 可以很好地支持 数组Array 的序列化与反序列化，
    /// 但是对于IList甚至是非泛型的ArrayList集合而言，其在反序列化Deserialize() 时，会出现将集合中的元素重复反序列化的情况。
    /// 比如原List集合中的元素个数有3个（不管有没有null值），通过Serialize()序列化到XML文档中也是正常的3个，
    /// 但是通过Deserialize() 反序列化回来的集合中，却有6个元素，即原来正常集合中的每一个元素都产生了一个重复项。
    /// </remarks>
    /// <typeparam name="T">泛型集合的类型</typeparam>
    [Serializable()]
    public class XmlList<T> : List<T>, IXmlSerializable, ICloneable
    {
        /// <summary> 构造函数 </summary>
        public XmlList()
        {
        }

        /// <summary> ICloneable.Clone() </summary>
        public object Clone()
        {
            return MemberwiseClone();
        }
        
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
                reader.ReadEndElement();
            }
            catch (Exception ex)
            {
                // 对于 T 为值类型的情况，在EndElement之前的那一次只能读到一个null，而null元素转换为值类型。
                throw new InvalidOperationException();
            }
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