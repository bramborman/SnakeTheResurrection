namespace SnakeTheResurrection
{
    public abstract class GameObjectBase
    {
        protected readonly int blockSize;
        protected readonly int gameBoardLeft;
        protected readonly int gameBoardTop;
        protected readonly int gameBoardRight;
        protected readonly int gameBoardBottom;

        public readonly int size;

        public int x;
        public int y;

        protected GameObjectBase(int size, int blockSize, int gameBoardLeft, int gameBoardTop, int gameBoardRight, int gameBoardBottom)
        {
            this.size = size;
            this.blockSize = blockSize;
            this.gameBoardLeft      = gameBoardLeft;
            this.gameBoardTop       = gameBoardTop;
            this.gameBoardRight     = gameBoardRight;
            this.gameBoardBottom    = gameBoardBottom;
        }

        public bool HitTest(GameObjectBase g)
        {
            return x <= g.x + g.size - 1 && x + size - 1 >= g.x && y <= g.y + g.size - 1 && y + size - 1 >= g.y;
        }

        public void AlignToGrid()
        {
            int padding = (blockSize % size) / 2;
            x = x - (x % blockSize) + (gameBoardLeft % blockSize) + padding;
            y = y - (y % blockSize) + (gameBoardTop % blockSize) + padding;
        }

        public static bool IsInGameBoard(int x, int y, int size, int gameBoardLeft, int gameBoardTop, int gameBoardRight, int gameBoardBottom)
        {
            return x >= gameBoardLeft && y >= gameBoardTop && x + size <= gameBoardRight && y + size <= gameBoardBottom;
        }
    }
}
