using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public unsafe static class DllImports
    {
        private const int STD_OUTPUT_HANDLE         = -11;
        private const int KEY_PRESSED               = 0x8000;
        private const int SW_MAXIMIZE               = 3;
        private const int GWL_STYLE                 = -16;
        private const int GWL_EXSTYLE               = -20;
        private const int WS_THICKFRAME             = 0x40000;
        private const int WS_CAPTION                = 0xC00000;
        private const int WS_EX_DLGMODALFRAME       = 0x00000001;
        private const int WS_EX_CLIENTEDGE          = 0x00000200;
        private const int WS_EX_WINDOWEDGE          = 0x00000100;
        private const int WS_EX_STATICEDGE          = 0x00020000;
        private const int MONITOR_DEFAULTTONEAREST  = 2;
        private const int SWP_NOZORDER              = 0x0004;
        private const int SWP_NOACTIVATE            = 0x0010;
        private const int SWP_FRAMECHANGED          = 0x0020;
        private const int INVALID_HANDLE_VALUE      = -1;
        private const int NULL                      = 0;

        private static readonly IntPtr mainWindowHandle;

        public static IntPtr StdOutputHandle { get; }

        static DllImports()
        {
            StdOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            ExceptionHelper.ValidateMagic(StdOutputHandle != new IntPtr(INVALID_HANDLE_VALUE) && StdOutputHandle != new IntPtr(NULL));

            mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
        }

        public static bool IsKeyDown(ConsoleKey key)
        {
            return (GetKeyState((int)key) & KEY_PRESSED) != 0;
        }

        // Original taken from here: https://src.chromium.org/viewvc/chrome/trunk/src/ui/views/win/fullscreen_handler.cc?revision=HEAD&view=markup
        // http://stackoverflow.com/a/5299718/6843321
        public static void EnterFullscreenMode()
        {
            ShowWindow(mainWindowHandle, SW_MAXIMIZE);

            ExceptionHelper.ValidateMagic(SetWindowLong(mainWindowHandle, GWL_STYLE, GetWindowLong(mainWindowHandle, GWL_STYLE) & ~(WS_CAPTION | WS_THICKFRAME)) != 0);
            ExceptionHelper.ValidateMagic(SetWindowLong(mainWindowHandle, GWL_EXSTYLE, GetWindowLong(mainWindowHandle, GWL_EXSTYLE) & ~(WS_EX_DLGMODALFRAME | WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE)) != 0);

            MONITORINFO monitor_info = new MONITORINFO
            {
                cbSize = Marshal.SizeOf<MONITORINFO>()
            };

            ExceptionHelper.ValidateMagic(GetMonitorInfo(MonitorFromWindow(mainWindowHandle, MONITOR_DEFAULTTONEAREST), ref monitor_info));
            RECT window_rect = new RECT(monitor_info.rcMonitor);
            ExceptionHelper.ValidateMagic(SetWindowPos(mainWindowHandle, new IntPtr(NULL), window_rect.x(), window_rect.y(), window_rect.width(), window_rect.height(), SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED));
        }

        public static unsafe void SetFont(string fontName, short x, short y)
        {
            CONSOLE_FONT_INFOEX info = new CONSOLE_FONT_INFOEX()
            {
                dwFontSize = new COORD(x, y)
            };

            info.cbSize = (uint)Marshal.SizeOf(info);

            Marshal.Copy(fontName.ToCharArray(), 0, new IntPtr(info.FaceName), fontName.Length);
            ExceptionHelper.ValidateMagic(SetCurrentConsoleFontEx(StdOutputHandle, false, ref info));
        }

        public static int MessageBox(string message, string title, uint type = 0 | 0x10, bool exitProgram = true)
        {
            int output = MessageBox((IntPtr)0, message, title, type);

            if (exitProgram)
            {
                Program.ExitWithError();
            }

            return output;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int key);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

#pragma warning disable IDE1006 // Naming Styles
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public long left;
            public long top;
            public long right;
            public long bottom;

            public RECT(RECT rect)
            {
                left    = rect.left;
                top     = rect.top;
                right   = rect.right;
                bottom  = rect.bottom;
            }

            public int x()
            {
                return (int)left;
            }

            public int y()
            {
                return (int)top;
            }

            public int width()
            {
                return (int)(right - left);
            }

            public int height()
            {
                return (int)(bottom - top);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            public fixed char FaceName[32];
        }
        
        [DebuggerDisplay("{X},{Y}")]
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_UNION
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public byte AsciiChar;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_INFO
        {
            [FieldOffset(0)]
            public CHAR_UNION Char;
            [FieldOffset(2)]
            public short Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;

            public SMALL_RECT(short left, short top, short right, short bottom)
            {
                Left    = left;
                Top     = top;
                Right   = right;
                Bottom  = bottom;
            }
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
