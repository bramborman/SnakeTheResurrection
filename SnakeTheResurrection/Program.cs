using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        private static int originalWindowLeft;
        private static int originalWindowTop;
        private static int originalWindowWidth;
        private static int originalWindowHeight;

        public static void Main(string[] args)
        {
            originalWindowLeft      = Console.WindowLeft;
            originalWindowTop       = Console.WindowTop; 
            originalWindowWidth     = Console.WindowWidth;
            originalWindowHeight    = Console.WindowHeight;

            Console.Title = Constants.APP_NAME;

            // Don't you dare try uncommenting this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;

            AppData.Load();
            ProfileManager.LoadProfiles();

            // Run the static constructor of Renderer
            RuntimeHelpers.RunClassConstructor(typeof(Renderer).TypeHandle);
            MainMenu();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            FullExit();
#endif
        }
        
        public static void MainMenu()
        {
            ListMenu mainMenu = new ListMenu
            {
                Items = new List<MenuItem>
                {
#pragma warning disable IDE0011 // Add braces
                    // I hope we're not filling the call stack using the while instead of calling the method in itself again to restart
                    new MenuItem("Singleplayer",    () => { while (Game.Play(false)) ; }    ),
                    new MenuItem("Multiplayer",     () => { while (Game.Play(true)) ; }     ),
                    new MenuItem("About",           About                                   ),
                    new MenuItem("Quit game",       () => FullExit()                        )
#pragma warning restore IDE0011 // Add braces
                }
            };

            ProfileManager.ShowProfileSelection();
            
            while (true)
            {
                Renderer.Clear();
                Symtext.WriteTitle(Constants.APP_SHORT_NAME, 7);
                
                mainMenu.InvokeResult();
                InputHelper.ClearInputBuffer();
            }
        }

        public static void About()
        {
            bool goBack = false;

            ListMenu aboutMenu = new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("GitHub repo", () => Process.Start("https://github.com/bramborman/SnakeTheResurrection")   ),
                    new MenuItem("Back",        () => goBack = true                                                         )
                },
                SelectedIndex = 1
            };

            // Whole screen has to be rendered every time, because opening the link causes everything on screen to disappear
            do
            {
                Symtext.WriteTitle("About", 0);
                Symtext.SetCenteredTextProperties();

                Version gameVersion = AppData.Current.LastRunAppVersion;
                Symtext.WriteLine($"{Constants.APP_SHORT_NAME} v{gameVersion.Major}.{gameVersion.Minor}.{gameVersion.Build} '{Constants.APP_NAME_ADDITION}'");
                Symtext.WriteLine("© 2017 Marian Dolinský\n");

                aboutMenu.InvokeResult();
            } while (!goBack);
        }

        public static void FullExit(bool error = false)
        {
            if (!error)
            {
                AppData.Current.Save();
                ProfileManager.SaveProfiles();
            }

            // Cleanup
            Renderer.Clear();
            Renderer.RenderFrame();

            DllImports.ConsoleFullscreen = false;

            Console.WindowLeft   = originalWindowLeft;
            Console.WindowTop    = originalWindowTop;
            Console.WindowWidth  = originalWindowWidth;
            Console.WindowHeight = originalWindowHeight;
            DllImports.SetFont("Consolas", 7, 14);

            Console.Clear();

            Environment.Exit(error ? 1 : 0);
        }
    }
}
