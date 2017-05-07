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
        public void SinglePlayer()
        {
            Snake snake = new Snake(ProfileManager.CurrentProfile);

            while (snake.IsAlive)
            {
                snake.Update();
                Renderer.RenderFrame();

                if (DllImports.IsKeyDown(ConsoleKey.Escape))
                {
                    //TODO: Show pause menu
                    return;
                }

                Thread.Sleep(100);
            }
        }

        private interface IGameObject
        {
            void Update();
        }

        private sealed class Snake : IGameObject
        {
            private SnakeBody head;
            private SnakeBody tail;

            private SnakeBody NewCenteredBody
            {
                get
                {
                    return new SnakeBody((Console.WindowWidth - SnakeBody.Size) / 2, (Console.WindowHeight - SnakeBody.Size) / 2, Profile.Color, Direction.Up);
                }
            }

            public bool IsAlive { get; private set; }
            public int Length { get; private set; }
            public Profile Profile { get; }

            public Snake(Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                IsAlive = true;
                Profile = profile;
            }

            public void Update()
            {
                if (Length < 3)
                {
                    AddBody();
                }

                head.Update(true, null);
            }

            private void AddBody()
            {
                if (head == null)
                {
                    head = NewCenteredBody;
                    tail = head;
                }
                else
                {
                    tail.NextBody = NewCenteredBody;
                    tail = tail.NextBody;
                }

                Length++;
            }
        }
        
        private sealed class SnakeBody
        {
            public static int Size
            {
                get { return 5; }
            }

            private readonly List<BendInfo> bendInfo = new List<BendInfo>();

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
                private set
                {
                    if (_direction != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(Direction));
                        _direction = value;
                    }
                }
            }
            public ConsoleColor Color { get; }
            public SnakeBody NextBody { get; set; }

            public SnakeBody(int x, int y, ConsoleColor color, Direction direction)
            {
                ExceptionHelper.ValidateEnumValueDefined(color, nameof(color));

                X           = x;
                Y           = y;
                Direction   = direction;
                Color       = color;
            }

            public void Update(bool isHead, BendInfo newBendInfo)
            {
                Renderer.RemoveFromBuffer(X, Y, Size, Size);
                
                if (isHead)
                {
                    Direction originalDirection = Direction;

                    bool up     = DllImports.IsKeyDown(ConsoleKey.UpArrow);
                    bool down   = DllImports.IsKeyDown(ConsoleKey.DownArrow);
                    bool left   = DllImports.IsKeyDown(ConsoleKey.LeftArrow);
                    bool right  = DllImports.IsKeyDown(ConsoleKey.RightArrow);

                    if (up)
                    {
                        if (left)
                        {
                            if (Direction != Direction.DownRight)
                            {
                                Direction = Direction.UpLeft;
                            }
                        }
                        else if (right)
                        {
                            if (Direction != Direction.DownLeft)
                            {
                                Direction = Direction.UpRight;
                            }
                        }
                        else
                        {
                            if (Direction != Direction.Down)
                            {
                                Direction = Direction.Up;
                            }
                        }
                    }
                    else if (down)
                    {
                        if (left)
                        {
                            if (Direction != Direction.UpRight)
                            {
                                Direction = Direction.DownLeft;
                            }
                        }
                        else if (right)
                        {
                            if (Direction != Direction.UpLeft)
                            {
                                Direction = Direction.DownRight;
                            }
                        }
                        else
                        {
                            if (Direction != Direction.Up)
                            {
                                Direction = Direction.Down;
                            }
                        }
                    }
                    else if (left)
                    {
                        if (Direction != Direction.Right)
                        {
                            Direction = Direction.Left;
                        }
                    }
                    else if (right)
                    {
                        if (Direction != Direction.Left)
                        {
                            Direction = Direction.Right;
                        }
                    }

                    if (Direction != originalDirection)
                    {
                        newBendInfo = new BendInfo(X, Y, Direction);
                    }
                }
                else
                {
                    if (newBendInfo != null)
                    {
                        bendInfo.Add(newBendInfo);
                    }

                    BendInfo currentBendInfo = bendInfo.FirstOrDefault(b => b.X == X && b.Y == Y);

                    if (currentBendInfo != null)
                    {
                        Direction = currentBendInfo.Direction;
                        bendInfo.Remove(currentBendInfo);
                    }
                }
                
                if (Direction == Direction.UpLeft || Direction == Direction.Up || Direction == Direction.UpRight)
                {
                    Y -= Size;
                }
                else if (Direction == Direction.DownLeft || Direction == Direction.Down || Direction == Direction.DownRight)
                {
                    Y += Size;
                }

                if (Direction == Direction.UpLeft || Direction == Direction.Left || Direction == Direction.DownLeft)
                {
                    X -= Size;
                }
                else if (Direction == Direction.UpRight || Direction == Direction.Right || Direction == Direction.DownRight)
                {
                    X += Size;
                }

                Renderer.AddToBuffer(Color, X, Y, Size, Size);
                NextBody?.Update(false, newBendInfo);
            }
        }

        private sealed class BendInfo
        {
            public int X { get; }
            public int Y { get; }
            public Direction Direction { get; }

            public BendInfo(int x, int y, Direction direction)
            {
                X           = x;
                Y           = y;
                Direction   = direction;
            }
        }

        private enum Direction
        {
            Left,
            UpLeft,
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft
        }
    }
}
