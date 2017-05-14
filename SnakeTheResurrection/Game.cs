﻿using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SnakeTheResurrection
{
    public static class Game
    {
        private const int BLOCK_SIZE = 5;

        private static int gameBoardLeft;
        private static int gameBoardTop;
        private static int gameBoardRight;
        private static int gameBoardBottom;
        private static int gameBoardWidth;
        private static int gameBoardHeight;

        private static bool BorderlessMode { get; set; }

        public static bool Play(bool multiplayer)
        {
            Renderer.ClearBuffer();
            int delay = 0;
            Snake[] snakes = null;

            for (int i = 0;; i++)
            {
                // Not using switch to be able to use continue and break
                if (i == 0)
                {
                    bool? getGameModeOutput = GetGameMode();

                    if (getGameModeOutput == null)
                    {
                        return false;
                    }
                    else
                    {
                        BorderlessMode = getGameModeOutput.Value;
                    }
                }
                else if (i == 1)
                {
                    int? getDelayOutput = GetDelay();

                    if (getDelayOutput == null)
                    {
                        i--;
                        continue;
                    }
                    else
                    {
                        delay = getDelayOutput.Value;
                    }
                }
                else if (i == 2)
                {
                    int playerCount = 1;

                    if (multiplayer)
                    {
                        int? getPlayerCountOutput = GetPlayerCount();

                        if (getPlayerCountOutput == null)
                        {
                            i--;
                            continue;
                        }

                        playerCount = getPlayerCountOutput.Value;
                    }

                    snakes = new Snake[playerCount];
                }
                else
                {
                    break;
                }
            }

            // Using try-finally to execute things even after 'return'
            try
            {
                Renderer.ClearBuffer();
                CreateGameBoard();
                
                for (int i = 0; i < snakes.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            snakes[i] = new Snake(ProfileManager.CurrentProfile, i, snakes.Length);
                            break;

                        case 1:
                            snakes[i] = new Snake(new Profile
                            {
                                Name = "Frogpanda",
                                Color = ConsoleColor.Yellow
                            }, i, snakes.Length);
                            snakes[i].Profile.SnakeControls.Left    = ConsoleKey.A;
                            snakes[i].Profile.SnakeControls.Up      = ConsoleKey.W;
                            snakes[i].Profile.SnakeControls.Right   = ConsoleKey.D;
                            snakes[i].Profile.SnakeControls.Down    = ConsoleKey.S;

                            break;
                    }

                    new Berry();
                }

                while (snakes.Any(s => s.IsAlive))
                {
                    foreach (Snake snake in snakes)
                    {
                        if (snake.IsAlive)
                        {
                            snake.Update();
                        }
                    }

                    Renderer.RenderFrame();

                    if (InputHelper.WasKeyPressed(ConsoleKey.Escape))
                    {
                        switch (PauseMenu())
                        {
                            case MenuResult.Restart:
                                return true;
                            
                            case MenuResult.MainMenu:
                                return false;
                            
                            case MenuResult.QuitGame:
                                Program.Exit();
                                break;
                        }
                    }
#if DEBUG
                    else if (InputHelper.WasKeyPressed(ConsoleKey.B))
                    {
                        while (Console.ReadKey(true).Key != ConsoleKey.B) ;
                    }
#endif

                    InputHelper.StartCaching();
                    Thread.Sleep(delay);
                    InputHelper.StopCaching();
                }

                return false;
            }
            finally
            {
                InputHelper.ClearCache();
                Berry.Reset();
                Snake.Reset();
            }
        }

        private static void CreateGameBoard()
        {
            int windowWidthOverlap  = Console.WindowWidth % BLOCK_SIZE;
            int windowHeightOverlap = Console.WindowHeight % BLOCK_SIZE;
            
            if (windowWidthOverlap >= 1 || windowHeightOverlap >= 1 || AppData.Current.ForceGameBoardBorders)
            {
                windowWidthOverlap  += BLOCK_SIZE * 2;
                windowHeightOverlap += BLOCK_SIZE * 2;
            }

            gameBoardLeft                   = (int)Math.Round(windowWidthOverlap / 2.0);
            gameBoardTop                    = (int)Math.Round(windowHeightOverlap / 2.0);

            int gameBoardBorderRightSize    = windowWidthOverlap - gameBoardLeft;
            int gameBoardBorderBottomSize   = windowHeightOverlap - gameBoardTop;

            gameBoardRight                  = Console.WindowWidth - gameBoardBorderRightSize;
            gameBoardBottom                 = Console.WindowHeight - gameBoardBorderBottomSize;

            gameBoardTop                    += gameBoardTop >= 1 ? BLOCK_SIZE : (BLOCK_SIZE * 2);

            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, gameBoardLeft, Console.WindowHeight);
            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, gameBoardRight, 0, gameBoardBorderRightSize, Console.WindowHeight);

            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, 0, Console.WindowWidth, gameBoardTop);
            Renderer.AddToBuffer(Constants.ACCENT_COLOR_DARK, 0, gameBoardBottom, Console.WindowWidth, gameBoardBorderBottomSize);

            gameBoardWidth  = gameBoardRight - gameBoardLeft;
            gameBoardHeight = gameBoardBottom - gameBoardTop;
        }

        private static bool? GetGameMode()
        {
            bool? output = null;

            Symtext.WriteTitle("Mode", 7);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Classic",     () => output = false    ),
                    new MenuItem("Borderless",  () => output = true     ),
                    new MenuItem(null,          null                    ),
                    new MenuItem("Back",        () => output = null     )
                }
            }.InvokeResult();

            return output;
        }

        private static int? GetDelay()
        {
            int? output = null;

            Symtext.WriteTitle("Level", 0);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Easy",    () => output = 200  ),
                    new MenuItem("Medium",  () => output = 50   ),
                    new MenuItem("Hard",    () => output = 30   ),
                    new MenuItem(null,      null                ),
                    new MenuItem("Back",    () => output = null )
                },
                SelectedIndex = 1
            }.InvokeResult();

            return output;
        }

        private static int? GetPlayerCount()
        {
            return 2;
        }

        private static MenuResult PauseMenu()
        {
            object gameBufferKey    = Renderer.BackupBuffer();
            MenuResult output       = default(MenuResult);
            
            Symtext.WriteTitle("Pause", 7);
            new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Continue",    () => output = MenuResult.Continue  ),
                    new MenuItem("Restart",     () => output = MenuResult.Restart   ),
                    new MenuItem("Main menu",   () => output = MenuResult.MainMenu  ),
                    new MenuItem("Quit game",   () => output = MenuResult.QuitGame  )
                }
            }.InvokeResult();

            Renderer.RestoreBuffer(gameBufferKey);
            return output;
        }

        private enum MenuResult
        {
            Continue,
            Restart,
            MainMenu,
            QuitGame
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
                        ExceptionHelper.ValidateNumberInRange(value, gameBoardLeft, gameBoardRight - Size, nameof(X));
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
                        ExceptionHelper.ValidateNumberInRange(value, gameBoardTop, gameBoardBottom - Size, nameof(Y));
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
                return newX >= gameBoardLeft && newY >= gameBoardTop && newX + Size <= gameBoardRight && newY + Size <= gameBoardBottom;
            }

            public void AlignPosition()
            {
                int alignment = (BLOCK_SIZE % Size) / 2;
                X = X - (X % BLOCK_SIZE) + (gameBoardLeft % BLOCK_SIZE) + alignment;
                Y = Y - (Y % BLOCK_SIZE) + (gameBoardTop % BLOCK_SIZE) + alignment;
            }
        }

        private sealed class Snake : SnakeBody, IEnumerable<SnakeBody>
        {
            private static readonly List<Snake> _current = new List<Snake>();

            public static IEnumerable<Snake> Current
            {
                get
                {
                    return _current.AsEnumerable();
                }
            }

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

            public Snake(Profile profile, int index, int totalSnakeCount) : base(GetX(index, totalSnakeCount), gameBoardTop + (gameBoardHeight / 2) - BLOCK_SIZE, Direction.Up, profile)
            {
                _isAlive = true;
                _current.Add(this);
            }

            public void Update()
            {
                if (Length < desiredLength)
                {
                    if (tail == null)
                    {
                        AlignPosition();
                        tail = this;
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

                        UpdateCoordinates(inverseDirection, ref newX, ref newY);

                        tail.NextBody   = new SnakeBody(newX, newY, tail.Direction, Profile);
                        tail            = tail.NextBody;
                    }

                    Length++;
                }

                Update(null);

                Berry berry = Berry.Current.FirstOrDefault(b => HitTest(b));

                if (berry != null)
                {
                    desiredLength += berry.Eat();
                }
            }

            public IEnumerator<SnakeBody> GetEnumerator()
            {
                SnakeBody body = this;

                while (body != null)
                {
                    yield return body;
                    body = body.NextBody;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public static void Reset()
            {
                _current.Clear();
            }

            private static int GetX(int index, int totalSnakeCount)
            {
                return gameBoardLeft + ((gameBoardWidth / (totalSnakeCount + 1)) * (index + 1)) - BLOCK_SIZE;
            }
        }

        private class SnakeBody : GameObjectBase
        {
            private const int SIZE = BLOCK_SIZE;

            private readonly List<BendInfo> bendInfo;

            private bool isNew = true;

            private Direction _direction;

            private bool IsHead
            {
                get { return this is Snake; }
            }

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

            public SnakeBody(int x, int y, Direction direction, Profile profile)
            {
                ExceptionHelper.ValidateObjectNotNull(profile, nameof(profile));
                
                X           = x;
                Y           = y;
                Direction   = direction;
                Profile     = profile;

                if (!IsHead)
                {
                    bendInfo = new List<BendInfo>();
                }
            }
            
            protected void Update(BendInfo newBendInfo)
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

                Snake thisSnake = this as Snake;
                
                if (IsHead)
                {
                    if (!IsInGameBoard(x, y))
                    {
                        thisSnake.IsAlive = false;
                    }
                    else
                    {
                        foreach (Snake snake in Snake.Current)
                        {
                            foreach (SnakeBody body in snake)
                            {
                                if (!ReferenceEquals(this, body) && HitTest(body))
                                {
                                    thisSnake.IsAlive = false;
                                    break;
                                }
                            }

                            if (!thisSnake.IsAlive)
                            {
                                break;
                            }
                        }
                    }
                }

                bool isAlive = thisSnake?.IsAlive != false;

                if (isAlive)
                {
                    X = x;
                    Y = y;
                }
                
                Renderer.AddToBuffer(Profile.Color, X, Y, Size, Size);

                if (!isAlive)
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

            protected static void UpdateCoordinates(Direction direction, ref int x, ref int y)
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

                if (BorderlessMode)
                {
                    if (y < gameBoardTop)
                    {
                        y = gameBoardBottom - SIZE;
                    }
                    else if (y > gameBoardBottom - SIZE)
                    {
                        y = gameBoardTop;
                    }

                    if (x < gameBoardLeft)
                    {
                        x = gameBoardRight - SIZE;
                    }
                    else if (x > gameBoardRight - SIZE)
                    {
                        x = gameBoardLeft;
                    }
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

                    X = random.Next(gameBoardLeft, gameBoardRight - Size);
                    Y = random.Next(gameBoardTop, gameBoardBottom - Size);

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

            public static void Reset()
            {
                _current.Clear();
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
