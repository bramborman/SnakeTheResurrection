using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System.Collections.Generic;
using System.Linq;
using static SnakeTheResurrection.Game;

namespace SnakeTheResurrection
{
    public sealed class Snake
    {
        private const int SIZE = BLOCK_SIZE;

        public static readonly HashSet<Snake> current = new HashSet<Snake>();
        
        private readonly int snakeIndex;
        private readonly int totalSnakeCount;
        
        private bool rerenderSecondBody;
        private int length;
        private int desiredLength = 3;
        private SnakeBody head;
        private SnakeBody tail;
        private readonly Profile profile;

        private IEnumerable<SnakeBody> Bodies
        {
            get
            {
                SnakeBody body = head;

                while (body != null)
                {
                    yield return body;
                    body = body.nextBody;
                }
            }
        }

        public Snake(Profile profile, int snakeIndex, int totalSnakeCount)
        {
            this.profile = profile;
            this.snakeIndex = snakeIndex;
            this.totalSnakeCount = totalSnakeCount;
            
            current.Add(this);
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
                int x = head.x;
                int y = head.y;
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
                    Die();

                    foreach (SnakeBody body in Bodies)
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

            if (length == desiredLength)
            {
                tail.RemoveFromBuffer();
                tail = tail.previousBody;
                tail.nextBody = null;
            }
            else
            {
                length++;
            }
            
            Berry berry = Berry.current.FirstOrDefault(b => head.HitTest(b));

            if (berry != null)
            {
                desiredLength += berry.Eat();
                rerenderSecondBody = true;
            }
        }

        public void LateUpdate()
        {
            foreach (Snake otherSnake in current)
            {
                foreach (SnakeBody body in otherSnake.Bodies)
                {
                    if (!ReferenceEquals(head, body) && head.HitTest(body))
                    {
                        Die();
                        return;
                    }
                }
            }
        }

        private void Die()
        {
            current.Remove(this);
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
            current.Clear();
        }
        
        private int GetX(int snakeIndex, int totalSnakeCount)
        {
            return gameBoardLeft + ((gameBoardWidth / (totalSnakeCount + 1)) * (snakeIndex + 1)) - BLOCK_SIZE;
        }
        
        private class SnakeBody : GameObjectBase
        {
            private readonly Snake snake;

            public Direction direction;
            public SnakeBody previousBody;
            public SnakeBody nextBody;

            public SnakeBody(int x, int y, Direction direction, Snake snake) : base(SIZE)
            {
                base.x          = x;
                base.y          = y;
                this.direction  = direction;
                this.snake      = snake;
            }

            public void AddToBuffer()
            {
                Renderer.AddToBuffer(snake.profile.Color, x, y, size, size);
            }

            public void RemoveFromBuffer()
            {
                Renderer.RemoveFromBuffer(x, y, size, size);
            }
        }
    }
}
