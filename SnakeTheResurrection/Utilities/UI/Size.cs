namespace SnakeTheResurrection.Utilities.UI
{
    public struct Size
    {
        public int Width { get; }
        public int Height { get; }

        public Size(int size) : this(size, size)
        {

        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
            {
                return false;
            }

            return this == (Size)obj;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        public static bool operator ==(Size s1, Size s2)
        {
            return s1.Width == s2.Width && s1.Height == s2.Height;
        }

        public static bool operator != (Size s1, Size s2)
        {
            return !(s1 == s2);
        }
    }
}
