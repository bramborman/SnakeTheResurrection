using System;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public sealed class Renderer
    {
        private static bool isInitialized = false;

        public ConsoleColor[,] Buffer { get; }

        public Renderer()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                
                FontHelper.SetFont("Lucida Console", 1, 1);
                DllImports.ConsoleFullscreenMode = true;

                short windowHeight  = (short)Console.WindowHeight;
                short windowWidth   = (short)Console.WindowWidth;
                Console.SetBufferSize(windowWidth, windowHeight);

                DllImports.CHAR_INFO[] lpBuffer = new DllImports.CHAR_INFO[windowWidth * windowHeight];

                for (int i = 0; i < lpBuffer.Length; i++)
                {
                    lpBuffer[i].Char.AsciiChar  = 219;
                    lpBuffer[i].Attributes      = (short)ConsoleColor.Green;
                }

                DllImports.SMALL_RECT lpWriteRegion = new DllImports.SMALL_RECT(0, 0, windowWidth, windowHeight);
                ExceptionHelper.ValidateMagic(WriteConsoleOutput(DllImports.StdOutputHandle, lpBuffer, new DllImports.COORD(windowWidth, windowHeight), new DllImports.COORD(), ref lpWriteRegion));
            }

            Buffer = new ConsoleColor[Console.WindowHeight, Console.WindowWidth];
        }

        public void RenderFrame()
        {
            short[] lpAttribute = new short[Buffer.Length];
            System.Buffer.BlockCopy(Buffer, 0, lpAttribute, 0, Buffer.Length);

            // int bufferWidth  = Buffer.GetLength(0);
            // int bufferHeight = Buffer.GetLength(1);
            // 
            // for (int row = 0; row < bufferHeight; row++)
            // {
            //     for (int column = 0; column < bufferWidth; column++)
            //     {
            //         lpAttribute[row * bufferWidth] = (short)Buffer[row, column];
            //     }
            // }
            
            int lpNumberOfAttrsWritten;
            ExceptionHelper.ValidateMagic(!WriteConsoleOutputAttribute(DllImports.StdOutputHandle, lpAttribute, lpAttribute.Length, new DllImports.COORD(), out lpNumberOfAttrsWritten));
        }

        [DllImport("kernel32.dll")]
        private static extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short[] lpAttribute, int nLength, DllImports.COORD dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, DllImports.CHAR_INFO[] lpBuffer, DllImports.COORD dwBufferSize, DllImports.COORD dwBufferCoord, ref DllImports.SMALL_RECT lpWriteRegion);
    }
}
