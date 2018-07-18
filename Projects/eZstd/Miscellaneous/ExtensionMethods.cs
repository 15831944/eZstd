using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using eZstd.Table;


namespace eZstd.Miscellaneous
{
    public static class ExtensionMethods
    {
        #region ---   Icollection

        /// <summary>
        /// 将某集合中的元素添加到另一个集合中去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        public static void AddRange<T>(this List<T> collection, List<T> items)
        {
            if (items == null)
            {
                Debug.WriteLine("Do extension metody AddRange byly poslany items == null");
                return;
            }
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Strong-typed object cloning for objects that implement <see cref="ICloneable"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(this T obj) where T : ICloneable
        {
            return (T)(obj as ICloneable).Clone();
        }


        /// <summary>
        /// 从集合中搜索指定项的下标位置（第一个元素的下标值为0），如果没有匹配项，则返回-1。
        /// </summary>
        /// <typeparam name="TCol"></typeparam>
        /// <param name="collection"> 匹配的数据源集合 </param>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="value"> 要进行匹配的值 </param>
        /// <returns> 从集合中搜索指定项的下标位置（第一个元素的下标值为0），如果没有匹配项，则返回-1。 </returns>
        public static int IndexOf<TCol, TVal>(this TCol collection, TVal value) where TCol : IEnumerable<TVal>
        {
            int index = -1;
            foreach (TVal item in collection)
            {
                index += 1;
                if (value.Equals(item))
                {
                    return index;
                }
            }
            return -1;
        }

        #endregion

        #region ---   DataGridView

        /// <summary>
        /// 提取 <seealso cref="DataGridView"/> 控件中，选择的矩形区域的角部单元格
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="cornerIndex"></param>
        /// <returns></returns>
        public static DataGridViewCell GetSelectedCornerCell(this DataGridView dgv, CornerIndex cornerIndex)
        {
            var ss = dgv.SelectedCells;
            int minRow = int.MaxValue;
            int minCol = int.MaxValue; ;
            int maxRow = int.MinValue;
            int maxCol = int.MinValue;

            if (cornerIndex == CornerIndex.UpLeft)
            {
                // 左上角点
                foreach (DataGridViewCell c in ss)
                {
                    if (c.RowIndex <= minRow)
                    {
                        minRow = c.RowIndex;
                    }
                    if (c.ColumnIndex <= minCol)
                    {
                        minCol = c.ColumnIndex;
                    }
                }
                return dgv.Rows[minRow].Cells[minCol];
            }
            else if (cornerIndex == CornerIndex.BottomRight)
            {

                // 右下角点
                foreach (DataGridViewCell c in ss)
                {
                    if (c.RowIndex >= maxRow)
                    {
                        maxRow = c.RowIndex;
                    }
                    if (c.ColumnIndex >= maxCol)
                    {
                        maxCol = c.ColumnIndex;
                    }
                }
                return dgv.Rows[maxRow].Cells[maxCol];
            }
            else if (cornerIndex == CornerIndex.BottomLeft)
            {

                // 左下角点
                foreach (DataGridViewCell c in ss)
                {
                    if (c.RowIndex >= maxRow)
                    {
                        maxRow = c.RowIndex;
                    }
                    if (c.ColumnIndex <= minCol)
                    {
                        minCol = c.ColumnIndex;
                    }
                }
                return dgv.Rows[maxRow].Cells[minCol];
            }
            else if (cornerIndex == CornerIndex.BottomLeft)
            {

                // 右上角点
                foreach (DataGridViewCell c in ss)
                {
                    if (c.RowIndex <= minRow)
                    {
                        minRow = c.RowIndex;
                    }
                    if (c.ColumnIndex >= maxCol)
                    {
                        maxCol = c.ColumnIndex;
                    }
                }
                return dgv.Rows[minRow].Cells[maxCol];
            }
            return null;
        }

        #endregion

    }
}