using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;

namespace SnakeTheResurrection
{
    public static class MainMenu
    {
        private static readonly ListMenu menuItems = new ListMenu
        {
            Items = new List<MenuItem>
            {
                new MenuItem("Singleplayer",    new Game().SinglePlayer             ),
             // new MenuItem("Multiplayer",     null                                ),
             // new MenuItem("Options",         null                                ),
                new MenuItem("Profiles",        ProfileManager.ShowProfileSelection ),
                new MenuItem("About",           About                               ),
                new MenuItem("Quit game",       () => Program.Exit()                )
            },
            VerticalOffset = 57
        };

        public static void Show()
        {
            ProfileManager.ShowProfileSelection();

            while (true)
            {
                Renderer.CleanBuffer();

                Symtext.ForegroundColor = Constants.ACCENT_COLOR;
                Symtext.BackgroundColor = Constants.BACKGROUND_COLOR;
                Symtext.FontSize        = Constants.BIG_TITLE_SYMTEXT_SIZE;
                Symtext.WriteLine(Constants.APP_SHORT_NAME, HorizontalAlignment.Center, VerticalAlignment.Center, 0, 7);
                
                menuItems.InvokeResult();
                Helpers.ClearInputBuffer();
            }
        }

        public static void About()
        {
            Symtext.ForegroundColor = Constants.ACCENT_COLOR;
            Symtext.BackgroundColor = Constants.BACKGROUND_COLOR;
            Symtext.FontSize        = Constants.BIG_TITLE_SYMTEXT_SIZE;
            Symtext.WriteLine("About", HorizontalAlignment.Center, VerticalAlignment.Center, 0, -8);

            Symtext.ForegroundColor = Constants.FOREGROUND_COLOR;
            Symtext.FontSize        = Constants.TEXT_SYMTEXT_SIZE;

            Symtext.WriteLine(Constants.APP_NAME + " v2.0", HorizontalAlignment.Center);
            Symtext.WriteLine("C 2017 Marian Dolinsky\n", HorizontalAlignment.Center);

            new MenuItem("Go back", null).Write(true);

            Renderer.RenderFrame();

            ConsoleKey pressedKey;

            do
            {
                pressedKey = Helpers.ReadKey().Key;
            } while (pressedKey != ConsoleKey.Escape && pressedKey != ConsoleKey.Enter);
        }
    }
}
