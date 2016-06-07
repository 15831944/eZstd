Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel

Public Class ExcelFunction

#Region "  ---  Range到数组的转换"
    ''' <summary>
    ''' 将Excel的Range对象的数据转换为指定数据类型的一维向量,
    ''' Range.Value返回一个二维的表格，此函数将其数据按列行拼接为一维数组。
    ''' （即按(0,0),(0,1),(1,0),(1,1),(2,0)...的顺序排列）
    ''' </summary>
    ''' <param name="rg">用于提取数据的Range对象</param>
    ''' <returns>返回一个指定类型的一维向量，如“Single()”</returns>
    ''' <remarks>直接用Range.Value来返回的数据，其类型只能是Object，
    ''' 而其中的数据是一个元素类型为Object的二维数据（即使此Range对象只有一行或者一列）。
    ''' 所以要进行显式的转换，将其转换为指定类型的向量或者二维数组，以便于后面的数据操作。</remarks>
    Public Shared Function ConvertRangeDataToVector(Of T)(ByVal rg As Excel.Range) As T()
        'Range中的数据，这是以向量的形式给出的，其第一个元素的下标值很有可能是1，而不是0
        Dim RangeData As Object(,) = rg.Value
        Dim elementCount As Integer = RangeData.Length
        '
        Dim Value_Out(0 To elementCount - 1) As T
        '获取输入的数据类型
        Dim DestiType As Type = GetType(T)
        Dim TC As TypeCode = Type.GetTypeCode(DestiType)
        '判断此类型的值
        Select Case TC
            Case TypeCode.DateTime
                Dim i As Integer = 0
                For Each V As Object In RangeData
                    Try
                        Value_Out(i) = CType(V, T)
                    Catch ex As Exception
                        'Debug.Print("数据：" & V.ToString & " 转换为日期出错！将其处理为日期的初始值。" _
                        '                & vbCrLf & ex.Message)
                        '如果输入的数据为double类型，则将其转换为等效的Date
                        Dim O As Object = Date.FromOADate(V)
                        Value_Out(i) = CType(O, T)
                    Finally
                        i += 1
                    End Try
                Next
            Case Else
                Dim i As Integer = 0
                For Each V As Object In RangeData
                    Value_Out(i) = V
                    i += 1
                Next
        End Select
        Return Value_Out
    End Function

    ''' <summary>
    ''' 将Excel的Range对象的数据转换为指定数据类型的二维数组
    ''' </summary>
    ''' <param name="rg">用于提取数据的Range对象</param>
    ''' <returns>返回一个指定类型的二维数组，如“Single(,)”</returns>
    ''' <remarks>直接用Range.Value来返回的数据，其类型只能是Object，
    ''' 而其中的数据是一个元素类型为Object的二维数据（即使此Range对象只有一行或者一列）。
    ''' 所以要进行显式的转换，将其转换为指定类型的向量或者二维数组，以便于后面的数据操作。</remarks>
    Public Shared Function ConvertRangeDataToMatrix(Of T)(ByVal rg As Excel.Range) As T(,)

        Dim RangeData As Object(,) = rg.Value
        '由Range.Value返回的二维数组的第一个元素的下标值，这里应该为1，而不是0
        Dim LB_Range As Byte = LBound(RangeData)
        '
        Dim intRowsCount As Integer = UBound(RangeData, 1) - LB_Range + 1
        Dim intColumnsCount As Integer = UBound(RangeData, 2) - LB_Range + 1
        '
        Dim OutputMatrix(0 To intRowsCount - 1, 0 To intColumnsCount - 1) As T
        '
        Select Case Type.GetTypeCode(GetType(T))
            Case TypeCode.DateTime
                For row As Integer = 0 To intRowsCount - 1
                    For col As Integer = 0 To intColumnsCount - 1
                        Try
                            OutputMatrix(row, col) = CType(RangeData(row + LB_Range, col + LB_Range), T)
                        Catch ex As Exception
                            MessageBox.Show("数据：" & RangeData(row + LB_Range, col + LB_Range).ToString & " 转换为日期出错！将其处理为日期的初始值。" _
                                & vbCrLf & ex.Message)
                        End Try
                    Next
                Next
            Case Else
                For row As Integer = 0 To intRowsCount - 1
                    For col As Integer = 0 To intColumnsCount - 1
                        OutputMatrix(row, col) = RangeData(row + LB_Range, col + LB_Range)
                    Next
                Next
        End Select
        Return OutputMatrix
    End Function
#End Region

    ''' <summary>
    ''' 将Excel表中的列的数值编号转换为对应的字符
    ''' </summary>
    ''' <param name="ColNum">Excel中指定列的数值序号</param>
    ''' <returns>以字母序号的形式返回指定列的列号</returns>
    ''' <remarks>1对应A；26对应Z；27对应AA</remarks>
    Public Shared Function ConvertColumnNumberToString(ByVal ColNum As Integer) As String
        ' 关键算法就是：连续除以基，直至商为0，从低到高记录余数！
        ' 其中value必须是十进制表示的数值
        'intLetterIndex的位数为digits=fix(log(value)/log(26))+1
        '本来算法很简单，但是要解决一个问题：当value为26时，其26进制数为[1 0]，这样的话，
        '以此为下标索引其下面的strAlphabetic时就会出错，因为下标0无法索引。实际上，这种特殊情况下，应该让所得的结果成为[26]，才能索引到字母Z。
        '处理的方法就是，当所得余数remain为零时，就将其改为26，然后将对应的商的值减1.
        If ColNum < 1 Then MsgBox("列数不能小于1")
        Dim intLetterIndex As New List(Of Byte)
        '
        Dim quotient As Integer          '商
        Dim remain As Byte                '余数
        '
        Dim i As Byte = 0
        Do
            quotient = Fix(ColNum / 26)          '商
            remain = ColNum Mod 26                  '余数
            If remain = 0 Then
                intLetterIndex.Add(26)
                quotient = quotient - 1
            Else
                intLetterIndex.Add(remain)

            End If
            i = i + 1
            ColNum = quotient
        Loop Until quotient = 0
        Dim alphabetic As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim ans As String = ""
        For i = 0 To intLetterIndex.Count - 1
            ans = alphabetic.Chars(intLetterIndex(i) - 1) & ans
        Next
        Return ans
    End Function

    ''' <summary>
    ''' 将Excel表中的字符编号转换为对应的数值
    ''' </summary>
    ''' <param name="colString">以A1形式表示的列的字母序号，不区分大小写</param>
    ''' <returns>以整数的形式返回指定列的数值编号，A列对应数值1</returns>
    ''' <remarks></remarks>
    Public Shared Function ConvertColumnStringToNumber(ByVal colString As String) As Integer
        colString = colString.ToUpper
        Dim ASC_A As Byte = Asc("A")
        Dim ans As Integer = 0
        For i As Byte = 0 To colString.Length - 1
            Dim Chr As Char = colString.Substring(i, 1)
            ans = ans + (Asc(Chr) - ASC_A + 1) * Math.Pow(26, colString.Length - i - 1)
        Next
        Return ans
    End Function

    ''' <summary>
    ''' 获取指定Range范围中的右下角的那一个单元格
    ''' </summary>
    ''' <param name="RangeForSearch">要进行搜索的Range区域</param>
    ''' <returns>指定Range区域中的左下角的单元格</returns>
    ''' <remarks></remarks>
    Public Shared Function GetBottomRightCell(ByVal RangeForSearch As Excel.Range) As Excel.Range
        Dim RowsCount As Integer, ColsCount As Integer
        Dim LeftTopCell As Excel.Range = RangeForSearch.Cells(1, 1)
        With RangeForSearch
            RowsCount = .Rows.Count
            ColsCount = .Columns.Count
        End With
        With RangeForSearch.Worksheet
            Return .Cells(LeftTopCell.Row + RowsCount - 1, LeftTopCell.Column + ColsCount - 1)
        End With
    End Function

#Region "  ---  工作簿或工作表的匹配"

    ''' <summary>
    ''' 比较两个工作表是否相同。
    ''' 判断的标准：先判断工作表的名称是否相同，如果相同，再判断工作表所属的工作簿的路径是否相同，
    ''' 如果二者都相同，则认为这两个工作表是同一个工作表
    ''' </summary>
    ''' <param name="sheet1">进行比较的第1个工作表对象</param>
    ''' <param name="sheet2">进行比较的第2个工作表对象</param>
    ''' <returns></returns>
    ''' <remarks>不用 blnSheetsMatched = sheet1.Equals(sheet2)，是因为这种方法并不能正确地返回True。</remarks>
    Public Shared Function SheetCompare(ByVal sheet1 As Excel.Worksheet, ByVal sheet2 As Excel.Worksheet) As Boolean
        Dim blnSheetsMatched As Boolean = False
        '先比较工作表名称
        If String.Compare(sheet1.Name, sheet2.Name) = 0 Then
            Dim wb1 As Excel.Workbook = sheet1.Parent
            Dim wb2 As Excel.Workbook = sheet2.Parent
            '再比较工作表所在工作簿的路径
            If String.Compare(wb1.FullName, wb2.FullName) = 0 Then
                blnSheetsMatched = True
            End If
        End If
        Return blnSheetsMatched
    End Function

    ''' <summary>
    ''' 检测工作簿是否已经在指定的Application程序中打开。
    ''' 如果最后此工作簿在程序中被打开（已经打开或者后期打开），则返回对应的Workbook对象，否则返回Nothing。
    ''' 这种比较方法的好处是不会额外去打开已经打开过了的工作簿。
    ''' </summary>
    ''' <param name="wkbkPath">进行检测的工作簿</param>
    ''' <param name="Application">检测工作簿所在的Application程序</param>
    ''' <param name="blnFileHasBeenOpened">指示此Excel工作簿是否已经在此Application中被打开</param>
    ''' <param name="OpenIfNotOpened">如果此Excel工作簿并没有在此Application程序中打开，是否要将其打开。</param>
    ''' <param name="OpenByReadOnly">是否以只读方式打开</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MatchOpenedWkbk(ByVal wkbkPath As String, _
                                           ByVal Application As Excel.Application, _
                                           Optional ByRef blnFileHasBeenOpened As Boolean = False, _
                                           Optional ByVal OpenIfNotOpened As Boolean = False, _
                                           Optional ByVal OpenByReadOnly As Boolean = True _
                                           ) As Excel.Workbook
        Dim wkbk As Excel.Workbook = Nothing
        If Application IsNot Nothing Then
            '进行具体的检测
            If File.Exists(wkbkPath) Then   '此工作簿存在
                '如果此工作簿已经打开
                For Each WkbkOpened As Excel.Workbook In Application.Workbooks
                    If String.Compare(WkbkOpened.FullName, wkbkPath, True) = 0 Then
                        wkbk = WkbkOpened
                        blnFileHasBeenOpened = True
                        Exit For
                    End If
                Next

                '如果此工作簿还没有在主程序中打开，则将此工作簿打开
                If Not blnFileHasBeenOpened Then
                    If OpenIfNotOpened Then
                        wkbk = Application.Workbooks.Open(Filename:=wkbkPath, UpdateLinks:=False, [ReadOnly]:=OpenByReadOnly)
                    End If
                End If
            End If
        End If
        '返回结果
        Return wkbk
    End Function

    ''' <summary>
    ''' 检测指定工作簿内是否有指定的工作表，如果存在，则返回对应的工作表对象，否则返回Nothing
    ''' </summary>
    ''' <param name="wkbk">进行检测的工作簿对象</param>
    ''' <param name="sheetName">进行检测的工作表的名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MatchWorksheet(ByVal wkbk As Excel.Workbook, ByVal sheetName As String) As Excel.Worksheet
        '工作表是否存在
        Dim ValidWorksheet As Excel.Worksheet = Nothing
        For Each sht As Excel.Worksheet In wkbk.Worksheets
            If String.Compare(sht.Name, sheetName) = 0 Then
                ValidWorksheet = sht
                Return ValidWorksheet
            End If
        Next
        '返回检测结果
        Return ValidWorksheet
    End Function

#End Region

#Region "  ---  几何绘图"


    ''' <summary>
    ''' 将任意形状以指定的值定位在Chart的某一坐标轴中。
    ''' </summary>
    ''' <param name="ShapeToLocate">要进行定位的形状</param>
    ''' <param name="Ax">此形状将要定位的轴</param>
    ''' <param name="Value">此形状在Chart中所处的值</param>
    ''' <param name="percent">将形状按指定的百分比的宽度或者高度的部位定位到坐标轴的指定值的位置。
    ''' 如果其值设定为0，则表示此形状的左端（或上端）定位在设定的位置处，
    ''' 如果其值为100，则表示此形状的右端（或下端）定位在设置的位置处。</param>
    ''' <remarks></remarks>
    Public Overloads Shared Sub setPositionInChart(ByVal ShapeToLocate As Excel.Shape, ByVal Ax As Excel.Axis, _
                                  ByVal Value As Double, Optional ByVal percent As Double = 0)
        Dim cht As Excel.Chart = DirectCast(Ax.Parent, Excel.Chart)
        If cht IsNot Nothing Then
            'Try          '先考察形状是否是在Chart之中

            '    ShapeToLocate = cht.Shapes.Item(ShapeToLocate.Name)
            'Catch ex As Exception           '如果形状不在Chart中，则将形状复制进Chart，并将原形状删除
            '    ShapeToLocate.Copy()
            '    cht.Paste()
            '    ShapeToLocate.Delete()
            '    ShapeToLocate = cht.Shapes.Item(cht.Shapes.Count)
            'End Try
            '
            Select Case Ax.Type

                Case Excel.XlAxisType.xlCategory                '横向X轴
                    Dim PositionInChartByValue As Double = GetPositionInChartByValue(Ax, Value)
                    With ShapeToLocate
                        .Left = PositionInChartByValue - percent * .Width
                    End With

                Case Excel.XlAxisType.xlValue                   '竖向Y轴
                    Dim PositionInChartByValue As Double = GetPositionInChartByValue(Ax, Value)
                    With ShapeToLocate
                        .Top = PositionInChartByValue - percent * .Height
                    End With
                Case Excel.XlAxisType.xlSeriesAxis
                    MessageBox.Show("暂时不知道这是什么坐标轴", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Select
        End If
    End Sub

    ''' <summary>
    ''' 将一组形状以指定的值定位在Chart的某一坐标轴中。
    ''' </summary>
    ''' <param name="ShapesToLocate">要进行定位的形状</param>
    ''' <param name="Ax">此形状将要定位的轴</param>
    ''' <param name="Values">此形状在Chart中所处的值</param>
    ''' <param name="percents">将形状按指定的百分比的宽度或者高度的部位定位到坐标轴的指定值的位置。
    ''' 如果其值设定为0，则表示此形状的左端（或上端）定位在设定的位置处，
    ''' 如果其值为100，则表示此形状的右端（或下端）定位在设置的位置处。</param>
    ''' <remarks></remarks>
    Public Overloads Shared Sub setPositionInChart(ByVal Ax As Excel.Axis, ByVal ShapesToLocate As Excel.Shape(), _
                                  ByVal Values As Double(), Optional ByVal Percents As Double() = Nothing)
        ' ------------------------------------------------------
        '检查输入的数组中的元素个数是否相同
        Dim Count As UShort = ShapesToLocate.Length
        If Values.Length <> Count Then
            MessageBox.Show("输入数组中的元素个数不相同。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Percents IsNot Nothing Then
            If Percents.Count <> 1 And Percents.Length <> Count Then
                MessageBox.Show("输入数组中的元素个数不相同。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        ' ------------------------------------------------------
        Dim cht As Excel.Chart = DirectCast(Ax.Parent, Excel.Chart)
        '
        Dim max As Double = Ax.MaximumScale
        Dim min As Double = Ax.MinimumScale
        '
        Dim PlotA As Excel.PlotArea = cht.PlotArea
        ' ------------------------------------------------------
        Dim shp As Excel.Shape
        Dim Value As Double
        Dim Percent As Double = Percents(0)
        Dim PositionInChartByValue As Double
        ' ------------------------------------------------------

        Select Case Ax.Type

            Case Excel.XlAxisType.xlCategory                '横向X轴


            Case Excel.XlAxisType.xlValue                   '竖向Y轴
                If Ax.ReversePlotOrder = False Then         '顺序刻度值，说明Y轴数据为下边小上边大
                    For i As UShort = 0 To Count - 1
                        shp = ShapesToLocate(i)
                        Value = Values(i)
                        If Percents.Count > 1 Then Percent = Percents(i)
                        With PlotA
                            PositionInChartByValue = .InsideTop + .InsideHeight * (max - Value) / (max - min)
                        End With
                        With shp
                            .Top = PositionInChartByValue - Percent * .Width
                        End With
                    Next
                Else                                         '逆序刻度值，说明Y轴数据为上边小下边大
                    For i As UShort = 0 To Count - 1
                        shp = ShapesToLocate(i)
                        Value = Values(i)
                        If Percents.Count > 1 Then Percent = Percents(i)
                        With PlotA
                            PositionInChartByValue = .InsideTop + .InsideHeight * (Value - min) / (max - min)
                        End With
                        With shp
                            .Top = PositionInChartByValue - Percent * .Width
                        End With
                    Next
                End If

            Case Excel.XlAxisType.xlSeriesAxis
                MessageBox.Show("暂时不知道这是什么坐标轴", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Select

    End Sub

    ''' <summary>
    ''' 根据在坐标轴中的值，来返回这个值在Chart中的几何位置
    ''' </summary>
    ''' <param name="Ax"></param>
    ''' <param name="Value"></param>
    ''' <returns>如果Ax是一个水平X轴，则返回的是坐标轴Ax中的值Value在Chart中的Left值；
    ''' 如果Ax是一个竖向Y轴，则返回的是坐标轴Ax中的值Value在Chart中的Top值。</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetPositionInChartByValue(ByVal Ax As Excel.Axis, ByVal Value As Double) As Double

        Dim PositionInChartByValue As Double
        Dim cht As Excel.Chart = DirectCast(Ax.Parent, Excel.Chart)
        '
        Dim max As Double = Ax.MaximumScale
        Dim min As Double = Ax.MinimumScale
        '
        Dim PlotA As Excel.PlotArea = cht.PlotArea
        Select Case Ax.Type

            Case Excel.XlAxisType.xlCategory                '横向X轴
                Dim PositionInPlot As Double
                If Ax.ReversePlotOrder = False Then         '正向分类，说明X轴数据为左边小右边大
                    PositionInPlot = PlotA.InsideWidth * (Value - min) / (max - min)
                Else                                        '逆序类别，说明X轴数据为左边大右边小
                    PositionInPlot = PlotA.InsideWidth * (max - Value) / (max - min)
                End If
                PositionInChartByValue = PlotA.InsideLeft + PositionInPlot

            Case Excel.XlAxisType.xlValue                   '竖向Y轴
                Dim PositionInPlot As Double
                If Ax.ReversePlotOrder = False Then         '顺序刻度值，说明Y轴数据为下边小上边大
                    PositionInPlot = PlotA.InsideHeight * (max - Value) / (max - min)
                Else                                        '逆序刻度值，说明Y轴数据为上边小下边大
                    PositionInPlot = PlotA.InsideHeight * (Value - min) / (max - min)
                End If
                PositionInChartByValue = PlotA.InsideTop + PositionInPlot
            Case Excel.XlAxisType.xlSeriesAxis
                'Debug.Print("暂时不知道这是什么坐标轴")
        End Select
        Return PositionInChartByValue
    End Function

    ''' <summary>
    ''' 根据一组形状在某一坐标轴中的值，来返回这些值在Chart中的几何位置
    ''' </summary>
    ''' <param name="Ax"></param>
    ''' <param name="Values">要在坐标轴中进行定位的多个形状在此坐标轴中的数值</param>
    ''' <returns>如果Ax是一个水平X轴，则返回的是坐标轴Ax中的值Value在Chart中的Left值；
    ''' 如果Ax是一个竖向Y轴，则返回的是坐标轴Ax中的值Value在Chart中的Top值。</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetPositionInChartByValue(ByVal Ax As Excel.Axis, ByVal Values As Double()) As Double()
        Dim Count As UShort = Values.Length
        Dim PositionInChartByValue(0 To Count - 1) As Double
        ' --------------------------------------------------
        Dim cht As Excel.Chart = DirectCast(Ax.Parent, Excel.Chart)
        '
        Dim max As Double = Ax.MaximumScale
        Dim min As Double = Ax.MinimumScale
        Dim Value As Double
        '
        Dim PlotA As Excel.PlotArea = cht.PlotArea
        With PlotA
            Select Case Ax.Type

                Case Excel.XlAxisType.xlCategory                '横向X轴
                    If Ax.ReversePlotOrder = False Then         '正向分类，说明X轴数据为左边小右边大
                        For i As UShort = 0 To Count - 1
                            Value = Values(i)
                            PositionInChartByValue(i) = PlotA.InsideLeft + PlotA.InsideWidth * (Value - min) / (max - min)
                        Next
                    Else                                        '逆序类别，说明X轴数据为左边大右边小
                        For i As UShort = 0 To Count - 1
                            Value = Values(i)
                            PositionInChartByValue(i) = PlotA.InsideLeft + PlotA.InsideWidth * (max - Value) / (max - min)
                        Next
                    End If

                Case Excel.XlAxisType.xlValue                   '竖向Y轴
                    If Ax.ReversePlotOrder = False Then         '顺序刻度值，说明Y轴数据为下边小上边大
                        For i As UShort = 0 To Count - 1
                            Value = Values(i)
                            PositionInChartByValue(i) = PlotA.InsideTop + PlotA.InsideHeight * (max - Value) / (max - min)
                        Next
                    Else                                        '逆序刻度值，说明Y轴数据为上边小下边大
                        For i As UShort = 0 To Count - 1
                            Value = Values(i)
                            PositionInChartByValue(i) = PlotA.InsideTop + PlotA.InsideHeight * (Value - min) / (max - min)
                        Next
                    End If
                Case Excel.XlAxisType.xlSeriesAxis
                    'Debug.Print("暂时不知道这是什么坐标轴")
            End Select
        End With
        Return PositionInChartByValue
    End Function

#End Region

End Class



''' <summary>
''' 指定的单元格在Excel的Worksheet中的位置。左上角第一个单元格的行号值为1，列号值为1。
''' 可以通过判断其行号或者列号是否为0来判断此类实例是否有赋值。
''' </summary>
''' <remarks></remarks>
Public Structure CellAddress

    ''' <summary>
    ''' 单元格在Excel的Worksheet中的行号，左上角第一个单元格的行号值为1。
    ''' 在64位的Office 2010中，一个worksheet中共有1048576行，即2^20行。
    ''' </summary>
    Public RowNum As UInt32
    ''' <summary>
    ''' 单元格在Excel的Worksheet中的列号，左上角第一个单元格的列号值为1。
    ''' 在64位的Office 2010中，一个worksheet中共有16384列（列号为XFD），即2^14列。
    ''' </summary>
    Public ColNum As UInt16

    ''' <summary>
    ''' 构造函数
    ''' </summary>
    ''' <param name="RowNum_">指定的单元格在Excel的Worksheet中的行号，左上角第一个单元格的行号值为1。</param>
    ''' <param name="ColNum_">指定的单元格在Excel的Worksheet中的列号，左上角第一个单元格的列号值为1。</param>
    Public Sub New(RowNum_ As Integer, ColNum_ As Short)
        With Me
            .RowNum = RowNum_
            .ColNum = ColNum_
        End With
    End Sub
End Structure
