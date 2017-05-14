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
        public static void Main(string[] args)
        {
            Console.Title = Constants.APP_NAME;

            // Don't you dare try uncommenting this (ง⸟ᨎ⸟)ง
            // Console.InputEncoding   = Constants.encoding;
            // Console.OutputEncoding  = Constants.encoding;

            // Run the static constructor of Renderer
            RuntimeHelpers.RunClassConstructor(typeof(Renderer).TypeHandle);

            AppData.Load();
            ProfileManager.LoadProfiles();

            MainMenu();

#if DEBUG
            throw new Exception("Y u do dis ಠ_ಠ");
#else
            Exit();
#endif
        }
        
        public static void MainMenu()
        {
            ListMenu mainMenu = new ListMenu
            {
                Items = new List<MenuItem>
                {
                    // I hope we're not filling the call stack using the while instead of calling the method in itself again to restart
                    new MenuItem("Singleplayer",    () => { while (Game.Singleplayer()) ; } ),
                 // new MenuItem("Multiplayer",     null                                    ),
                    new MenuItem("Profiles",        ProfileManager.ShowProfileSelection     ),
                 // new MenuItem("Options",         null                                    ),
                    new MenuItem("About",           About                                   ),
                    new MenuItem("Quit game",       () => Exit()                            )
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
                
                Symtext.WriteLine($"{Constants.APP_SHORT_NAME} v2.0 '{Constants.APP_NAME_ADDITION}'");
                Symtext.WriteLine("© 2017 Marian Dolinský\n");

                aboutMenu.InvokeResult();
            } while (!goBack);
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
