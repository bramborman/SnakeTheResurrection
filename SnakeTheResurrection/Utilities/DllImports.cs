using System;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public unsafe static class DllImports
    {
        private const int STD_OUTPUT_HANDLE = -11;
        
        public static IntPtr StdOutputHandle
        {
            get
            {
                IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);

                // Comparing it with INVALID_HANDLE_VALUE and NULL.
                if (stdHandle == new IntPtr(-1) || stdHandle == new IntPtr(0))
                {
                    MessageBox(@"We are so sorry, but some unknown dark power prevented us from doing the required magic ¯\_ツ_/¯", "No magic");
                }

                return stdHandle;
            }
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

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        private static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
    }
}
