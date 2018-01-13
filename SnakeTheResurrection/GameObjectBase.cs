using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public abstract class GameObjectBase
    {
        public readonly int size;

        public int X { get; protected set; }
        public int Y { get; protected set; }

        protected GameObjectBase(int size)
        {
            this.size = size;
        }

        public bool HitTest(GameObjectBase g)
        {
            return X <= g.X + g.size - 1 && X + size - 1 >= g.X && Y <= g.Y + g.size - 1 && Y + size - 1 >= g.Y;
        }

        public void AlignToGrid()
        {
            int padding = (BLOCK_SIZE % size) / 2;
            X = X - (X % BLOCK_SIZE) + (gameBoardLeft % BLOCK_SIZE) + padding;
            Y = Y - (Y % BLOCK_SIZE) + (gameBoardTop % BLOCK_SIZE) + padding;
        }

        public static bool IsInGameBoard(int x, int y, int size)
        {
            return x >= gameBoardLeft && y >= gameBoardTop && x + size <= gameBoardRight && y + size <= gameBoardBottom;
        }
    }
}
