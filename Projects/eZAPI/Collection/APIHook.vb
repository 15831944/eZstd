Imports System.Runtime.InteropServices
Public Class APIHook

#Region "SetWindowsHookEx 安装钩子"

    ''' <summary>
    ''' Represents the method called when a hook catches a monitored event.
    ''' </summary>
    Public Delegate Function HookProc(ByVal code As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function SetWindowsHookEx(ByVal hookType As HookType, ByVal lpfn As HookProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
    End Function

#End Region

    ''' <summary>
    ''' 在安装钩子后要记得卸载钩子
    ''' </summary>
    ''' <param name="hhk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function UnhookWindowsHookEx(ByVal hhk As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#Region "  --- 传递到下一个Hook"

    ''' <summary>
    ''' 将hook信息传递到链表中下一个hook处理过程
    ''' </summary>
    ''' <param name="hhk">注：hhk is ignored, so you can use IntPtr.Zero</param>
    ''' <param name="nCode"></param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll")> _
    Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                          ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function


    ''' <summary>
    ''' CallNextHookEx 鼠标 将hook信息传递到链表中下一个hook处理过程。
    ''' overload for use with LowLevelMouseProc
    ''' </summary>
    ''' <param name="hhk"></param>
    ''' <param name="nCode"></param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll")> _
    Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                          ByVal wParam As WindowsMessages, _
                                          <[In]()> ByVal lParam As MSLLHOOKSTRUCT) As IntPtr
    End Function

    ''' <summary>
    ''' CallNextHookEx 键盘 将hook信息传递到链表中下一个hook处理过程。
    ''' overload for use with LowLevelKeyboardProc
    ''' </summary>
    ''' <param name="hhk"></param>
    ''' <param name="nCode"></param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll")> _
    Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                          ByVal wParam As WindowsMessages, _
                                          <[In]()> ByVal lParam As KBDLLHOOKSTRUCT) As IntPtr
    End Function

#End Region

End Class
