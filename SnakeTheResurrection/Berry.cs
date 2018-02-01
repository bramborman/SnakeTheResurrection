using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;

namespace SnakeTheResurrection
{
    public sealed class Berry : GameObjectBase
    {
        private const bool o = true;
        private const bool _ = false;

        private static readonly Random random = new Random();
        private static readonly bool[,] texture = new bool[,]
        {
            { _, o, o, o, _ },
            { o, o, o, o, o },
            { o, o, o, o, o },
            { o, o, o, o, o },
            { _, o, o, o, _ }
        };
        private static readonly int textureSize = texture.GetLength(0);

        public static readonly HashSet<Berry> current = new HashSet<Berry>();

        private readonly int gameBoardLeft;
        private readonly int gameBoardTop;
        private readonly int gameBoardRight;
        private readonly int gameBoardBottom;

        public readonly int power;

        private bool generateNew = true;

        public Berry(int power, int gameBoardLeft, int gameBoardTop, int gameBoardRight, int gameBoardBottom) : base(textureSize)
        {
            this.power              = power;
            this.gameBoardLeft      = gameBoardLeft;
            this.gameBoardTop       = gameBoardTop;
            this.gameBoardRight     = gameBoardRight;
            this.gameBoardBottom    = gameBoardBottom;

            current.Add(this);
        }

        public void Update()
        {
            if (generateNew)
            {
                generateNew = false;
                bool regenerate;

                do
                {
                    regenerate = false;

                    x = random.Next(gameBoardLeft, gameBoardRight - size);
                    y = random.Next(gameBoardTop, gameBoardBottom - size);

                    AlignToGrid();

                    // Do not generate berry in a snake xD
                    for (int row = y; row < y + size; row++)
                    {
                        for (int column = x; column < x + size; column++)
                        {
                            if (Renderer.GetColorOnCoordinates(column, row) != Constants.BACKGROUND_COLOR)
                            {
                                regenerate = true;
                                break;
                            }
                        }

                        if (regenerate)
                        {
                            break;
                        }
                    }
                } while (regenerate);

                Renderer.AddToBuffer(texture, Colors.Red, x, y);
            }
        }
            
        public int Eat()
        {
            Renderer.AddToBuffer(Colors.White, x, y, size, size);
            generateNew = true;

            return Cheats.CheatCodeInfo[Cheats.CheatCode.Hungry] ? 2 * power : power;
        }

        public static void Reset()
        {
            current.Clear();
        }
    }
}
