using SnakeTheResurrection.Data;
using System;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            #region Initialization
            Console.Title           = Constants.APP_NAME;
            Console.InputEncoding   = Constants.encoding;
            Console.OutputEncoding  = Constants.encoding;
            Console.ForegroundColor = Constants.FOREGROUND_COLOR;

            AppData.Load();
            ProfileManager.LoadProfiles();
            #endregion

            Console.Write("Well, this is new ^_^");
            Console.ReadKey();

            // Main game method - main menu + profile selection

            #region Unitialization
            AppData.Current.Save();
            ProfileManager.SaveProfiles();
            #endregion
        }
    }
}
