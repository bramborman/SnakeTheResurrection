using SnakeTheResurrection.Data;
using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SnakeTheResurrection
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = Constants.APP_NAME;

            // Don't you dare try uncommenting this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;

            AppData.Load();

            List<string> appDataParseStatus = AppData.Current.TryParse(args);
            appDataParseStatus.ForEach(Console.WriteLine);

            if (appDataParseStatus.Count != 0)
            {
                Console.Write("Press any key to continue or press escape to exit . . .");

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    return;
                }

                Console.Clear();
            }

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
                Renderer.ClearBuffer();
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

                Version gameVersion = Assembly.GetExecutingAssembly().GetName().Version;
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

            Environment.Exit(error ? 1 : 0);
        }
    }
}
