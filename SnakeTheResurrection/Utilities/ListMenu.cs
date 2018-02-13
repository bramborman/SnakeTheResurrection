using System;

namespace SnakeTheResurrection.Utilities
{
    public sealed class ListMenu
    {
        private MenuItem[] _items = new MenuItem[0];
        private int _selectedIndex;

        public MenuItem[] Items
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
                    ExceptionHelper.ValidateNumberInRange(value, 0, Items.Length - 1, nameof(SelectedIndex));
                    _selectedIndex = value;
                }
            }
        }

        public void InvokeResult()
        {
            Items[GetResult()].Action?.Invoke();
        }
        
        public int GetResult()
        {
            if (Items.Length < 1)
            {
                throw new InvalidOperationException("Cannot draw menu with no items.");
            }

            int? symtextCursorTop = null;

            while (true)
            {
                lock (Symtext.syncRoot)
                {
                    Symtext.SetCenteredTextProperties();

                    if (symtextCursorTop == null)
                    {
                        symtextCursorTop = Symtext.CursorTop;
                    }
                    else
                    {
                        Symtext.CursorTop = symtextCursorTop.Value;
                    }

                    for (int i = 0; i < Items.Length; i++)
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
                    ConsoleKey key = InputHelper.ReadKey().Key;
                    InputHelper.ClearInputBuffer();

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (SelectedIndex != 0)
                            {
                                handled = true;
                                SelectedIndex--;

                                while (string.IsNullOrWhiteSpace(Items[SelectedIndex].Text))
                                {
                                    SelectedIndex--;
                                }
                            }

                            break;
                        case ConsoleKey.DownArrow:
                            if (SelectedIndex != Items.Length - 1)
                            {
                                handled = true;
                                SelectedIndex++;

                                while (string.IsNullOrWhiteSpace(Items[SelectedIndex].Text))
                                {
                                    SelectedIndex++;
                                }
                            }

                            break;
                        case ConsoleKey.Enter:
                            handled = true;
                            Renderer.Clear();
                            return SelectedIndex;
                    }
                }
            }
        }
    }
}
