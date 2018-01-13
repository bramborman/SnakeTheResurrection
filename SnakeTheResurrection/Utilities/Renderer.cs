using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SnakeTheResurrection.Utilities
{
    public static class Renderer
    {
        private static readonly object syncRoot = new object();

        private static readonly int bufferHeight;
        private static readonly int bufferWidth;
        private static readonly short[] lpAttribute;

        private static Dictionary<object, ConsoleColor[,]> bufferBackups;

        public static ConsoleColor[,] Buffer { get; private set; }

        static Renderer()
        {
            lock (syncRoot)
            {
                // This is brighter, but the other one looks more retro xD
                // DllImports.SetFont("Consolas", 2, 2);
                DllImports.SetFont("Lucida Console", 1, 1);
                DllImports.DisableWindowButtons();
                DllImports.ConsoleFullscreen = true;

                // Make it a real fullscreen :D
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                short windowHeight  = (short)Console.WindowHeight;
                short windowWidth   = (short)Console.WindowWidth;
                Console.SetBufferSize(windowWidth, windowHeight);

                DllImports.CHAR_INFO[] lpBuffer = new DllImports.CHAR_INFO[windowWidth * windowHeight];

                for (int i = 0; i < lpBuffer.Length; i++)
                {
                    // Fill the buffer with black full chars
                    lpBuffer[i].Char.AsciiChar = 219;
                }

                DllImports.SMALL_RECT lpWriteRegion = new DllImports.SMALL_RECT(0, 0, windowWidth, windowHeight);
                ExceptionHelper.ValidateMagic(WriteConsoleOutput(DllImports.StdOutputHandle, lpBuffer, new DllImports.COORD(windowWidth, windowHeight), new DllImports.COORD(), ref lpWriteRegion));

                Console.CursorVisible = false;

                Buffer          = new ConsoleColor[Console.WindowHeight, Console.WindowWidth];
                lpAttribute     = new short[Buffer.Length];

                bufferHeight    = Buffer.GetLength(0);
                bufferWidth     = Buffer.GetLength(1);
            }
        }

        public static void RenderFrame()
        {
            RenderRectangle(0, 0, bufferWidth, bufferHeight);
            DisplayFrame();
        }

        public static void DisplayFrame()
        {
            ExceptionHelper.ValidateMagic(WriteConsoleOutputAttribute(DllImports.StdOutputHandle, lpAttribute, lpAttribute.Length, new DllImports.COORD(), out int lpNumberOfAttrsWritten));
        }

        private static void RenderRectangle(int x, int y, int width, int height)
        {
            lock (syncRoot)
            {
                for (int row = y; row < y + height; row++)
                {
                    for (int column = x; column < x + width; column++)
                    {
                        lpAttribute[(row * bufferWidth) + column] = (short)Buffer[row, column];
                    }
                }
            }
        }

        public static void AddToBuffer(ConsoleColor[,] element, int x, int y)
        {
            lock (syncRoot)
            {
                int elementWidth = element.GetLength(1);

                for (int row = 0; row < element.GetLength(0); row++)
                {
                    Array.Copy(element, row * elementWidth, Buffer, ((y + row) * bufferWidth) + x, elementWidth);
                }
            }
        }

        public static void AddToBuffer(ConsoleColor color, int x, int y, int width, int height)
        {
            lock (syncRoot)
            {
                for (int row = y; row < y + height; row++)
                {
                    for (int column = x; column < x + width; column++)
                    {
                        Buffer[row, column] = color;
                    }
                }
            }
        }

        public static void AddToBufferAndRender(ConsoleColor[,] element, int x, int y)
        {
            AddToBuffer(element, x, y);
            RenderRectangle(x, y, element.GetLength(1), element.GetLength(0));
        }

        public static void AddToBufferAndRender(ConsoleColor color, int x, int y, int width, int height)
        {
            AddToBuffer(color, x, y, width, height);
            RenderRectangle(x, y, width, height);
        }

        public static void RemoveFromBuffer(int x, int y, int height, int width)
        {
            AddToBuffer(Constants.BACKGROUND_COLOR, x, y, width, height);
        }

        public static void RemoveFromBufferAndRender(int x, int y, int height, int width)
        {
            RemoveFromBuffer(x, y, height, width);
            RenderRectangle(x, y, width, height);
        }

        public static void ClearBuffer()
        {
            Array.Clear(Buffer, 0, Buffer.Length);
            // AddToBuffer(Constants.BACKGROUND_COLOR, 0, 0, bufferHeight, bufferWidth);
        }

        public static object BackupBuffer()
        {
            lock (syncRoot)
            {
                if (bufferBackups == null)
                {
                    bufferBackups = new Dictionary<object, ConsoleColor[,]>();
                }

                object key = new object();
                bufferBackups.Add(key, Buffer);
                Buffer = new ConsoleColor[bufferHeight, bufferWidth];

                return key;
            }
        }

        public static void RestoreBuffer(object key)
        {
            lock (syncRoot)
            {
                ExceptionHelper.ValidateObjectNotNull(key, nameof(key));
                ExceptionHelper.ValidateObjectNotNull(bufferBackups, null);

                Buffer = bufferBackups[key];
                bufferBackups.Remove(key);

                if (bufferBackups.Count == 0)
                {
                    bufferBackups = null;
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short[] lpAttribute, int nLength, DllImports.COORD dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, DllImports.CHAR_INFO[] lpBuffer, DllImports.COORD dwBufferSize, DllImports.COORD dwBufferCoord, ref DllImports.SMALL_RECT lpWriteRegion);
    }
}
