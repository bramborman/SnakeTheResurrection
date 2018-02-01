namespace SnakeTheResurrection.Utilities
{
    public static partial class Symtext
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
    }
}
