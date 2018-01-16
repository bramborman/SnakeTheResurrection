using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTheResurrection.Utilities
{
    public static class InputHelper
    {
        private static readonly HashSet<ConsoleKey> cache       = new HashSet<ConsoleKey>();
        private static readonly ManualResetEventSlim resetEvent = new ManualResetEventSlim(false);

        static InputHelper()
        {
            Task.Run(async () =>
            {
                while (resetEvent.WaitHandle.WaitOne())
                {
                    if (Console.KeyAvailable)
                    {
                        cache.Add(Console.ReadKey(true).Key);
                    }

                    await Task.Delay(10);
                }
            });
        }
        
        public static void StartCaching()
        {
            resetEvent.Set();
        }

        public static void StopCaching()
        {
            resetEvent.Reset();
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
        
        public static bool WasKeyPressed(ConsoleKey key)
        {
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
