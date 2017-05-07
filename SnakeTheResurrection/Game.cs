using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeTheResurrection
{
    public sealed class Game
    {
        public void Start()
        {
            Snake snake = new Snake(ProfileManager.CurrentProfile);

            while (snake.IsAlive)
            {
                snake.Update();
                Renderer.RenderFrame();
                Thread.Sleep(100);
            }
        }

        private interface IGameObject
        {
            void Update();
        }

        private sealed class Snake : IGameObject
        {
            private readonly List<SnakeBody> snakeBodies = new List<SnakeBody>();

            public bool IsAlive { get; private set; }
            public int Length
            {
                get { return snakeBodies.Count; }
            }
            public Profile Profile { get; }
            public SnakeBody Head
            {
                get { return snakeBodies.FirstOrDefault(); }
            }

            public Snake(Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                IsAlive = true;
                Profile = profile;
            }

            public void Update()
            {
                if (snakeBodies.Count < 3)
                {
                    AddBody();
                }

                Head.Update(true);

                for (int i = 1; i < snakeBodies.Count; i++)
                {
                    snakeBodies[i].Update(false);
                }
            }

            private void AddBody()
            {
                snakeBodies.Add(new SnakeBody((Console.WindowWidth - SnakeBody.Size) / 2, (Console.WindowHeight - SnakeBody.Size) / 2, Profile.Color, Direction.Up));
            }
        }


        private sealed class SnakeBody
        {
            public static int Size
            {
                get { return 5; }
            }

            private int _x;
            private int _y;
            private Direction _direction;

            public int X
            {
                get { return _x; }
                private set
                {
                    if (_x != value)
                    {
                        ExceptionHelper.ValidateNumberInWindowHorizontalRange(value, nameof(X));
                        _x = value;
                    }
                }
            }
            public int Y
            {
                get { return _y; }
                private set
                {
                    if (_y != value)
                    {
                        ExceptionHelper.ValidateNumberInWindowHorizontalRange(value, nameof(Y));
                        _y = value;
                    }
                }
            }
            public Direction Direction
            {
                get { return _direction; }
                set
                {
                    if (_direction != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(Direction));
                        _direction = value;
                    }
                }
            }
            public ConsoleColor Color { get; }

            public SnakeBody(int x, int y, ConsoleColor color, Direction direction)
            {
                ExceptionHelper.ValidateEnumValueDefined(color, nameof(color));

                X           = x;
                Y           = y;
                Direction   = direction;
                Color       = color;
            }

            public void Update(bool isHead)
            {
                Renderer.RemoveFromBuffer(X, Y, Size, Size);

                if (isHead)
                {
                    //TODO: Direction change logic
                }

                switch (Direction)
                {
                    case Direction.Left:    X -= Size;  break;
                    case Direction.Up:      Y -= Size;  break;
                    case Direction.Right:   X += Size;  break;
                    case Direction.Down:    Y += Size;  break;
                }

                Renderer.AddToBuffer(Color, X, Y, Size, Size);
            }
        }

        private enum Direction
        {
            Left,
            Up,
            Right,
            Down
        }
    }
}
