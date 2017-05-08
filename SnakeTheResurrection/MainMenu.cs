using SnakeTheResurrection.Utilities;
using System.Collections.Generic;
using System.Diagnostics;

namespace SnakeTheResurrection
{
    public static class MainMenu
    {
        public static void Show()
        {
            ListMenu mainMenu = new ListMenu
            {
                Items = new List<MenuItem>
                {
                    new MenuItem("Singleplayer",    new Game().SinglePlayer             ),
                 // new MenuItem("Multiplayer",     null                                ),
                 // new MenuItem("Options",         null                                ),
                    new MenuItem("Profiles",        ProfileManager.ShowProfileSelection ),
                    new MenuItem("About",           About                               ),
                    new MenuItem("Quit game",       () => Program.Exit()                )
                }
            };

            ProfileManager.ShowProfileSelection();
            
            while (true)
            {
                Renderer.CleanBuffer();
                
                Symtext.WriteTitle(Constants.APP_SHORT_NAME, 7);
                
                mainMenu.InvokeResult();
                Helpers.ClearInputBuffer();
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
                Symtext.SetTextProperties();

                Symtext.WriteLine($"{Constants.APP_SHORT_NAME} v2.0 '{Constants.APP_NAME_ADDITION}'", HorizontalAlignment.Center);
                Symtext.WriteLine("© 2017 Marian Dolinský\n", HorizontalAlignment.Center);

                aboutMenu.InvokeResult();
            } while (!goBack);
        }
    }
}
