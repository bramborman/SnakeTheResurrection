using SnakeTheResurrection.Utilities.UI;
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

        private static int lowestFrameX;
        private static int lowestFrameY;
        private static int uppermostFrameX;
        private static int uppermostFrameY;
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
                ExceptionHelper.ValidateMagic(WriteConsoleOutput(DllImports.stdOutputHandle, lpBuffer, new DllImports.COORD(windowWidth, windowHeight), new DllImports.COORD(), ref lpWriteRegion));

                Console.CursorVisible = false;
                
                bufferHeight    = windowHeight;
                bufferWidth     = windowWidth;

                lpAttribute = new short[lpBuffer.Length];
                ResetFrameBounds();
            }
        }

        public static unsafe void RenderFrame()
        {
            if (lowestFrameX <= uppermostFrameX && lowestFrameY <= uppermostFrameY)
            {
                fixed (short* origin = lpAttribute)
                {
                    short* ptr = origin + (lowestFrameY * bufferWidth) + lowestFrameX;
                    int length = (bufferWidth - lowestFrameX) + ((uppermostFrameY - lowestFrameY - 1) * bufferWidth) + (bufferWidth - (bufferWidth - uppermostFrameX - 1));
                    DllImports.COORD coord = new DllImports.COORD((short)lowestFrameX, (short)lowestFrameY);

                    ExceptionHelper.ValidateMagic(WriteConsoleOutputAttribute(DllImports.stdOutputHandle, ptr, length, coord, out int lpNumberOfAttrsWritten));
                }

                ResetFrameBounds();
            }
        }

        public static class Safe
        {
            public static void AddToBuffer(in Color color, in int x, in int y, in int width, in int height, in Rectangle bounds)
            {
                short shortColor = (short)color;
                int left    = Math.Max(x, bounds.Left);
                int top     = Math.Max(y, bounds.Top);
                int right   = Math.Min(x + width, bounds.Right);
                int bottom  = Math.Min(y + height, bounds.Bottom);

                for (; top < bottom; top++)
                {
                    for (int column = left; column < right; column++)
                    {
                        lpAttribute[(top * bufferWidth) + column] = shortColor;
                    }
                }
            }

            public static void AddToBuffer(in bool[,] element, in Color color, in int x, in int y, in Rectangle elementBounds, in Rectangle bounds)
            {
                short shortColor = (short)color;
                int left    = Math.Max(Math.Max(x, elementBounds.Left), bounds.Left);
                int top     = Math.Max(Math.Max(y, elementBounds.Top), bounds.Top);
                int right   = Math.Min(Math.Min(x + element.GetLength(1), elementBounds.Right), bounds.Right);
                int bottom  = Math.Min(Math.Min(y + element.GetLength(0), elementBounds.Bottom), bounds.Bottom);

                for (; top < bottom; top++)
                {
                    for (int column = left; column < right; column++)
                    {
                        if (element[top - y, column - x])
                        {
                            lpAttribute[(top * bufferWidth) + column] = shortColor;
                        }
                    }
                }
            }

            public static void RenderFrame()
            {
                ExceptionHelper.ValidateMagic(WriteConsoleOutputAttribute(DllImports.stdOutputHandle, lpAttribute, lpAttribute.Length, new DllImports.COORD(), out int lpNumberOfAttrsWritten));
            }
        }

        public static class Fast
        {
            // Once everything UI-related is ported to UI, rewrite old Renderer methods here
        }
        
        public static void AddToBuffer(bool[,] element, short color, int x, int y)
        {
            lock (syncRoot)
            {
                int right = x + element.GetLength(1);
                int bottom = y + element.GetLength(0);

                AssignFrameBounds(x, y, right - x, bottom - y);
                
                for (int row = y; row < bottom; row++)
                {
                    for (int column = x; column < right; column++)
                    {
                        if (element[row - y, column - x])
                        {
                            lpAttribute[(row * bufferWidth) + column] = color;
                        }
                    }
                }
            }
        }

        public static void AddToBuffer(bool[,] element, short foregroundColor, short backgroundColor, int x, int y)
        {
            lock (syncRoot)
            {
                int elementHeight = element.GetLength(0);
                int elementWidth = element.GetLength(1);

                AssignFrameBounds(x, y, elementWidth, elementHeight);

                int right = Math.Min(x + elementWidth, Console.WindowWidth);
                int bottom = Math.Min(y + elementHeight, Console.WindowHeight);
                
                for (int row = y; row < bottom; row++)
                {
                    for (int column = x; column < right; column++)
                    {
                        lpAttribute[(row * bufferWidth) + column] = element[row - y, column - x] ? foregroundColor : backgroundColor;
                    }
                }
            }
        }

        public static void AddToBuffer(short[,] element, int x, int y)
        {
            lock (syncRoot)
            {
                int elementHeight = element.GetLength(0);
                int elementWidth = element.GetLength(1);

                AssignFrameBounds(x, y, elementWidth, elementHeight);

                int right = Math.Min(x + elementWidth, Console.WindowWidth);
                int bottom = Math.Min(y + elementHeight, Console.WindowHeight);

                for (int row = y; row < bottom; row++)
                {
                    for (int column = x; column < right; column++)
                    {
                        lpAttribute[(row * bufferWidth) + column] = element[row - y, column - x];
                    }
                }
            }
        }

        public static void AddToBuffer(short color, int x, int y, int width, int height)
        {
            lock (syncRoot)
            {
                AssignFrameBounds(x, y, width, height);

                int right = Math.Min(x + width, Console.WindowWidth);
                int bottom = Math.Min(y + height, Console.WindowHeight);

                for (int row = y; row < bottom; row++)
                {
                    for (int column = x; column < right; column++)
                    {
                        lpAttribute[(row * bufferWidth) + column] = color;
                    }
                }
            }
        }

        public static void RemoveFromBuffer(int x, int y, int width, int height)
        {
            AddToBuffer(Constants.BACKGROUND_COLOR, x, y, width, height);
        }

        public static bool[,] Scale(bool[,] element, int multiplier)
        {
            if (multiplier == 1)
            {
                return element;
            }

            int height = element.GetLength(0);
            int width = element.GetLength(1);
            bool[,] output = new bool[height * multiplier, width * multiplier];
            bool currentValue = false;

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    currentValue = element[row, column];

                    for (int row2 = 0; row2 < multiplier; row2++)
                    {
                        for (int column2 = 0; column2 < multiplier; column2++)
                        {
                            output[(row * multiplier) + row2, (column * multiplier) + column2] = currentValue;
                        }
                    }
                }
            }

            return output;
        }

        private static void AssignFrameBounds(int x, int y, int width, int height)
        {
            if (x < lowestFrameX)
            {
                lowestFrameX = x;
            }

            if (y < lowestFrameY)
            {
                lowestFrameY = y;
            }

            // DONT REMOVE '-1'
            // Imagine the following situation
            // y = 0, height = 1
            // using just 'y + height' would
            // return '1' but the uppermost
            // index used is '0' so that's
            // why the '-1' is here
            int right = x + width - 1;
            int bottom = y + height - 1;

            if (right > uppermostFrameX)
            {
                uppermostFrameX = right;
            }

            if (bottom > uppermostFrameY)
            {
                uppermostFrameY = bottom;
            }
        }

        private static void SetFullscreenFrameBounds()
        {
            lowestFrameX = 0;
            lowestFrameY = 0;
            uppermostFrameX = bufferWidth - 1;
            uppermostFrameY = bufferHeight - 1;
        }

        private static void ResetFrameBounds()
        {
            lowestFrameX = bufferWidth;
            lowestFrameY = bufferHeight;
            uppermostFrameX = 0;
            uppermostFrameY = 0;
        }

        public static short GetColorOnCoordinates(int x, int y)
        {
            return lpAttribute[(y * bufferWidth) + x];
        }

        public static void Clear()
        {
            Array.Clear(lpAttribute, 0, lpAttribute.Length);
            SetFullscreenFrameBounds();
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
                SetFullscreenFrameBounds();

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
                SetFullscreenFrameBounds();
                RenderFrame();

                bufferBackups.Remove(key);

                if (bufferBackups.Count == 0)
                {
                    bufferBackups = null;
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static unsafe extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short* lpAttribute, int nLength, DllImports.COORD dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll")]
        private static unsafe extern bool WriteConsoleOutputAttribute(IntPtr hConsoleOutput, short[] lpAttribute, int nLength, DllImports.COORD dwWriteCoord, out int lpNumberOfAttrsWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, DllImports.CHAR_INFO[] lpBuffer, DllImports.COORD dwBufferSize, DllImports.COORD dwBufferCoord, ref DllImports.SMALL_RECT lpWriteRegion);
    }
}
