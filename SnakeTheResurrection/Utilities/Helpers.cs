using System;

namespace SnakeTheResurrection.Utilities
{
    public static class Helpers
    {
        public static ConsoleKeyInfo ReadKey()
        {
            return Cheats.ValidateCheat(Console.ReadKey(true));
        }
    }
}
