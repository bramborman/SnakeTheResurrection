using System;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public sealed class Renderer
    {
        private static bool isInitialized = false;

        private readonly int bufferHeight;
        private readonly int bufferWidth;
        private readonly short[] lpAttribute;

        public ConsoleColor[,] Buffer { get; }

        public Renderer()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                
                DllImports.SetFont("Lucida Console", 1, 1);
                DllImports.ConsoleFullscreenMode = true;

                short windowHeight  = (short)Console.WindowHeight;
                short windowWidth   = (short)Console.WindowWidth;
                Console.SetBufferSize(windowWidth, windowHeight);

                DllImports.CHAR_INFO[] lpBuffer = new DllImports.CHAR_INFO[windowWidth * windowHeight];
                short attributes = (short)Constants.BACKGROUND_COLOR;

                for (int i = 0; i < lpBuffer.Length; i++)
                {
                    // Fill the buffer with black full chars
                    lpBuffer[i].Char.AsciiChar  = 219;
                    lpBuffer[i].Attributes      = attributes;
                }

                DllImports.SMALL_RECT lpWriteRegion = new DllImports.SMALL_RECT(0, 0, windowWidth, windowHeight);
                ExceptionHelper.ValidateMagic(WriteConsoleOutput(DllImports.StdOutputHandle, lpBuffer, new DllImports.COORD(windowWidth, windowHeight), new DllImports.COORD(), ref lpWriteRegion));

                Console.CursorVisible = false;
            }

            Buffer          = new ConsoleColor[Console.WindowHeight, Console.WindowWidth];
            lpAttribute     = new short[Buffer.Length];

            bufferHeight    = Buffer.GetLength(0);
            bufferWidth     = Buffer.GetLength(1);
        }

        public void RenderFrame()
        {
            for (int row = 0; row < bufferHeight; row++)
            {
                for (int column = 0; column < bufferWidth; column++)
                {
                    lpAttribute[(row * bufferWidth) + column] = (short)Buffer[row, column];
                }
            }
            
            int lpNumberOfAttrsWritten;
            ExceptionHelper.ValidateMagic(WriteConsoleOutputAttribute(DllImports.StdOutputHandle, lpAttribute, lpAttribute.Length, new DllImports.COORD(), out lpNumberOfAttrsWritten));
        }

        public void AddToBuffer(ConsoleColor[,] element, int x, int y)
        {
            int elementHeight   = element.GetLength(0);
            int elementWidth    = element.GetLength(1);

            for (int row = 0; row < elementHeight; row++)
            {
                for (int column = 0; column < elementWidth; column++)
                {
                    Buffer[row + y, column + x] = element[row, column];
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short[] lpAttribute, int nLength, DllImports.COORD dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, DllImports.CHAR_INFO[] lpBuffer, DllImports.COORD dwBufferSize, DllImports.COORD dwBufferCoord, ref DllImports.SMALL_RECT lpWriteRegion);
    }
}
