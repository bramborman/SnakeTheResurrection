using SnakeTheResurrection.Utilities;
using System.Text;

namespace SnakeTheResurrection
{
    public static class Constants
    {
        public const string APP_SHORT_NAME       = "Snake";
        public const string APP_NAME_ADDITION    = "The Resurrection";
        public const string APP_NAME             = APP_SHORT_NAME + " " + APP_NAME_ADDITION;
        public const short ACCENT_COLOR          = Colors.Green;
        public const short ACCENT_COLOR_DARK     = Colors.DarkGreen;
        public const short FOREGROUND_COLOR      = Colors.White;
        public const short BACKGROUND_COLOR      = Colors.Black;

        public static readonly Encoding encoding = Encoding.UTF8;
    }
}
