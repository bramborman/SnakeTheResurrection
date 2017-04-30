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
                    ExceptionHelper.ValidateNotNullOrWhiteSpace(value, nameof(Text));
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
                    ExceptionHelper.ValidateNotNull(value, nameof(Action));
                    _action = value;
                }
            }
        }
        public bool IsSelected { get; set; }

        public MenuItem(string text, Action action)
        {
            Text    = text;
            Action  = action;
        }

        public void Write()
        {
            if (IsSelected)
            {
                Symtext.ForegroundColor = ConsoleColor.White;
                Symtext.BackgroundColor = ConsoleColor.DarkGreen;
            }
            else
            {
                Symtext.ForegroundColor = Constants.FOREGROUND_COLOR;
                Symtext.BackgroundColor = Constants.BACKGROUND_COLOR;
            }

            Symtext.WriteLine(Text, HorizontalAlignment.Center);
        }
    }
}
