Imports System.Runtime.InteropServices
Public Class APIMessage

    Public Delegate Function WndProcDelegate(ByVal hWnd As IntPtr,
                                      ByVal msg As UInteger, ByVal wParam As IntPtr,
                                      ByVal lParam As IntPtr) As IntPtr
    ''' <summary>
    ''' 将指定消息信息传送给指定的窗口过程。
    ''' CallWindowProc 是发送某个消息给某个处理函数(包括自定义消息处理函数),
    ''' 而DefWindowProc 是发送某个消息给Windows默认的消息处理函数。
    ''' </summary>
    ''' <param name="lpPrevWndFunc">窗口消息处理函数指针(函数名)</param>
    ''' <param name="hWnd">接受窗体句柄.</param>
    ''' <param name="Msg">指定消息类型</param>
    ''' <param name="wParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关.</param>
    ''' <param name="lParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关。</param>
    ''' <returns>返回值指定了消息处理结果，它与发送的消息有关。</returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll")>
    Public Shared Function CallWindowProc(lpPrevWndFunc As WndProcDelegate,
                                           hWnd As IntPtr, Msg As UInteger,
                                           wParam As IntPtr, lParam As IntPtr) As IntPtr
        'CallWindowProc与DefWindowProc关键的区别在于：
        'CallWindowProc 是发送某个消息给某个处理函数(包括自定义消息处理函数),  而DefWindowProc 是发送某个消息给Windows默认的消息处理函数.
        '如果理解了他们这一点的区别, 就能明白,
        'CalWindowPro(DefWindowProc, hWnd,Msg,wParam, lParam) 和 DefWindowProc (hWnd,Msg ,wParam,lParam) 二者此时执行效果一样

    End Function

    ''' <summary>
    ''' 让Windows的默认消息处理函数处理消息。
    ''' 该函数调用默认的窗口过程来为应用程序没有处理的任何窗口消息提供默认的处理。
    ''' 该函数确保每一个消息得到处理。调用DefWindowProc函数时使用窗口过程接收的相同参数。
    ''' </summary>
    ''' <param name="hWnd"></param>
    ''' <param name="uMsg">指定消息类型</param>
    ''' <param name="wParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关.</param>
    ''' <param name="lParam">指定其余的、消息特定的信息。该参数的内容与Msg参数值有关。</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll")>
    Public Shared Function DefWindowProc(ByVal hWnd As IntPtr,
                                         ByVal uMsg As WindowsMessages, ByVal wParam As IntPtr,
                                         ByVal lParam As IntPtr) As IntPtr
        'CallWindowProc与DefWindowProc关键的区别在于：
        'CallWindowProc 是发送某个消息给某个处理函数(包括自定义消息处理函数),  而DefWindowProc 是发送某个消息给Windows默认的消息处理函数.
        '如果理解了他们这一点的区别, 就能明白,
        'CalWindowPro(DefWindowProc, hWnd,Msg,wParam, lParam) 和 DefWindowProc (hWnd,Msg ,wParam,lParam) 二者此时执行效果一样

    End Function


#Region "  ---  发送消息"

    ''' <summary>
    ''' 调用一个窗口的窗口函数，将一条消息发给那个窗口。除非消息处理完毕，否则该函数不会返回。
    ''' SendMessageBynum， SendMessageByString是该函数的“类型安全”声明形式
    ''' </summary>
    ''' <param name="hWnd">要接收消息的那个窗口的句柄</param>
    ''' <param name="Msg">指定要发送的消息</param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns>返回值反映了此函数执行的结果.它是由接收消息的窗口的窗口函数返回的值。这个返回值取决于被发送的消息。</returns>
    ''' <remarks>如果接收消息的窗口是同一应用程序的一部分，那么这个窗口的窗口函数就被作为-个子程序马上被调用。
    ''' 如果接收消息的窗口是被另外的线程所创建的，那么窗口系统就切换到相应的线程并且调用相应的窗口函数，这条消息不放在目标应用程序队列中。
    ''' </remarks>
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr,
                                       ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    ''' <summary>
    ''' 将一条消息投递到指定窗口的消息队列。投递的消息会在Windows事件处理过程中得到处理。在那个时候，会随同投递的消息调用指定窗口的窗口函数。特别适合那些不需要立即处理的窗口消息的发送
    ''' </summary>
    ''' <param name="hWnd">接收消息的那个窗口的句柄。如设为HWND_BROADCAST，表示投递给系统中的所有顶级窗口。如设为零，表示投递一条线程消息（参考<see cref="PostThreadMessage"/>)</param>
    ''' <param name="Msg">消息标识符</param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function PostMessage(ByVal hWnd As IntPtr,
                                       ByVal Msg As UInteger, ByVal wParam As IntPtr,
                                       ByVal lParam As IntPtr) As Boolean
    End Function

    ''' <summary>
    ''' 将一条消息投递给应用程序。这条消息由应用程序的内部GetMessage循环获得，但不会传给一个特定的窗口
    ''' </summary>
    ''' <param name="idThread">用于接收消息的那个线程的标识符</param>
    ''' <param name="msg">消息标识符</param>
    ''' <param name="wParam"></param>
    ''' <param name="lParam"></param>
    ''' <returns>如消息投递成功，则返回TRUE（非零）。会设置GetLastError</returns>
    ''' <remarks></remarks>
    <DllImport("user32.dll", EntryPoint:="PostThreadMessageW", ExactSpelling:=True)>
    Public Shared Function PostThreadMessage(ByVal idThread As UInt32,
                                             ByVal msg As UInt32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Function

#End Region

#Region "  ---  获取消息"

    ''' <summary>
    ''' GetMessage不会提取属性其他线程或者程序的窗口的消息。
    ''' The GetMessage function retrieves a message from the calling thread's message queue and places it in the specified structure. This function can retrieve both messages associated with a specified window and thread messages posted via the PostThreadMessage function. 
    ''' The function retrieves messages that lie within a specified range of message values. 
    ''' GetMessage does not retrieve messages for windows that belong to other threads or applications.
    ''' </summary>
    ''' <param name="lpMsg">Points to an MSG structure that receives message information from the thread's message queue.</param>
    ''' <param name="hWnd">Identifies the window whose messages are to be retrieved. The value NULL means that GetMessage retrieves messages for any window that belongs to the calling thread and thread messages posted to the calling thread via PostThreadMessage.</param>
    ''' <param name="wMsgFilterMin">Specifies the integer value of the lowest message value to be retrieved.If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range filtering is performed).</param>
    ''' <param name="wMsgFilterMax">Specifies the integer value of the highest message value to be retrieved.If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range filtering is performed).</param>
    ''' <returns>If the function retrieves a message other than WM_QUIT, the return value is nonzero.
    ''' If the function retrieves the WM_QUIT message, the return value is zero.
    ''' If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle.
    ''' </returns>
    ''' <remarks>
    ''' 参考：http://www.pinvoke.net/default.aspx/user32/GetMessage.html
    ''' An application typically uses the return value to determine whether to end the main message loop and exit the program.
    ''' The GetMessage function only retrieves messages associated with the window identified by the hWnd parameter or any of its children as specified by the IsChild function, 
    ''' and within the range of message values given by the wMsgFilterMin and wMsgFilterMax parameters. 
    ''' GetMessage does not retrieve messages for windows that belong to other threads nor for threads other than the calling thread. 
    ''' Thread messages, posted by the PostThreadmessage function, have a message hWnd value of NULL. 
    ''' </remarks>
    <DllImport("user32.dll")>
    Public Shared Function GetMessage(
                                     ByRef lpMsg As MSG,
                                     ByVal hWnd As IntPtr,
                                     ByVal wMsgFilterMin As UInteger,
                                     ByVal wMsgFilterMax As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
        ' ' ----- 举例 ----------------
        '获取属于程序主线程的所有窗口的所有消息
        'while (GetMessage(&msg, NULL, 0, 0))
        '    {    
        '    if (!TranslatcAccelcrator(msg.hwnd, hAccelTable, &tnsg)>
        '    {
        '        TranslateMessage(&msg);
        '        DispatchMessage(&msg);
        '    }
        '}
        'Return msg.wParam
        '...

        'The GetMessage function only retrieves messages associated with the window identified by the hWnd parameter or any of its children as specified by the IsChild function, and within the range of message values given by the wMsgFilterMin and wMsgFilterMax parameters. If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the calling thread and thread messages posted to the calling thread via PostThreadMessage. GetMessage does not retrieve messages for windows that belong to other threads nor for threads other than the calling thread. Thread messages, posted by the PostThreadmessage function, have a message hWnd value of NULL. If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range filtering is performed).
    End Function

    ''' <summary>
    ''' 该函数查看应用程序的消息队列，如果其中有消息就将其放入lpMsg所指的结构中，
    ''' 与GetMessage函数不同，PeekMessage函数不会等到有消息放入队列时才返回。
    ''' </summary>
    ''' <param name="message">
    ''' 指向NativeMessage结构的指针，用来接收函数从Windows应用程序队列中取来的消息。
    ''' Do not use System.Windows.Forms.Message for the first argument - this is a different data structure.</param>
    ''' <param name="hWnd">指定其消息等检验的窗口</param>
    ''' <param name="wMsgFilterMin">指定待检消息号的最小值</param>
    ''' <param name="wMsgFilterMax">指定待检消息号的最大值</param>
    ''' <param name="wRemoveMsg">其值为下面二者之一：PM_MOREMOVE / PM_REMOVE</param>
    ''' <returns></returns>
    ''' <remarks>PeekMessage函数只检索与由hWnd指定的窗口或由IsChild函数指定的子窗口相关的, 范围在
    ''' wMsgFiltcrMin和wMsgFiherMax之间的消息。如果hWnd为NULL,那么PeckMessage检索属于当前
    ''' 调用线程的所有窗口的消息（不检索属于其他线程的窗口消息如果hWnd为-1,那么函数只返回把
    ''' hWnd 参数置为 NULL 的 PostAppMessage 函数送去的消息。如果 wMsgFilterMin 和 wMsgFilterMax 都
    ''' 是零.那么PeekMessagc返回所有可用消息，不再对消息进行范围上的过滤。
    ''' 用WM_KEYFIRST和WMJCEYLAST作为过滤范围可以检索到所有键盘消息。用
    ''' WM_MOUSEFIRST和WM_MOUSSLAST可检索到所有鼠标消息.
    ''' 返回值表明是否找到了消息。如果有消息可用则返回TRUE,否则返冋FALSE。
    ''' PeekMessage消息不会从队列中删除WM_ PA丨NT消息.该消息在被处理之前一直留在队列中。
    ''' 但是如果WM—PAINT消息中更新区域为NULL,则函数将删除该消息。</remarks>
    <DllImport("User32.dll", SetLastError:=True)>
    Public Shared Function PeekMessage(ByRef message As NativeMessage, ByVal hWnd As IntPtr,
                                       ByVal wMsgFilterMin As UInteger, ByVal wMsgFilterMax As UInteger, ByVal wRemoveMsg As PeekMessageParams) _
                                   As <MarshalAs(UnmanagedType.Bool)> Boolean

    End Function

#End Region

#Region "  ---  处理消息"

    ''' <summary>
    ''' 将虚拟键消息转换成字符信息。函数TranslateMessage并不修改由参数IpMsg指定的消息，它仅仅为由键盘驱动器产生出ASCII字符的键产生WM_CHAR消息。
    ''' 该函数按照下列方式将虚拟键消息转换成字符信息：
    ''' 1、WM KEYDOWN 与 WMJCEYUP 组合产生一个 WM—CHAR 或 WM_DEADCHAR 消息；
    ''' 2、WM_SYSKEYDOWN 与 WM—SYSKEYUP 组合产生一个 WM_SYSCHAR 或 WM_SYSDEADCHAR消息。
    ''' 字符消息被发送给应用程序消息队列.为了获取该消息.应用程序可调用GetMessage或PeekMessage.
    ''' </summary>
    ''' <param name="lpMsg">指向一个由函数GetMessage或PeekMessage恢复的MSG类型的数据结构.
    ''' 该结构包含有Windows应用程序队列中的消息.</param>
    ''' <returns></returns>
    ''' <remarks>返回值表明了函数执行结果。若消息被转换（即是指字符消息被发送到应用程序队列）则返回True；否则返回False。
    ''' 函数TranslateMessage并不修改由参数IpMsg指定的消息，它仅仅为由键盘驱动器产生出ASCII字符的键产生WM_CHAR消息。若应用程序为其他目的而处理虚拟控消息时，不应调用函数TranslateMessage.
    ''' 例如，当TranslateMessage函数返回非零值时，用户不应该再调用TranslateMessage函数。</remarks>
    <DllImport("user32.dll")>
    Public Shared Function TranslateMessage(ByRef lpMsg As MSG) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' 该函数将lpmsg参数指定的MSG数据结构中的消息传送给指定窗口的窗口函数。
    ''' </summary>
    ''' <param name="lpmsg">指向一个MSG数据结构.其中包含来自Windows应用程序队列的消息信息.
    ''' 此数据结构必须包含有效的消息值.如果lpmsg指向一条WM_TIMER消息,而WM_TIMER消息的lParam参数又非NULL,
    ''' 则lParam参数是一个函数的地址，该函数被用来代替窗口函数而被调用。</param>
    ''' <returns></returns>
    ''' <remarks>返回值给出窗口函数返回的值.它的意义取决于被发送的消息，但返回值通常被忽略。</remarks>
    <DllImport("user32.dll")>
    Public Shared Function DispatchMessage(ByRef lpmsg As MSG) As IntPtr
    End Function

#End Region
    
#Region "  ---  鼠标与键盘的消息发送"

    ''' <summary>
    ''' 向任意进程发送键盘消息
    ''' </summary>
    ''' <param name="bVk">按键的虚拟键值，如回车键为vk_return, tab键为vk_tab,可以参考常用模拟键的键值对照表，也可以通过System.Windows.Forms.Keys枚举来查看。</param>
    ''' <param name="bScan">扫描码，一般不用设置，用0代替就行；</param>
    ''' <param name="dwFlags">选项标志，如果为keydown则置0即可，如果为keyup则设成数值2，即常数 KEYEVENTF_KEYUP；</param>
    ''' <param name="dwExtraInfo">一般也是置0即可。</param>
    ''' <remarks>
    '''  调用案例1：
    ''' keybd_event(System.Windows.Forms.Keys.Escape, 0, 0, 0)  ' 按下 ESCAPE键
    ''' keybd_event(System.Windows.Forms.Keys.NumLock, 0, KEYEVENTF_KEYUP, 0)  ' 按键弹起，其中 KEYEVENTF_KEYUP=2 
    '''  调用案例2：    模拟按下 'ALT+F4'键
    ''' keybd_event(18, 0, 0, 0);
    ''' keybd_event(115, 0, 0, 0);
    ''' keybd_event(115, 0, KEYEVENTF_KEYUP, 0); 
    ''' keybd_event(18, 0, KEYEVENTF_KEYUP, 0);
    '''</remarks>
    <DllImport("user32.dll")>
    Public Shared Sub keybd_event(bVk As Byte, bScan As Byte, dwFlags As UInteger, dwExtraInfo As UIntPtr)
        '声明方式二： Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
        ' Public Const 

        ' 调用案例1：
        'keybd_event(System.Windows.Forms.Keys.Escape, 0, 0, 0)  ' 按下 ESCAPE键
        'keybd_event(System.Windows.Forms.Keys.NumLock, 0, KEYEVENTF_KEYUP, 0)  ' 按键弹起

        ' 调用案例2：    模拟按下 'ALT+F4'键
        'keybd_event(18, 0, 0, 0);
        'keybd_event(115, 0, 0, 0);
        'keybd_event(115, 0, KEYEVENTF_KEYUP, 0); 
        'keybd_event(18, 0, KEYEVENTF_KEYUP, 0);

    End Sub


    ''' <summary> 发送鼠标消息 </summary>
    ''' <param name="dwFlags"> (位编码)要如何操作鼠标。如果不指定 MOUSEEVENTF_ABSOLUTE，则是相对于当前的鼠标位置。 </param>
    ''' <param name="dx">根据MOUSEEVENTF_ABSOLUTE标志，指定x，y方向的绝对位置或相对位置 </param>
    ''' <param name="dy">根据MOUSEEVENTF_ABSOLUTE标志，指定x，y方向的绝对位置或相对位置 </param>
    ''' <param name="dwData">没有使用，直接赋值为0 </param>
    ''' <param name="dwExtraInfo">没有使用，直接赋值为0 </param>
    <DllImport("user32.dll")>
    Public Shared Sub mouse_event(dwFlags As MouseOperation, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As UInteger)
        '程序中我们直接调用mouse_event函数就可以了
        'mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 500, 500, 0, 0);

        '1、这里是鼠标左键按下和松开两个事件的组合即一次单击：
        'mouse_event (MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 )

        '2、模拟鼠标右键单击事件：
        'mouse_event (MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0 )

        '3、两次连续的鼠标左键单击事件构成一次鼠标双击事件：
        'mouse_event (MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 )
        'mouse_event (MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 )

        '4、使用绝对坐标
        'MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, 500, 500, 0, 0

        '需要说明的是，如果没有使用MOUSEEVENTF_ABSOLUTE，函数默认的是相对于鼠标当前位置的点，如果dx，和dy，用0，0表示，这函数认为是当前鼠标所在的点。５、直接设定绝对坐标并单击
        'mouse_event(MOUSEEVENTF_LEFTDOWN, X * 65536 / 1024, Y * 65536 / 768, 0, 0);
        'mouse_event(MOUSEEVENTF_LEFTUP, X * 65536 / 1024, Y * 65536 / 768, 0, 0);
        '其中X，Y分别是你要点击的点的横坐标和纵坐标
    End Sub

    <Flags>
    Public Enum MouseOperation As UInteger
        ''' <Summary> 移动鼠标 </Summary>
        MouseEventF_Move = &H1
        ' 移动鼠标
        ''' <Summary> 模拟鼠标左键按下 </Summary>
        MouseEventF_LeftDown = &H2
        ' 模拟鼠标左键按下
        ''' <Summary> 模拟鼠标左键抬起 </Summary>
        MouseEventF_LeftUp = &H4
        ' 模拟鼠标左键抬起
        ''' <Summary> 模拟鼠标右键按下 </Summary>
        MouseEventF_RightDown = &H8
        ' 模拟鼠标右键按下
        ''' <Summary> 模拟鼠标右键抬起 </Summary>
        MouseEventF_RightUp = &H10
        ' 模拟鼠标右键抬起
        ''' <Summary> 模拟鼠标中键按下 </Summary>
        MouseEventF_MiddleDown = &H20
        ' 模拟鼠标中键按下
        ''' <Summary> 模拟鼠标中键抬起 </Summary>
        MouseEventF_MiddleUp = &H40
        ' 模拟鼠标中键抬起
        ''' <Summary> 标示是否采用绝对坐标 </Summary>
        MouseEventF_Absolute = &H8000
        ' 标示是否采用绝对坐标
    End Enum

#End Region

End Class
