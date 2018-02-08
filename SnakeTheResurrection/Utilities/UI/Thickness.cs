namespace SnakeTheResurrection.Utilities.UI
{
    public struct Thickness
    {
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }

        public Thickness(int thickness) : this(thickness, thickness)
        {

        }

        public Thickness(int horizontal, int vertical) : this(horizontal, vertical, horizontal, vertical)
        {

        }

        public Thickness(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Thickness))
            {
                return false;
            }

            return this == (Thickness)obj;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() + (Right.GetHashCode() * Bottom.GetHashCode());
        }

        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return t1.Left == t2.Left
                && t1.Top == t2.Top
                && t1.Right == t2.Right
                && t1.Bottom == t2.Bottom;
        }

        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !(t1 == t2);
        }
    }
}
