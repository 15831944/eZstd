using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using eZstd.Miscellaneous;

namespace eZstd.Enumerable
{
    /// <summary> 添加了 <seealso cref="ItemChanged"/> 事件的 List对象 </summary>
    /// <typeparam name="T">泛型集合的类型</typeparam>
    /// <remarks>
    /// </remarks>
    public class ListEv<T> : IList<T>, IList
    {
        #region ---   事件定义

        /// <summary>
        /// 集合中的元素发生 增加、减少、修改 时触发
        /// </summary>
        public event EventHandler ItemChanged;

        #endregion

        /// <summary> 集合内部维护的 IList 集合</summary>
        private readonly IList<T> _sourceList;

        #region ---   构造函数

        /// <summary> 构造函数（不推荐，因为通过此方法构造出来的 XmlList 内部维护的IList集合
        /// 与这里的输入参数 wrapped 集合代表同一个地址） </summary>
        /// <param name="wrapped">在外部继续对此集合元素的增加、插入、移除会同步到 SourceList 属性中，
        /// 因为这二者引用的是内存中同一个集合的地址。</param>
        public ListEv(IList<T> wrapped)
        {
            if (wrapped == null) throw new ArgumentNullException(nameof(wrapped));
            _sourceList = wrapped;
        }

        public ListEv() : this(new List<T>()) { }

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

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        public void RemoveAt(int index)
        {
            _sourceList.RemoveAt(index);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        public T this[int index]
        {
            get { return _sourceList[index]; }
            set { _sourceList[index] = value; }
        }

        /// <summary> 将内部维护的集合进行复制 </summary>
        public IList<T> Clone()
        {
            return _sourceList.ToList();
        }

        #endregion

        #region ICollection<T> Members  

        public void Add(T item)
        {
            _sourceList.Add(item);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        public void Clear()
        {
            _sourceList.Clear();

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
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

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        #endregion

        #region IEnumerable<T> Members  

        public IEnumerator<T> GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }

        #endregion

        #endregion

        #region  ---   IList 及各种子接口的实现  

        #region IList Members  

        object IList.this[int index]
        {
            get { return _sourceList[index]; }
            set { _sourceList[index] = (T)value; }
        }

        bool IList.IsFixedSize
        {
            get { return false; }

        }

        bool IList.IsReadOnly
        {
            get { return _sourceList.IsReadOnly; }

        }
        int IList.Add(object value)
        {
            _sourceList.Add((T)value);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
            // 返回此 value 被插入到集合中的位置
            return _sourceList.Count - 1;
        }

        void IList.Clear()
        {
            _sourceList.Clear();

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        bool IList.Contains(object value)
        {
            return _sourceList.Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return _sourceList.IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            _sourceList.Insert(index, (T)value);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        void IList.Remove(object value)
        {
            _sourceList.Remove((T)value);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }

        void IList.RemoveAt(int index)
        {
            _sourceList.RemoveAt(index);

            // 触发事件
            if (ItemChanged != null) { ItemChanged(this, null); }
        }
        #endregion

        #region ICollection Members  

        int ICollection.Count
        {
            get { return _sourceList.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        private object _syncRoot;
        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                }
                return _syncRoot;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            _sourceList.CopyTo(array as T[], index);

        }

        #endregion

        #endregion

        #region IEnumerable Members : IList<T> 与 IList 接口 共用

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _sourceList.GetEnumerator();
        }

        #endregion

    }
}