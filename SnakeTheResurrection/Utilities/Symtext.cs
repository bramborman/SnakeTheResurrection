using System;

namespace SnakeTheResurrection.Utilities
{
    // Font inspired by Symtext (4/26/2017): http://www.dafont.com/symtext.font
    public static partial class Symtext
    {
        public static readonly object syncRoot = new object();

        private static int _cursorLeft;
        private static int _cursorTop;
        private static int _fontSize;
        private static short _foregroundColor;
        private static short _backgroundColor;
        private static HorizontalAlignment _horizontalAlignment;
        private static VerticalAlignment _verticalAlignment;

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
            SetDefaultProperties();
        }

        public static void SetCursorPosition(int left, int top)
        {
            CursorLeft = left;
            CursorTop  = top;
        }

        public static void Write(object value)
        {
            Write(value, 0);
        }
        
        public static void Write(object value, int verticalOffset)
        {
            if (value == null || (string)value == string.Empty)
            {
                return;
            }

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

                        bool[,] texture = GetScaledBoolChar(line[j], FontSize);
                        int textureWidth = texture.GetLength(1);

                        if (IsOverWindowX(CursorLeft + textureWidth))
                        {
                            CursorLeft = 0;
                        }

                        Renderer.AddToBuffer(texture, ForegroundColor, BackgroundColor, CursorLeft, CursorTop);
                        CursorLeft += textureWidth;

                        if (j != line.Length - 1)
                        {
                            int width = CharacterSpacing;

                            if (IsOverWindowX(CursorLeft + CharacterSpacing))
                            {
                                width = Console.WindowWidth - CursorLeft;
                            }

                            Renderer.AddToBuffer(BackgroundColor, CursorLeft, CursorTop, width, CharHeight);
                            CursorLeft += CharacterSpacing;
                        }


                        bool IsOverWindowX(int x)
                        {
                            return x > Console.WindowWidth;
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
            WriteLine(null);
        }

        public static void WriteLine(object value)
        {
            WriteLine(value, 0);
        }

        public static void WriteLine(object value, int verticalOffset)
        {
            Write(value?.ToString() + '\n', verticalOffset);
            CursorLeft = 0;
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

        public static bool[][,] Render(string str, int fontSize)
        {
            bool[][,] output = new bool[str.Length][,];

            for (int i = 0; i < str.Length; i++)
            {
                output[i] = GetScaledBoolChar(str[i], fontSize);
            }

            return output;
        }
        
        public static void SetDefaultProperties()
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

        public static void SetTextProperties()
        {
            lock (syncRoot)
            {
                ForegroundColor     = Constants.FOREGROUND_COLOR;
                BackgroundColor     = Constants.BACKGROUND_COLOR;
                FontSize            = 2;
                HorizontalAlignment = HorizontalAlignment.None;
                VerticalAlignment   = VerticalAlignment.None;
            }
        }

        public static void SetCenteredTextProperties()
        {
            lock (syncRoot)
            {
                SetTextProperties();
                HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        public static int GetSymtextWidth(string str)
        {
            lock (syncRoot)
            {
                int output = 0;

                foreach (char ch in str)
                {
                    output += GetScaledBoolChar(ch, FontSize).GetLength(1) + CharacterSpacing;
                }

                // We are not adding the character spacing after the word
                return output - CharacterSpacing;
            }
        }
        
        private static bool[,] GetScaledBoolChar(char ch, int fontSize)
        {
            return Renderer.Scale(GetBoolChar(), fontSize);


            bool[,] GetBoolChar()
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
                    case '(': return bracketLeft;
                    case ')': return bracketRight;
                    case '[': return squareBracketLeft;
                    case ']': return squareBracketRight;
                    case '©': return copyrightMark;

                    default: ExceptionHelper.ThrowMagicException(); return null;
                }
            }
        }
    }
}
