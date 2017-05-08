using System;
using System.Text;

namespace SnakeTheResurrection
{
    public static class Constants
    {
        public const string APP_SHORT_NAME          = "Snake";
        public const string APP_NAME_ADDITION       = "The Resurrection";
        public const string APP_NAME                = APP_SHORT_NAME + " " + APP_NAME_ADDITION;
        public const ConsoleColor ACCENT_COLOR      = ConsoleColor.Green;
        public const ConsoleColor ACCENT_COLOR_DARK = ConsoleColor.DarkGreen;
        public const ConsoleColor FOREGROUND_COLOR  = ConsoleColor.White;
        public const ConsoleColor BACKGROUND_COLOR  = ConsoleColor.Black;

        public static readonly Encoding encoding    = Encoding.UTF8;
    }
}
