using SnakeTheResurrection.Utilities;
using System;
using System.Collections.Generic;

namespace SnakeTheResurrection
{
    public static class MainMenu
    {
        private static readonly List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem("Singleplayer",    new Game().Start                    ) { IsSelected = true },
         // new MenuItem("Multiplayer",     null                                ),
         // new MenuItem("Options",         null                                ),
            new MenuItem("Profile manager", ProfileManager.ShowProfileSelection ),
            new MenuItem("About",           null                                ),
            new MenuItem("Quit game",       () => Program.Exit()                )
        };

        public static void Show()
        {
            ProfileManager.ShowProfileSelection();

            while (true)
            {
                Program.Renderer.ClearBuffer();

                Symtext.ForegroundColor = ConsoleColor.Green;
                Symtext.FontSize        = 15;
                Symtext.Write("Snake", HorizontalAlignment.Center, VerticalAlignment.Center);

                Symtext.FontSize = 1;
                Program.Renderer.RenderFrame();

                // foreach (MenuItem menuItem in menuItems)
                // {
                //     menuItem.Write();
                // }
            }
        }
    }
}
