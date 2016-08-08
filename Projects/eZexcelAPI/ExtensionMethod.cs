using System;
using Microsoft.Office.Interop.Excel;

namespace eZexcelAPI
{
    public static class ExcelExtension //扩展方法只能在模块中声明。
    {
        public enum CornerIndex
        {
            UpLeft,
            UpRight,
            BottomLeft,
            BottomRight
        }

        /// <summary>
        /// 返回Range对象范围中的最右下角点的那个单元格对象
        /// </summary>
        /// <param name="SourceRange">对于对Range.Areas.Item(1)中的单元格区域进行操作</param>
        /// <param name="Corner">要返回哪一个角落的单元格</param>
        /// <returns></returns>
        public static Microsoft.Office.Interop.Excel.Range Ex_CornerCell(this Microsoft.Office.Interop.Excel.Range SourceRange, CornerIndex Corner = CornerIndex.BottomRight)
        {
            Range myCornerCell = null;
            //
            SourceRange = SourceRange.Areas[1];
            Microsoft.Office.Interop.Excel.Range LeftTopCell = SourceRange.Cells[1, 1];
            //
            switch (Corner)
            {
                case CornerIndex.BottomRight:
                    myCornerCell = SourceRange.Worksheet.Cells[LeftTopCell.Row + SourceRange.Rows.Count - 1, LeftTopCell.Column + SourceRange.Columns.Count - 1];
                    break;
                case CornerIndex.UpRight:
                    myCornerCell = SourceRange.Worksheet.Cells[LeftTopCell.Row, LeftTopCell.Column + SourceRange.Columns.Count - 1];
                    break;
                case CornerIndex.BottomLeft:
                    myCornerCell = SourceRange.Worksheet.Cells[LeftTopCell.Row + SourceRange.Rows.Count - 1, LeftTopCell.Column];
                    break;
                case CornerIndex.UpLeft:
                    myCornerCell = LeftTopCell;
                    break;
            }
            return myCornerCell;
        }


        /// <summary>
        /// 收缩Range.Areas.Item(1)的单元格范围。
        /// 在选择一个单元格范围时，有时为了界面操作简单，往往会选择一整列或者一整行，但是并不是要对基本所有的单元格进行操作，
        /// 而只需要操作其中有数据的那些区域。此函数即是将选择的整行或者整列的单元格收缩到有数据的范围内。
        /// </summary>
        /// <param name="rg"></param>
        /// <returns></returns>
        /// <remarks>在选择一个单元格范围时，有时为了界面操作简单，往往会选择一整列或者一整行，但是并不是要对基本所有的单元格进行操作，
        /// 而只需要操作其中有数据的那些区域。此函数即是将选择的整行或者整列的单元格收缩到有数据的范围内。</remarks>
        public static Range Ex_ShrinkedRange(this Microsoft.Office.Interop.Excel.Range rg)
        {
            rg = rg.Areas[1];
            int ColCount = rg.Columns.Count;
            int RowCount = rg.Rows.Count;
            //
            Range BottomRightCell = rg.Ex_CornerCell(CornerIndex.BottomRight);
            Range UsedBottomRightCell = rg.Worksheet.UsedRange.Ex_CornerCell(CornerIndex.BottomRight);

            //  将最下面的单元格收缩到UsedRange的最下面的位置
            if (RowCount == Math.Pow(2, 20) & ColCount == Math.Pow(2, 14)) // 说明选择了整个表格
            {
                BottomRightCell = UsedBottomRightCell;
            }
            else if (RowCount == Math.Pow(2, 20) & ColCount < Math.Pow(2, 14)) // 说明选择了整列
            {
                BottomRightCell = BottomRightCell.Offset[UsedBottomRightCell.Row - Math.Pow(2, 20), 0];
            }
            else if (RowCount < Math.Pow(2, 20) & ColCount == Math.Pow(2, 14)) // 说明选择了整行
            {
                BottomRightCell = BottomRightCell.Offset[0, UsedBottomRightCell.Column - Math.Pow(2, 14)];
                // Else  ' 说明选择了一个有限的范围
            }
            return rg.Worksheet.Range[rg.Cells[1, 1], BottomRightCell];
        }
    }


}
