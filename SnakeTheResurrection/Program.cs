using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static Renderer MainRenderer { get; private set; }

        public static void Main(string[] args)
        {
            Console.Title           = Constants.APP_NAME;
            Console.CursorVisible   = false;

            // Don't you dare trying to uncomment this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;

            // Is this even needed? ಠ_ಠ
            Console.ForegroundColor = Constants.FOREGROUND_COLOR;
            
            MainRenderer = new Renderer();
            DllImports.MessageBox("U still there?!", "");

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
