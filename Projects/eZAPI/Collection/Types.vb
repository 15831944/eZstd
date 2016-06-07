Imports System.Runtime.InteropServices

#Region "  ---  Windows消息"

''' <summary>
''' The MSG structure contains message information from a thread's message queue.
''' Alternative Managed API: Use the <see cref="System.Windows.Forms.Message"/> or System.Windows.Interop.MSG struct.
''' </summary>
''' <remarks></remarks>
<StructLayout(LayoutKind.Sequential)> _
Public Structure MSG
    Public hwnd As IntPtr
    Public message As Integer
    Public wParam As IntPtr
    Public lParam As IntPtr
    Public time As Integer
    Public pt As Point
End Structure

''' <summary>
''' This is a different data structure from <see cref="System.Windows.Forms.Message"/>
''' </summary>
''' <remarks></remarks>
<StructLayout(LayoutKind.Sequential)> _
Public Structure NativeMessage
    Public handle As IntPtr
    Public msg As UInteger
    Public wParam As IntPtr
    Public lParam As IntPtr
    Public time As UInteger
    Public pt As System.Drawing.Point
End Structure

#End Region

''' <summary>
''' 这是windows广泛采用的一种数据结构，通常作为参数传递给许多api函数。
''' RECT结构表示一个矩形区域，left和top字段描叙了矩形第一个角（通常是左上角），
''' right和bottom字段描叙了矩形的第二个角（通常是右下角）。这两个位置决定了矩形的大小与位置。
''' 这些字段采用的单位及坐标系统取决于当前的有效缩放比例、准备表示的对象以及准备调用的api函数。
''' 并不要求bottom字段的绝对值大于top字段，而且也可以为负数。
''' </summary>
''' <remarks>由right及bottom字段指定的点通常不是矩形的一部分；矩形对象描叙的是个空矩形（其中不包含像素）；
''' RECT结构要求按引用传递给windows函数，不要试图使用ByVal </remarks>
<StructLayout(LayoutKind.Sequential)>
Public Structure RECT
    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer
End Structure

#Region "  ---  钩子Hook"

<StructLayout(LayoutKind.Sequential)> _
Public Structure KBDLLHOOKSTRUCT
    Public vkCode As UInt32
    Public scanCode As UInt32
    Public flags As KBDLLHOOKSTRUCTFlags
    Public time As UInt32
    Public dwExtraInfo As UIntPtr
End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure MSLLHOOKSTRUCT
    Public pt As Point
    Public mouseData As Int32
    Public flags As MSLLHOOKSTRUCTFlags
    Public time As Int32
    Public dwExtraInfo As UIntPtr
End Structure

#End Region
