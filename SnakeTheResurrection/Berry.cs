using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public sealed class Berry : GameObjectBase
    {
        private const ConsoleColor x = ConsoleColor.Red;
        private const ConsoleColor _ = Constants.BACKGROUND_COLOR;

        private static readonly HashSet<Berry> _current     = new HashSet<Berry>();
        private static readonly Random random               = new Random();
        private static readonly ConsoleColor[,] texture     = new ConsoleColor[,]
        {
            { _, x, x, x, _ },
            { x, x, x, x, x },
            { x, x, x, x, x },
            { x, x, x, x, x },
            { _, x, x, x, _ }
        };
        private static readonly int textureSize             = texture.GetLength(0);
            
        public static IEnumerable<Berry> Current
        {
            get
            {
                return _current.AsEnumerable();
            }
        }

        public readonly ConsoleColor color;
        public readonly int power;

        private bool generateNew = true;

        public Berry(int power) : base(textureSize)
        {
            color = ConsoleColor.Red;
            this.power = power;

            _current.Add(this);
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

                    X = random.Next(gameBoardLeft, gameBoardRight - size);
                    Y = random.Next(gameBoardTop, gameBoardBottom - size);

                    AlignToGrid();

                    // Do not generate berry in a snake xD
                    for (int row = Y; row < Y + size; row++)
                    {
                        for (int column = X; column < X + size; column++)
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

                Renderer.AddToBufferAndRender(texture, X, Y);
            }
        }
            
        public int Eat()
        {
            Renderer.AddToBufferAndRender(ConsoleColor.White, X, Y, size, size);
            generateNew = true;

            return power;
        }

        public static void Reset()
        {
            _current.Clear();
        }
    }
}
