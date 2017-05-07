using System;
using System.Collections.Generic;

namespace SnakeTheResurrection.Utilities
{
    public sealed class ListMenu
    {
        private List<MenuItem> _items;
        private int _selectedIndex;
        private int _relativeY;

        public List<MenuItem> Items
        {
            get { return _items; }
            set
            {
                if (!ReferenceEquals(_items, value))
                {
                    ExceptionHelper.ValidateObjectNotNull(value, nameof(Items));
                    _items = value;
                }
            }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    ExceptionHelper.ValidateNumberInRange(value, 0, Items.Count - 1, nameof(SelectedIndex));
                    _selectedIndex = value;
                }
            }
        }
        public MenuItem SelectedItem
        {
            get
            {
                return Items[SelectedIndex];
            }
        }
        public int RelativeY
        {
            get { return _relativeY; }
            set
            {
                if (_relativeY != value)
                {
                    ExceptionHelper.ValidateNumberInWindowVerticalRange(value, nameof(RelativeY));
                    _relativeY = value;
                }
            }
        }

        public ListMenu()
        {
            Items = new List<MenuItem>();
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

            while (true)
            {
                lock (Symtext.SyncRoot)
                {
                    Symtext.FontSize    = 2;
                    Symtext.CursorTop   = RelativeY + (Console.WindowHeight - (Items.Count * Symtext.CharHeight)) / 2;

                    for (int i = 0; i < Items.Count; i++)
                    {
                        Symtext.ForegroundColor = Constants.FOREGROUND_COLOR;
                        Symtext.BackgroundColor = i == SelectedIndex ? ConsoleColor.DarkGreen : Constants.BACKGROUND_COLOR;

                        Symtext.WriteLine($" {Items[i].Text} ", HorizontalAlignment.Center);
                    }
                }

                Renderer.RenderFrame();

                switch (Helpers.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (SelectedIndex != 0)
                        {
                            SelectedIndex--;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (SelectedIndex != Items.Count - 1)
                        {
                            SelectedIndex++;
                        }

                        break;

                    case ConsoleKey.Enter:
                        Renderer.CleanBuffer();
                        return SelectedIndex;
                }
            }
        }
    }
}