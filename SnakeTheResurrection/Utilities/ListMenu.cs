using System;
using System.Collections.Generic;

namespace SnakeTheResurrection.Utilities
{
    public sealed class ListMenu
    {
        private List<MenuItem> _items;
        private int _selectedIndex;

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

            int? symtextCursorTop = null;

            while (true)
            {
                lock (Symtext.SyncRoot)
                {
                    Symtext.SetTextProperties();

                    if (symtextCursorTop == null)
                    {
                        symtextCursorTop    = Symtext.CursorTop;
                    }
                    else
                    {
                        Symtext.CursorTop   = symtextCursorTop.Value;
                    }

                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i].Write(i == SelectedIndex);
                    }
                }

                Renderer.RenderFrame();

                bool handled = false;

                while (!handled)
                {
                    switch (Helpers.ReadKey().Key)
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
                            Renderer.CleanBuffer();
                            return SelectedIndex;
                    }
                }
            }
        }
    }
}