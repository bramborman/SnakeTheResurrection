using System;

namespace SnakeTheResurrection.Utilities.UI
{
    public struct Color
    {
        private int color;
        
        private Color(int color)
        {
            this.color = color;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
            {
                return false;
            }

            return this == (Color)obj;
        }

        public override int GetHashCode()
        {
            return color.GetHashCode();
        }

        public static explicit operator int(Color color)
        {
            return color.color;
        }

        public static explicit operator Color(int color)
        {
            return new Color(color);
        }

        public static explicit operator ConsoleColor(Color color)
        {
            return (ConsoleColor)color.color;
        }

        public static explicit operator Color(ConsoleColor color)
        {
            return new Color((int)color);
        }

        public static bool operator ==(Color c1, Color c2)
        {
            return c1.color == c2.color;
        }

        public static bool operator !=(Color c1, Color c2)
        {
            return !(c1.color == c2.color);
        }
    }
}
