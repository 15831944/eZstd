using System;

namespace eZstd.API
{
    #region   ---  Windows消息

    /// <summary>
    /// 响应窗口的消息
    /// </summary>
    /// <remarks></remarks>
    public enum WindowsMessages : uint
    {
        /// <summary>
        ///The WM_ACTIVATE message is sent when a window is being activated or deactivated. This message is sent first to the window procedure of the top-level window being deactivated; it is then sent to the window procedure of the top-level window being activated.
        /// </summary>
        /// <remarks></remarks>
        WM_ACTIVATE = 0x6,

        /// <summary>
        ///The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
        /// </summary>
        /// <remarks></remarks>
        WM_ACTIVATEAPP = 0x1C,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_AFXFIRST = 0x360,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_AFXLAST = 0x37F,

        /// <summary>
        ///The WM_APP constant is used by applications to help define private messages usually of the form WM_APP+X where X is an integer value.
        /// </summary>
        /// <remarks></remarks>
        WM_APP = 0x8000,

        /// <summary>
        ///The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
        /// </summary>
        /// <remarks></remarks>
        WM_ASKCBFORMATNAME = 0x30C,

        /// <summary>
        ///The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.
        /// </summary>
        /// <remarks></remarks>
        WM_CANCELJOURNAL = 0x4B,

        /// <summary>
        ///The WM_CANCELMODE message is sent to cancel certain modes such as mouse capture. For example the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example the EnableWindow function sends this message when disabling the specified window.
        /// </summary>
        /// <remarks></remarks>
        WM_CANCELMODE = 0x1F,

        /// <summary>
        ///The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
        /// </summary>
        /// <remarks></remarks>
        WM_CAPTURECHANGED = 0x215,

        /// <summary>
        ///The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
        /// </summary>
        /// <remarks></remarks>
        WM_CHANGECBCHAIN = 0x30D,

        /// <summary>
        ///An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
        /// </summary>
        /// <remarks></remarks>
        WM_CHANGEUISTATE = 0x127,

        /// <summary>
        ///The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.
        /// </summary>
        /// <remarks></remarks>
        WM_CHAR = 0x102,

        /// <summary>
        ///Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.
        /// </summary>
        /// <remarks></remarks>
        WM_CHARTOITEM = 0x2F,

        /// <summary>
        ///The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated moved or sized.
        /// </summary>
        /// <remarks></remarks>
        WM_CHILDACTIVATE = 0x22,

        /// <summary>
        ///An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection if any from the edit control.
        /// </summary>
        /// <remarks></remarks>
        WM_CLEAR = 0x303,

        /// <summary>
        ///The WM_CLOSE message is sent as a signal that a window or an application should terminate.
        /// </summary>
        /// <remarks></remarks>
        WM_CLOSE = 0x10,

        /// <summary>
        ///The WM_COMMAND message is sent when the user selects a command item from a menu when a control sends a notification message to its parent window or when an accelerator keystroke is translated.
        /// </summary>
        /// <remarks></remarks>
        WM_COMMAND = 0x111,

        /// <summary>
        ///The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
        /// </summary>
        /// <remarks></remarks>
        WM_COMPACTING = 0x41,

        /// <summary>
        ///The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.
        /// </summary>
        /// <remarks></remarks>
        WM_COMPAREITEM = 0x39,

        /// <summary>
        ///The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
        /// </summary>
        /// <remarks></remarks>
        WM_CONTEXTMENU = 0x7B,

        /// <summary>
        ///An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.
        /// </summary>
        /// <remarks></remarks>
        WM_COPY = 0x301,

        /// <summary>
        ///An application sends the WM_COPYDATA message to pass data to another application.
        /// </summary>
        /// <remarks></remarks>
        WM_COPYDATA = 0x4A,

        /// <summary>
        ///The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created but before the window becomes visible.
        /// </summary>
        /// <remarks></remarks>
        WM_CREATE = 0x1,

        /// <summary>
        ///The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However only owner-drawn buttons respond to the parent window processing this message.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORBTN = 0x135,

        /// <summary>
        ///The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message the dialog box can set its text and background colors using the specified display device context handle.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORDLG = 0x136,

        /// <summary>
        ///An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message the parent window can use the specified device context handle to set the text and background colors of the edit control.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLOREDIT = 0x133,

        /// <summary>
        ///Sent to the parent window of a list box before the system draws the list box. By responding to this message the parent window can set the text and background colors of the list box by using the specified display device context handle.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORLISTBOX = 0x134,

        /// <summary>
        ///The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message the owner window can set the text and background colors of the message box by using the given display device context handle.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORMSGBOX = 0x132,

        /// <summary>
        ///The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message the parent window can use the display context handle to set the background color of the scroll bar control.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORSCROLLBAR = 0x137,

        /// <summary>
        ///A static control or an edit control that is read-only or disabled sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message the parent window can use the specified device context handle to set the text and background colors of the static control.
        /// </summary>
        /// <remarks></remarks>
        WM_CTLCOLORSTATIC = 0x138,

        /// <summary>
        ///An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection if any in the edit control and copy the deleted text to the clipboard in CF_TEXT format.
        /// </summary>
        /// <remarks></remarks>
        WM_CUT = 0x300,

        /// <summary>
        ///The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character such as the umlaut (double-dot) that is combined with another character to form a composite character. For example the umlaut-O character (?) is generated by typing the dead key for the umlaut character and then typing the O key.
        /// </summary>
        /// <remarks></remarks>
        WM_DEADCHAR = 0x103,

        /// <summary>
        ///Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING LB_RESETCONTENT CB_DELETESTRING or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
        /// </summary>
        /// <remarks></remarks>
        WM_DELETEITEM = 0x2D,

        /// <summary>
        ///The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen. This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message it can be assumed that all child windows still exist.
        /// </summary>
        /// <remarks></remarks>
        WM_DESTROY = 0x2,

        /// <summary>
        ///The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
        /// </summary>
        /// <remarks></remarks>
        WM_DESTROYCLIPBOARD = 0x307,

        /// <summary>
        ///Notifies an application of a change to the hardware configuration of a device or the computer.
        /// </summary>
        /// <remarks></remarks>
        WM_DEVICECHANGE = 0x219,

        /// <summary>
        ///The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
        /// </summary>
        /// <remarks></remarks>
        WM_DEVMODECHANGE = 0x1B,

        /// <summary>
        ///The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
        /// </summary>
        /// <remarks></remarks>
        WM_DISPLAYCHANGE = 0x7E,

        /// <summary>
        ///The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
        /// </summary>
        /// <remarks></remarks>
        WM_DRAWCLIPBOARD = 0x308,

        /// <summary>
        ///The WM_DRAWITEM message is sent to the parent window of an owner-drawn button combo box list box or menu when a visual aspect of the button combo box list box or menu has changed.
        /// </summary>
        /// <remarks></remarks>
        WM_DRAWITEM = 0x2B,

        /// <summary>
        ///Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
        /// </summary>
        /// <remarks></remarks>
        WM_DROPFILES = 0x233,

        /// <summary>
        ///The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns but after the enabled state (WS_DISABLED style bit) of the window has changed.
        /// </summary>
        /// <remarks></remarks>
        WM_ENABLE = 0xA,

        /// <summary>
        ///The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
        /// </summary>
        /// <remarks></remarks>
        WM_ENDSESSION = 0x16,

        /// <summary>
        ///The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.
        /// </summary>
        /// <remarks></remarks>
        WM_ENTERIDLE = 0x121,

        /// <summary>
        ///The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.
        /// </summary>
        /// <remarks></remarks>
        WM_ENTERMENULOOP = 0x211,

        /// <summary>
        ///The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// </summary>
        /// <remarks></remarks>
        WM_ENTERSIZEMOVE = 0x231,

        /// <summary>
        ///The WM_ERASEBKGND message is sent when the window background must be erased (for example when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.
        /// </summary>
        /// <remarks></remarks>
        WM_ERASEBKGND = 0x14,

        /// <summary>
        ///The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.
        /// </summary>
        /// <remarks></remarks>
        WM_EXITMENULOOP = 0x212,

        /// <summary>
        ///The WM_EXITSIZEMOVE message is sent one time to a window after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        /// </summary>
        /// <remarks></remarks>
        WM_EXITSIZEMOVE = 0x232,

        /// <summary>
        ///An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
        /// </summary>
        /// <remarks></remarks>
        WM_FONTCHANGE = 0x1D,

        /// <summary>
        ///The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
        /// </summary>
        /// <remarks></remarks>
        WM_GETDLGCODE = 0x87,

        /// <summary>
        ///An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.
        /// </summary>
        /// <remarks></remarks>
        WM_GETFONT = 0x31,

        /// <summary>
        ///An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.
        /// </summary>
        /// <remarks></remarks>
        WM_GETHOTKEY = 0x33,

        /// <summary>
        ///The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog and the small icon in the window caption.
        /// </summary>
        /// <remarks></remarks>
        WM_GETICON = 0x7F,

        /// <summary>
        ///The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position or its default minimum or maximum tracking size.
        /// </summary>
        /// <remarks></remarks>
        WM_GETMINMAXINFO = 0x24,

        /// <summary>
        ///Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application. Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint AccessibleObjectFromEvent or AccessibleObjectFromWindow. However server applications handle this message.
        /// </summary>
        /// <remarks></remarks>
        WM_GETOBJECT = 0x3D,

        /// <summary>
        ///An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
        /// </summary>
        /// <remarks></remarks>
        WM_GETTEXT = 0xD,

        /// <summary>
        ///An application sends a WM_GETTEXTLENGTH message to determine the length in characters of the text associated with a window.
        /// </summary>
        /// <remarks></remarks>
        WM_GETTEXTLENGTH = 0xE,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_HANDHELDFIRST = 0x358,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_HANDHELDLAST = 0x35F,

        /// <summary>
        ///Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed WM_HELP is sent to the window associated with the menu; otherwise WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus WM_HELP is sent to the currently active window.
        /// </summary>
        /// <remarks></remarks>
        WM_HELP = 0x53,

        /// <summary>
        ///The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.
        /// </summary>
        /// <remarks></remarks>
        WM_HOTKEY = 0x312,

        /// <summary>
        ///This message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
        /// </summary>
        /// <remarks></remarks>
        WM_HSCROLL = 0x114,

        /// <summary>
        ///The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        /// <remarks></remarks>
        WM_HSCROLLCLIPBOARD = 0x30E,

        /// <summary>
        ///Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
        /// </summary>
        /// <remarks></remarks>
        WM_ICONERASEBKGND = 0x27,

        /// <summary>
        ///Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_CHAR = 0x286,

        /// <summary>
        ///Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_COMPOSITION = 0x10F,

        /// <summary>
        ///Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_COMPOSITIONFULL = 0x284,

        /// <summary>
        ///Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message the application calls the SendMessage function with the following parameters.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_CONTROL = 0x283,

        /// <summary>
        ///Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_ENDCOMPOSITION = 0x10E,

        /// <summary>
        ///Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_KEYDOWN = 0x290,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_IME_KEYLAST = 0x10F,

        /// <summary>
        ///Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_KEYUP = 0x291,

        /// <summary>
        ///Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_NOTIFY = 0x282,

        /// <summary>
        ///Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_REQUEST = 0x288,

        /// <summary>
        ///Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_SELECT = 0x285,

        /// <summary>
        ///Sent to an application when a window is activated. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_SETCONTEXT = 0x281,

        /// <summary>
        ///Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_IME_STARTCOMPOSITION = 0x10D,

        /// <summary>
        ///The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.
        /// </summary>
        /// <remarks></remarks>
        WM_INITDIALOG = 0x110,

        /// <summary>
        ///The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.
        /// </summary>
        /// <remarks></remarks>
        WM_INITMENU = 0x116,

        /// <summary>
        ///The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed without changing the entire menu.
        /// </summary>
        /// <remarks></remarks>
        WM_INITMENUPOPUP = 0x117,

        /// <summary>
        ///The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows and so on.
        /// </summary>
        /// <remarks></remarks>
        WM_INPUTLANGCHANGE = 0x51,

        /// <summary>
        ///The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
        /// </summary>
        /// <remarks></remarks>
        WM_INPUTLANGCHANGEREQUEST = 0x50,

        /// <summary>
        ///The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
        /// </summary>
        /// <remarks></remarks>
        WM_KEYDOWN = 0x100,

        /// <summary>
        ///This message filters for keyboard messages.
        /// </summary>
        /// <remarks></remarks>
        WM_KEYFIRST = 0x100,

        /// <summary>
        ///This message filters for keyboard messages.
        /// </summary>
        /// <remarks></remarks>
        WM_KEYLAST = 0x108,

        /// <summary>
        ///The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed or a keyboard key that is pressed when a window has the keyboard focus.
        /// </summary>
        /// <remarks></remarks>
        WM_KEYUP = 0x101,

        /// <summary>
        ///The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.
        /// </summary>
        /// <remarks></remarks>
        WM_KILLFOCUS = 0x8,

        /// <summary>
        ///The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_LBUTTONDBLCLK = 0x203,

        /// <summary>
        ///The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_LBUTTONDOWN = 0x201,

        /// <summary>
        ///The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_LBUTTONUP = 0x202,

        /// <summary>
        ///The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_MBUTTONDBLCLK = 0x209,

        /// <summary>
        ///The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_MBUTTONDOWN = 0x207,

        /// <summary>
        ///The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_MBUTTONUP = 0x208,

        /// <summary>
        ///An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIACTIVATE = 0x222,

        /// <summary>
        ///An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
        /// </summary>
        /// <remarks></remarks>
        WM_MDICASCADE = 0x227,

        /// <summary>
        ///An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDICREATE = 0x220,

        /// <summary>
        ///An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIDESTROY = 0x221,

        /// <summary>
        ///An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIGETACTIVE = 0x229,

        /// <summary>
        ///An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIICONARRANGE = 0x228,

        /// <summary>
        ///An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIMAXIMIZE = 0x225,

        /// <summary>
        ///An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDINEXT = 0x224,

        /// <summary>
        ///An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIREFRESHMENU = 0x234,

        /// <summary>
        ///An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
        /// </summary>
        /// <remarks></remarks>
        WM_MDIRESTORE = 0x223,

        /// <summary>
        ///An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window to replace the window menu of the frame window or both.
        /// </summary>
        /// <remarks></remarks>
        WM_MDISETMENU = 0x230,

        /// <summary>
        ///An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
        /// </summary>
        /// <remarks></remarks>
        WM_MDITILE = 0x226,

        /// <summary>
        ///The WM_MEASUREITEM message is sent to the owner window of a combo box list box list view control or menu item when the control or menu is created.
        /// </summary>
        /// <remarks></remarks>
        WM_MEASUREITEM = 0x2C,

        /// <summary>
        ///The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.
        /// </summary>
        /// <remarks></remarks>
        WM_MENUCHAR = 0x120,

        /// <summary>
        ///The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.
        /// </summary>
        /// <remarks></remarks>
        WM_MENUCOMMAND = 0x126,

        /// <summary>
        ///The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.
        /// </summary>
        /// <remarks></remarks>
        WM_MENUDRAG = 0x123,

        /// <summary>
        ///The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
        /// </summary>
        /// <remarks></remarks>
        WM_MENUGETOBJECT = 0x124,

        /// <summary>
        ///The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.
        /// </summary>
        /// <remarks></remarks>
        WM_MENURBUTTONUP = 0x122,

        /// <summary>
        ///The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.
        /// </summary>
        /// <remarks></remarks>
        WM_MENUSELECT = 0x11F,

        /// <summary>
        ///The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEACTIVATE = 0x21,

        /// <summary>
        ///Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEFIRST = 0x200,

        /// <summary>
        ///The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEHOVER = 0x2A1,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSELAST = 0x20D,

        /// <summary>
        ///The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSELEAVE = 0x2A3,

        /// <summary>
        ///The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured the message is posted to the window that contains the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEMOVE = 0x200,

        /// <summary>
        ///The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEWHEEL = 0x20A,

        /// <summary>
        ///The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        /// </summary>
        /// <remarks></remarks>
        WM_MOUSEHWHEEL = 0x20E,

        /// <summary>
        ///The WM_MOVE message is sent after a window has been moved.
        /// </summary>
        /// <remarks></remarks>
        WM_MOVE = 0x3,

        /// <summary>
        ///The WM_MOVING message is sent to a window that the user is moving. By processing this message an application can monitor the position of the drag rectangle and if needed change its position.
        /// </summary>
        /// <remarks></remarks>
        WM_MOVING = 0x216,

        /// <summary>
        ///Non Client Area Activated Caption(Title) of the Form
        /// </summary>
        /// <remarks></remarks>
        WM_NCACTIVATE = 0x86,

        /// <summary>
        ///The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message an application can control the content of the window's client area when the size or position of the window changes.
        /// </summary>
        /// <remarks></remarks>
        WM_NCCALCSIZE = 0x83,

        /// <summary>
        ///The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
        /// </summary>
        /// <remarks></remarks>
        WM_NCCREATE = 0x81,

        /// <summary>
        ///The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
        /// </summary>
        /// <remarks></remarks>
        WM_NCDESTROY = 0x82,

        /// <summary>
        ///The WM_NCHITTEST message is sent to a window when the cursor moves or when a mouse button is pressed or released. If the mouse is not captured the message is sent to the window beneath the cursor. Otherwise the message is sent to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_NCHITTEST = 0x84,

        /// <summary>
        ///The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCLBUTTONDBLCLK = 0xA3,

        /// <summary>
        ///The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCLBUTTONDOWN = 0xA1,

        /// <summary>
        ///The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCLBUTTONUP = 0xA2,

        /// <summary>
        ///The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCMBUTTONDBLCLK = 0xA9,

        /// <summary>
        ///The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCMBUTTONDOWN = 0xA7,

        /// <summary>
        ///The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCMBUTTONUP = 0xA8,

        /// <summary>
        /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
        /// </summary>
        WM_NCMOUSELEAVE = 0x2A2,

        /// <summary>
        ///The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCMOUSEMOVE = 0xA0,

        /// <summary>
        ///The WM_NCPAINT message is sent to a window when its frame must be painted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCPAINT = 0x85,

        /// <summary>
        ///The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCRBUTTONDBLCLK = 0xA6,

        /// <summary>
        ///The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCRBUTTONDOWN = 0xA4,

        /// <summary>
        ///The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse this message is not posted.
        /// </summary>
        /// <remarks></remarks>
        WM_NCRBUTTONUP = 0xA5,

        /// <summary>
        ///The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box
        /// </summary>
        /// <remarks></remarks>
        WM_NEXTDLGCTL = 0x28,

        /// <summary>
        ///The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
        /// </summary>
        /// <remarks></remarks>
        WM_NEXTMENU = 0x213,

        /// <summary>
        ///Sent by a common control to its parent window when an event has occurred or the control requires some information.
        /// </summary>
        /// <remarks></remarks>
        WM_NOTIFY = 0x4E,

        /// <summary>
        ///Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
        /// </summary>
        /// <remarks></remarks>
        WM_NOTIFYFORMAT = 0x55,

        /// <summary>
        ///The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
        /// </summary>
        /// <remarks></remarks>
        WM_NULL = 0x0,

        /// <summary>
        ///Occurs when the control needs repainting
        /// </summary>
        /// <remarks></remarks>
        WM_PAINT = 0xF,

        /// <summary>
        ///The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.
        /// </summary>
        /// <remarks></remarks>
        WM_PAINTCLIPBOARD = 0x309,

        /// <summary>
        ///Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows except in unusual circumstances explained in the Remarks.
        /// </summary>
        /// <remarks></remarks>
        WM_PAINTICON = 0x26,

        /// <summary>
        ///This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette. This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
        /// </summary>
        /// <remarks></remarks>
        WM_PALETTECHANGED = 0x311,

        /// <summary>
        ///The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
        /// </summary>
        /// <remarks></remarks>
        WM_PALETTEISCHANGING = 0x310,

        /// <summary>
        ///The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed the system sends the message before any processing to destroy the window takes place.
        /// </summary>
        /// <remarks></remarks>
        WM_PARENTNOTIFY = 0x210,

        /// <summary>
        ///An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.
        /// </summary>
        /// <remarks></remarks>
        WM_PASTE = 0x302,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_PENWINFIRST = 0x380,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_PENWINLAST = 0x38F,

        /// <summary>
        ///Notifies applications that the system typically a battery-powered personal computer is about to enter a suspended mode. Obsolete : use POWERBROADCAST instead
        /// </summary>
        /// <remarks></remarks>
        WM_POWER = 0x48,

        /// <summary>
        ///Notifies applications that a power-management event has occurred.
        /// </summary>
        /// <remarks></remarks>
        WM_POWERBROADCAST = 0x218,

        /// <summary>
        ///The WM_PRINT message is sent to a window to request that it draw itself in the specified device context most commonly in a printer device context.
        /// </summary>
        /// <remarks></remarks>
        WM_PRINT = 0x317,

        /// <summary>
        ///The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context most commonly in a printer device context.
        /// </summary>
        /// <remarks></remarks>
        WM_PRINTCLIENT = 0x318,

        /// <summary>
        ///The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
        /// </summary>
        /// <remarks></remarks>
        WM_QUERYDRAGICON = 0x37,

        /// <summary>
        ///The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero. After processing this message the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
        /// </summary>
        /// <remarks></remarks>
        WM_QUERYENDSESSION = 0x11,

        /// <summary>
        ///This message informs a window that it is about to receive the keyboard focus giving the window the opportunity to realize its logical palette when it receives the focus.
        /// </summary>
        /// <remarks></remarks>
        WM_QUERYNEWPALETTE = 0x30F,

        /// <summary>
        ///The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
        /// </summary>
        /// <remarks></remarks>
        WM_QUERYOPEN = 0x13,

        /// <summary>
        ///The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.
        /// </summary>
        /// <remarks></remarks>
        WM_QUEUESYNC = 0x23,

        /// <summary>
        ///Once received it ends the application's Message Loop signaling the application to end. It can be sent by pressing Alt+F4 Clicking the X in the upper right-hand of the program or going to File->Exit.
        /// </summary>
        /// <remarks></remarks>
        WM_QUIT = 0x12,

        /// <summary>
        ///he WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_RBUTTONDBLCLK = 0x206,

        /// <summary>
        ///The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_RBUTTONDOWN = 0x204,

        /// <summary>
        ///The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured the message is posted to the window beneath the cursor. Otherwise the message is posted to the window that has captured the mouse.
        /// </summary>
        /// <remarks></remarks>
        WM_RBUTTONUP = 0x205,

        /// <summary>
        ///The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications the clipboard owner must render data in all the formats it is capable of generating and place the data on the clipboard by calling the SetClipboardData function.
        /// </summary>
        /// <remarks></remarks>
        WM_RENDERALLFORMATS = 0x306,

        /// <summary>
        ///The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.
        /// </summary>
        /// <remarks></remarks>
        WM_RENDERFORMAT = 0x305,

        /// <summary>
        ///The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
        /// </summary>
        /// <remarks></remarks>
        WM_SETCURSOR = 0x20,

        /// <summary>
        ///When the controll got the focus
        /// </summary>
        /// <remarks></remarks>
        WM_SETFOCUS = 0x7,

        /// <summary>
        ///An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.
        /// </summary>
        /// <remarks></remarks>
        WM_SETFONT = 0x30,

        /// <summary>
        ///An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key the system activates the window.
        /// </summary>
        /// <remarks></remarks>
        WM_SETHOTKEY = 0x32,

        /// <summary>
        ///An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box and the small icon in the window caption.
        /// </summary>
        /// <remarks></remarks>
        WM_SETICON = 0x80,

        /// <summary>
        ///An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
        /// </summary>
        /// <remarks></remarks>
        WM_SETREDRAW = 0xB,

        /// <summary>
        ///Text / Caption changed on the control. An application sends a WM_SETTEXT message to set the text of a window.
        /// </summary>
        /// <remarks></remarks>
        WM_SETTEXT = 0xC,

        /// <summary>
        ///An application sends the WM_SETTINGCHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        /// </summary>
        /// <remarks></remarks>
        WM_SETTINGCHANGE = 0x1A,

        /// <summary>
        ///The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown
        /// </summary>
        /// <remarks></remarks>
        WM_SHOWWINDOW = 0x18,

        /// <summary>
        ///The WM_SIZE message is sent to a window after its size has changed.
        /// </summary>
        /// <remarks></remarks>
        WM_SIZE = 0x5,

        /// <summary>
        ///The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.
        /// </summary>
        /// <remarks></remarks>
        WM_SIZECLIPBOARD = 0x30B,

        /// <summary>
        ///The WM_SIZING message is sent to a window that the user is resizing. By processing this message an application can monitor the size and position of the drag rectangle and if needed change its size or position.
        /// </summary>
        /// <remarks></remarks>
        WM_SIZING = 0x214,

        /// <summary>
        ///The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
        /// </summary>
        /// <remarks></remarks>
        WM_SPOOLERSTATUS = 0x2A,

        /// <summary>
        ///The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles.
        /// </summary>
        /// <remarks></remarks>
        WM_STYLECHANGED = 0x7D,

        /// <summary>
        ///The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
        /// </summary>
        /// <remarks></remarks>
        WM_STYLECHANGING = 0x7C,

        /// <summary>
        ///The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
        /// </summary>
        /// <remarks></remarks>
        WM_SYNCPAINT = 0x88,

        /// <summary>
        ///The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is a character key that is pressed while the ALT key is down.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSCHAR = 0x106,

        /// <summary>
        ///This message is sent to all top-level windows when a change is made to a system color setting.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSCOLORCHANGE = 0x15,

        /// <summary>
        ///A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control menu) or when the user chooses the maximize button minimize button restore button or close button.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSCOMMAND = 0x112,

        /// <summary>
        ///The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is a dead key that is pressed while holding down the ALT key.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSDEADCHAR = 0x107,

        /// <summary>
        ///The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSKEYDOWN = 0x104,

        /// <summary>
        ///The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        /// </summary>
        /// <remarks></remarks>
        WM_SYSKEYUP = 0x105,

        /// <summary>
        ///Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
        /// </summary>
        /// <remarks></remarks>
        WM_TCARD = 0x52,

        /// <summary>
        ///A message that is sent whenever there is a change in the system time.
        /// </summary>
        /// <remarks></remarks>
        WM_TIMECHANGE = 0x1E,

        /// <summary>
        ///The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.
        /// </summary>
        /// <remarks></remarks>
        WM_TIMER = 0x113,

        /// <summary>
        ///An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control the previously deleted text is restored or the previously added text is deleted.
        /// </summary>
        /// <remarks></remarks>
        WM_UNDO = 0x304,

        /// <summary>
        ///The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.
        /// </summary>
        /// <remarks></remarks>
        WM_UNINITMENUPOPUP = 0x125,

        /// <summary>
        ///The WM_USER constant is used by applications to help define private messages for use by private window classes usually of the form WM_USER+X where X is an integer value.
        /// </summary>
        /// <remarks></remarks>
        WM_USER = 0x400,

        /// <summary>
        ///The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off the system updates the user-specific settings. The system sends this message immediately after updating the settings.
        /// </summary>
        /// <remarks></remarks>
        WM_USERCHANGED = 0x54,

        /// <summary>
        ///Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.
        /// </summary>
        /// <remarks></remarks>
        WM_VKEYTOITEM = 0x2E,

        /// <summary>
        ///The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
        /// </summary>
        /// <remarks></remarks>
        WM_VSCROLL = 0x115,

        /// <summary>
        ///The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
        /// </summary>
        /// <remarks></remarks>
        WM_VSCROLLCLIPBOARD = 0x30A,

        /// <summary>
        ///The WM_WINDOWPOSCHANGED message is sent to a window whose size position or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        /// <remarks></remarks>
        WM_WINDOWPOSCHANGED = 0x47,

        /// <summary>
        ///The WM_WINDOWPOSCHANGING message is sent to a window whose size position or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
        /// </summary>
        /// <remarks></remarks>
        WM_WINDOWPOSCHANGING = 0x46,

        /// <summary>
        ///An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI. Note The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        /// </summary>
        /// <remarks></remarks>
        WM_WININICHANGE = 0x1A,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_XBUTTONDBLCLK = 0x20D,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_XBUTTONDOWN = 0x20B,

        /// <summary>
        ///Definition Needed
        /// </summary>
        /// <remarks></remarks>
        WM_XBUTTONUP = 0x20C
    }

    /// <summary>
    /// System command values used in the WM_SYSCOMMAND notification.
    /// </summary>
    /// <remarks></remarks>
    public enum SysCommands
    {

        ///<summary>Sizes the window.</summary>
        SC_SIZE = 0xF000,

        ///<summary>Moves the window.</summary>
        SC_MOVE = 0xF010,

        ///<summary>Minimizes the window.</summary>
        SC_MINIMIZE = 0xF020,

        ///<summary>Maximizes the window.</summary>
        SC_MAXIMIZE = 0xF030,

        ///<summary>Moves to the next window.</summary>
        SC_NEXTWINDOW = 0xF040,

        ///<summary>Moves to the previous window.</summary>
        SC_PREVWINDOW = 0xF050,

        ///<summary>Closes the window.</summary>
        SC_CLOSE = 0xF060,

        ///<summary>Scrolls vertically.</summary>
        SC_VSCROLL = 0xF070,

        ///<summary>Scrolls horizontally.</summary>
        SC_HSCROLL = 0xF080,

        ///<summary>Retrieves the window menu as a result of a mouse click.</summary>
        SC_MOUSEMENU = 0xF090,

        ///<summary>
        ///Retrieves the window menu as a result of a keystroke.
        ///For more information, see the Remarks section.
        ///</summary>
        SC_KEYMENU = 0xF100,

        ///<summary>TODO</summary>
        SC_ARRANGE = 0xF110,

        ///<summary>Restores the window to its normal position and size.</summary>
        SC_RESTORE = 0xF120,

        ///<summary>Activates the Start menu.</summary>
        SC_TASKLIST = 0xF130,

        ///<summary>Executes the screen saver application specified in the [boot] section of the System.ini file.</summary>
        SC_SCREENSAVE = 0xF140,

        ///<summary>
        ///Activates the window associated with the application-specified hot key.
        ///The lParam parameter identifies the window to activate.
        ///</summary>
        SC_HOTKEY = 0xF150,

        //#if(WINVER >= &h0400) 'Win95

        ///<summary>Selects the default item; the user double-clicked the window menu.</summary>
        SC_DEFAULT = 0xF160,

        ///<summary>
        ///Sets the state of the display. This command supports devices that
        ///have power-saving features, such as a battery-powered personal computer.
        ///The lParam parameter can have the following values: -1 = the display is powering on,
        ///1 = the display is going to low power, 2 = the display is being shut off
        ///</summary>
        SC_MONITORPOWER = 0xF170,

        ///<summary>
        ///Changes the cursor to a question mark with a pointer. If the user
        ///then clicks a control in the dialog box, the control receives a WM_HELP message.
        ///</summary>
        SC_CONTEXTHELP = 0xF180,

        ///<summary>TODO</summary>
        SC_SEPARATOR = 0xF00F,
        //#endif 'WINVER >= &h0400

        //#if(WINVER >= &h0600) 'Vista

        ///<summary>Indicates whether the screen saver is secure.</summary>
        SCF_ISSECURE = 0x1,

        //#endif 'WINVER >= &h0600


        SC_ICON = SC_MINIMIZE,


        SC_ZOOM = SC_MAXIMIZE

    }

    [Flags()]
    public enum PeekMessageParams
    {
        /// <summary>
        /// 经过PeekMessage处理后，消息依然留在队列中
        /// </summary>
        /// <remarks></remarks>
        PM_NOREMOVE = 0x0,
        /// <summary>
        /// 经过PeekMessage处理后，消息被从队列中删除
        /// </summary>
        /// <remarks></remarks>
        PM_REMOVE = 0x1,
        PM_NOYIELD = 0x2,
        PM_QS_INPUT = QueueStatusFlags.QS_INPUT << 16,
        PM_QS_POSTMESSAGE = (QueueStatusFlags.QS_POSTMESSAGE | QueueStatusFlags.QS_HOTKEY | QueueStatusFlags.QS_TIMER) << 16,
        PM_QS_PAINT = QueueStatusFlags.QS_PAINT << 16,
        PM_QS_SENDMESSAGE = QueueStatusFlags.QS_SENDMESSAGE << 16
    }

    public enum QueueStatusFlags
    {

        QS_KEY = 0x1,
        QS_MOUSEMOVE = 0x2,
        QS_MOUSEBUTTON = 0x4,
        QS_MOUSE = (QS_MOUSEMOVE | QS_MOUSEBUTTON),
        QS_INPUT = (QS_MOUSE | QS_KEY),
        QS_POSTMESSAGE = 0x8,
        QS_TIMER = 0x10,
        QS_PAINT = 0x20,
        QS_SENDMESSAGE = 0x40,
        QS_HOTKEY = 0x80,
        QS_REFRESH = (QS_HOTKEY | QS_KEY | QS_MOUSEBUTTON | QS_PAINT),
        QS_ALLEVENTS = (QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY),
        QS_ALLINPUT = (QS_SENDMESSAGE | QS_PAINT | QS_TIMER | QS_POSTMESSAGE | QS_MOUSEBUTTON | QS_MOUSEMOVE | QS_HOTKEY | QS_KEY),
        QS_ALLPOSTMESSAGE = 0x100,
        QS_RAWINPUT = 0x400
    }

    #endregion

    #region   ---  Windows窗口

    /// <summary>
    /// 传递给SetWindowPos函数
    /// 参考： http://www.pinvoke.net/default.aspx/Constants/HWND.html
    /// </summary>
    /// <remarks></remarks>
    public enum SWP_HWND
    {
        /// <summary>
        /// 将窗口置于窗口列表底部
        /// </summary>
        /// <remarks></remarks>
        HWND_BOTTOM = 1,
        /// <summary>
        ///
        /// </summary>
        /// <remarks></remarks>
        HWND_BROADCAST = 0xFFFF,
        /// <summary>
        ///
        /// </summary>
        /// <remarks></remarks>
        HWND_DESKTOP = 0,
        /// <summary>
        ///
        /// </summary>
        /// <remarks></remarks>
        HWND_MESSAGE = -3,
        /// <summary>
        /// 将窗口置于列表顶部，并位于任何最顶部窗口的后面
        /// </summary>
        /// <remarks></remarks>
        HWND_NOTOPMOST = -2,
        /// <summary>
        /// 将窗口置于Z序列的顶部；Z序列代表在分级结构中，窗口针对一个给定级别的窗口显示的顺序
        /// </summary>
        /// <remarks></remarks>
        HWND_TOP = 0,
        /// <summary>
        /// 窗体置于屏幕最顶端，可以缩小移动，而且不会被其他窗口覆盖。但是用户可以编辑后面窗口。
        /// </summary>
        /// <remarks></remarks>
        HWND_TOPMOST = -1
    }

    [Flags]
    public enum SetWindowPosFlags
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        SynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x20,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x20,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x80,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x10,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        /// contents of the client area are saved and copied back into the client area after the window is sized or
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x2,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        /// window uncovered as a result of the window being moved. When this flag is set, the application must
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x8,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x1,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x4,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x40
    }

    /// <summary>
    /// Specifies the relationship between the specified window and the window whose handle is to be retrieved.
    /// </summary>
    /// <remarks>兄弟或同级是指在整个分级结构中位于同一级别的窗口。如某个窗口有五个子窗口，那五个窗口就是兄弟窗口。
    /// 尽管GetWindow可用于枚举窗口，但倘若要在枚举过程中重新定位、创建和清除窗口，那么EnumWindows和EnumChildWindows更为可靠</remarks>
    public enum GetWndConsts
    {
        /// <summary>
        /// 为一个源子窗口寻找第一个兄弟（同级）窗口，或寻找第一个顶级窗口
        /// 要获取的窗口与原窗口处于同一级别，但它的Z序最大。
        /// </summary>
        /// <remarks></remarks>
        GW_HWNDFIRST = 0,
        /// <summary>
        /// 为一个源子窗口寻找最后一个兄弟（同级）窗口，或寻找最后一个顶级窗口。
        /// 要获取的窗口与原窗口处于同一级别，但它的Z序最小。
        /// </summary>
        /// <remarks></remarks>
        GW_HWNDLAST = 1,
        /// <summary>
        /// 为源窗口寻找下一个兄弟窗口。
        /// 要获取的窗口与原窗口处于同一级别，并且它的Z序恰好在原窗口之上。
        /// </summary>
        /// <remarks></remarks>
        GW_HWNDNEXT = 2,
        /// <summary>
        /// 为源窗口寻找前一个兄弟窗口。
        /// 要获取的窗口与原窗口处于同一级别，并且它的Z序恰好在原窗口之下。
        /// </summary>
        /// <remarks></remarks>
        GW_HWNDPREV = 3,
        /// <summary>
        /// 寻找窗口的所有者
        /// </summary>
        /// <remarks></remarks>
        GW_OWNER = 4,
        /// <summary>
        /// 寻找源窗口的第一个子窗口
        /// </summary>
        /// <remarks></remarks>
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6,
        GW_MAX = 6
    }

    /// <summary>
    /// Used with ShowWindow and WINDOWPLACEMENT.specifying how the window is to be shown
    /// </summary>
    /// <remarks></remarks>
    public enum ShowWindowCommands
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,
        /// <summary>
        /// Activates and displays a window. If the window is minimized or
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window
        /// for the first time.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        ShowMinimized = 2,
        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        Maximize = 3,
        // is this the right value
        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        ShowMaximized = 3,
        /// <summary>
        /// Displays a window in its most recent size and position. This value
        /// is similar to <see cref="ShowWindowCommands.Normal"/>, except
        /// the window is not actived.
        /// </summary>
        ShowNoActivate = 4,
        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        Show = 5,
        /// <summary>
        /// Minimizes the specified window and activates the next top-level
        /// window in the Z order.
        /// </summary>
        Minimize = 6,
        /// <summary>
        /// Displays the window as a minimized window. This value is similar to
        /// <see cref="ShowWindowCommands.ShowMinimized"/>, except the
        /// window is not activated.
        /// </summary>
        ShowMinNoActive = 7,
        /// <summary>
        /// Displays the window in its current size and position. This value is
        /// similar to <see cref="ShowWindowCommands.Show"/>, except the
        /// window is not activated.
        /// </summary>
        ShowNA = 8,
        /// <summary>
        /// Activates and displays the window. If the window is minimized or
        /// maximized, the system restores it to its original size and position.
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        Restore = 9,
        /// <summary>
        /// Sets the show state based on the SW_* value specified in the
        /// STARTUPINFO structure passed to the CreateProcess function by the
        /// program that started the application.
        /// </summary>
        ShowDefault = 10,
        /// <summary>
        ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
        /// that owns the window is not responding. This flag should only be
        /// used when minimizing windows from a different thread.
        /// </summary>
        ForceMinimize = 11
    }

    /// <summary>
    /// Flags for GetWindowLong, GetWindowLongPtr, SetWindowLong and SetWindowLongPtr
    /// </summary>
    /// <remarks></remarks>
    public enum WindowLongFlags
    {
        /// <summary>
        /// 窗口的扩展样式
        /// </summary>
        /// <remarks></remarks>
        GWL_EXSTYLE = -20,
        /// <summary>
        /// 窗口样式
        /// </summary>
        /// <remarks></remarks>
        GWL_STYLE = -16,
        /// <summary>
        /// 该窗口的窗口函数的地址，即指向窗口函数的指针。
        /// 用GWL_WNDPROC索引调用函数<see cref="apiWindows.SetWindowLong"/>可以创建窗口类的一个子类。
        /// 应用程序不要试图为其他进程创建的窗口产生子类，那样不起作用。
        /// </summary>
        /// <remarks></remarks>
        GWL_WNDPROC = -4,
        /// <summary>
        /// 拥有窗口的实例的句柄
        /// </summary>
        /// <remarks></remarks>
        GWL_HINSTANCE = -6,
        /// <summary>
        /// 该窗口之父的句柄。不要用SetWindowWord来改变这个值。
        /// SetParent函数修改子窗口的父窗口。应用程序不应调用SetWindowLong函数来修改子窗口的父窗口。
        /// </summary>
        /// <remarks></remarks>
        GWL_HWNDPARENT = -8,
        /// <summary>
        /// 对话框中一个子窗口的标识符
        /// </summary>
        /// <remarks></remarks>
        GWL_ID = -12,
        /// <summary>
        /// 含义由应用程序规定。此索引可以用来存取一个保存在每一个窗口结构中的Long值。这个Long值是特意为应用程序保留的。
        /// 通过，窗口是被另外的应用程序创建的话，那么应用程序不应该在这个窗口的Long值中存储东西。
        /// </summary>
        /// <remarks></remarks>
        GWL_USERDATA = -21,
        /// <summary>
        /// 这个窗口的对话框函数地址
        /// </summary>
        /// <remarks></remarks>
        DWL_DLGPROC = 4,
        /// <summary>
        /// 在对话框函数中处理的一条消息返回的值
        /// </summary>
        /// <remarks></remarks>
        DWL_MSGRESULT = 0,
        /// <summary>
        /// 含义由应用程序规定
        /// </summary>
        /// <remarks></remarks>
        DWL_USER = 8

    }

    /// <summary>
    /// Window Styles.
    /// The following styles can be specified wherever a window style is required.
    /// After the control has been created, these styles cannot be modified, except as noted.
    /// </summary>
    [Flags()]
    public enum WindowStyle : uint
    {
        /// <summary>The window has a thin-line border.</summary>
        WS_BORDER = 0x800000,

        /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
        WS_CAPTION = 0xC00000,

        /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
        WS_CHILD = 0x40000000,

        /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
        WS_CLIPCHILDREN = 0x2000000,

        /// <summary>
        /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
        /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
        /// </summary>
        WS_CLIPSIBLINGS = 0x4000000,

        /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
        WS_DISABLED = 0x8000000,

        /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
        WS_DLGFRAME = 0x400000,

        /// <summary>
        /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
        /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// </summary>
        WS_GROUP = 0x20000,

        /// <summary>The window has a horizontal scroll bar.</summary>
        WS_HSCROLL = 0x100000,

        /// <summary>The window is initially maximized.</summary>
        WS_MAXIMIZE = 0x1000000,

        /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
        WS_MAXIMIZEBOX = 0x10000,

        /// <summary>The window is initially minimized.</summary>
        WS_MINIMIZE = 0x20000000,

        /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
        WS_MINIMIZEBOX = 0x20000,

        /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
        WS_OVERLAPPED = 0x0,

        /// <summary>The window is an overlapped window.</summary>
        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
        WS_POPUP = 0x80000000u,

        /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

        /// <summary>The window has a sizing border.</summary>
        WS_SIZEFRAME = 0x40000,

        /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
        WS_SYSMENU = 0x80000,

        /// <summary>
        /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
        /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
        /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
        /// </summary>
        WS_TABSTOP = 0x10000,

        /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
        WS_VISIBLE = 0x10000000,

        /// <summary>The window has a vertical scroll bar.</summary>
        WS_VSCROLL = 0x200000,

        //'---------------------------------------------------------------------------------------------------------------
        // The Next Enumerations(WindowStylesEx) are to Specify the extended window style of a window. Used by CreateWindowEx.
        //'---------------------------------------------------------------------------------------------------------------

        /// <summary>Specifies a window that accepts drag-drop files.</summary>
        WS_EX_ACCEPTFILES = 0x10,

        /// <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
        WS_EX_APPWINDOW = 0x40000,

        /// <summary>Specifies a window that has a border with a sunken edge.</summary>
        WS_EX_CLIENTEDGE = 0x200,

        /// <summary>
        /// Specifies a window that paints all descendants in bottom-to-top painting order using double-buffering.
        /// This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. This style is not supported in Windows 2000.
        /// </summary>
        /// <remarks>
        /// With WS_EX_COMPOSITED set, all descendants of a window get bottom-to-top painting order using double-buffering.
        /// Bottom-to-top painting order allows a descendent window to have translucency (alpha) and transparency (color-key) effects,
        /// but only if the descendent window also has the WS_EX_TRANSPARENT bit set.
        /// Double-buffering allows the window and its descendents to be painted without flicker.
        /// </remarks>
        WS_EX_COMPOSITED = 0x2000000,

        /// <summary>
        /// Specifies a window that includes a question mark in the title bar. When the user clicks the question mark,
        /// the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message.
        /// The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command.
        /// The Help application displays a pop-up window that typically contains help for the child window.
        /// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
        /// </summary>
        WS_EX_CONTEXTHELP = 0x400,

        /// <summary>
        /// Specifies a window which contains child windows that should take part in dialog box navigation.
        /// If this style is specified, the dialog manager recurses into children of this window when performing navigation operations
        /// such as handling the TAB key, an arrow key, or a keyboard mnemonic.
        /// </summary>
        WS_EX_CONTROLPARENT = 0x10000,

        /// <summary>Specifies a window that has a double border.</summary>
        WS_EX_DLGMODALFRAME = 0x1,

        /// <summary>
        /// Specifies a window that is a layered window.
        /// This cannot be used for child windows or if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        /// </summary>
        WS_EX_LAYERED = 0x80000,

        /// <summary>
        /// Specifies a window with the horizontal origin on the right edge. Increasing horizontal values advance to the left.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        WS_EX_LAYOUTRTL = 0x400000,

        /// <summary>Specifies a window that has generic left-aligned properties. This is the default.</summary>
        WS_EX_LEFT = 0x0,

        /// <summary>
        /// Specifies a window with the vertical scroll bar (if present) to the left of the client area.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 0x4000,

        /// <summary>
        /// Specifies a window that displays text using left-to-right reading-order properties. This is the default.
        /// </summary>
        WS_EX_LTRREADING = 0x0,

        /// <summary>
        /// Specifies a multiple-document interface (MDI) child window.
        /// </summary>
        WS_EX_MDICHILD = 0x40,

        /// <summary>
        /// Specifies a top-level window created with this style does not become the foreground window when the user clicks it.
        /// The system does not bring this window to the foreground when the user minimizes or closes the foreground window.
        /// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
        /// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
        /// </summary>
        WS_EX_NOACTIVATE = 0x8000000,

        /// <summary>
        /// Specifies a window which does not pass its window layout to its child windows.
        /// </summary>
        WS_EX_NOINHERITLAYOUT = 0x100000,

        /// <summary>
        /// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 0x4,

        /// <summary>Specifies an overlapped window.</summary>
        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

        /// <summary>Specifies a palette window, which is a modeless dialog box that presents an array of commands.</summary>
        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

        /// <summary>
        /// Specifies a window that has generic "right-aligned" properties. This depends on the window class.
        /// The shell language must support reading-order alignment for this to take effect.
        /// Using the WS_EX_RIGHT style has the same effect as using the SS_RIGHT (static), ES_RIGHT (edit), and BS_RIGHT/BS_RIGHTBUTTON (button) control styles.
        /// </summary>
        WS_EX_RIGHT = 0x1000,

        /// <summary>Specifies a window with the vertical scroll bar (if present) to the right of the client area. This is the default.</summary>
        WS_EX_RIGHTSCROLLBAR = 0x0,

        /// <summary>
        /// Specifies a window that displays text using right-to-left reading-order properties.
        /// The shell language must support reading-order alignment for this to take effect.
        /// </summary>
        WS_EX_RTLREADING = 0x2000,

        /// <summary>Specifies a window with a three-dimensional border style intended to be used for items that do not accept user input.</summary>
        WS_EX_STATICEDGE = 0x20000,

        /// <summary>
        /// Specifies a window that is intended to be used as a floating toolbar.
        /// A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font.
        /// A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB.
        /// If a tool window has a system menu, its icon is not displayed on the title bar.
        /// However, you can display the system menu by right-clicking or by typing ALT+SPACE.
        /// </summary>
        WS_EX_TOOLWINDOW = 0x80,

        /// <summary>
        /// Specifies a window that should be placed above all non-topmost windows and should stay above them, even when the window is deactivated.
        /// To add or remove this style, use the SetWindowPos function.
        /// </summary>
        WS_EX_TOPMOST = 0x8,

        /// <summary>
        /// Specifies a window that should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        /// The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// To achieve transparency without these restrictions, use the SetWindowRgn function.
        /// </summary>
        WS_EX_TRANSPARENT = 0x20,

        /// <summary>Specifies a window that has a border with a raised edge.</summary>
        WS_EX_WINDOWEDGE = 0x100
    }

    /// <summary>
    /// Used with the RedrawWindow function.
    /// </summary>
    /// <remarks></remarks>
    [Flags()]
    public enum RedrawWindowFlags
    {
        /// <summary>
        /// Invalidates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
        /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_INVALIDATE invalidates the entire window.
        /// 禁用（屏蔽）重画区域。把矩形或区域加入窗口的无效区。
        /// </summary>
        Invalidate = 0x1,

        /// <summary>Causes the OS to post a WM_PAINT message to the window regardless of whether a portion of the window is invalid.
        /// 即使窗口并非无效，也向其投递一条WM_PAINT消息
        ///</summary>
        InternalPaint = 0x2,

        /// <summary>
        /// Causes the window to receive a WM_ERASEBKGND message when the window is repainted.
        /// Specify this value in combination with the RDW_INVALIDATE value; otherwise, RDW_ERASE has no effect.
        /// 重画前，先清除重画区域的背景。也必须指定RDW_INVALIDATE。
        /// 重画操作包括了所有的子窗口
        /// </summary>
        @Erase = 0x4,

        /// <summary>
        /// Validates the rectangle or region that you specify in lprcUpdate or hrgnUpdate.
        /// You can set only one of these parameters to a non-NULL value. If both are NULL, RDW_VALIDATE validates the entire window.
        /// This value does not affect internal WM_PAINT messages.
        /// 检验重画区域。把矩形或区域从窗口的更新区域中删除。
        /// </summary>
        Validate = 0x8,

        /// <summary>
        /// 禁止内部生成或由这个函数生成的任何待决WM_PAINT消息。针对无效区域，仍会生成WM_PAINT消息
        /// </summary>
        NoInternalPaint = 0x10,

        /// <summary>Suppresses any pending WM_ERASEBKGND messages.
        /// 禁止删除重画区域的背景。重画操作不包括子窗口。
        ///</summary>
        NoErase = 0x20,

        /// <summary>Excludes child windows, if any, from the repainting operation.
        /// 重画操作排除子窗口（前提是它们存在于重画区域）
        ///</summary>
        NoChildren = 0x40,

        /// <summary>Includes child windows, if any, in the repainting operation.
        /// 重画操作包括子窗口（前提是它们存在于重画区域）
        ///</summary>
        AllChildren = 0x80,

        /// <summary>Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND and WM_PAINT messages before the RedrawWindow returns, if necessary.
        /// 立即更新指定的重画区域
        ///</summary>
        UpdateNow = 0x100,

        /// <summary>
        /// Causes the affected windows, which you specify by setting the RDW_ALLCHILDREN and RDW_NOCHILDREN values, to receive WM_ERASEBKGND messages before RedrawWindow returns, if necessary.
        /// The affected windows receive WM_PAINT messages at the ordinary time.
        /// 立即删除指定的重画区域
        /// </summary>
        EraseNow = 0x200,

        /// <summary>
        /// 如非客户区包含在重画区域中，则对非客户区进行更新。也必须指定RDW_INVALIDATE。
        /// 在窗口重画时使窗口收到一条WM_ERASEBKGND消息。
        /// </summary>
        /// <remarks></remarks>
        Frame = 0x400,

        /// <summary>
        /// 禁止非客户区域重画（如果它是重画区域的一部分）。也必须指定RDW_VALIDATE
        /// </summary>
        /// <remarks></remarks>
        NoFrame = 0x800
    }
    #endregion

    #region   ---  钩子

    /// <summary>
    /// 枚举传递给SetWindowsHookEx函数的有效的hook类型值
    /// </summary>
    /// <remarks></remarks>
    public enum HookType
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }

    [Flags()]
    public enum KBDLLHOOKSTRUCTFlags
    {
        LLKHF_EXTENDED = 0x1,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80
    }

    [Flags()]
    public enum MSLLHOOKSTRUCTFlags
    {
        LLMHF_INJECTED = 1
    }

    /// <summary>
    /// 用在 LoadLibraryEx 函数中
    /// </summary>
    /// <remarks></remarks>
    [System.Flags]
    public enum LoadLibraryFlags
    {
        /// <summary> 不对DLL进行初始化，仅用于NT </summary>
        DONT_RESOLVE_DLL_REFERENCES = 0x1,
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x10,
        /// <summary> 不准备DLL执行。如装载一个DLL只是为了访问它的资源，就可以改善一部分性能 </summary>
        LOAD_LIBRARY_AS_DATAFILE = 0x2,
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x40,
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x20,
        /// <summary> 指定搜索的路径 </summary>
        LOAD_WITH_ALTERED_SEARCH_PATH = 0x8
    }

    #endregion

    #region ---   IO
    /// <summary>
    /// ShellExecute()函数的枚举参数
    /// </summary>
    public enum ShowCommands : int
    {
        /// <summary>
        /// 隐藏窗口，活动状态给另一个窗口
        /// </summary>
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        /// <summary>
        /// 与SW_RESTORE相同
        /// </summary>
        SW_NORMAL = 1,
        /// <summary>
        /// 最小化窗口，并将其激活
        /// </summary>
        SW_SHOWMINIMIZED = 2,
        /// <summary>
        /// 最大化窗口，并将其激活
        /// </summary>
        SW_SHOWMAXIMIZED = 3,
        /// <summary>
        /// 
        /// </summary>
        SW_MAXIMIZE = 3,
        /// <summary>
        /// 最小化一个窗口，同时不改变活动窗口
        /// </summary>
        SW_SHOWNOACTIVATE = 4,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SW_SHOW = 5,
        /// <summary>
        /// 最小化窗口，活动状态给另一个窗口
        /// </summary>
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        /// <summary>
        /// 用当前的大小和位置显示一个窗口，不改变活动窗口
        /// </summary>
        SW_SHOWNA = 8,
        /// <summary>
        /// 用原来的大小和位置显示一个窗口，同时令其进入活动状态
        /// </summary>
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11,
        SW_MAX = 11
    }

    #endregion
}
