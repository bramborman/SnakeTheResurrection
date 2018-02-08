namespace SnakeTheResurrection.Utilities.UI
{
    public struct Rectangle
    {
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
            X = left;
            Y = top;
            Width = right - left;
            Height = bottom - top;
        }

        public Rectangle(int x, int y, Size size)
        {
            Left = x;
            Top = y;
            Right = x + size.Width;
            Bottom = y + size.Height;
            X = x;
            Y = y;
            Width = size.Width;
            Height = size.Height;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
            {
                return false;
            }

            return this == (Rectangle)obj;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() + (Right.GetHashCode() * Bottom.GetHashCode());
        }

        public static bool operator ==(Rectangle t1, Rectangle t2)
        {
            return t1.Left == t2.Left
                && t1.Top == t2.Top
                && t1.Right == t2.Right
                && t1.Bottom == t2.Bottom;
        }

        public static bool operator !=(Rectangle t1, Rectangle t2)
        {
            return !(t1 == t2);
        }
    }
}
