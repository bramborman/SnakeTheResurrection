using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public unsafe static class DllImports
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const int KEY_PRESSED = 0x8000;

        public static IntPtr StdOutputHandle
        {
            get
            {
                IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);

                // Comparing it with INVALID_HANDLE_VALUE and NULL.
                ExceptionHelper.ValidateMagic(stdHandle != new IntPtr(-1) && stdHandle != new IntPtr(0));
                return stdHandle;
            }
        }
        public static bool ConsoleFullscreenMode
        {
            get
            {
                uint lpModeFlags;
                ExceptionHelper.ValidateMagic(GetConsoleDisplayMode(out lpModeFlags));

                return lpModeFlags == 1;
            }
            set
            {
                COORD lpNewScreenBufferDimensions;
                ExceptionHelper.ValidateMagic(SetConsoleDisplayMode(StdOutputHandle, (uint)(value ? 1 : 2), out lpNewScreenBufferDimensions));
            }
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
