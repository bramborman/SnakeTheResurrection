using System;

namespace SnakeTheResurrection.Utilities
{
    // Font inspired by Symtext (4/26/2017): http://www.dafont.com/symtext.font
    public static class Symtext
    {
        private const bool X = true;
        private const bool _ = false;

        // All chars should be 7 rows tall
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

        private static int _cursorLeft;
        private static int _cursorTop;
        private static int _fontSize;
        private static SymtextScalingStyle _scalingStyle;
        private static ConsoleColor _foregroundColor;
        private static ConsoleColor _backgroundColor;

        private static int CharacterSpacing
        {
            get
            {
                return FontSize;
            }
        }

        public static object SyncRoot { get; }
        public static int CursorLeft
        {
            get { return _cursorLeft; }
            set
            {
                lock (SyncRoot)
                {
                    if (_cursorLeft != value)
                    {
                        ExceptionHelper.ValidateNumberInWindowHorizontalRange(value, nameof(CursorLeft));
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
                lock (SyncRoot)
                {
                    if (_cursorTop != value)
                    {
                        ExceptionHelper.ValidateNumberInWindowVerticalRange(value, nameof(CursorTop));
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
                lock (SyncRoot)
                {
                    if (_fontSize != value)
                    {
                        ExceptionHelper.ValidateNumberGreaterOrEqual(value, 0, nameof(FontSize));
                        _fontSize = value;

                        characterSpacingBackgroundFiller = new ConsoleColor[CharHeight, value];
                        FillCharacterSpacingBackgroundFiller();
                    }
                }
            }
        }
        public static SymtextScalingStyle ScalingStyle
        {
            get { return _scalingStyle; }
            set
            {
                lock (SyncRoot)
                {
                    if (_scalingStyle != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(ScalingStyle));
                        _scalingStyle = value;
                    }
                }
            }
        }
        public static ConsoleColor ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                lock (SyncRoot)
                {
                    if (_foregroundColor != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(ForegroundColor));
                        _foregroundColor = value;
                    }
                }
            }
        }
        public static ConsoleColor BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                lock (SyncRoot)
                {
                    if (_backgroundColor != value)
                    {
                        ExceptionHelper.ValidateEnumValueDefined(value, nameof(BackgroundColor));
                        _backgroundColor = value;

                        FillCharacterSpacingBackgroundFiller();
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
            SyncRoot            = new object();
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

        public static void SetCursorPosition(int left, int top)
        {
            CursorLeft   = left;
            CursorTop   = top;
        }

        public static void Write(object value)
        {
            lock (SyncRoot)
            {
                string[] lines = value.ToString().Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i != 0)
                    {
                        CursorLeft = 0;
                    }

                    string line = lines[i];

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

        public static void Write(object value, HorizontalAlignment horizontalAlignment)
        {
            lock (SyncRoot)
            {
                string[] lines = value.ToString().Split('\n');

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int cursorX = 0;

                    switch (horizontalAlignment)
                    {
                        case HorizontalAlignment.Left:      cursorX += 0;                                                   break;
                        case HorizontalAlignment.Center:    cursorX += (Console.WindowWidth - GetSymtextWidth(line)) / 2;   break;
                        case HorizontalAlignment.Right:     cursorX += Console.WindowWidth - GetSymtextWidth(line);         break;
                    }
                
                    for (int j = 0; j < line.Length; j++)
                    {
                        cursorX += AddRenderedCharToBuffer(line[j], cursorX, CursorTop);

                        if (j != line.Length - 1)
                        {
                            Renderer.AddToBuffer(characterSpacingBackgroundFiller, cursorX, CursorTop);
                            cursorX += CharacterSpacing;
                        }
                    }

                    if (i != lines.Length - 1)
                    {
                        CursorTop += CharHeight;
                    }
                }
            }
        }

        public static void Write(object value, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            Write(value, horizontalAlignment, verticalAlignment, 0, 0);
        }

        public static void Write(object value, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, int horizontalOffset, int verticalOffset)
        {
            lock (SyncRoot)
            {
                string[] lines = value.ToString().Split('\n');
                
                switch (verticalAlignment)
                {
                    case VerticalAlignment.Top:     CursorTop = 0;                                                            break;
                    case VerticalAlignment.Center:  CursorTop = (Console.WindowHeight - (lines.Length * CharHeight)) / 2;     break;
                    case VerticalAlignment.Bottom:  CursorTop = Console.WindowHeight - (lines.Length * CharHeight);           break;
                }

                CursorTop += verticalOffset;

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    switch (horizontalAlignment)
                    {
                        case HorizontalAlignment.Left:      CursorLeft = 0;                                                    break;
                        case HorizontalAlignment.Center:    CursorLeft = (Console.WindowWidth - GetSymtextWidth(line)) / 2;    break;
                        case HorizontalAlignment.Right:     CursorLeft = Console.WindowWidth - GetSymtextWidth(line);          break;
                    }

                    CursorLeft += horizontalOffset;

                    for (int j = 0; j < line.Length; j++)
                    {
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

        public static void WriteLine(object value, HorizontalAlignment horizontalAlignment)
        {
            Write(value.ToString() + '\n', horizontalAlignment);
        }

        public static void WriteLine(object value, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            Write(value.ToString() + '\n', horizontalAlignment, verticalAlignment);
        }

        public static void WriteLine(object value, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, int horizontalOffset, int verticalOffset)
        {
            Write(value.ToString() + '\n', horizontalAlignment, verticalAlignment, horizontalOffset, verticalOffset);
        }

        public static void WriteTitle(object value, int verticalOffset)
        {
            ForegroundColor = Constants.ACCENT_COLOR;
            BackgroundColor = Constants.BACKGROUND_COLOR;
            FontSize        = 15;
            WriteLine(value, HorizontalAlignment.Center, VerticalAlignment.Center, 0, verticalOffset);

            FontSize = 3;
            WriteLine();

            SetTextProperties();
        }

        public static void SetTextProperties()
        {
            ForegroundColor = Constants.FOREGROUND_COLOR;
            BackgroundColor = Constants.BACKGROUND_COLOR;
            FontSize        = 2;
        }

        public static int GetSymtextWidth(string str)
        {
            int output = 0;

            foreach (char ch in str)
            {
                output += GetScaledBoolChar(ch).GetLength(1) + CharacterSpacing;
            }
            
            // We are not adding the character spacing behind the word
            return output - CharacterSpacing;
        }

        private static int AddRenderedCharToBuffer(char ch, int x, int y)
        {
            bool[,] character               = GetScaledBoolChar(ch);
                
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

            Renderer.AddToBuffer(renderedChar, x, y);
            return characterWidth;
        }

        private static bool[,] GetScaledBoolChar(char ch)
        {
            bool[,] original    = GetBoolChar(ch);

            int originalHeight  = original.GetLength(0);
            int originalWidth   = original.GetLength(1);
            bool[,] output      = new bool[originalHeight * FontSize, originalWidth * FontSize];

            if (ScalingStyle == SymtextScalingStyle.Normal)
            {
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
            }
            else
            {
                for (int row = 0; row < originalHeight; row++)
                {
                    for (int column = 0; column < originalWidth; column++)
                    {
                        bool currentValue = original[row, column];

                        for (int difference = 0; difference < FontSize; difference++)
                        {
                            output[(row * FontSize) + difference, (column * FontSize) + difference] = currentValue;
                        }
                    }
                }
            }

            return output;
        }

        private static bool[,] GetBoolChar(char ch)
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

                case 'ý': return ý;

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
                case '<': return arrowLeft;
                case '>': return arrowRight;
                case '[': return squareBracketLeft;
                case ']': return squareBracketRight;
                case '©': return copyrightMark;
                
                default: ExceptionHelper.ThrowMagicException(); return null;
            }
        }
    }

    public enum SymtextScalingStyle
    {
        Normal,
        Stripped
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
