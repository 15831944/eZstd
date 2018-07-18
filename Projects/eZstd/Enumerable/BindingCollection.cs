using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eZstd.Enumerable
{

    /// <summary>
    /// 解决DataGridView绑定List后不能排序的问题。
    /// 一般的BindingList《》与List《》集合作为Datasource绑定到Datagridview控件后，是不能排序的。
    /// 如果要满足排序的要求，可以将表格中的所有数据汇总到 System.Data.DataTable 中，再将其赋值给 Datagridview.Datasource；
    /// 也可以利用本  类。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindingCollection<T> : BindingList<T>
    {
        private bool isSorted;
        private PropertyDescriptor sortProperty;
        private ListSortDirection sortDirection;

        /// <summary> 构造函数 </summary>
        public BindingCollection() : base()
        { }

        /// <summary> 构造函数 </summary>
        /// <param name="list">IList类型的列表对象</param>
        public BindingCollection(IList<T> list)
        : base(list)
        { }

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> items = Items as List<T>;

            if (items != null)
            {
                ObjectPropertyCompare<T> pc = new ObjectPropertyCompare<T>(property, direction);
                items.Sort(pc);
                isSorted = true;
            }
            else
            {
                isSorted = false;
            }

            sortProperty = property;
            sortDirection = direction;

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        //排序
        public void Sort(PropertyDescriptor property, ListSortDirection direction)
        {
            ApplySortCore(property, direction);
        }


        private class ObjectPropertyCompare<T> : IComparer<T>
        {
            private readonly PropertyDescriptor property;
            private readonly ListSortDirection direction;

            public ObjectPropertyCompare(PropertyDescriptor property, ListSortDirection direction)
            {
                this.property = property;
                this.direction = direction;
            }

            #region IComparer<T>

            /// <summary>
            /// 比较方法
            /// </summary>
            /// <param name="x">相对属性x</param>
            /// <param name="y">相对属性y</param>
            /// <returns></returns>
            public int Compare(T x, T y)
            {
                object xValue = x.GetType().GetProperty(property.Name).GetValue(x, null);
                object yValue = y.GetType().GetProperty(property.Name).GetValue(y, null);

                int returnValue;

                // 值的比较
                if (xValue is IComparable)
                {
                    returnValue = ((IComparable)xValue).CompareTo(yValue);
                }
                else if (null == xValue && null != yValue)
                {
                    returnValue = -1;
                }
                else if (null != xValue && null == yValue)
                {
                    returnValue = 1;
                }
                else if (null == xValue && null == yValue)
                {
                    returnValue = 0;
                }
                else if (xValue.Equals(yValue))
                {
                    returnValue = 0;
                }
                else
                {
                    returnValue = xValue.ToString().CompareTo(yValue.ToString());
                }

                // 升序或降序
                if (direction == ListSortDirection.Ascending)
                {
                    return returnValue;
                }
                else
                {
                    return returnValue * -1;
                }
            }

            public bool Equals(T xWord, T yWord)
            {
                return xWord.Equals(yWord);
            }

            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }

            #endregion
        }
    }
}