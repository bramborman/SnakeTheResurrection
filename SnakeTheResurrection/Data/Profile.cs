using System;

namespace SnakeTheResurrection.Data
{
    public sealed class Profile
    {
        public string Name { get; set; }
        public short Color { get; set; }
        public SnakeControls SnakeControls { get; set; }

        public Profile()
        {
            SnakeControls = new SnakeControls();
        }
    }

    public sealed class SnakeControls
    {
        public ConsoleKey Up { get; set; }
        public ConsoleKey Down { get; set; }
        public ConsoleKey Left { get; set; }
        public ConsoleKey Right { get; set; }

        public SnakeControls()
        {
            Up      = ConsoleKey.UpArrow;
            Down    = ConsoleKey.DownArrow;
            Left    = ConsoleKey.LeftArrow;
            Right   = ConsoleKey.RightArrow;
        }
    }
}
