using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Effects;

namespace eZstd.Table
{
    /// <summary>
    /// 一些二维数组所特有的操作。
    /// 注意，有很多集合的算法都可以通过 LINQ 语句来实现，如果可以，则推荐用 Linq。但是对于一些特殊的情况，比如二维数组的运算，则在此类中实现。
    /// </summary>
    public static class TableUtil 
    {

        /// <summary> 从剪切板中提取表格数据 </summary>
        public static string[,] GetTableFromClipBoard()
        {
            string pastTest = Clipboard.GetText();

            if (string.IsNullOrEmpty(pastTest))
            { return null; }

            // excel中是以"空格"和"换行"来当做字段和行，所以用"\r\n"来分隔，即"回车 + 换行"
            string[] lines = pastTest.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            int writeRowsCount = lines.Length; //要写入多少行数据
            int writeColsCount = lines[0].Split('\t').Length; //要写入的每一行数据中有多少列
            string[,] table = new string[writeRowsCount, writeColsCount];
            for (int r = 0; r < writeRowsCount; r++)
            {
                var rowValues = lines[r].Split('\t');//在每一行的单元格间，作为单元格的分隔的字符为"\t",即水平换行符
                var colCount = Math.Min(writeColsCount, rowValues.Length);
                for (int c = 0; c < colCount; c++)
                {
                    table[r, c] = rowValues[c];
                }
            }
            return table;
        }
    }
}