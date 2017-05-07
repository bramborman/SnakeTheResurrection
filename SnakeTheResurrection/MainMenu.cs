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
                new MenuItem("Singleplayer",    new Game().Start                    ),
             // new MenuItem("Multiplayer",     null                                ),
             // new MenuItem("Options",         null                                ),
                new MenuItem("Profiles",        ProfileManager.ShowProfileSelection ),
                new MenuItem("About",           null                                ),
                new MenuItem("Quit game",       () => Program.Exit()                )
            },
            RelativeY = 57
        };

        public static void Show()
        {
            ProfileManager.ShowProfileSelection();

            while (true)
            {
                Renderer.CleanBuffer();
                
                Symtext.ForegroundColor = ConsoleColor.Green;
                Symtext.FontSize        = 15;
                Symtext.WriteLine("Snake", HorizontalAlignment.Center, VerticalAlignment.Center, 0, 7);
                
                menuItems.InvokeResult();
                Helpers.ClearInputBuffer();
            }
        }
    }
}
