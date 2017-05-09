using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTheResurrection.Utilities
{
    public static class InputCacher
    {
        private static readonly List<ConsoleKey> cache = new List<ConsoleKey>();

        private static CancellationTokenSource cts;
        private static Task inputCachingTask;
        
        public static void StartCaching()
        {
            if (cts != null)
            {
                throw new InvalidOperationException();
            }

            ClearCache();
            cts = new CancellationTokenSource();
            inputCachingTask = Task.Factory.StartNew(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        cache.Add(Console.ReadKey(true).Key);
                    }
                
                    Thread.Sleep(10);
                }
            }, cts.Token);
        }

        public static void StopCaching()
        {
            if (cts == null)
            {
                throw new InvalidOperationException();
            }
            
            cts.Cancel();
            inputCachingTask.Wait();

            inputCachingTask.Dispose();
            inputCachingTask = null;

            cts.Dispose();
            cts = null;
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
        
        public static bool WasKeyPressed(ConsoleKey key)
        {
            if (cts != null)
            {
                throw new InvalidOperationException();
            }

            return DllImports.IsKeyDown(key) || cache.Contains(key);
        }
    }
}
