using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace SnakeTheResurrection
{
    public static class Game
    {
        private static Rectangle gameBoard = new Rectangle();

        public static void Singleplayer()
        {
            // Using try-finally to execute things even after 'return'
            try
            {
                CreateGameBoard();

                Snake snake = new Snake(ProfileManager.CurrentProfile);
                Berry berry = new Berry(10);
                
                while (snake.IsAlive)
                {
                    snake.Update();
                    Renderer.RenderFrame();

                    if (InputCacher.WasKeyPressed(ConsoleKey.Escape))
                    {
                        //TODO: Show pause menu
                        return;
                    }

                    int sleep = InputCacher.WasKeyPressed(ConsoleKey.Spacebar) ? 10 : 500;
                    InputCacher.StartCaching();
                    Thread.Sleep(sleep);
                    InputCacher.StopCaching();
                }
            }
            finally
            {
                InputCacher.ClearCache();
            }
        }

        private static void CreateGameBoard()
        {
            int windowWidthExcess       = Console.WindowWidth % SnakeBody.SIZE;
            int windowHeightExcess      = Console.WindowHeight % SnakeBody.SIZE;

            gameBoard.X                 = (int)Math.Round(windowWidthExcess / 2.0);
            gameBoard.Y                 = (int)Math.Round(windowHeightExcess / 2.0);

            int gameBoardBorderRight    = windowWidthExcess - gameBoard.Left;
            int gameBoardBorderBottom   = windowHeightExcess - gameBoard.Top;

            gameBoard.Width             = Console.WindowWidth - gameBoardBorderRight - gameBoard.Left;
            gameBoard.Height            = Console.WindowHeight - gameBoardBorderBottom - gameBoard.Top;

            if (windowHeightExcess >= 1)
            {
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, Console.WindowWidth, gameBoard.Top);
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, gameBoard.Bottom, Console.WindowWidth, gameBoardBorderBottom);
            }

            if (windowWidthExcess >= 1)
            {
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, gameBoard.Left, Console.WindowHeight);
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, gameBoard.Right, 0, gameBoardBorderRight, Console.WindowHeight);
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
                        ExceptionHelper.ValidateNumberInRange(value, gameBoard.Left, gameBoard.Right - Size, nameof(X));
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
                        ExceptionHelper.ValidateNumberInRange(value, gameBoard.Top, gameBoard.Bottom - Size, nameof(Y));
                        _y = value;
                    }
                }
            }
            public abstract int Size { get; }
            
            public bool HitTest(GameObjectBase g)
            {
                return X <= g.X + g.Size && X + Size >= g.X && Y <= g.Y + g.Size && Y + Size >= g.Y;
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
                        int gameBoardWidthHalf  = gameBoard.Width / 2;
                        int gameBoardHeightHalf = gameBoard.Height / 2;

                        gameBoardWidthHalf      -= gameBoardWidthHalf % SnakeBody.SIZE;
                        gameBoardHeightHalf     -= gameBoardHeightHalf % SnakeBody.SIZE;

                        head = new SnakeBody(true, gameBoard.Left + gameBoardWidthHalf - SnakeBody.SIZE, gameBoard.Top + gameBoardHeightHalf - SnakeBody.SIZE, Direction.Up, Profile);
                        tail = head;
                    }
                    else
                    {
                        int newX = tail.X;
                        int newY = tail.Y;

                        Direction inverseDirection = tail.Direction;

                        switch (tail.Direction)
                        {
                            case Direction.Left:
                                inverseDirection = Direction.Right;
                                break;
                            
                            case Direction.UpLeft:
                                inverseDirection = Direction.DownRight;
                                break;
                            
                            case Direction.Up:
                                inverseDirection = Direction.Down;
                                break;
                            
                            case Direction.UpRight:
                                inverseDirection = Direction.DownLeft;
                                break;
                            
                            case Direction.Right:
                                inverseDirection = Direction.Left;
                                break;
                            
                            case Direction.DownRight:
                                inverseDirection = Direction.UpLeft;
                                break;
                            
                            case Direction.Down:
                                inverseDirection = Direction.Up;
                                break;
                            
                            case Direction.DownLeft:
                                inverseDirection = Direction.UpRight;
                                break;
                        }

                        SnakeBody.UpdateCoordinates(inverseDirection, ref newX, ref newY);

                        tail.NextBody   = new SnakeBody(false, newX, newY, tail.Direction, Profile);
                        tail            = tail.NextBody;
                    }

                    Length++;
                }

                head.Update(null);

                Berry berry = Berry.Current.FirstOrDefault(b => head.HitTest(b));

                if (berry != null)
                {
                    desiredLength += berry.Eat();
                }
            }
        }

        private sealed class SnakeBody : GameObjectBase
        {
            public const int SIZE = 5;

            private readonly List<BendInfo> bendInfo;

            private bool isNew = true;

            private Direction _direction;

            public bool IsHead { get; }
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
            public Profile Profile { get; }
            public SnakeBody NextBody { get; set; }

            public SnakeBody(bool isHead, int x, int y, Direction direction, Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                IsHead      = isHead;
                X           = x;
                Y           = y;
                Direction   = direction;
                Profile     = profile;

                if (!isHead)
                {
                    bendInfo = new List<BendInfo>();
                }
            }
            
            public void Update(BendInfo newBendInfo)
            {
                Renderer.RemoveFromBuffer(X, Y, SIZE, SIZE);
                bool removeFirstBendInfo = false;

                if (IsHead)
                {
                    Direction originalDirection = Direction;
                    
                    bool up     = InputCacher.WasKeyPressed(Profile.SnakeControls.Up);
                    bool down   = InputCacher.WasKeyPressed(Profile.SnakeControls.Down);
                    bool left   = InputCacher.WasKeyPressed(Profile.SnakeControls.Left);
                    bool right  = InputCacher.WasKeyPressed(Profile.SnakeControls.Right);
                    
                    if (up)
                    {
                        bool assigned = false;

                        if (AppData.Current.EnableDiagonalMovement)
                        {
                            if (left)
                            {
                                if (Direction != Direction.DownRight)
                                {
                                    assigned = true;
                                    Direction = Direction.UpLeft;
                                }
                            }
                            else if (right)
                            {
                                if (Direction != Direction.DownLeft)
                                {
                                    assigned = true;
                                    Direction = Direction.UpRight;
                                }
                            }
                        }

                        if (!assigned && Direction != Direction.Down)
                        {
                            Direction = Direction.Up;
                        }
                    }
                    else if (down)
                    {
                        bool assigned = false;

                        if (AppData.Current.EnableDiagonalMovement)
                        {
                            if (left)
                            {
                                if (Direction != Direction.UpRight)
                                {
                                    assigned = true;
                                    Direction = Direction.DownLeft;
                                }
                            }
                            else if (right)
                            {
                                if (Direction != Direction.UpLeft)
                                {
                                    assigned = true;
                                    Direction = Direction.DownRight;
                                }
                            }
                        }

                        if (!assigned && Direction != Direction.Up)
                        {
                            Direction = Direction.Down;
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
                    
                    if (bendInfo.Count >= 1 && bendInfo[0].X == X && bendInfo[0].Y == Y)
                    {
                        Direction = bendInfo[0].Direction;

                        // Need to remove it after passing it using AddRange to the new SnakeBody
                        removeFirstBendInfo = true;
                    }
                }

                // Cannot pass property as ref or out parameter
                int x = X;
                int y = Y;
                UpdateCoordinates(Direction, ref x, ref y);
                X = x;
                Y = y;

                Renderer.AddToBuffer(Profile.Color, X, Y, SIZE, SIZE);

                if (NextBody != null)
                {
                    if (NextBody.isNew)
                    {
                        NextBody.isNew = false;

                        if (!IsHead)
                        {
                            NextBody.bendInfo.AddRange(bendInfo);

                            // It's already in the bendInfo list
                            newBendInfo = null;
                        }
                    }

                    NextBody.Update(newBendInfo);
                }

                if (removeFirstBendInfo)
                {
                    bendInfo.RemoveAt(0);
                }
            }

            public static void UpdateCoordinates(Direction direction, ref int x, ref int y)
            {
                if (direction == Direction.UpLeft || direction == Direction.Up || direction == Direction.UpRight)
                {
                    y -= SIZE;
                }
                else if (direction == Direction.DownLeft || direction == Direction.Down || direction == Direction.DownRight)
                {
                    y += SIZE;
                }

                if (direction == Direction.UpLeft || direction == Direction.Left || direction == Direction.DownLeft)
                {
                    x -= SIZE;
                }
                else if (direction == Direction.UpRight || direction == Direction.Right || direction == Direction.DownRight)
                {
                    x += SIZE;
                }
            }
        }

        private sealed class Berry : GameObjectBase
        {
            private static readonly List<Berry> _current    = new List<Berry>();
            private static readonly Random random           = new Random();

            private static bool isOnScreen = false;

            public static IEnumerable<Berry> Current
            {
                get
                {
                    return _current.AsEnumerable();
                }
            }
            
            public override int Size
            {
                get { return 2; }
            }
            public ConsoleColor Color { get; }
            public int Power { get; }

            public Berry() : this(1)
            {

            }

            public Berry(int power)
            {
                ExceptionHelper.ValidateNumberGreaterOrEqual(power, 0, nameof(power));

                Color = ConsoleColor.Red;
                Power = power;

                _current.Add(this);
                GenerateNewPosition();
            }
            
            public int Eat()
            {
                GenerateNewPosition();
                return Power;
            }

            private void GenerateNewPosition()
            {
                // It will remove the Game board border on 0,0 without this condition
                if (isOnScreen)
                {
                    Renderer.RemoveFromBuffer(X, Y, Size, Size);
                }

                while (true)
                {
                    X = random.Next(gameBoard.Left, gameBoard.Right - Size);
                    Y = random.Next(gameBoard.Top, gameBoard.Bottom - Size);

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

                Renderer.AddToBuffer(Color, X, Y, Size, Size);
                isOnScreen = true;
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
