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
            Console.Title = Constants.APP_NAME;

            // Don't you dare try uncommenting this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;
            
            MainRenderer = new Renderer();
            Symtext.ForegroundColor = ConsoleColor.Green;
            Symtext.Write("Hello world!!!\nThis is magic", HorizontalAlignment.Center, VerticalAlignment.Center);
            MainRenderer.RenderFrame();
            Console.ReadKey();

            DllImports.MessageBox("Even better, don't u think?", "");

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
