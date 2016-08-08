using System;
using System.Runtime.InteropServices;
using System.Text;

namespace eZstd.API
{
    /// <summary>
    /// 与界面中的窗口相关的函数
    /// 最关键的是对windows操作系统中窗口本质的认识，使用Spy++工具，查找窗口就可以发现，其实对于给定的对话框窗口，
    /// 其中的任何控件，如图标、文本、确定、取消按钮等都是它的子窗口，本质上还是窗口，所不同的只是，顶级父窗口查找时，用FindWindow函数，而查找子窗口时用FindWindowEx。   
    /// 另外比较有用的是EnumWindows，可以遍历所有的顶级父窗口，而EnumChildWindows则是遍历其子窗口。
    /// 所以问题的解决思路就是使用EnumWindows遍历所有的顶级父窗口，对每个顶级父窗口使用EnumChildWindows遍历它的所有控件，
    /// 每个控件其实也是窗口，拿到该控件的句柄后，就可以调用GetWindowText来获取文本信息了。
    /// </summary>
    public class Windows
    {
        /// <summary>
        /// 判断一个窗口句柄是否有效
        /// </summary>
        /// <param name="hwnd">待检查窗口的句柄</param>
        /// <returns>True表示成功，False表示失败</returns>
        /// <remarks>如在一个程序变量里容纳了窗口句柄，为了解它是否仍然有效，就可考虑使用这个函数</remarks>
        [DllImport("user32", EntryPoint = "IsWindow", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool IsWindow(IntPtr hwnd);

        #region   ---  查找窗口

        /// <summary>
        /// 搜索指定类型或标题名的窗口句柄
        /// </summary>
        /// <param name="lpClassName">指向包含了窗口类名的空中止（C语言）字串的指针；或设为vbNullString（或C#中的null），表示接收任何类</param>
        /// <param name="lpWindowName">指向包含了窗口文本（或标签）的空中止（C语言）字串的指针；或设为vbNullString（或C#中的null），表示接收任何窗口标题</param>
        /// <returns>找到窗口的句柄。如未找到相符窗口，则返回零。会设置GetLastError</returns>
        /// <remarks>寻找窗口列表中第一个符合指定条件的顶级窗口
        /// （在vb里使用：FindWindow最常见的一个用途是获得ThunderRTMain类的隐藏窗口的句柄；该类是所有运行中vb执行程序的一部分。
        /// 获得句柄后，可用api函数GetWindowText取得这个窗口的名称；该名也是应用程序的标题）。
        /// 很少要求同时按类与窗口名搜索。为向自己不准备参数传递一个零，最简便的办法是传递vbNullString常数</remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //        常用软件的窗口、标题、类名、句柄等
        //        窗口 类名
        //        Excel FindWindow("XLMAIN", Application.Caption)
        //        Word FindWindow("OpusApp ", Application.Name)VISIOA
        //        Visio	FindWindow("VISIOA", Application.Window.Caption)
        //        VBA窗体 FindWindow("ThunderDFrame", 某窗体的Caption)
        //        Gtalk FindWindow("Google Talk - Google Xmpp Client GUI Window", "Google Talk")
        //        Chrome浏览器 FindWindow("Chrome_WidgetWin_0", "网页标题 - Google Chrome 浏览器")
        //        windows标准的对话框类（如inputbox对话框）	Findwindow("#32770",vbNullString)。但是要注意，Windows的inputbox对话框的标题名，并不是对话框顶部显示的名称（测试中好像是“"Source Local"”，但是不知道是否具有通用性。）
        // --------------------------------------------
        // VB.NET示例：
        //        Dim hw&, cnt&
        //        Dim rttitle As String * 256
        //        hw& = FindWindow("ThunderRT5Main", vbNullString) ' ThunderRTMain under VB4
        //        cnt = GetWindowText(hw&, rttitle, 255)
        //        MsgBox(Left$(rttitle, cnt), 0, "RTMain title")
        // --------------------------------------------
        // C#示例：
        //    IntPtr __handle;
        //    __handle = FindWindow("Notepad", null);
        //    if (__handle!=IntPtr.Zero){
        //    }
        //    __handle = FindWindow(null, "无标题 - 记事本");
        //    if (__handle != IntPtr.Zero)
        //    {
        //    }

        /// <summary>
        /// 在窗口列表中寻找与指定条件相符的第一个子窗口
        /// </summary>
        /// <param name="parentHandle">在其中查找子的父窗口。如设为零，表示使用桌面窗口
        /// （通常说的顶级窗口都被认为是桌面的子窗口，所以也会对它们进行查找）</param>
        /// <param name="childAfter">从这个窗口后开始查找。这样便可利用对FindWindowEx的多次调用找到符合条件的所有子窗口。
        /// 如设为零，表示从第一个子窗口开始搜索</param>
        /// <param name="lclassName">欲搜索的类名。或设为vbNullString（或C#中的null），表示忽略。</param>
        /// <param name="windowTitle">欲搜索的窗口名。或设为vbNullString（或C#中的null），表示忽略。</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        /// <summary>获得前台窗口的句柄。这里的“前台窗口”是指前台应用程序的活动窗口。
        /// 系统将生成前台窗口的线程给予较高的优先级。
        /// The GetForegroundWindow function returns a handle to the foreground window.</summary>
        /// <returns>The return value is a handle to the foreground window.
        /// The foreground window can be NULL in certain circumstances, such as when a window is losing activation.
        /// 如果函数调用失败，则返回零值。 </returns>
        /// <remarks>windows nt支持多个桌面，它们相互间是独立的。每个桌面都有自己的前台窗口</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 获得包含指定点的窗口的句柄
        /// </summary>
        /// <param name="p">指定一个被检测的点，该点为struct类型</param>
        /// <returns> 包含了指定点的窗口的句柄。如指定的点处没有窗口存在，则返回零  </returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(System.Drawing.Point p);

        #endregion

        #region   ---  窗口间的父子、兄弟等关系

        /// <summary>
        /// 该函数是一个与EnumWindows、EnumChildWindows或EnumDesktopWindows一起使用的应用程序定义的回调函数。它接收顶层窗口句柄。如果要停止遍历，请返回 false。
        /// </summary>
        /// <param name="hwnd"> 顶层窗口的句柄 </param>
        /// <param name="lParam"> 应用程序定义的一个值(即EnumWindows中lParam) </param>
        /// <returns>如果为 false，则停止遍历</returns>
        /// <remarks> 调用时，先定义一个与此委托相同签名的函数，然后将此函数（函数名后面不能有括号）作为参数赋值给 EnumWindows 等API函数 </remarks>
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        /// <summary>
        /// 为窗体设置父窗体
        /// </summary>
        /// <param name="hWndChild"></param>
        /// <param name="hWndNewParent"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// 获得一个窗口的句柄，该窗口与某源窗口有特定的关系
        /// </summary>
        /// <param name="hWnd">源窗口</param>
        /// <param name="uCmd">指定结果窗口与源窗口的关系，它们建立在下述常数基础上：</param>
        /// <returns>由wCmd决定的一个窗口的句柄。如没有找到相符窗口，或者遇到错误，则返回零值。会设置GetLastError</returns>
        /// <remarks>兄弟或同级是指在整个分级结构中位于同一级别的窗口。如某个窗口有五个子窗口，那五个窗口就是兄弟窗口。
        /// 尽管GetWindow可用于枚举窗口，但倘若要在枚举过程中重新定位、创建和清除窗口，那么EnumWindows和EnumChildWindows更为可靠</remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWndConsts uCmd);

        /// <summary>
        /// 枚举一个父窗口的所有子窗口。(父窗口中的button，slider、ComboBox、windows标准的对话框类（#32770）等控件其本质都是窗口对象)
        /// </summary>
        /// <param name="hwndParent">父窗口句柄</param>
        /// <param name="lpEnumFunc">回调函数的委托,注意：回调函数的返回值将会影响到这个API函数的行为。
        /// 如果回调函数返回true，则枚举继续直到枚举完成；如果返回false，则将会中止枚举。</param>
        /// <param name="lParam"> 自定义的参数，无参数则输入null或者0即可。 </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        ///     遍历屏幕中的所有top-level windows。
        ///     Enumerates all top-level windows on the screen by passing the handle to each window, in turn, to an
        ///     application-defined callback function. <see cref="EnumWindows" /> continues until the last top-level window is
        ///     enumerated or the callback function returns FALSE.
        /// </summary>
        /// <param name="lpEnumFunc">指向一个应用程序定义的回调函数指针，请参看EnumWindowsProc。</param>
        /// <param name="lParam">An application-defined value to be passed to the callback function. 无参数则输入null或者0即可。</param>
        /// <returns>
        ///     <c>true</c> if the return value is nonzero., <c>false</c> otherwise. If the function fails, the return value
        ///     is zero.<br />To get extended error information, call GetLastError.<br />If <see cref="EnumWindowsProc" /> returns
        ///     zero, the return value is also zero. In this case, the callback function should call SetLastError to obtain a
        ///     meaningful error code to be returned to the caller of EnumWindows.
        /// </returns>
        /// <remarks>
        ///    EnumWindows函数不列举子窗口。function does not enumerate child windows, with the exception of a few top-level windows owned by the system that have the WS_CHILD style.
        ///    在循环体中调用这个函数比调用GetWindow函数更可靠。调用GetWindow函数中执行这个任务的应用程序可能会陷入死循环或指向一个已被销毁的窗口的句柄。 This function is more reliable than calling the GetWindow function in a loop. An application that calls the GetWindow function to perform this task risks being caught in an
        ///     infinite loop or referencing a handle to a window that has been destroyed.<br />Note For Windows 8 and later,
        ///     EnumWindows enumerates only top-level windows of desktop apps.
        /// </remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        /// <summary>
        /// 枚举与指定桌面相关联的所有顶级窗口。它把每个窗口的句柄，依次传递到应用程序定义回调函数。
        /// </summary>
        /// <param name="hDesktop"> 要枚举其顶级窗口的桌面句柄，此句柄由CreateDesktop、 GetThreadDesktop、 OpenDesktop或OpenInputDesktop函数返回。并且必须拥有 DESKTOP_READOBJECTS 的访问权。 </param>
        /// <param name="lpfn">回调函数的委托,注意：回调函数的返回值将会影响到这个API函数的行为。如果回调函数返回true，则枚举继续直到枚举完成；如果返回false，则将会中止枚举。</param>
        /// <param name="lParam">要传递给回调函数应用程序定义的值。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpfn, IntPtr lParam);

        #endregion

        #region   ---窗口的大小、位置与布局、与激活（前后关系）

        /// <summary>
        /// 设置或获取窗口的位置与相对位置
        /// </summary>
        /// <param name="hwnd">欲定位的窗口</param>
        /// <param name="hWndInsertAfter">窗口句柄。在窗口列表中，窗口hwnd会置于这个窗口句柄的后面。也可以选用枚举SWP_HWND中的值。 </param>
        /// <param name="x">窗口新的x坐标。如hwnd是一个子窗口，则x用父窗口的客户区坐标表示</param>
        /// <param name="y">窗口新的y坐标。如hwnd是一个子窗口，则y用父窗口的客户区坐标表示</param>
        /// <param name="cx">指定新的窗口宽度</param>
        /// <param name="cy">指定新的窗口高度</param>
        /// <param name="uFlags">SetWindowPosFlags枚举 </param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        /// <remarks>窗口成为最顶级窗口后，它下属的所有窗口也会进入最顶级。
        /// 一旦将其设为非最顶级，则它的所有下属和物主窗口也会转为非最顶级。
        /// Z序列用垂直于屏幕的一根假想Z轴量化这种从顶部到底部排列的窗口顺序</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        // 举例1、让窗口A始终显示在窗口B之上
        // SetWindowLong(窗口A句柄, WindowLongFlags.GWL_HWNDPARENT, 窗口B句柄);

        /// <summary>
        /// 设置窗口为前台窗口，这个函数可用于改变用户目前正在操作的应用程序。但是它是可以通过鼠标点击其他窗口而被覆盖的。
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        /// <remarks>不应随便使用它，因为一旦程序突然从后台进入前台，可能会使用户产生迷惑</remarks>
        [DllImport("user32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        /// <summary>
        /// 激活指定的窗口。
        /// 在vb里使用这个函数要小心，它不会改变输入焦点，所以焦点可能设向一个不活动窗口，
        /// 最好换用SetFocus API函数来激活窗口。如指定的窗口不从属于当前输入线程，则没有任何效果
        /// </summary>
        /// <param name="hWnd">待激活窗口的句柄</param>
        /// <returns>前一个活动窗口的句柄</returns>
        /// <remarks> SetActiveWindow函数激活一个窗口，但当应用程序处于后台时，将不激活指定窗口。当应用程序激活窗口时处于前台，则窗口将被放到前台。
        /// SetActiveWindow不会改变输入焦点，所以焦点可能设向一个不活动窗口，最好换用SetFocusAPI函数来激活窗口。如指定的窗口不从属于当前输入线程，则没有任何效果
        /// 从另一方面讲，SetForegroundWindow窗口函数激活一个窗口并将其强制为前台的。当应用程序要显示关键错误或需要用户立即注意的信息时，应只能调用SetForegroundWindow函数。</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);


        /// <summary>
        /// 对指定的窗口设置键盘焦点。该窗口必须与调用线程的消息队列相关。
        /// 在vb里对窗体和控件最好使用SetFocus方法。如指定的窗口不属于当前输入线程，则该函数是没有效果的。它用SetFocusAPI这个别名避免与vb的SetFocus方法发生冲突。
        /// </summary>
        /// <param name="hWnd"> 接收键盘输入的窗口指针。若该参数为NULL，则击键被忽略。 </param>
        /// <returns> 若函数调用成功，则返回原先拥有键盘焦点的窗口句柄。若hWnd参数无效或窗口未与调用线程的消息队列相关，则返回值为NULL。若想要获得更多错误信息，可以调用GetLastError函数。 </returns>
        /// <remarks>SetFocus函数发送WM_KILLFOCUS消息到失去键盘焦点的窗口，并且发送WM_SETFOCUS消息到接受键盘焦点的窗口。
        /// 它也激活接受键盘焦点的窗口或接受键盘焦点的窗口的父窗口。</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// 将指定的窗口带至窗口列表顶部。倘若它部分或全部隐藏于其他窗口下面，则将隐藏的部分完全显示出来。
        /// 将窗口显示在窗口顶部后，还可以通过鼠标点击其他窗口而将此窗口覆盖。
        /// 该函数也对弹出式窗口、顶级窗口以及MDI子窗口产生作用
        /// </summary>
        /// <param name="hwnd">欲带至顶部的那个窗口的句柄</param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        /// <remarks>这个函数也许能随同子窗口使用。函数对一个特定的输入线程来说是“本地的”——换言之，倘若某窗口并非前台应用程序的一部分，
        /// 那么一旦随同该窗口调用本函数，仍会将窗口带至它自己那个应用程序的窗口列表顶部。
        /// 但是，不会同时使那个应用成为前台应用程序。这意味着在调用了本函数后，窗口仍会保持隐藏状态</remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hwnd);

        /// <summary>
        /// 获得整个窗口（或控件）的范围矩形。此矩形在屏幕坐标系中，屏幕左上角点为原点(0,0)。
        /// 窗口的边框、标题栏、滚动条及菜单等都在这个矩形内
        /// </summary>
        /// <param name="hwnd">想获得范围矩形的那个窗口的句柄，窗口的边框、标题栏、滚动条及菜单等都在这个矩形内</param>
        /// <param name="lpRect">屏幕坐标中随同窗口装载的矩形</param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        /// <remarks></remarks>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        /// <summary>
        /// 返回指定窗口（或控件）客户区矩形的大小。此矩形在窗口客户坐标系中，客户区的左上角为原点(0,0)。
        /// </summary>
        /// <param name="hwnd">欲计算大小的目标窗口</param>
        /// <param name="lpRect">指定一个矩形，用客户区域的大小载入（以像素为单位）</param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        /// <remarks>lpRect的左侧及顶部区域肯定会被这个函数设为零</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClientRect(System.IntPtr hWnd, ref RECT lpRECT);

        #endregion

        #region   ---  操作窗口：显示、隐藏、启用、刷新界面等

        /// <summary>
        /// 在指定的窗口里允许或禁止所有鼠标及键盘输入（在vb里使用：在vb窗体和控件中使用Enabled属性）。
        /// 当被禁止时，窗口不响应鼠标和按键的输入，允许时，窗口接受所有的输入。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="bEnable">True允许窗口，False禁止</param>
        /// <returns>True表示成功，False表示失败</returns>
        /// <remarks>zengfy注：如果将Excel或Visio等的窗口禁用后，再去调用其Application属性，则会报错：应用程序正在使用中。</remarks>
        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);


        /// <summary>
        /// 强制立即更新窗口，窗口中以前屏蔽的所有区域都会重画（在vb里使用：如vb窗体或控件的任何部分需要更新，可考虑直接使用refresh方法
        /// </summary>
        /// <param name="hWnd">欲更新窗口的句柄</param>
        /// <returns>True表示成功，False表示失败</returns>
        /// <remarks></remarks>
        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        /// <summary>
        /// 根据参数nCmdShow显示或改变指定的窗口
        /// </summary>
        /// <param name="hWnd">窗口句柄，要向这个窗口应用由nCmdShow指定的命令</param>
        /// <param name="nCmdShow">为窗口指定可视性方面的一个命令。</param>
        /// <returns></returns>
        /// <remarks>如窗口之前是可见的，则返回TRUE（非零），否则返回FALSE（零）</remarks>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hwnd, ShowWindowCommands nCmdShow);

        /// <summary>
        /// 根据fuRedraw旗标的设置，重画全部或部分窗口
        /// </summary>
        /// <param name="hwnd">要重画的窗口的句柄。零表示更新整个桌面窗口</param>
        /// <param name="lprcUpdate">窗口中需要重画的一个矩形区域，如果hrgnUpdate指定的是一个合法的区域句柄，则此参数被忽略。</param>
        /// <param name="hrgnUpdate">一个“区”的句柄，这个区描述了要重画的窗口区域。“区”：Region。
        /// 如果<paramref name="lprcUpdate"/>和<paramref name="hrgnUpdate"/>都是Null，则整个用户区域被加入到更新区域中。</param>
        /// <param name="flags">规定具体重画操作的旗标。这些值可组合使用，从而进行复杂的重画行动</param>
        /// <returns>True表示成功，False表示失败</returns>
        /// <remarks>如针对桌面窗口应用这个函数，则应用程序必须用RDW_ERASE旗标重画桌面</remarks>
        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, [In]ref RECT lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlags flags);

        #endregion

        #region   ---  窗口属性

        /// <summary>
        /// 判断窗口是否处于活动状态（在vb里使用：针对vb窗体和控件，请用enabled属性）
        /// </summary>
        /// <param name="hWnd">待检查窗口的句柄</param>
        /// <returns>True表示成功，False表示失败</returns>
        /// <remarks></remarks>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        /// <summary>
        /// 获取与指定窗口关联在一起的一个进程和线程标识符
        /// </summary>
        /// <param name="handle">指定窗口句柄</param>
        /// <param name="processId">指定一个变量，用于装载拥有那个窗口的一个进程的标识符</param>
        /// <returns></returns>
        /// <remarks>拥有窗口的线程的标识符</remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowThreadProcessId(int handle, ref int processId);
        //中间没有任何执行方法

        /// <summary>
        /// 该函数获得指定窗口(或控件)所属的类的类名
        /// </summary>
        /// <param name="hWnd">窗口的句柄及间接给出的窗口所属的类</param>
        /// <param name="className">[out]得到的类名会保存在此参数中。</param>
        /// <param name="nMaxCount">指定由参数lpClassName指示的缓冲区的字节数。如果类名字符串大于缓冲区的长度，则多出的部分被截断</param>
        /// <remarks>
        ///   在C#中调用时，请严格按照如下格式调用：
        ///   StringBuilder className = new StringBuilder(255);
        ///   Windows.GetClassName(hwnd, className, className.Capacity);</remarks>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder className, int nMaxCount);

        /// <summary>
        /// 如果目标窗口属于当前进程，GetWindowText函数给指定的窗口或控件发送WM_GETTEXT消息。
        /// 如果目标窗口属于其他进程，并且有一个窗口标题，则GetWindowText返回窗口的标题文本，如果窗口无标题，则函数返回空字符串。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString">[out] 得到的窗口文本会保存在此参数中。 If the string is as long or longer than the buffer, the string is truncated and terminated with a NULL character.</param>
        /// <param name="nMaxCount">[in] Specifies the maximum number of characters to copy to the buffer, including the NULL character. If the text exceeds this limit, it is truncated.</param>
        /// <returns>如果目标窗口属于当前进程，GetWindowText函数给指定的窗口或控件发送WM_GETTEXT消息。如果目标窗口属于其他进程，并且有一个窗口标题，则GetWindowText返回窗口的标题文本，如果窗口无标题，则函数返回空字符串。</returns>
        /// <remarks>If the hWnd object is a control, the GetWindowText member function copies the text within the control instead of copying the caption.
        ///   在C#中调用时，请严格按照如下格式调用：
        ///   StringBuilder windowText = new StringBuilder(255);
        ///   Windows.GetWindowText(hwnd, windowText, windowText.Capacity);</remarks>
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);


        /// <summary>
        /// 从指定窗口的结构中取得信息
        /// </summary>
        /// <param name="hwnd">欲为其获取信息的窗口的句柄</param>
        /// <param name="nIndex">欲取回的信息</param>
        /// <returns>由nIndex决定。零表示出错。会设置GetLastError</returns>
        /// <remarks></remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, [MarshalAs(UnmanagedType.I4)]WindowLongFlags nIndex);

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs..</param>
        /// <param name="nIndex">The zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer.
        /// To set any other value, specify one of the following values: GWL_EXSTYLE, GWL_HINSTANCE, GWL_ID, GWL_STYLE, GWL_USERDATA, GWL_WNDPROC </param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, [MarshalAs(UnmanagedType.I4)]WindowLongFlags nIndex, IntPtr dwNewLong);
        // 举例1、让窗口A始终显示在窗口B之上
        // SetWindowLong(窗口A句柄, WindowLongFlags.GWL_HWNDPARENT, 窗口B句柄);


        #endregion
    }
}
