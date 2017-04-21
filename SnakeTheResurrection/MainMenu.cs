using SnakeTheResurrection.Utilities;
using System;

namespace SnakeTheResurrection
{
    public static class MainMenu
    {
        private static readonly ListMenu menu = new ListMenu(
            new MenuItem("Singleplayer",   null                 ),
            new MenuItem("Multiplayer",    null                 ),
            new MenuItem("Options",        null                 ),
            new MenuItem("Quit game",      () => Program.Exit() ));

        public static void Show()
        {
            Console.Clear();
            menu.InvokeResult();
        }
    }
}
