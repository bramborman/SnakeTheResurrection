using System;

namespace SnakeTheResurrection.Utilities
{
    // Font inspired by Symtext (4/26/2017): http://www.dafont.com/symtext.font
    public static class Symtext
    {
        private const bool X = true;
        private const bool _ = false;

        #region Alphabet
        private static readonly bool[,] a = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] b = new bool[,]
        {
            { X, X, X, _ },
            { X, _, X, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] c = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, X, X, X }
        };
        private static readonly bool[,] d = new bool[,]
        {
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] e = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, _ },
            { X, _, _, _ },
            { X, X, X, X }
        };
        private static readonly bool[,] f = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, _ },
            { X, _, _, _ },
            { X, _, _, _ }
        };
        private static readonly bool[,] g = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] h = new bool[,]
        {
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] i = new bool[,]
        {
            { X },
            { X },
            { X },
            { X },
            { X }
        };
        private static readonly bool[,] j = new bool[,]
        {
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] k = new bool[,]
        {
            { X, _, X, _ },
            { X, _, X, _ },
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] l = new bool[,]
        {
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, _, _, _ },
            { X, X, X, X }
        };
        private static readonly bool[,] m = new bool[,]
        {
            { X, X, X, X, X },
            { X, _, X, _, X },
            { X, _, X, _, X },
            { X, _, _, _, X },
            { X, _, _, _, X }
        };
        private static readonly bool[,] n = new bool[,]
        {
            { X, X, X, _ },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] o = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] p = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, _ },
            { X, _, _, _ }
        };
        private static readonly bool[,] q = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, X, X },
            { X, X, X, X }
        };
        private static readonly bool[,] r = new bool[,]
        {
            { X, X, X, _ },
            { X, _, X, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] s = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] t = new bool[,]
        {
            { X, X, X, X, X },
            { _, _, X, _, _ },
            { _, _, X, _, _ },
            { _, _, X, _, _ },
            { _, _, X, _, _ }
        };
        private static readonly bool[,] u = new bool[,]
        {
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] v = new bool[,]
        {
            { X, _, _, X },
            { X, _, _, X },
            { X, _, _, X },
            { X, _, X, _ },
            { _, X, _, _ }
        };
        private static readonly bool[,] w = new bool[,]
        {
            { X, _, _, _, X },
            { X, _, _, _, X },
            { X, _, X, _, X },
            { X, _, X, _, X },
            { X, X, X, X, X }
        };
        private static readonly bool[,] x = new bool[,]
        {
            { X, _, _, X },
            { X, _, _, X },
            { _, X, X, _ },
            { X, _, _, X },
            { X, _, _, X }
        };
        private static readonly bool[,] y = new bool[,]
        {
            { X, _, _, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] z = new bool[,]
        {
            { X, X, X, X },
            { _, _, _, X },
            { _, X, X, _ },
            { X, _, _, _ },
            { X, X, X, X }
        };
        #endregion
        #region Numbers
        private static readonly bool[,] _0 = new bool[,]
        {
            { X, X, X, X, X },
            { X, _, _, _, X },
            { X, _, X, _, X },
            { X, _, _, _, X },
            { X, X, X, X, X }
        };
        private static readonly bool[,] _1 = new bool[,]
        {
            { X, X },
            { _, X },
            { _, X },
            { _, X },
            { _, X }
        };
        private static readonly bool[,] _2 = new bool[,]
        {
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X }
        };
        private static readonly bool[,] _3 = new bool[,]
        {
            { X, X, X, X },
            { _, _, _, X },
            { _, _, X, X },
            { _, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] _4 = new bool[,]
        {
            { X, _, _, _ },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X }
        };
        private static readonly bool[,] _5 = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { _, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] _6 = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, _ },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] _7 = new bool[,]
        {
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X },
            { _, _, _, X }
        };
        private static readonly bool[,] _8 = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X }
        };
        private static readonly bool[,] _9 = new bool[,]
        {
            { X, X, X, X },
            { X, _, _, X },
            { X, X, X, X },
            { _, _, _, X },
            { _, _, _, X }
        };
        #endregion
        #region Special characters
        private static readonly bool[,] plus = new bool[,]
        {
            { _, X, _ },
            { X, X, X },
            { _, X, _ }
        };
        private static readonly bool[,] minus = new bool[,]
        {
            { X, X, X }
        };
        private static readonly bool[,] cross = new bool[,]
        {
            { X, _, X },
            { _, X, _ },
            { X, _, X }
        };
        private static readonly bool[,] slash = new bool[,]
        {
            { _, _, X },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { X, _, _ }
        };
        private static readonly bool[,] equals = new bool[,]
        {
            { X, X, X },
            { _, _, _ },
            { X, X, X }
        };
        private static readonly bool[,] percents = new bool[,]
        {
            { X, _, X },
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { X, _, X }
        };
        private static readonly bool[,] hash = new bool[,]
        {
            { _, X, _, X, _ },
            { X, X, X, X, X },
            { _, X, _, X, _ },
            { X, X, X, X, X },
            { _, X, _, X, _ }
        };
        private static readonly bool[,] comma = new bool[,]
        {
            { X },
            { X }
        };
        private static readonly bool[,] dot = new bool[,]
        {
            { X }
        };
        private static readonly bool[,] colon = new bool[,]
        {
            { X },
            { _ },
            { X }
        };
        private static readonly bool[,] questionMark = new bool[,]
        {
            { X, X, X, X },
            { _, _, _, X },
            { _, X, X, X },
            { _, _, _, _ },
            { _, X, _, _ }
        };
        private static readonly bool[,] exclamationMark = new bool[,]
        {
            { X },
            { X },
            { X },
            { _ },
            { X }
        };
        private static readonly bool[,] arrowLeft = new bool[,]
        {
            { _, _, X },
            { _, X, _ },
            { X, _, _ },
            { _, X, _ },
            { _, _, X }
        };
        private static readonly bool[,] arrowRight = new bool[,]
        {
            { X, _, _ },
            { _, X, _ },
            { _, _, X },
            { _, X, _ },
            { X, _, _ }
        };
        private static readonly bool[,] squareBracketLeft = new bool[,]
        {
            { X, X },
            { X, _ },
            { X, _ },
            { X, _ },
            { X, X }
        };
        private static readonly bool[,] squareBracketRight = new bool[,]
        {
            { X, X },
            { _, X },
            { _, X },
            { _, X },
            { X, X }
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

        private static ConsoleColor _color;

        public static ConsoleColor Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    ExceptionHelper.ValidateEnumValueDefined(value, nameof(Color));
                    _color = value;
                }
            }
        }
        
        public static void Write(object value)
        {
            //TODO: this

            foreach (char ch in value.ToString())
            {
                bool[,] character = GetChar(ch);

                // Program.MainRenderer.Buffer
            }

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
}
