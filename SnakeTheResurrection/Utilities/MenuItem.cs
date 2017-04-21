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

        public MenuItem(string text, Action action)
        {
            Text    = text;
            Action  = action;
        }
    }
}
