using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace eZexcelAPI
{

    public class ExcelFunction
    {

        #region   ---  Range到数组的转换
        /// <summary>
        /// 将Excel的Range对象的数据转换为指定数据类型的一维向量,
        /// Range.Value返回一个二维的表格，此函数将其数据按列行拼接为一维数组。
        /// （即按(0,0),(0,1),(1,0),(1,1),(2,0)...的顺序排列）
        /// </summary>
        /// <param name="rg">用于提取数据的Range对象</param>
        /// <returns>返回一个指定类型的一维向量，如“Single()”</returns>
        /// <remarks>直接用Range.Value来返回的数据，其类型只能是Object，
        /// 而其中的数据是一个元素类型为Object的二维数据（即使此Range对象只有一行或者一列）。
        /// 所以要进行显式的转换，将其转换为指定类型的向量或者二维数组，以便于后面的数据操作。</remarks>
        public static T[] ConvertRangeDataToVector<T>(Microsoft.Office.Interop.Excel.Range rg)
        {
            //Range中的数据，这是以向量的形式给出的，其第一个元素的下标值很有可能是1，而不是0
            object[,] RangeData = rg.Value;
            int elementCount = RangeData.Length;
            //
            T[] Value_Out = new T[elementCount - 1 + 1];
            //获取输入的数据类型
            Type DestiType = typeof(T);
            TypeCode TC = Type.GetTypeCode(DestiType);
            //判断此类型的值
            switch (TC)
            {
                case TypeCode.DateTime:
                    int i_1 = 0;
                    foreach (object V in RangeData)
                    {
                        try
                        {
                            Value_Out[i_1] = (T)V;
                        }
                        catch (Exception)
                        {
                            //Debug.Print("数据：" & V.ToString & " 转换为日期出错！将其处理为日期的初始值。" _
                            //                & vbCrLf & ex.Message)
                            //如果输入的数据为double类型，则将其转换为等效的Date
                            object O = DateTime.FromOADate(System.Convert.ToDouble(V));
                            Value_Out[i_1] = (T)O;
                        }
                        finally
                        {
                            i_1++;
                        }
                    }
                    break;
                default:
                    int i = 0;
                    foreach (object V in RangeData)
                    {
                        Value_Out[i] = (T)V;
                        i++;
                    }
                    break;
            }
            return Value_Out;
        }

        /// <summary>
        /// 将Excel的Range对象的数据转换为指定数据类型的二维数组
        /// </summary>
        /// <param name="rg">用于提取数据的Range对象</param>
        /// <returns>返回一个指定类型的二维数组，如“Single(,)”</returns>
        /// <remarks>直接用Range.Value来返回的数据，其类型只能是Object，
        /// 而其中的数据是一个元素类型为Object的二维数据（即使此Range对象只有一行或者一列）。
        /// 所以要进行显式的转换，将其转换为指定类型的向量或者二维数组，以便于后面的数据操作。</remarks>
        public static T[,] ConvertRangeDataToMatrix<T>(Microsoft.Office.Interop.Excel.Range rg)
        {

            object[,] RangeData = rg.Value;
            //由Range.Value返回的二维数组的第一个元素的下标值，这里应该为1，而不是0
            byte LB_Range = (byte)0;
            //
            int intRowsCount = System.Convert.ToInt32((RangeData.Length - 1) - LB_Range + 1);
            int intColumnsCount = System.Convert.ToInt32(Information.UBound((System.Array)RangeData, 2) - LB_Range + 1);
            //
            T[,] OutputMatrix = new T[intRowsCount - 1 + 1, intColumnsCount - 1 + 1];
            //
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.DateTime:
                    for (int row = 0; row <= intRowsCount - 1; row++)
                    {
                        for (int col = 0; col <= intColumnsCount - 1; col++)
                        {
                            try
                            {
                                OutputMatrix[row, col] = (T)(RangeData[row + LB_Range, col + LB_Range]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("数据：" + RangeData[row + LB_Range, col + LB_Range].ToString() + " 转换为日期出错！将其处理为日期的初始值。"
                                    + "\r\n" + ex.Message);
                            }
                        }
                    }
                    break;
                default:
                    for (int row = 0; row <= intRowsCount - 1; row++)
                    {
                        for (int col = 0; col <= intColumnsCount - 1; col++)
                        {
                            OutputMatrix[row, col] = (T)RangeData[row + LB_Range, col + LB_Range];
                        }
                    }
                    break;
            }
            return OutputMatrix;
        }
        #endregion

        /// <summary>
        /// 将Excel表中的列的数值编号转换为对应的字符
        /// </summary>
        /// <param name="ColNum">Excel中指定列的数值序号</param>
        /// <returns>以字母序号的形式返回指定列的列号</returns>
        /// <remarks>1对应A；26对应Z；27对应AA</remarks>
        public static string ConvertColumnNumberToString(int ColNum)
        {
            // 关键算法就是：连续除以基，直至商为0，从低到高记录余数！
            // 其中value必须是十进制表示的数值
            //intLetterIndex的位数为digits=fix(log(value)/log(26))+1
            //本来算法很简单，但是要解决一个问题：当value为26时，其26进制数为[1 0]，这样的话，
            //以此为下标索引其下面的strAlphabetic时就会出错，因为下标0无法索引。实际上，这种特殊情况下，应该让所得的结果成为[26]，才能索引到字母Z。
            //处理的方法就是，当所得余数remain为零时，就将其改为26，然后将对应的商的值减1.
            if (ColNum < 1)
            {
                MessageBox.Show("列数不能小于1");
            }
            List<byte> intLetterIndex = new List<byte>();
            //
            int quotient = 0; //商
            byte remain = 0; //余数
            //
            byte i = (byte)0;
            do
            {
                quotient = (int)(Conversion.Fix((double)ColNum / 26)); //商
                remain = (byte)(ColNum % 26); //余数
                if (remain == 0)
                {
                    intLetterIndex.Add(26);
                    quotient--;
                }
                else
                {
                    intLetterIndex.Add(remain);

                }
                i++;
                ColNum = quotient;
            } while (!(quotient == 0));
            string alphabetic = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string ans = "";
            for (i = 0; i <= intLetterIndex.Count - 1; i++)
            {
                ans = alphabetic[System.Convert.ToInt32(intLetterIndex[i] - 1)] + ans;
            }
            return ans;
        }

        /// <summary>
        /// 将Excel表中的字符编号转换为对应的数值
        /// </summary>
        /// <param name="colString">以A1形式表示的列的字母序号，不区分大小写</param>
        /// <returns>以整数的形式返回指定列的数值编号，A列对应数值1</returns>
        /// <remarks></remarks>
        public static int ConvertColumnStringToNumber(string colString)
        {
            colString = colString.ToUpper();
            byte ASC_A = (byte)(Strings.Asc("A"));
            int ans = 0;
            for (byte i = 0; i <= colString.Length - 1; i++)
            {
                char Chr = colString.ToCharArray(i, 1)[0];
                ans = ans + (Strings.Asc(Chr) - ASC_A + 1) * (int)Math.Pow(26, colString.Length - i - 1);
            }
            return ans;
        }

        /// <summary>
        /// 获取指定Range范围中的右下角的那一个单元格
        /// </summary>
        /// <param name="RangeForSearch">要进行搜索的Range区域</param>
        /// <returns>指定Range区域中的左下角的单元格</returns>
        /// <remarks></remarks>
        public static Microsoft.Office.Interop.Excel.Range GetBottomRightCell(Microsoft.Office.Interop.Excel.Range RangeForSearch)
        {
            int RowsCount = 0;
            int ColsCount = 0;
            Microsoft.Office.Interop.Excel.Range LeftTopCell = RangeForSearch.Cells[1, 1];
            Microsoft.Office.Interop.Excel.Range with_1 = RangeForSearch;
            RowsCount = with_1.Rows.Count;
            ColsCount = with_1.Columns.Count;
            return RangeForSearch.Worksheet.Cells[LeftTopCell.Row + RowsCount - 1, LeftTopCell.Column + ColsCount - 1];
        }

        #region   ---  工作簿或工作表的匹配

        /// <summary>
        /// 比较两个工作表是否相同。
        /// 判断的标准：先判断工作表的名称是否相同，如果相同，再判断工作表所属的工作簿的路径是否相同，
        /// 如果二者都相同，则认为这两个工作表是同一个工作表
        /// </summary>
        /// <param name="sheet1">进行比较的第1个工作表对象</param>
        /// <param name="sheet2">进行比较的第2个工作表对象</param>
        /// <returns></returns>
        /// <remarks>不用 blnSheetsMatched = sheet1.Equals(sheet2)，是因为这种方法并不能正确地返回True。</remarks>
        public static bool SheetCompare(Microsoft.Office.Interop.Excel.Worksheet sheet1, Microsoft.Office.Interop.Excel.Worksheet sheet2)
        {
            bool blnSheetsMatched = false;
            //先比较工作表名称
            if (string.Compare(sheet1.Name, sheet2.Name) == 0)
            {
                Microsoft.Office.Interop.Excel.Workbook wb1 = sheet1.Parent;
                Microsoft.Office.Interop.Excel.Workbook wb2 = sheet2.Parent;
                //再比较工作表所在工作簿的路径
                if (string.Compare(wb1.FullName, wb2.FullName) == 0)
                {
                    blnSheetsMatched = true;
                }
            }
            return blnSheetsMatched;
        }

        /// <summary>
        /// 检测工作簿是否已经在指定的Application程序中打开。
        /// 如果最后此工作簿在程序中被打开（已经打开或者后期打开），则返回对应的Workbook对象，否则返回Nothing。
        /// 这种比较方法的好处是不会额外去打开已经打开过了的工作簿。
        /// </summary>
        /// <param name="wkbkPath">进行检测的工作簿</param>
        /// <param name="Application">检测工作簿所在的Application程序</param>
        /// <param name="blnFileHasBeenOpened">指示此Excel工作簿是否已经在此Application中被打开</param>
        /// <param name="OpenIfNotOpened">如果此Excel工作簿并没有在此Application程序中打开，是否要将其打开。</param>
        /// <param name="OpenByReadOnly">是否以只读方式打开</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Microsoft.Office.Interop.Excel.Workbook MatchOpenedWkbk(string wkbkPath, Microsoft.Office.Interop.Excel.Application Application, ref bool blnFileHasBeenOpened, bool OpenIfNotOpened = false, bool OpenByReadOnly = true)
		{
			Microsoft.Office.Interop.Excel.Workbook wkbk = null;
			if (Application != null)
			{
				//进行具体的检测
				if (File.Exists(wkbkPath)) //此工作簿存在
				{
					//如果此工作簿已经打开
					foreach (Microsoft.Office.Interop.Excel.Workbook WkbkOpened in Application.Workbooks)
					{
						if (string.Compare(WkbkOpened.FullName, wkbkPath, true) == 0)
						{
							wkbk = WkbkOpened;
							blnFileHasBeenOpened = true;
							break;
						}
					}
					
					//如果此工作簿还没有在主程序中打开，则将此工作簿打开
					if (!blnFileHasBeenOpened)
					{
						if (OpenIfNotOpened)
						{
						    wkbk = Application.Workbooks.Open(Filename: wkbkPath, UpdateLinks: false, ReadOnly: OpenByReadOnly);
						}
					}
				}
			}
			//返回结果
			return wkbk;
		}

        /// <summary>
        /// 检测指定工作簿内是否有指定的工作表，如果存在，则返回对应的工作表对象，否则返回Nothing
        /// </summary>
        /// <param name="wkbk">进行检测的工作簿对象</param>
        /// <param name="sheetName">进行检测的工作表的名称</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Microsoft.Office.Interop.Excel.Worksheet MatchWorksheet(Microsoft.Office.Interop.Excel.Workbook wkbk, string sheetName)
        {
            //工作表是否存在
            Microsoft.Office.Interop.Excel.Worksheet ValidWorksheet = null;
            foreach (Microsoft.Office.Interop.Excel.Worksheet sht in wkbk.Worksheets)
            {
                if (string.Compare(sht.Name, sheetName) == 0)
                {
                    ValidWorksheet = sht;
                    return ValidWorksheet;
                }
            }
            //返回检测结果
            return ValidWorksheet;
        }

        #endregion

        #region   ---  几何绘图


        /// <summary>
        /// 将任意形状以指定的值定位在Chart的某一坐标轴中。
        /// </summary>
        /// <param name="ShapeToLocate">要进行定位的形状</param>
        /// <param name="Ax">此形状将要定位的轴</param>
        /// <param name="Value">此形状在Chart中所处的值</param>
        /// <param name="percent">将形状按指定的百分比的宽度或者高度的部位定位到坐标轴的指定值的位置。
        /// 如果其值设定为0，则表示此形状的左端（或上端）定位在设定的位置处，
        /// 如果其值为100，则表示此形状的右端（或下端）定位在设置的位置处。</param>
        /// <remarks></remarks>
        public static void setPositionInChart(Microsoft.Office.Interop.Excel.Shape ShapeToLocate, Microsoft.Office.Interop.Excel.Axis Ax, double Value, double percent = 0)
        {
            Microsoft.Office.Interop.Excel.Chart cht = (Microsoft.Office.Interop.Excel.Chart)Ax.Parent;
            if (cht != null)
            {
                //Try          '先考察形状是否是在Chart之中

                //    ShapeToLocate = cht.Shapes.Item(ShapeToLocate.Name)
                //Catch ex As Exception           '如果形状不在Chart中，则将形状复制进Chart，并将原形状删除
                //    ShapeToLocate.Copy()
                //    cht.Paste()
                //    ShapeToLocate.Delete()
                //    ShapeToLocate = cht.Shapes.Item(cht.Shapes.Count)
                //End Try
                //
                switch (Ax.Type)
                {

                    case Microsoft.Office.Interop.Excel.XlAxisType.xlCategory: //横向X轴
                        double PositionInChartByValue_1 = GetPositionInChartByValue(Ax, Value);
                        Microsoft.Office.Interop.Excel.Shape with_1 = ShapeToLocate;
                        with_1.Left = (float)(PositionInChartByValue_1 - percent * with_1.Width);
                        break;

                    case Microsoft.Office.Interop.Excel.XlAxisType.xlValue: //竖向Y轴
                        double PositionInChartByValue = GetPositionInChartByValue(Ax, Value);
                        Microsoft.Office.Interop.Excel.Shape with_2 = ShapeToLocate;
                        with_2.Top = (float)(PositionInChartByValue - percent * with_2.Height);
                        break;
                    case Microsoft.Office.Interop.Excel.XlAxisType.xlSeriesAxis:
                        MessageBox.Show("暂时不知道这是什么坐标轴", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
        }

        /// <summary>
        /// 将一组形状以指定的值定位在Chart的某一坐标轴中。
        /// </summary>
        /// <param name="ShapesToLocate">要进行定位的形状</param>
        /// <param name="Ax">此形状将要定位的轴</param>
        /// <param name="Values">此形状在Chart中所处的值</param>
        /// <param name="percents">将形状按指定的百分比的宽度或者高度的部位定位到坐标轴的指定值的位置。
        /// 如果其值设定为0，则表示此形状的左端（或上端）定位在设定的位置处，
        /// 如果其值为100，则表示此形状的右端（或下端）定位在设置的位置处。</param>
        /// <remarks></remarks>
        public static void setPositionInChart(Microsoft.Office.Interop.Excel.Axis Ax, Microsoft.Office.Interop.Excel.Shape[] ShapesToLocate, double[] Values, double[] Percents = null)
        {
            // ------------------------------------------------------
            //检查输入的数组中的元素个数是否相同
            int Count = ShapesToLocate.Length;
            if (Values.Length != Count)
            {
                MessageBox.Show("输入数组中的元素个数不相同。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Percents != null)
            {
                if (Percents.Count() != 1 & Percents.Length != Count)
                {
                    MessageBox.Show("输入数组中的元素个数不相同。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // ------------------------------------------------------
            Microsoft.Office.Interop.Excel.Chart cht = (Microsoft.Office.Interop.Excel.Chart)Ax.Parent;
            //
            double max = Ax.MaximumScale;
            double min = Ax.MinimumScale;
            //
            Microsoft.Office.Interop.Excel.PlotArea PlotA = cht.PlotArea;
            // ------------------------------------------------------
            Microsoft.Office.Interop.Excel.Shape shp = default(Microsoft.Office.Interop.Excel.Shape);
            double Value = 0;
            double Percent = Percents[0];
            double PositionInChartByValue = 0;
            // ------------------------------------------------------

            switch (Ax.Type)
            {

                case Microsoft.Office.Interop.Excel.XlAxisType.xlCategory: //横向X轴
                    break;


                case Microsoft.Office.Interop.Excel.XlAxisType.xlValue: //竖向Y轴
                    if (Ax.ReversePlotOrder == false) //顺序刻度值，说明Y轴数据为下边小上边大
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            shp = ShapesToLocate[i];
                            Value = Values[i];
                            if (Percents.Count() > 1)
                            {
                                Percent = Percents[i];
                            }
                            PositionInChartByValue = PlotA.InsideTop + PlotA.InsideHeight * (max - Value) / (max - min);
                            shp.Top = (float)(PositionInChartByValue - Percent * shp.Width);
                        }
                    }
                    else //逆序刻度值，说明Y轴数据为上边小下边大
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            shp = ShapesToLocate[i];
                            Value = Values[i];
                            if (Percents.Count() > 1)
                            {
                                Percent = Percents[i];
                            }
                            PositionInChartByValue = PlotA.InsideTop + PlotA.InsideHeight * (Value - min) / (max - min);
                            shp.Top = (float)(PositionInChartByValue - Percent * shp.Width);
                        }
                    }
                    break;

                case Microsoft.Office.Interop.Excel.XlAxisType.xlSeriesAxis:
                    MessageBox.Show("暂时不知道这是什么坐标轴", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }

        }

        /// <summary>
        /// 根据在坐标轴中的值，来返回这个值在Chart中的几何位置
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Value"></param>
        /// <returns>如果Ax是一个水平X轴，则返回的是坐标轴Ax中的值Value在Chart中的Left值；
        /// 如果Ax是一个竖向Y轴，则返回的是坐标轴Ax中的值Value在Chart中的Top值。</returns>
        /// <remarks></remarks>
        public static double GetPositionInChartByValue(Microsoft.Office.Interop.Excel.Axis Ax, double Value)
        {

            double PositionInChartByValue = 0;
            Microsoft.Office.Interop.Excel.Chart cht = (Microsoft.Office.Interop.Excel.Chart)Ax.Parent;
            //
            double max = Ax.MaximumScale;
            double min = Ax.MinimumScale;
            //
            Microsoft.Office.Interop.Excel.PlotArea PlotA = cht.PlotArea;
            switch (Ax.Type)
            {

                case Microsoft.Office.Interop.Excel.XlAxisType.xlCategory: //横向X轴
                    double PositionInPlot_1 = 0;
                    if (Ax.ReversePlotOrder == false) //正向分类，说明X轴数据为左边小右边大
                    {
                        PositionInPlot_1 = PlotA.InsideWidth * (Value - min) / (max - min);
                    }
                    else //逆序类别，说明X轴数据为左边大右边小
                    {
                        PositionInPlot_1 = PlotA.InsideWidth * (max - Value) / (max - min);
                    }
                    PositionInChartByValue = PlotA.InsideLeft + PositionInPlot_1;
                    break;

                case Microsoft.Office.Interop.Excel.XlAxisType.xlValue: //竖向Y轴
                    double PositionInPlot = 0;
                    if (Ax.ReversePlotOrder == false) //顺序刻度值，说明Y轴数据为下边小上边大
                    {
                        PositionInPlot = PlotA.InsideHeight * (max - Value) / (max - min);
                    }
                    else //逆序刻度值，说明Y轴数据为上边小下边大
                    {
                        PositionInPlot = PlotA.InsideHeight * (Value - min) / (max - min);
                    }
                    PositionInChartByValue = PlotA.InsideTop + PositionInPlot;
                    break;
                case Microsoft.Office.Interop.Excel.XlAxisType.xlSeriesAxis:
                    break;
                //Debug.Print("暂时不知道这是什么坐标轴")
            }
            return PositionInChartByValue;
        }

        /// <summary>
        /// 根据一组形状在某一坐标轴中的值，来返回这些值在Chart中的几何位置
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Values">要在坐标轴中进行定位的多个形状在此坐标轴中的数值</param>
        /// <returns>如果Ax是一个水平X轴，则返回的是坐标轴Ax中的值Value在Chart中的Left值；
        /// 如果Ax是一个竖向Y轴，则返回的是坐标轴Ax中的值Value在Chart中的Top值。</returns>
        /// <remarks></remarks>
        public static double[] GetPositionInChartByValue(Microsoft.Office.Interop.Excel.Axis Ax, double[] Values)
        {
            int Count = Values.Length;
            double[] PositionInChartByValue = new double[Count - 1 + 1];
            // --------------------------------------------------
            Microsoft.Office.Interop.Excel.Chart cht = (Microsoft.Office.Interop.Excel.Chart)Ax.Parent;
            //
            double max = Ax.MaximumScale;
            double min = Ax.MinimumScale;
            double Value = 0;
            //
            Microsoft.Office.Interop.Excel.PlotArea PlotA = cht.PlotArea;
            switch (Ax.Type)
            {

                case Microsoft.Office.Interop.Excel.XlAxisType.xlCategory: //横向X轴
                    if (Ax.ReversePlotOrder == false) //正向分类，说明X轴数据为左边小右边大
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            Value = Values[i];
                            PositionInChartByValue[i] = PlotA.InsideLeft + PlotA.InsideWidth * (Value - min) / (max - min);
                        }
                    }
                    else //逆序类别，说明X轴数据为左边大右边小
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            Value = Values[i];
                            PositionInChartByValue[i] = PlotA.InsideLeft + PlotA.InsideWidth * (max - Value) / (max - min);
                        }
                    }
                    break;

                case Microsoft.Office.Interop.Excel.XlAxisType.xlValue: //竖向Y轴
                    if (Ax.ReversePlotOrder == false) //顺序刻度值，说明Y轴数据为下边小上边大
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            Value = Values[i];
                            PositionInChartByValue[i] = PlotA.InsideTop + PlotA.InsideHeight * (max - Value) / (max - min);
                        }
                    }
                    else //逆序刻度值，说明Y轴数据为上边小下边大
                    {
                        for (UInt16 i = 0; i <= Count - 1; i++)
                        {
                            Value = Values[i];
                            PositionInChartByValue[i] = PlotA.InsideTop + PlotA.InsideHeight * (Value - min) / (max - min);
                        }
                    }
                    break;
                case Microsoft.Office.Interop.Excel.XlAxisType.xlSeriesAxis:
                    break;
                //Debug.Print("暂时不知道这是什么坐标轴")
            }
            return PositionInChartByValue;
        }

        #endregion

    }



    /// <summary>
    /// 指定的单元格在Excel的Worksheet中的位置。左上角第一个单元格的行号值为1，列号值为1。
    /// 可以通过判断其行号或者列号是否为0来判断此类实例是否有赋值。
    /// </summary>
    /// <remarks></remarks>
    public struct CellAddress
    {

        /// <summary>
        /// 单元格在Excel的Worksheet中的行号，左上角第一个单元格的行号值为1。
        /// 在64位的Office 2010中，一个worksheet中共有1048576行，即2^20行。
        /// </summary>
        public UInt32 RowNum;
        /// <summary>
        /// 单元格在Excel的Worksheet中的列号，左上角第一个单元格的列号值为1。
        /// 在64位的Office 2010中，一个worksheet中共有16384列（列号为XFD），即2^14列。
        /// </summary>
        public UInt16 ColNum;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="RowNum_">指定的单元格在Excel的Worksheet中的行号，左上角第一个单元格的行号值为1。</param>
        /// <param name="ColNum_">指定的单元格在Excel的Worksheet中的列号，左上角第一个单元格的列号值为1。</param>
        public CellAddress(UInt32 RowNum_, UInt16 ColNum_)
        {
            RowNum = RowNum_;
            ColNum = ColNum_;
        }
    }

}
