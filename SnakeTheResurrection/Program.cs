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
            MainRenderer.RenderFrame();
            Console.ReadKey();

            for (int i = 0; i < 200; i++)
            {
                MainRenderer.Buffer[i, i] = ConsoleColor.Yellow;
            }

            MainRenderer.RenderFrame();
            Console.ReadKey();
            DllImports.MessageBox("I know what u been lookin for ;)", "");

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
