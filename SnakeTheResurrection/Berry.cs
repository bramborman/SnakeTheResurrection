using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public sealed class Berry : GameObjectBase
    {
        private const ConsoleColor o = ConsoleColor.Red;
        private const ConsoleColor _ = Constants.BACKGROUND_COLOR;

        private static readonly Random random = new Random();
        private static readonly ConsoleColor[,] texture = new ConsoleColor[,]
        {
            { _, o, o, o, _ },
            { o, o, o, o, o },
            { o, o, o, o, o },
            { o, o, o, o, o },
            { _, o, o, o, _ }
        };
        private static readonly int textureSize = texture.GetLength(0);

        public static readonly HashSet<Berry> current = new HashSet<Berry>();

        public readonly ConsoleColor color;
        public readonly int power;

        private bool generateNew = true;

        public Berry(int power) : base(textureSize)
        {
            color = ConsoleColor.Red;
            this.power = power;

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
                            if (Renderer.Buffer[row, column] != Constants.BACKGROUND_COLOR)
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

                Renderer.AddToBufferAndRender(texture, x, y);
            }
        }
            
        public int Eat()
        {
            Renderer.AddToBufferAndRender(ConsoleColor.White, x, y, size, size);
            generateNew = true;

            return Cheats.CheatCodeInfo[Cheats.CheatCode.Hungry] ? 2 * power : power;
        }

        public static void Reset()
        {
            current.Clear();
        }
    }
}
