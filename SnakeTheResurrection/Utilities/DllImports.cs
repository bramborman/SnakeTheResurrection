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
        private const int CONSOLE_FULLSCREEN_MODE   = 1;
        private const int CONSOLE_WINDOWED_MODE     = 2;
        private const int GWL_STYLE                 = -16;
        private const int WS_OVERLAPPED             = 0;
        private const int WS_CAPTION                = 0xC00000;
        private const int WS_SYSMENU                = 0x80000;
        private const int WS_MINIMIZEBOX            = 0x20000;
        private const int WS_MAXIMIZEBOX            = 0x10000;
        private const int INVALID_HANDLE_VALUE      = -1;
        private const int NULL                      = 0;

        private static readonly IntPtr mainWindowHandle;

        public static IntPtr StdOutputHandle { get; }

        public static bool ConsoleFullscreen
        {
            get
            {
                uint lpModeFlags;
                ExceptionHelper.ValidateMagic(GetConsoleDisplayMode(out lpModeFlags));

                return lpModeFlags == CONSOLE_FULLSCREEN_MODE;
            }
            set
            {
                COORD lpNewScreenBufferDimensions;

                if (!SetConsoleDisplayMode(StdOutputHandle, (uint)(value ? CONSOLE_FULLSCREEN_MODE : CONSOLE_WINDOWED_MODE), out lpNewScreenBufferDimensions))
                {
                    // Compatibility with Windows Vista, 7, 8.x
                    ShowWindow(mainWindowHandle, SW_MAXIMIZE);
                }
            }
        }

        static DllImports()
        {
            StdOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            ExceptionHelper.ValidateMagic(StdOutputHandle != new IntPtr(INVALID_HANDLE_VALUE) && StdOutputHandle != new IntPtr(NULL));

            mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
        }

        public static void DisableWindowButtons()
        {
            // Backup
            // SetWindowLong(mainWindowHandle, GWL_STYLE, GetWindowLong(mainWindowHandle, GWL_STYLE) & ~(WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX));
            SetWindowLong(mainWindowHandle, GWL_STYLE, GetWindowLong(mainWindowHandle, GWL_STYLE) & ~WS_CAPTION);
        }

        public static bool IsKeyDown(ConsoleKey key)
        {
            return (GetKeyState((int)key) & KEY_PRESSED) != 0;
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
        private static extern bool GetConsoleDisplayMode(out uint lpModeFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleDisplayMode(IntPtr hConsoleOutput, uint dwFlags, out COORD lpNewScreenBufferDimensions);

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

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

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
    }
}
