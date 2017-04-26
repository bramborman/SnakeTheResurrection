using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title           = Constants.APP_NAME;
            Console.CursorVisible   = false;

            // Are these even needed? ಠ_ಠ
            Console.InputEncoding   = Constants.encoding;
            Console.OutputEncoding  = Constants.encoding;
            Console.ForegroundColor = Constants.FOREGROUND_COLOR;

            new Renderer();
            DllImports.MessageBox("Lol what did you expect?!", "");

            AppData.Load();
            ProfileManager.LoadProfiles();

            MainMenu.Show();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            Exit();
#endif
        }

        public static void Exit([CallerMemberName]string callerMemberName = null)
        {
            AppData.Current.Save();
            ProfileManager.SaveProfiles();
            Environment.Exit(callerMemberName == nameof(Main) ? 1 : 0);
        }

        public static void ExitWithError()
        {
            Environment.Exit(1);
        }
    }
}
