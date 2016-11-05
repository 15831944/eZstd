using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace eZstd.Data
{
    /// <summary> 专门用来作为 XmlSerializer 序列化与反序列化的 IList 泛型集合，
    /// 以解决 XmlSerializer 不能直接对 IList 泛型进行序列化的问题。 
    /// </summary>
    /// <remarks>
    /// 参考： <see cref="http://www.telerik.com/help/openaccess-classic/openaccess-tasks-using-xmlserializer-with-generic-lists-and-persistent-objects.html"/>
    /// XmlSerializer 可以很好地支持 数组Array 的序列化与反序列化，
    /// 但是对于IList<>甚至是非泛型的ArrayList集合而言，其在反序列化Deserialize() 时，会出现将集合中的元素重复反序列化的情况。
    /// 比如原List集合中的元素个数有3个（不管有没有null值），通过Serialize()序列化到XML文档中也是正常的3个，
    /// 但是通过Deserialize() 反序列化回来的集合中，却有6个元素，即原来正常集合中的每一个元素都产生了一个重复项。
    /// </remarks>
    /// <typeparam name="T">泛型集合的类型</typeparam>
    [Serializable()]
    public class XmlList<T> : List<T>, IXmlSerializable
    {
        /// <summary> 构造函数 </summary>
        public XmlList()
        {
        }

        #region IXmlSerializable Members  

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false)
                return;
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

        public override string ToString()
        {
            return "Count = " + this.Count.ToString();
        }
    }
    
    /// <summary> 专门用来作为 XmlSerializer 序列化与反序列化的 IList 泛型集合，
    /// 以解决 XmlSerializer 不能直接对 IList 泛型进行序列化的问题。 
    /// </summary>
    /// <remarks>
    /// 参考： <see cref="http://www.telerik.com/help/openaccess-classic/openaccess-tasks-using-xmlserializer-with-generic-lists-and-persistent-objects.html"/>
    /// XmlSerializer 可以很好地支持 数组Array 的序列化与反序列化，
    /// 但是对于IList<>甚至是非泛型的ArrayList集合而言，其在反序列化Deserialize() 时，会出现将集合中的元素重复反序列化的情况。
    /// 比如原List集合中的元素个数有3个（不管有没有null值），通过Serialize()序列化到XML文档中也是正常的3个，
    /// 但是通过Deserialize() 反序列化回来的集合中，却有6个元素，即原来正常集合中的每一个元素都产生了一个重复项。 </remarks>
    /// <typeparam name="T">泛型集合的类型</typeparam>
    internal class XmlListTest<T> : IList<T>, IXmlSerializable
    {
        /// <summary> 集合内部维护的 IList 集合</summary>
        private readonly IList<T> _sourceList;

        #region ---   构造函数

        /// <summary> 构造函数（不推荐，因为通过此方法构造出来的 XmlList 内部维护的IList集合
        /// 与这里的输入参数 wrapped 集合代表同一个地址） </summary>
        /// <param name="wrapped">在外部继续对此集合元素的增加、插入、移除会同步到 SourceList 属性中，
        /// 因为这二者引用的是内存中同一个集合的地址。</param>
        public XmlListTest(IList<T> wrapped)
        {
            if (wrapped == null) throw new ArgumentNullException(nameof(wrapped));
            _sourceList = wrapped;
        }

        public XmlListTest() : this(new List<T>()) { }

        #endregion

        /// <summary> 将内部维护的集合进行复制 </summary>
        public IList<T> Clone()
        {
            IList<T> target = new List<T>();
            for (int i = 0; i < _sourceList.Count; i++)
            {
                target[i] = _sourceList[i];
            }
            return target;
        }

        #region IXmlSerializable Members  

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement || reader.Read() == false)
                return;
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
                        _sourceList.Add(e);
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

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            if (_sourceList.Count == 0)
                return;
            XmlSerializer inner = new XmlSerializer(typeof(T));
            for (int i = 0; i < _sourceList.Count; i++)
            {
                inner.Serialize(writer, _sourceList[i]);
            }
        }

        #endregion  

        #region  ---   IList<T> 及各种子接口的实现  

        #region IList<T> Members  

        public int IndexOf(T item)
        {
            return _sourceList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _sourceList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _sourceList.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _sourceList[index]; }
            set { _sourceList[index] = value; }
        }

        #endregion

        #region ICollection<T> Members  

        public void Add(T item)
        {
            _sourceList.Add(item);
        }

        public void Clear()
        {
            _sourceList.Clear();
        }

        public bool Contains(T item)
        {
            return _sourceList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _sourceList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _sourceList.Count; }
        }

        public bool IsReadOnly
        {
            get { return _sourceList.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return _sourceList.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members  

        public IEnumerator<T> GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members  

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }

        #endregion

        #endregion

        public override string ToString()
        {
            return "Count = " + _sourceList.Count.ToString();
        }
    }
}