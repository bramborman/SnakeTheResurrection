using System;

namespace SnakeTheResurrection.Utilities
{
    // Font inspired by Symtext (4/26/2017): http://www.dafont.com/symtext.font
    public static class Symtext
    {
        private const bool X = true;
        private const bool _ = false;

        // All chars should be 7 rows tall
        // If changed, change CharHeight value
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
        private static readonly bool[,] ý = new bool[,]
        {
            { _, _, X, _ },
            { X, _, X, X },
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
        private static readonly bool[,] quotationMark = new bool[,]
        {
            { X, X },
            { X, X },
            { _, _ },
            { _, _ },
            { _, _ },
            { _, _ },
            { _, _ }
        };
        private static readonly bool[,] apostrophe = new bool[,]
        {
            { X },
            { X },
            { _ },
            { _ },
            { _ },
            { _ },
            { _ }
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
        private static readonly bool[,] underline = new bool[,]
        {
            { _, _, _, _ },
            { _, _, _, _ },
            { _, _, _, _ },
            { _, _, _, _ },
            { _, _, _, _ },
            { X, X, X, X },
            { _, _, _, _ }
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

        private static short[,] characterSpacingBackgroundFiller;

        private static int _cursorLeft;
        private static int _cursorTop;
        private static int _fontSize;
        private static short _foregroundColor;
        private static short _backgroundColor;
        private static HorizontalAlignment _horizontalAlignment;
        private static VerticalAlignment _verticalAlignment;

        public static readonly object syncRoot;

        private static int CharacterSpacing
        {
            get
            {
                return FontSize;
            }
        }

        public static int CursorLeft
        {
            get { return _cursorLeft; }
            set
            {
                lock (syncRoot)
                {
                    if (_cursorLeft != value)
                    {
                        ExceptionHelper.ValidateNumberInRange(value, 0, Console.WindowWidth, nameof(CursorLeft));
                        _cursorLeft = value;
                    }
                }
            }
        }
        public static int CursorTop
        {
            get { return _cursorTop; }
            set
            {
                lock (syncRoot)
                {
                    if (_cursorTop != value)
                    {
                        ExceptionHelper.ValidateNumberInRange(value, 0, Console.WindowHeight, nameof(CursorTop));
                        _cursorTop = value;
                    }
                }
            }
        }
        public static int FontSize
        {
            get { return _fontSize; }
            set
            {
                lock (syncRoot)
                {
                    if (_fontSize != value)
                    {
                        ExceptionHelper.ValidateNumberGreaterOrEqual(value, 0, nameof(FontSize));
                        _fontSize = value;

                        characterSpacingBackgroundFiller = new short[CharHeight, value];
                        FillCharacterSpacingBackgroundFiller();
                    }
                }
            }
        }
        public static short ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                lock (syncRoot)
                {
                    if (_foregroundColor != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined((ConsoleColor)value, nameof(ForegroundColor));
                        _foregroundColor = value;
                    }
                }
            }
        }
        public static short BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                lock (syncRoot)
                {
                    if (_backgroundColor != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined((ConsoleColor)value, nameof(BackgroundColor));
                        _backgroundColor = value;

                        FillCharacterSpacingBackgroundFiller();
                    }
                }
            }
        }
        public static HorizontalAlignment HorizontalAlignment
        {
            get { return _horizontalAlignment; }
            set
            {
                lock (syncRoot)
                {
                    if (_horizontalAlignment != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(HorizontalAlignment));
                        _horizontalAlignment = value;
                    }
                }
            }
        }
        public static VerticalAlignment VerticalAlignment
        {
            get { return _verticalAlignment; }
            set
            {
                lock (syncRoot)
                {
                    if (_verticalAlignment != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(VerticalAlignment));
                        _verticalAlignment = value;
                    }
                }
            }
        }
        public static int CharHeight
        {
            get
            {
                return 7 * FontSize;
            }
        }

        static Symtext()
        {
            syncRoot            = new object();
            Reset();
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

        public static void SetCursorPosition(int left, int top)
        {
            CursorLeft = left;
            CursorTop  = top;
        }

        public static void Reset()
        {
            lock (syncRoot)
            {
                CursorLeft          = 0;
                CursorTop           = 0;
                FontSize            = 1;
                ForegroundColor     = Constants.FOREGROUND_COLOR;
                BackgroundColor     = Constants.BACKGROUND_COLOR;
                HorizontalAlignment = default;
                VerticalAlignment   = default;
            }
        }

        public static void Write(object value)
        {
            Write(value, 0);
        }
        
        public static void Write(object value, int verticalOffset)
        {
            lock (syncRoot)
            {
                string[] lines = value.ToString().Split('\n');
                
                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Top:     CursorTop = 0;                                                            break;
                    case VerticalAlignment.Center:  CursorTop = (Console.WindowHeight - (lines.Length * CharHeight)) / 2;     break;
                    case VerticalAlignment.Bottom:  CursorTop = Console.WindowHeight - (lines.Length * CharHeight);           break;
                }

                CursorTop += verticalOffset;

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    switch (HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:      CursorLeft = 0;                                                    break;
                        case HorizontalAlignment.Center:    CursorLeft = (Console.WindowWidth - GetSymtextWidth(line)) / 2;    break;
                        case HorizontalAlignment.Right:     CursorLeft = Console.WindowWidth - GetSymtextWidth(line);          break;
                    }

                    for (int j = 0; j < line.Length; j++)
                    {
                        // Is an escape character probably
                        if (line[j] == '\\')
                        {
                            continue;
                        }

                        CursorLeft += AddRenderedCharToBuffer(line[j], CursorLeft, CursorTop);

                        if (j != line.Length - 1)
                        {
                            Renderer.AddToBuffer(characterSpacingBackgroundFiller, CursorLeft, CursorTop);
                            CursorLeft += CharacterSpacing;
                        }
                    }

                    if (i != lines.Length - 1)
                    {
                        CursorTop += CharHeight;
                    }
                }
            }
        }

        public static void WriteLine()
        {
            Write('\n');
        }

        public static void WriteLine(object value)
        {
            Write(value.ToString() + '\n');
        }

        public static void WriteLine(object value, int verticalOffset)
        {
            Write(value.ToString() + '\n', verticalOffset);
        }

        public static void WriteTitle(object value, int verticalOffset)
        {
            ForegroundColor     = Constants.ACCENT_COLOR;
            BackgroundColor     = Constants.BACKGROUND_COLOR;
            FontSize            = 15;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment   = VerticalAlignment.Center;
            WriteLine(value, verticalOffset);

            HorizontalAlignment = HorizontalAlignment.None;
            VerticalAlignment   = VerticalAlignment.None;

            FontSize = 3;
            WriteLine();
        }

        public static void SetTextProperties()
        {
            ForegroundColor     = Constants.FOREGROUND_COLOR;
            BackgroundColor     = Constants.BACKGROUND_COLOR;
            FontSize            = 2;
            HorizontalAlignment = HorizontalAlignment.None;
            VerticalAlignment   = VerticalAlignment.None;
        }

        public static void SetCenteredTextProperties()
        {
            SetTextProperties();
            HorizontalAlignment = HorizontalAlignment.Center;
        }

        public static int GetSymtextWidth(string str)
        {
            int output = 0;

            foreach (char ch in str)
            {
                output += GetScaledBoolChar(ch).GetLength(1) + CharacterSpacing;
            }
            
            // We are not adding the character spacing after the word
            return output - CharacterSpacing;
        }

        private static int AddRenderedCharToBuffer(char ch, int x, int y)
        {
            bool[,] texture = GetScaledBoolChar(ch);
            Renderer.AddToBuffer(texture, ForegroundColor, BackgroundColor, x, y);

            return texture.GetLength(1);
        }

        private static bool[,] GetScaledBoolChar(char ch)
        {
            bool[,] original    = GetBoolChar(ch);

            int originalHeight  = original.GetLength(0);
            int originalWidth   = original.GetLength(1);
            bool[,] output      = new bool[originalHeight * FontSize, originalWidth * FontSize];
            
            for (int row = 0; row < originalHeight; row++)
            {
                for (int column = 0; column < originalWidth; column++)
                {
                    bool currentValue = original[row, column];

                    for (int row2 = 0; row2 < FontSize; row2++)
                    {
                        for (int column2 = 0; column2 < FontSize; column2++)
                        {
                            output[(row * FontSize) + row2, (column * FontSize) + column2] = currentValue;
                        }
                    }
                }
            }

            return output;
        }

        private static bool[,] GetBoolChar(char ch)
        {
            switch (char.ToLowerInvariant(ch))
            {
                case 'a':
                case 'á': return a;
                case 'b': return b;
                case 'c':
                case 'č': return c;
                case 'd':
                case 'ď': return d;
                case 'e':
                case 'é':
                case 'ě': return e;
                case 'f': return f;
                case 'g': return g;
                case 'h': return h;
                case 'i':
                case 'í': return i;
                case 'j': return j;
                case 'k': return k;
                case 'l': return l;
                case 'm': return m;
                case 'n':
                case 'ň': return n;
                case 'o':
                case 'ó': return o;
                case 'p': return p;
                case 'q': return q;
                case 'r':
                case 'ř': return r;
                case 's':
                case 'š': return s;
                case 't':
                case 'ť': return t;
                case 'u':
                case 'ú':
                case 'ů': return u;
                case 'v': return v;
                case 'w': return w;
                case 'x': return x;
                case 'y':
                case 'ý': return y;
                case '§': return ý;
                case 'z':
                case 'ž': return z;
                
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
                case '"': return quotationMark;
                case '\'': return apostrophe;
                case '#': return hash;
                case ',': return comma;
                case '.': return dot;
                case ':': return colon;
                case '?': return questionMark;
                case '!': return exclamationMark;
                case '_': return underline;
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
        None,
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        None,
        Top,
        Center,
        Bottom
    }
}
