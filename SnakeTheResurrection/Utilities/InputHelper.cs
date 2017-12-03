using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTheResurrection.Utilities
{
    public static class InputHelper
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
            
            cts = new CancellationTokenSource();
            inputCachingTask = Task.Run(() =>
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
            
            try
            {
                cts.Cancel();
                inputCachingTask.Wait();
            }
            catch
            {

            }
            finally
            {
                inputCachingTask.Dispose();
                inputCachingTask = null;

                cts.Dispose();
                cts = null;
            }
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
        public static ConsoleKeyInfo ReadKey()
        {
            return Cheats.ValidateCheat(Console.ReadKey(true));
        }

        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
