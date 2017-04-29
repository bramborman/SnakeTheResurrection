using System;

namespace SnakeTheResurrection.Utilities
{
    // Font inspired by Symtext (4/26/2017): http://www.dafont.com/symtext.font
    public static class Symtext
    {
        private const bool X            = true;
        private const bool _            = false;
        private const int CHAR_HEIGHT   = 7;

        // All chars should be 7 (CHAR_HEIGHT) rows tall
        #region Alphabet
        private static readonly bool[,] a = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] b = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, _ },
            { X, _, X, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] c = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] d = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] e = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, _ },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] f = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { _, _, _, _ }
        };
        private static readonly bool[,] g = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] h = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] i = new bool[,]
        {
            { _ },
            { X },
            { X },
            { X },
            { X },
            { X },
            { _ }
        };
        private static readonly bool[,] j = new bool[,]
        {
            { _, _, _, _ },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] k = new bool[,]
        {
            { _, _, _, _ },
            { X, _, X, _ },
            { X, _, X, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] l = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] m = new bool[,]
        {
            { _, _, _, _, _ },
            { X, X, X, X, X },
            { X, _, X, _, X },
            { X, _, X, _, X },
            { X, _, _, _, X },
            { X, _, _, _, X },
            { _, _, _, _, _ }
        };
        private static readonly bool[,] n = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] o = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] p = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, _ },
            { _, _, _, _ }
        };
        private static readonly bool[,] q = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, X, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] r = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, _ },
            { X, _, X, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] s = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] t = new bool[,]
        {
            { _, _, _, _, _ },
            { X, X, X, X, X },
            { _, _, X, _, _ },
            { _, _, X, _, _ },
            { _, _, X, _, _ },
            { _, _, X, _, _ },
            { _, _, _, _, _ }
        };
        private static readonly bool[,] u = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] v = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, X, _ },
            { _, X, _, _ },
            { _, _, _, _ }
        };
        private static readonly bool[,] w = new bool[,]
        {
            { _, _, _, _, _ },
            { X, _, _, _, X },
            { X, _, _, _, X },
            { X, _, X, _, X },
            { X, _, X, _, X },
            { X, X, X, X, X },
            { _, _, _, _, _ }
        };
        private static readonly bool[,] x = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { _, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] y = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] z = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { _, X, X, _ },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
        };
        #endregion
        #region Numbers
        private static readonly bool[,] _0 = new bool[,]
        {
            { _, _, _, _, _ },
            { X, X, X, X, X },
            { X, _, _, _, X },
            { X, _, X, _, X },
            { X, _, _, _, X },
            { X, X, X, X, X },
            { _, _, _, _, _ }
        };
        private static readonly bool[,] _1 = new bool[,]
        {
            { _, _ },
            { X, X },
            { _, X },
            { _, X },
            { _, X },
            { _, X },
            { _, _ }
        };
        private static readonly bool[,] _2 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _3 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _4 = new bool[,]
        {
            { _, _, _, _ },
            { X, _, _, _ },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _5 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _6 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _7 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _8 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, _ }
        };
        private static readonly bool[,] _9 = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, _ }
        };
        #endregion
        #region Special characters
        private static readonly bool[,] space = new bool[,]
        {
            { _, _, _ },
            { _, _, _ },
            { _, _, _ },
            { _, _, _ },
            { _, _, _ },
            { _, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] plus = new bool[,]
        {
            { _, _, _ },
            { _, _, _ },
            { _, X, _ },
            { X, X, X },
            { _, X, _ },
            { _, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] minus = new bool[,]
        {
            { _, _, _ },
            { _, _, _ },
            { _, _, _ },
            { X, X, X },
            { _, _, _ },
            { _, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] cross = new bool[,]
        {
            { _, _, _ },
            { _, _, _ },
            { X, _, X },
            { _, X, _ },
            { X, _, X },
            { _, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] slash = new bool[,]
        {
            { _, _, _ },
            { _, _, X },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { X, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] equals = new bool[,]
        {
            { _, _, _ },
            { _, _, _ },
            { X, X, X },
            { _, _, _ },
            { X, X, X },
            { _, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] percents = new bool[,]
        {
            { _, _, _ },
            { X, _, X },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { X, _, X },
            { _, _, _ }
        };
        private static readonly bool[,] hash = new bool[,]
        {
            { _, _, _, _, _ },
            { _, X, _, X, _ },
            { X, X, X, X, X },
            { _, X, _, X, _ },
            { X, X, X, X, X },
            { _, X, _, X, _ },
            { _, _, _, _, _ }
        };
        private static readonly bool[,] comma = new bool[,]
        {
            { _ },
            { _ },
            { _ },
            { _ },
            { _ },
            { X },
            { X }
        };
        private static readonly bool[,] dot = new bool[,]
        {
            { _ },
            { _ },
            { _ },
            { _ },
            { _ },
            { X },
            { _ }
        };
        private static readonly bool[,] colon = new bool[,]
        {
            { _ },
            { _ },
            { _ },
            { X },
            { _ },
            { X },
            { _ }
        };
        private static readonly bool[,] questionMark = new bool[,]
        {
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { _, X, X, X },
            { _, _, _, _ },
            { _, X, _, _ },
            { _, _, _, _ }
        };
        private static readonly bool[,] exclamationMark = new bool[,]
        {
            { _ },
            { X },
            { X },
            { X },
            { _ },
            { X },
            { _ }
        };
        private static readonly bool[,] arrowLeft = new bool[,]
        {
            { _, _, _ },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { _, X, _ },
            { _, _, X },
            { _, _, _ }
        };
        private static readonly bool[,] arrowRight = new bool[,]
        {
            { _, _, _ },
            { X, _, _ },
            { _, X, _ },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { _, _, _ }
        };
        private static readonly bool[,] squareBracketLeft = new bool[,]
        {
            { _, _ },
            { X, X },
            { X, _ },
            { X, _ },
            { X, _ },
            { X, X },
            { _, _ }
        };
        private static readonly bool[,] squareBracketRight = new bool[,]
        {
            { _, _ },
            { X, X },
            { _, X },
            { _, X },
            { _, X },
            { X, X },
            { _, _ }
        };
        private static readonly bool[,] copyrightMark = new bool[,]
        {
            { _, X, X, X, X, _ },
            { X, _, _, _, _, X },
            { X, _, X, X, _, X },
            { X, _, X, _, _, X },
            { X, _, X, X, _, X },
            { X, _, _, _, _, X },
            { _, X, X, X, X, _ }
        };
        #endregion

        private static ConsoleColor[,] characterSpacingBackgroundFiller;

        private static int _cursorX;
        private static int _cursorY;
        private static int _characterSpacing;
        private static float _fontSize;
        private static ConsoleColor _foregroundColor;
        private static ConsoleColor _backgroundColor;

        public static int CursorX
        {
            get { return _cursorX; }
            set
            {
                if (_cursorX != value)
                {
                    if (_cursorX < 0 || _cursorX > Console.WindowWidth)
                    {
                        throw new ArgumentOutOfRangeException(nameof(CursorX));
                    }

                    _cursorX = value;
                }
            }
        }
        public static int CursorY
        {
            get { return _cursorY; }
            set
            {
                if (_cursorY != value)
                {
                    if (_cursorY < 0 || _cursorY > Console.WindowHeight)
                    {
                        throw new ArgumentOutOfRangeException(nameof(CursorY));
                    }

                    _cursorY = value;
                }
            }
        }
        public static int CharacterSpacing
        {
            get { return _characterSpacing; }
            set
            {
                if (_characterSpacing != value)
                {
                    if (_characterSpacing < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(CharacterSpacing));
                    }

                    _characterSpacing = value;

                    characterSpacingBackgroundFiller = new ConsoleColor[CHAR_HEIGHT, value];
                    FillCharacterSpacingBackgroundFiller();
                }
            }
        }
        public static float FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    if (_fontSize < 0f)
                    {
                        throw new ArgumentOutOfRangeException(nameof(FontSize));
                    }

                    _fontSize = value;
                }
            }
        }
        public static ConsoleColor ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                if (_foregroundColor != value)
                {
                    ExceptionHelper.ValidateEnumValueDefined(value, nameof(ForegroundColor));
                    _foregroundColor = value;
                }
            }
        }
        public static ConsoleColor BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                if (_backgroundColor != value)
                {
                    ExceptionHelper.ValidateEnumValueDefined(value, nameof(BackgroundColor));
                    _backgroundColor = value;

                    FillCharacterSpacingBackgroundFiller();
                }
            }
        }

        static Symtext()
        {
            CharacterSpacing    = 1;
            FontSize            = 1;
            ForegroundColor     = Constants.FOREGROUND_COLOR;
            BackgroundColor     = Constants.BACKGROUND_COLOR;
        }

        private static void FillCharacterSpacingBackgroundFiller()
        {
            for (int row = 0; row < characterSpacingBackgroundFiller.GetLength(0); row++)
            {
                for (int column = 0; column < characterSpacingBackgroundFiller.GetLength(1); column++)
                {
                    characterSpacingBackgroundFiller[row, column] = BackgroundColor;
                }
            }
        }

        public static void Write(object value)
        {
            foreach (char ch in value.ToString())
            {
                if (ch == '\n')
                {
                    CursorX = 0;
                    CursorY += CHAR_HEIGHT;
                    continue;
                }

                bool[,] character               = GetChar(ch);
                
                int characterHeight             = character.GetLength(0);
                int characterWidth              = character.GetLength(1);
                ConsoleColor[,] renderedChar    = new ConsoleColor[characterHeight, characterWidth];

                for (int row = 0; row < characterHeight; row++)
                {
                    for (int column = 0; column < characterWidth; column++)
                    {
                        renderedChar[row, column] = character[row, column] ? ForegroundColor : BackgroundColor;
                    }
                }

                Program.MainRenderer.AddToBuffer(renderedChar, CursorX, CursorY);
                CursorX += characterWidth;

                Program.MainRenderer.AddToBuffer(characterSpacingBackgroundFiller, CursorX, CursorY);
                CursorX += CharacterSpacing;
            }
        }

        public static void Write(object value, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            string[] lines = value.ToString().Split('\n');

            int cursorX = 0;
            int cursorY = 0;

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top:     cursorY = 0;                                                            break;
                case VerticalAlignment.Center:  cursorY = (Console.WindowHeight - (lines.Length * CHAR_HEIGHT)) / 2;    break;
                case VerticalAlignment.Bottom:  cursorY = Console.WindowHeight - (lines.Length * CHAR_HEIGHT);          break;
            }

            foreach (string line in lines)
            {
                switch (horizontalAlignment)
                {
                    case HorizontalAlignment.Left:      cursorX = 0;                                                    break;
                    case HorizontalAlignment.Center:    cursorX = (Console.WindowWidth - GetSymtextWidth(line)) / 2;    break;
                    case HorizontalAlignment.Right:     cursorX = Console.WindowWidth - GetSymtextWidth(line);          break;
                }
                

                foreach (char ch in line)
                {
                    bool[,] character               = GetChar(ch);
                
                    int characterHeight             = character.GetLength(0);
                    int characterWidth              = character.GetLength(1);
                    ConsoleColor[,] renderedChar    = new ConsoleColor[characterHeight, characterWidth];

                    for (int row = 0; row < characterHeight; row++)
                    {
                        for (int column = 0; column < characterWidth; column++)
                        {
                            renderedChar[row, column] = character[row, column] ? ForegroundColor : BackgroundColor;
                        }
                    }

                    Program.MainRenderer.AddToBuffer(renderedChar, cursorX, cursorY);
                    cursorX += characterWidth;

                    Program.MainRenderer.AddToBuffer(characterSpacingBackgroundFiller, cursorX, cursorY);
                    cursorX += CharacterSpacing;
                }

                cursorY += CHAR_HEIGHT;
            }
        }

        private static int GetSymtextWidth(string str)
        {
            int output = 0;

            foreach (char ch in str)
            {
                output += GetChar(ch).GetLength(1) + CharacterSpacing;
            }

            return output;
        }

        private static bool[,] GetChar(char ch)
        {
            switch (char.ToLower(ch))
            {
                case 'a': return a;
                case 'b': return b;
                case 'c': return c;
                case 'd': return d;
                case 'e': return e;
                case 'f': return f;
                case 'g': return g;
                case 'h': return h;
                case 'i': return i;
                case 'j': return j;
                case 'k': return k;
                case 'l': return l;
                case 'm': return m;
                case 'n': return n;
                case 'o': return o;
                case 'p': return p;
                case 'q': return q;
                case 'r': return r;
                case 's': return s;
                case 't': return t;
                case 'u': return u;
                case 'v': return v;
                case 'w': return w;
                case 'x': return x;
                case 'y': return y;
                case 'z': return z;

                case ' ': return space;

                case '0': return _0;
                case '1': return _1;
                case '2': return _2;
                case '3': return _3;
                case '4': return _4;
                case '5': return _5;
                case '6': return _6;
                case '7': return _7;
                case '8': return _8;
                case '9': return _9;

                case '+': return plus;
                case '-': return minus;
                case '*': return cross;
                case '/': return slash;
                case '=': return equals;
                case '%': return percents;
                case '#': return hash;
                case ',': return comma;
                case '.': return dot;
                case ':': return colon;
                case '?': return questionMark;
                case '!': return exclamationMark;
                case '<': return arrowLeft;
                case '>': return arrowRight;
                case '[': return squareBracketLeft;
                case ']': return squareBracketRight;
                case '©': return copyrightMark;
                
                default: ExceptionHelper.ThrowMagicException(); return null;
            }
        }
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }
}
