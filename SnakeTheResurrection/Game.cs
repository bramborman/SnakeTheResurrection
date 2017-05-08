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
            Berry berry = new Berry();

            while (snake.IsAlive)
            {
                snake.Update();
                berry.Update();
                Renderer.RenderFrame();

                if (DllImports.IsKeyDown(ConsoleKey.Escape))
                {
                    //TODO: Show pause menu
                    return;
                }

                Thread.Sleep(100);
            }
        }

        private abstract class GameObjectBase
        {
            private int _x;
            private int _y;

            public int X
            {
                get { return _x; }
                protected set
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
                protected set
                {
                    if (_y != value)
                    {
                        ExceptionHelper.ValidateNumberInWindowHorizontalRange(value, nameof(Y));
                        _y = value;
                    }
                }
            }
            public abstract int Size { get; }
            
            public bool HitTest(GameObjectBase other)
            {
                if (X >= other.X && X + Size <= other.X + other.Size && Y >= other.Y && Y + Size <= other.Y + other.Size)
                {
                    return true;
                }

                return false;
            }
        }

        private sealed class Snake
        {
            private SnakeBody head;
            private SnakeBody tail;
            private int desiredLength = 3;
            
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
                if (Length < desiredLength)
                {
                    if (head == null)
                    {
                        head            = GetNewCenteredBody();
                        tail            = head;
                    }
                    else
                    {
                        tail.NextBody   = GetNewCenteredBody();
                        tail            = tail.NextBody;
                    }

                    Length++;
                }

                head.Update(true, null);

                // if (head.HitTest(berry))
                // {
                // 
                // }
            }

            private SnakeBody GetNewCenteredBody()
            {
                return new SnakeBody((Console.WindowWidth - SnakeBody.SIZE) / 2, (Console.WindowHeight - SnakeBody.SIZE) / 2, Direction.Up, Profile.Color, Profile);
            }
        }

        private sealed class SnakeBody : GameObjectBase
        {
            public const int SIZE = 5;

            private readonly List<BendInfo> bendInfo = new List<BendInfo>();
            
            private Direction _direction;
            
            public override int Size
            {
                get { return SIZE; }
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
            public Profile Profile { get; }
            public SnakeBody NextBody { get; set; }

            public SnakeBody(int x, int y, Direction direction, ConsoleColor color, Profile profile)
            {
                ExceptionHelper.ValidateEnumValueDefined(color, nameof(color));
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                X           = x;
                Y           = y;
                Direction   = direction;
                Color       = color;
                Profile     = profile;
            }
            
            public void Update(bool isHead, BendInfo newBendInfo)
            {
                Renderer.RemoveFromBuffer(X, Y, SIZE, SIZE);
                
                if (isHead)
                {
                    Direction originalDirection = Direction;

                    bool up     = DllImports.IsKeyDown(Profile.SnakeControls.Up);
                    bool down   = DllImports.IsKeyDown(Profile.SnakeControls.Down);
                    bool left   = DllImports.IsKeyDown(Profile.SnakeControls.Left);
                    bool right  = DllImports.IsKeyDown(Profile.SnakeControls.Right);

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
                    Y -= SIZE;
                }
                else if (Direction == Direction.DownLeft || Direction == Direction.Down || Direction == Direction.DownRight)
                {
                    Y += SIZE;
                }

                if (Direction == Direction.UpLeft || Direction == Direction.Left || Direction == Direction.DownLeft)
                {
                    X -= SIZE;
                }
                else if (Direction == Direction.UpRight || Direction == Direction.Right || Direction == Direction.DownRight)
                {
                    X += SIZE;
                }

                Renderer.AddToBuffer(Color, X, Y, SIZE, SIZE);
                NextBody?.Update(false, newBendInfo);
            }
        }

        private sealed class Berry : GameObjectBase
        {
            private static readonly Random random = new Random();
            
            public override int Size
            {
                get { return 2; }
            }
            public ConsoleColor Color { get; }

            public Berry()
            {
                GenerateNewPosition();
                Color = ConsoleColor.Red;
            }

            public void Update()
            {
                Renderer.AddToBuffer(Color, X, Y, Size, Size);
            }

            public void GenerateNewPosition()
            {
                Renderer.RemoveFromBuffer(X, Y, Size, Size);

                while (true)
                {
                    X = random.Next(0, Console.WindowWidth);
                    Y = random.Next(0, Console.WindowHeight);

                    for (int row = Y; row < Y + Size; row++)
                    {
                        for (int column = X; column < X + Size; column++)
                        {
                            if (Renderer.Buffer[row, column] != Constants.BACKGROUND_COLOR)
                            {
                                continue;
                            }
                        }
                    }

                    break;
                }
            }
        }

        private sealed class BendInfo
        {
            public int X { get; }
            public int Y { get; }
            public Direction Direction { get; }

            public BendInfo(int x, int y, Direction direction)
            {
                X = x;
                Y = y;
                Direction = direction;
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
