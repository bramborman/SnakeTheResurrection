using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SnakeTheResurrection
{
    public sealed class Snake
    {
        private readonly int size;
        private readonly int blockSize;
        private readonly bool borderlessMode;
        private readonly Profile profile;
        private readonly int gameBoardLeft;
        private readonly int gameBoardTop;
        private readonly int gameBoardRight;
        private readonly int gameBoardBottom;

        private bool rerenderSecondBody;
        private int length;
        private int desiredLength = 3;
        private SnakeBody head;
        private SnakeBody tail;
        private Direction direction = Direction.Up;

        public bool IsAlive { get; private set; } = true;

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

        public Snake(int originX, int originY, int size, int blockSize, bool borderlessMode, Profile profile, int gameBoardLeft, int gameBoardTop, int gameBoardRight, int gameBoardBottom)
        {
            this.size               = size;
            this.blockSize          = blockSize;
            this.borderlessMode     = borderlessMode;
            this.profile            = profile;
            this.gameBoardLeft      = gameBoardLeft;
            this.gameBoardTop       = gameBoardTop;
            this.gameBoardRight     = gameBoardRight;
            this.gameBoardBottom    = gameBoardBottom;
            
            head = new SnakeBody(originX, originY, this);
            head.AlignToGrid();
        }

        public void Update()
        {
            if (tail == null)
            {
                tail = head;
            }
            else
            {
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

                int x = head.x;
                int y = head.y;
                UpdateCoordinates(direction, ref x, ref y);

                if (GameObjectBase.IsInGameBoard(x, y, size, gameBoardLeft, gameBoardTop, gameBoardRight, gameBoardBottom))
                {
                    head.previousBody = new SnakeBody(x, y, this)
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
            
            Berry berry = Game.berries.FirstOrDefault(head.HitTest);

            if (berry != null)
            {
                desiredLength += berry.Eat();
                rerenderSecondBody = true;
            }


            void UpdateCoordinates(Direction direction, ref int x, ref int y)
            {
                if (direction == Direction.Up)
                {
                    y -= size;
                }
                else if (direction == Direction.Down)
                {
                    y += size;
                }

                if (direction == Direction.Left)
                {
                    x -= size;
                }
                else if (direction == Direction.Right)
                {
                    x += size;
                }

                if (borderlessMode)
                {
                    if (y < gameBoardTop)
                    {
                        y = gameBoardBottom - size;
                    }
                    else if (y > gameBoardBottom - size)
                    {
                        y = gameBoardTop;
                    }

                    if (x < gameBoardLeft)
                    {
                        x = gameBoardRight - size;
                    }
                    else if (x > gameBoardRight - size)
                    {
                        x = gameBoardLeft;
                    }
                }
            }
        }

        public void LateUpdate()
        {
            foreach (Snake otherSnake in Game.snakes)
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
            IsAlive = false;
        }
        
        private class SnakeBody : GameObjectBase
        {
            private readonly Snake snake;
            
            public SnakeBody previousBody;
            public SnakeBody nextBody;

            public SnakeBody(int x, int y, Snake snake) : base(snake.size, snake.blockSize, snake.gameBoardLeft, snake.gameBoardTop, snake.gameBoardRight, snake.gameBoardBottom)
            {
                base.x      = x;
                base.y      = y;
                this.snake  = snake;
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
