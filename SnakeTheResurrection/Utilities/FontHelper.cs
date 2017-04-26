using System;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public static class FontHelper
    {
        public static unsafe void SetFont(string fontName, short x, short y)
        {
            CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX()
            {
                dwFontSize = new DllImports.COORD(x, y)
            };

            info.cbSize = (uint)Marshal.SizeOf(info);

            Marshal.Copy(fontName.ToCharArray(), 0, new IntPtr(info.FaceName), fontName.Length);
            SetCurrentConsoleFontEx(DllImports.StdOutputHandle, false, ref info);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct CONSOLE_FONT_INFO_EX
        {
            public uint cbSize;
            public uint nFont;
            public DllImports.COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            public fixed char FaceName[32];
        }
    }
}
