﻿using SnakeTheResurrection.Data;
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
        private const int BLOCK_SIZE = 5;

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

                    if (InputHelper.WasKeyPressed(ConsoleKey.Escape))
                    {
                        //TODO: Show pause menu
                        return;
                    }
#if DEBUG
                    else if (InputHelper.WasKeyPressed(ConsoleKey.B))
                    {
                        while (Console.ReadKey(true).Key != ConsoleKey.B) ;
                    }
#endif

                    int sleep = InputHelper.WasKeyPressed(ConsoleKey.Spacebar) ? 5 : 100;
                    InputHelper.StartCaching();
                    Thread.Sleep(sleep);
                    InputHelper.StopCaching();
                }
            }
            finally
            {
                InputHelper.ClearCache();
            }
        }

        private static void CreateGameBoard()
        {
            int windowWidthOverlap      = Console.WindowWidth % BLOCK_SIZE;
            int windowHeightOverlap     = Console.WindowHeight % BLOCK_SIZE;

            bool showBorders = windowWidthOverlap >= 1 || windowHeightOverlap >= 1 || AppData.Current.ForceGameBoardBorders;

            if (showBorders)
            {
                windowWidthOverlap      += BLOCK_SIZE * 2;
                windowHeightOverlap     += BLOCK_SIZE * 2;
            }

            gameBoard.X                 = (int)Math.Round(windowWidthOverlap / 2.0);
            gameBoard.Y                 = (int)Math.Round(windowHeightOverlap / 2.0);

            int gameBoardBorderRight    = windowWidthOverlap - gameBoard.Left;
            int gameBoardBorderBottom   = windowHeightOverlap - gameBoard.Top;

            gameBoard.Width             = Console.WindowWidth - gameBoardBorderRight - gameBoard.Left;
            gameBoard.Height            = Console.WindowHeight - gameBoardBorderBottom - gameBoard.Top;

            if (showBorders)
            {
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, gameBoard.Left, Console.WindowHeight);
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, gameBoard.Right, 0, gameBoardBorderRight, Console.WindowHeight);

                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, Console.WindowWidth, gameBoard.Top);
                Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, gameBoard.Bottom, Console.WindowWidth, gameBoardBorderBottom);
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
                return X <= g.X + g.Size - 1 && X + Size - 1 >= g.X && Y <= g.Y + g.Size - 1 && Y + Size - 1 >= g.Y;
            }

            protected bool IsInGameBoard(int newX, int newY)
            {
                return newX >= gameBoard.Left && newY >= gameBoard.Top && newX + Size <= gameBoard.Right && newY + Size <= gameBoard.Bottom;
            }

            public void AlignPosition()
            {
                int alignment = (BLOCK_SIZE % Size) / 2;
                X = X - (X % BLOCK_SIZE) + (gameBoard.Left % BLOCK_SIZE) + alignment;
                Y = Y - (Y % BLOCK_SIZE) + (gameBoard.Top % BLOCK_SIZE) + alignment;
            }
        }

        private sealed class Snake
        {
            private SnakeBody head;
            private SnakeBody tail;
            private int desiredLength = 3;

            private bool _isAlive;
            
            public bool IsAlive
            {
                get { return _isAlive; }
                set
                {
                    if (!_isAlive)
                    {
                        if (value)
                        {
                            throw new InvalidOperationException("Cannot revive a dead snake.");
                        }
                        else
                        {
                            throw new InvalidOperationException("Cannot kill a dead snake.");
                        }
                    }

                    _isAlive = value;
                }
            }
            public int Length { get; private set; }
            public Profile Profile { get; }

            public Snake(Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                _isAlive = true;
                Profile = profile;
            }

            public void Update()
            {
                if (Length < desiredLength)
                {
                    if (head == null)
                    {
                        head = new SnakeBody(true, gameBoard.Left + (gameBoard.Width / 2) - BLOCK_SIZE, gameBoard.Top + (gameBoard.Height / 2) - BLOCK_SIZE, Direction.Up, this, Profile);
                        tail = head;

                        head.AlignPosition();
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

                        tail.NextBody   = new SnakeBody(false, newX, newY, tail.Direction, this, Profile);
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
            private readonly List<BendInfo> bendInfo;

            private bool isNew = true;

            private Direction _direction;

            public bool IsHead { get; }
            public override int Size
            {
                get { return BLOCK_SIZE; }
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
            public Snake Snake { get; }
            public Profile Profile { get; }
            public SnakeBody NextBody { get; set; }

            public SnakeBody(bool isHead, int x, int y, Direction direction, Snake snake, Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(snake, nameof(snake));
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));

                IsHead      = isHead;
                X           = x;
                Y           = y;
                Direction   = direction;
                Snake       = snake;
                Profile     = profile;

                if (!isHead)
                {
                    bendInfo = new List<BendInfo>();
                }
            }
            
            public void Update(BendInfo newBendInfo)
            {
                Renderer.RemoveFromBuffer(X, Y, Size, Size);
                bool removeFirstBendInfo = false;

                if (IsHead)
                {
                    Direction originalDirection = Direction;
                    
                    bool up     = InputHelper.WasKeyPressed(Profile.SnakeControls.Up);
                    bool down   = InputHelper.WasKeyPressed(Profile.SnakeControls.Down);
                    bool left   = InputHelper.WasKeyPressed(Profile.SnakeControls.Left);
                    bool right  = InputHelper.WasKeyPressed(Profile.SnakeControls.Right);
                    
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
                
                if (IsHead)
                {
                    if (!IsInGameBoard(x, y))
                    {
                        Snake.IsAlive = false;
                    }
                    else
                    {
                        SnakeBody nextBody = NextBody;

                        while (nextBody != null)
                        {
                            if (HitTest(nextBody))
                            {
                                Snake.IsAlive = false;
                                break;
                            }

                            nextBody = nextBody.NextBody;
                        }
                    }
                }

                if (Snake.IsAlive)
                {
                    X = x;
                    Y = y;
                }
                
                Renderer.AddToBuffer(Profile.Color, X, Y, Size, Size);

                if (!Snake.IsAlive)
                {
                    return;
                }

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
                    y -= BLOCK_SIZE;
                }
                else if (direction == Direction.DownLeft || direction == Direction.Down || direction == Direction.DownRight)
                {
                    y += BLOCK_SIZE;
                }

                if (direction == Direction.UpLeft || direction == Direction.Left || direction == Direction.DownLeft)
                {
                    x -= BLOCK_SIZE;
                }
                else if (direction == Direction.UpRight || direction == Direction.Right || direction == Direction.DownRight)
                {
                    x += BLOCK_SIZE;
                }
            }
        }

        private sealed class Berry : GameObjectBase
        {
            private const ConsoleColor x = ConsoleColor.Red;
            private const ConsoleColor _ = Constants.BACKGROUND_COLOR;

            private static readonly List<Berry> _current        = new List<Berry>();
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
            
            public override int Size
            {
                get { return textureSize; }
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
                GenerateNewPosition(false);
            }
            
            public int Eat()
            {
                GenerateNewPosition(true);
                return Power;
            }

            private void GenerateNewPosition(bool removePrevious)
            {
                // It will remove the Game board border on 0,0 without this condition
                if (removePrevious)
                {
                    int size = Size;
                    Renderer.AddToBuffer(ConsoleColor.White, X, Y, size, size);
                }

                bool regenerate;

                do
                {
                    regenerate = false;

                    X = random.Next(gameBoard.Left, gameBoard.Right - Size);
                    Y = random.Next(gameBoard.Top, gameBoard.Bottom - Size);

                    // Do not generate berry in a snake xD
                    for (int row = Y; row < Y + Size; row++)
                    {
                        for (int column = X; column < X + Size; column++)
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

                AlignPosition();
                Renderer.AddToBuffer(texture, X, Y);
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
