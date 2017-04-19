using SnakeTheResurrection.Data;
using System;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            #region Initialization
            AppData.Load();

            Console.Title           = Constants.APP_NAME;
            Console.InputEncoding   = Constants.encoding;
            Console.OutputEncoding  = Constants.encoding;
            #endregion

            Console.Write("What do you want? ಠ_ಠ");
            Console.ReadKey();

            #region Unitialization
            AppData.Current.Save();
            #endregion
        }
    }
}
