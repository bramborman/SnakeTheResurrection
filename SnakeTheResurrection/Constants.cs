using SnakeTheResurrection.Utilities;
using System.Text;

namespace SnakeTheResurrection
{
    public static class Constants
    {
        public const string APP_SHORT_NAME       = "Snake";
        public const string APP_NAME_ADDITION    = "The Resurrection";
        public const string APP_NAME             = APP_SHORT_NAME + " " + APP_NAME_ADDITION;
        public const short ACCENT_COLOR          = FastColors.Green;
        public const short ACCENT_COLOR_DARK     = FastColors.DarkGreen;
        public const short FOREGROUND_COLOR      = FastColors.White;
        public const short BACKGROUND_COLOR      = FastColors.Black;

        public static readonly Encoding encoding = Encoding.UTF8;
    }
}
