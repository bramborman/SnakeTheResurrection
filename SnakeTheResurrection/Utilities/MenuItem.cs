using System;

namespace SnakeTheResurrection.Utilities
{
    public sealed class MenuItem
    {
        private string _text;
        private Action _action;

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    ExceptionHelper.ValidateStringNotNullOrWhiteSpace(value, nameof(Text));
                    _text = value;
                }
            }
        }
        public Action Action
        {
            get { return _action; }
            set
            {
                if (_action != value)
                {
                    ExceptionHelper.ValidateObjectNotNull(value, nameof(Action));
                    _action = value;
                }
            }
        }

        public MenuItem(string text, Action action)
        {
            Text    = text;
            Action  = action;
        }

        public void Write(bool isSelected)
        {
            Symtext.ForegroundColor = Constants.FOREGROUND_COLOR;
            Symtext.BackgroundColor = isSelected ? Constants.ACCENT_COLOR_DARK : Constants.BACKGROUND_COLOR;

            Symtext.WriteLine($" {Text} ", HorizontalAlignment.Center);
        }
    }
}
