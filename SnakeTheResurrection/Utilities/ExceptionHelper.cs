using System;

namespace SnakeTheResurrection.Utilities
{
    public static class ExceptionHelper
    {
        public static void ValidateObjectNotNull(object obj, string parameterName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ValidateStringNotNullOrWhiteSpace(string str, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Value cannot be white space or null.", parameterName);
            }
        }

        public static void ValidateEnumValueDefined(Enum enumValue, string parameterName)
        {
            if (!Enum.IsDefined(enumValue.GetType(), enumValue))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        
        public static void ValidateNumberGreaterOrEqual(int value, int min, string parameterName)
        {
            if (value < min)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value ({value}) is out of range (smaller than {min}).");
            }
        }

        public static void ValidateNumberSmallerOrEqual(int value, int max, string parameterName)
        {
            if (value > max)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value ({value}) is out of range (greater than {max}).");
            }
        }

        public static void ValidateNumberInRange(int value, int min, int max, string parameterName)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value ({value}) is out of range ({min} - {max}).");
            }
        }

        public static void ValidateNumberInWindowHorizontalRange(int value, string parameterName)
        {
            ValidateNumberInRange(value, 0, Console.WindowWidth, parameterName);
        }

        public static void ValidateNumberInWindowVerticalRange(int value, string parameterName)
        {
            ValidateNumberInRange(value, 0, Console.WindowHeight, parameterName);
        }

        public static void ValidateMagic(bool magic)
        {
            if (!magic)
            {
                ThrowMagicException();
            }
        }

        public static void ThrowMagicException()
        {
            // To be able to debug the call stack etc
#if DEBUG
            throw new Exception();
#else
            DllImports.MessageBox(@"We are so sorry but some unknown dark power prevented us from doing the required magic ¯\_(ツ)_/¯", "No magic");
#endif
        }
    }
}
