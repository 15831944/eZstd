Imports System.Runtime.InteropServices

Public Class APIDll

    ''' <summary>
    ''' 将指定的文件夹添加到此程序的DLL文件的搜索路径中.
    ''' adds a directory to the search path used to locate DLLs for the application.
    ''' </summary>
    ''' <param name="lpPathName">要添加的文件夹路径</param>
    ''' <remarks>Pretty straight-forward to use. Obviously, is usually going to be called before calling LoadLibraryEx().
    ''' 另外,在PInvoke中,只有SetDllDirectory这个函数,但是它的真实的名称是SetDllDirectoryW.</remarks>
    Public Declare Function SetDllDirectoryW Lib "kernel32.dll" (ByVal lpPathName As String) As Boolean

    ''' <summary>
    ''' 装载指定的动态链接库，并为当前进程把它映射到地址空间。一旦载入，就可以访问库内保存的资源。一旦不需要，用FreeLibrary函数释放DLL
    ''' </summary>
    ''' <param name="lpFileName">指定要载入的动态链接库的名称。采用与CreateProcess函数的lpCommandLine参数指定的同样的搜索顺序</param>
    ''' <param name="hReservedNull">未用，设为零</param>
    ''' <param name="dwFlags"></param>
    ''' <returns>成功则返回库模块的句柄，零表示失败。会设置GetLastError</returns>
    ''' <remarks>参考 http://www.pinvoke.net/default.aspx/kernel32/LoadLibraryEx.html .
    ''' If you only want to load resources from the library, specify LoadLibraryFlags.LoadLibraryAsDatafile as dwFlags. 
    ''' In this case, nothing is done to execute or prepare to execute the mapped file.</remarks>
    <DllImport("kernel32.dll")> _
    Private Shared Function LoadLibraryEx(lpFileName As String, hReservedNull As IntPtr, dwFlags As LoadLibraryFlags) As IntPtr
    End Function


End Class
