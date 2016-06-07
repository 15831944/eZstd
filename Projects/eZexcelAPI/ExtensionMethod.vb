Imports System.Runtime.CompilerServices
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Module ExcelExtension    '扩展方法只能在模块中声明。
    Public Enum CornerIndex
        UpLeft
        UpRight
        BottomLeft
        BottomRight
    End Enum

    ''' <summary>
    ''' 返回Range对象范围中的最右下角点的那个单元格对象
    ''' </summary>
    ''' <param name="SourceRange">对于对Range.Areas.Item(1)中的单元格区域进行操作</param>
    ''' <param name="Corner">要返回哪一个角落的单元格</param>
    ''' <returns></returns>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Ex_CornerCell(ByVal SourceRange As Excel.Range, Optional ByVal Corner As CornerIndex = CornerIndex.BottomRight) As Excel.Range
        Dim myCornerCell As Range = Nothing
        '
        SourceRange = SourceRange.Areas.Item(1)
        Dim LeftTopCell As Excel.Range = SourceRange.Cells(1, 1)
        '
        Select Case Corner
            Case CornerIndex.BottomRight
                With SourceRange.Worksheet
                    myCornerCell = .Cells(LeftTopCell.Row + SourceRange.Rows.Count - 1, LeftTopCell.Column + SourceRange.Columns.Count - 1)
                End With
            Case CornerIndex.UpRight
                With SourceRange.Worksheet
                    myCornerCell = .Cells(LeftTopCell.Row, LeftTopCell.Column + SourceRange.Columns.Count - 1)
                End With
            Case CornerIndex.BottomLeft
                With SourceRange.Worksheet
                    myCornerCell = .Cells(LeftTopCell.Row + SourceRange.Rows.Count - 1, LeftTopCell.Column)
                End With
            Case CornerIndex.UpLeft
                myCornerCell = LeftTopCell
        End Select
        Return myCornerCell
    End Function


    ''' <summary>
    ''' 收缩Range.Areas.Item(1)的单元格范围。
    ''' 在选择一个单元格范围时，有时为了界面操作简单，往往会选择一整列或者一整行，但是并不是要对基本所有的单元格进行操作，
    ''' 而只需要操作其中有数据的那些区域。此函数即是将选择的整行或者整列的单元格收缩到有数据的范围内。
    ''' </summary>
    ''' <param name="rg"></param>
    ''' <returns></returns>
    ''' <remarks>在选择一个单元格范围时，有时为了界面操作简单，往往会选择一整列或者一整行，但是并不是要对基本所有的单元格进行操作，
    ''' 而只需要操作其中有数据的那些区域。此函数即是将选择的整行或者整列的单元格收缩到有数据的范围内。</remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function Ex_ShrinkedRange(ByVal rg As Range) As Range
        rg = rg.Areas.Item(1)
        Dim ColCount As Integer = rg.Columns.Count
        Dim RowCount As Integer = rg.Rows.Count
        '
        Dim BottomRightCell As Range = rg.Ex_CornerCell(CornerIndex.BottomRight)
        Dim UsedBottomRightCell As Range = rg.Worksheet.UsedRange.Ex_CornerCell(CornerIndex.BottomRight)

        '  将最下面的单元格收缩到UsedRange的最下面的位置
        If RowCount = 2 ^ 20 And ColCount = 2 ^ 14 Then  ' 说明选择了整个表格
            BottomRightCell = UsedBottomRightCell
        ElseIf RowCount = 2 ^ 20 And ColCount < 2 ^ 14 Then  ' 说明选择了整列
            BottomRightCell = BottomRightCell.Offset(UsedBottomRightCell.Row - 2 ^ 20, 0)
        ElseIf RowCount < 2 ^ 20 And ColCount = 2 ^ 14 Then  ' 说明选择了整行
            BottomRightCell = BottomRightCell.Offset(0, UsedBottomRightCell.Column - 2 ^ 14)
            ' Else  ' 说明选择了一个有限的范围
        End If
        With rg.Worksheet
            Return .Range(rg.Cells(1, 1), BottomRightCell)
        End With
    End Function
End Module

