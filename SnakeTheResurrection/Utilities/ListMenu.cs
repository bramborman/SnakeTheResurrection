using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeTheResurrection.Utilities
{
    public sealed class ListMenu
    {
        public List<MenuItem> Items { get; }
        public int SelectedIndex { get; set; }
        public MenuItem SelectedItem
        {
            get
            {
                return Items[SelectedIndex];
            }
        }
        public int RelativeX { get; set; }
        public int RelativeY { get; set; }

        public ListMenu() : this(null)
        {

        }

        public ListMenu(params MenuItem[] menuItems) : this(menuItems.AsEnumerable())
        {

        }

        public ListMenu(IEnumerable<MenuItem> menuItems)
        {
            Items = menuItems?.ToList() ?? new List<MenuItem>();
        }

        public void InvokeResult()
        {
            GetResult();
            SelectedItem.Action();
        }
        
        public int GetResult()
        {
            if (Items.Count < 1)
            {
                throw new InvalidOperationException("Cannot draw menu with no items.");
            }

            bool handled;
            int horizontalPosition  = (Console.WindowWidth - Items.OrderByDescending(i => i.Text.Length).First().Text.Length) / 2;
            int verticalPosition    = (Console.WindowHeight - Items.Count) / 2;

            horizontalPosition  += RelativeX;
            verticalPosition    += RelativeY;

            Console.CursorTop = verticalPosition;

            foreach (MenuItem menuItem in Items)
            {
                Console.CursorLeft = horizontalPosition;
                Console.WriteLine(menuItem.Text);
            }

            while (true)
            {
                handled = false;

                Console.SetCursorPosition(horizontalPosition - 2, verticalPosition + SelectedIndex);
                Console.Write(">");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (SelectedIndex != 0)
                        {
                            handled = true;
                            SelectedIndex--;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (SelectedIndex != Items.Count - 1)
                        {
                            handled = true;
                            SelectedIndex++;
                        }

                        break;

                    case ConsoleKey.Enter:
                        handled = true;

                        Console.Clear();
                        return SelectedIndex;
                }

                if (handled)
                {
                    // Remove the '>' char
                    Console.Write("\b ");
                }
            }
        }
    }
}
