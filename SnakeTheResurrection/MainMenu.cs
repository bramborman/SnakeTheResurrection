using SnakeTheResurrection.Utilities;
using System;

namespace SnakeTheResurrection
{
    public static class MainMenu
    {
        private static readonly ListMenu menu = new ListMenu(
            new MenuItem("Singleplayer",    new Game().Start                    ),
            // new MenuItem("Multiplayer",     null                                 ),
            // new MenuItem("Options",         null                                 ),
            new MenuItem("Profile manager", ProfileManager.ShowProfileSelection ),
            new MenuItem("About",           null                                ),
            new MenuItem("Quit game",       () => Program.Exit()                ))
        {
            RelativeY = 4
        };

        public static void Show()
        {
            ProfileManager.ShowProfileSelection();

            while (true)
            {
                Console.Clear();

                Console.CursorTop = Console.WindowHeight / 2 - 7;
                BigTextWriter.Write(Constants.APP_SHORT_NAME, ConsoleColor.Green);
                menu.InvokeResult();
            }
        }
    }
}
