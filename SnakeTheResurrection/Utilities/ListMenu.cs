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
                    Symtext.SetCenteredTextProperties();

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
                        Symtext.ForegroundColor = Constants.FOREGROUND_COLOR;
                        Symtext.BackgroundColor = i == SelectedIndex ? Constants.ACCENT_COLOR_DARK : Constants.BACKGROUND_COLOR;

                        Symtext.WriteLine($" {Items[i].Text} ");
                    }
                }

                Renderer.RenderFrame();

                bool handled = false;

                while (!handled)
                {
                    switch (InputHelper.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (SelectedIndex != 0)
                            {
                                handled = true;
                                SelectedIndex--;

                                if (string.IsNullOrWhiteSpace(SelectedItem.Text))
                                {
                                    SelectedIndex--;
                                }
                            }

                            break;

                        case ConsoleKey.DownArrow:
                            if (SelectedIndex != Items.Count - 1)
                            {
                                handled = true;
                                SelectedIndex++;

                                if (string.IsNullOrWhiteSpace(SelectedItem.Text))
                                {
                                    SelectedIndex++;
                                }
                            }

                            break;

                        case ConsoleKey.LeftArrow:
                            {
                                if (SelectedItem is MenuSwitchItem selectedMenuSwitchItem)
                                {
                                    handled = true;
                                    selectedMenuSwitchItem.IsOn = true;
                                }

                                break;
                            }

                        case ConsoleKey.RightArrow:
                            {
                                if (SelectedItem is MenuSwitchItem selectedMenuSwitchItem)
                                {
                                    handled = true;
                                    selectedMenuSwitchItem.IsOn = false;
                                }

                                break;
                            }

                        case ConsoleKey.Enter:
                            {
                                handled = true;
                                
                                if (SelectedItem is MenuSwitchItem selectedMenuSwitchItem)
                                {
                                    selectedMenuSwitchItem.IsOn = !selectedMenuSwitchItem.IsOn;
                                    break;
                                }
                                else
                                {
                                    Renderer.ClearBuffer();
                                    return SelectedIndex;
                                }
                            }
                    }
                }
            }
        }
    }
}
