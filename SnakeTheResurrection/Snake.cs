using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public sealed class Snake
    {
        private const int SIZE = BLOCK_SIZE;

        private static readonly HashSet<Snake> _current = new HashSet<Snake>();

        public static IEnumerable<Snake> Current
        {
            get
            {
                foreach (Snake snake in _current.ToList())
                {
                    if (snake.IsAlive)
                    {
                        yield return snake;
                    }
                    else
                    {
                        _current.Remove(snake);
                    }
                }
            }
        }

        private readonly int snakeIndex;
        private readonly int totalSnakeCount;
        
        private bool rerenderSecondBody;
        private int desiredLength = 3;
        private SnakeBody head;
        private SnakeBody tail;
        private readonly Profile profile;

        public int Length { get; private set; }
        public bool IsAlive { get; private set; }

        public Snake(Profile profile, int snakeIndex, int totalSnakeCount)
        {
            this.profile = profile;
            this.snakeIndex = snakeIndex;
            this.totalSnakeCount = totalSnakeCount;

            IsAlive = true;
            _current.Add(this);
        }

        public void Update()
        {
            if (head == null)
            {
                head = new SnakeBody(GetX(snakeIndex, totalSnakeCount), gameBoardTop + (gameBoardHeight / 2) - SIZE, Direction.Up, this);
                head.AlignToGrid();
                tail = head;
            }
            else
            {
                int x = head.X;
                int y = head.Y;
                Direction direction = head.direction;
                    
                bool up     = InputHelper.WasKeyPressed(profile.SnakeControls.Up);
                bool down   = InputHelper.WasKeyPressed(profile.SnakeControls.Down);
                bool left   = InputHelper.WasKeyPressed(profile.SnakeControls.Left);
                bool right  = InputHelper.WasKeyPressed(profile.SnakeControls.Right);
                    
                if (up && direction != Direction.Up && direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
                else if (down && direction != Direction.Down && direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
                else if (left && direction != Direction.Left && direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
                else if (right && direction != Direction.Right && direction != Direction.Left)
                {
                    direction = Direction.Right;
                }

                UpdateCoordinates(direction, ref x, ref y);

                if (GameObjectBase.IsInGameBoard(x, y, SIZE))
                {
                    head.previousBody = new SnakeBody(x, y, direction, this)
                    {
                        nextBody = head
                    };
                    head = head.previousBody;
                }
                else
                {
                    IsAlive = false;

                    foreach (SnakeBody body in EnumerateBodies())
                    {
                        body.RemoveFromBuffer();
                    }
                    
                    return;
                }
            }

            if (rerenderSecondBody)
            {
                head.nextBody.AddToBuffer();
                rerenderSecondBody = false;
            }

            head.AddToBuffer();

            if (Length == desiredLength)
            {
                tail.RemoveFromBuffer();
                tail = tail.previousBody;
                tail.nextBody = null;
            }
            else
            {
                Length++;
            }
            
            Berry berry = Berry.Current.FirstOrDefault(b => head.HitTest(b));

            if (berry != null)
            {
                desiredLength += berry.Eat();
                rerenderSecondBody = true;
            }
        }

        public void LateUpdate()
        {
            foreach (Snake otherSnake in Current)
            {
                foreach (SnakeBody body in otherSnake.EnumerateBodies())
                {
                    if (!ReferenceEquals(head, body) && head.HitTest(body))
                    {
                        IsAlive = false;
                        return;
                    }
                }
            }
        }

        private void UpdateCoordinates(Direction direction, ref int x, ref int y)
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

            if (borderlessMode)
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
        
        public static void Reset()
        {
            _current.Clear();
        }
        
        private int GetX(int snakeIndex, int totalSnakeCount)
        {
            return gameBoardLeft + ((gameBoardWidth / (totalSnakeCount + 1)) * (snakeIndex + 1)) - BLOCK_SIZE;
        }

        private IEnumerable<SnakeBody> EnumerateBodies()
        {
            SnakeBody body = head;

            while (body != null)
            {
                yield return body;
                body = body.nextBody;
            }
        }

        private class SnakeBody : GameObjectBase
        {
            private readonly Snake snake;

            public Direction direction;
            public SnakeBody previousBody;
            public SnakeBody nextBody;

            public SnakeBody(int x, int y, Direction direction, Snake snake) : base(SIZE)
            {
                X               = x;
                Y               = y;
                this.direction  = direction;
                this.snake      = snake;
            }

            public void AddToBuffer()
            {
                Renderer.AddToBufferAndRender(snake.profile.Color, X, Y, size, size);
            }

            public void RemoveFromBuffer()
            {
                Renderer.RemoveFromBufferAndRender(X, Y, size, size);
            }
        }
    }
}
