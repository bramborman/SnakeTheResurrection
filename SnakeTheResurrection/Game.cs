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
            Berry berry = new Berry(snake);

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

            public virtual int X
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
            public virtual int Y
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

            public abstract void Update();

            public bool HitTest(GameObjectBase other)
            {
                return X >= other.X && X + Size <= other.X + other.Size && Y >= other.Y && Y + Size <= other.Y + other.Size;
            }
        }

        private sealed class Snake : GameObjectBase
        {
            private SnakeBody head;
            private SnakeBody tail;

            private SnakeBody NewCenteredBody
            {
                get
                {
                    return new SnakeBody((Console.WindowWidth - SnakeBody.SIZE) / 2, (Console.WindowHeight - SnakeBody.SIZE) / 2, Direction.Up, Profile.Color, Profile);
                }
            }

            public override int X
            {
                get { return head.X; }
            }
            public override int Y
            {
                get { return head.Y; }
            }
            public override int Size
            {
                get { return SnakeBody.SIZE; }
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

            public override void Update()
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
            public SnakeBody NextBody { get; set; }
            public ConsoleColor Color { get; }
            public Profile Profile { get; }

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

            public override void Update()
            {
                throw new NotImplementedException();
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
            private const int SIZE = 2;

            private static readonly Random random = new Random();

            private readonly IEnumerable<Snake> snakes;
            
            public override int Size
            {
                get { return SIZE; }
            }
            public ConsoleColor Color { get; }

            public Berry(Snake snake) : this(new List<Snake> { snake })
            {

            }

            public Berry(IEnumerable<Snake> snakes)
            {
                ExceptionHelper.ValidateObjectNotNull(snakes, nameof(snakes));
                this.snakes = snakes;

                GenerateNewPosition();
                Color = ConsoleColor.Red;
            }

            public override void Update()
            {
                Renderer.AddToBuffer(Color, X, Y, SIZE, SIZE);

                if (snakes.Any(s => HitTest(s)))
                {
                    GenerateNewPosition();
                }
            }

            private void GenerateNewPosition()
            {
                X = random.Next(0, Console.WindowWidth);
                Y = random.Next(0, Console.WindowHeight);
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
