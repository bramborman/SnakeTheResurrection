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
        
        private static short[] lpAttribute;
        private static Dictionary<object, short[]> bufferBackups;
        
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

                lpAttribute     = new short[lpBuffer.Length];

                bufferHeight    = windowHeight;
                bufferWidth     = windowWidth;
            }
        }

        public static void RenderFrame()
        {
            ExceptionHelper.ValidateMagic(WriteConsoleOutputAttribute(DllImports.StdOutputHandle, lpAttribute, lpAttribute.Length, new DllImports.COORD(), out int lpNumberOfAttrsWritten));
        }
        
        public static void AddToBuffer(short[,] element, int x, int y)
        {
            lock (syncRoot)
            {
                int elementHeight = element.GetLength(0);
                int elementWidth = element.GetLength(1);

                for (int row = 0; row < elementHeight; row++)
                {
                    for (int column = 0; column < elementWidth; column++)
                    {
                        lpAttribute[((y + row) * bufferWidth) + x + column] = element[row, column];
                    }
                }
            }
        }

        public static void AddToBuffer(short color, int x, int y, int width, int height)
        {
            lock (syncRoot)
            {
                for (int row = y; row < y + height; row++)
                {
                    for (int column = x; column < x + width; column++)
                    {
                        lpAttribute[(row * bufferWidth) + column] = color;
                    }
                }
            }
        }

        public static void RemoveFromBuffer(int x, int y, int height, int width)
        {
            AddToBuffer(Constants.BACKGROUND_COLOR, x, y, width, height);
        }

        public static short GetColorOnCoordinates(int x, int y)
        {
            return lpAttribute[(y * bufferWidth) + x];
        }

        public static void ClearBuffer()
        {
            Array.Clear(lpAttribute, 0, lpAttribute.Length);
            // AddToBuffer(Constants.BACKGROUND_COLOR, 0, 0, bufferHeight, bufferWidth);
        }

        public static object BackupBuffer()
        {
            lock (syncRoot)
            {
                if (bufferBackups == null)
                {
                    bufferBackups = new Dictionary<object, short[]>();
                }

                object key = new object();
                bufferBackups.Add(key, lpAttribute);
                lpAttribute = new short[lpAttribute.Length];

                return key;
            }
        }

        public static void RestoreBuffer(object key)
        {
            lock (syncRoot)
            {
                ExceptionHelper.ValidateObjectNotNull(key, nameof(key));
                ExceptionHelper.ValidateObjectNotNull(bufferBackups, null);

                lpAttribute = bufferBackups[key];
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
