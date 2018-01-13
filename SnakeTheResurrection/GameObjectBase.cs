using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public abstract class GameObjectBase
    {
        public readonly int size;

        public int x;
        public int y;

        protected GameObjectBase(int size)
        {
            this.size = size;
        }

        public bool HitTest(GameObjectBase g)
        {
            return x <= g.x + g.size - 1 && x + size - 1 >= g.x && y <= g.y + g.size - 1 && y + size - 1 >= g.y;
        }

        public void AlignToGrid()
        {
            int padding = (BLOCK_SIZE % size) / 2;
            x = x - (x % BLOCK_SIZE) + (gameBoardLeft % BLOCK_SIZE) + padding;
            y = y - (y % BLOCK_SIZE) + (gameBoardTop % BLOCK_SIZE) + padding;
        }

        public static bool IsInGameBoard(int x, int y, int size)
        {
            return x >= gameBoardLeft && y >= gameBoardTop && x + size <= gameBoardRight && y + size <= gameBoardBottom;
        }
    }
}
