using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SnakeTheResurrection.Utilities.UI
{
    public static class Window
    {
        public static int Width
        {
            get { return Console.WindowWidth; }
            set { Console.WindowWidth = value; }
        }
        public static int Height
        {
            get { return Console.WindowHeight; }
            set { Console.WindowHeight = value; }
        }
        public static List<UIElement> Children { get; } = new List<UIElement>();


        public static class Compositor
        {
            private static readonly ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);
            private static readonly Thread renderingThread = new Thread(Rendering);

            public static int TargetFramerate { get; set; } = 60;

            public static event Action BeforeRendering;

            public static void Run()
            {
                if (!renderingThread.IsAlive)
                {
                    renderingThread.Start();
                }

                resetEvent.Set();
            }

            public static void Stop()
            {
                resetEvent.Reset();
            }

            private static void Rendering()
            {
                Stopwatch stopwatch = new Stopwatch();

                while (resetEvent.Wait(-1))
                {
                    stopwatch.Restart();
                    Renderer.Clear();

                    BeforeRendering?.Invoke();

                    foreach (UIElement child in Children)
                    {
                        child.Render();
                    }

                    Renderer.RenderFrame();
                    stopwatch.Stop();

                    int currentDelay = (1000 / TargetFramerate) - (int)stopwatch.ElapsedMilliseconds;

                    if (currentDelay > 0)
                    {
                        Thread.Sleep(currentDelay);
                    }
                }
            }
        }
    }
}
