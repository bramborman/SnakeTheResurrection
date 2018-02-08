using System;

namespace SnakeTheResurrection.Utilities
{
    public class MenuItem
    {
        public string Text { get; set; }
        public Action Action { get; set; }

        public MenuItem(string text) : this(text, null)
        {

        }

        public MenuItem(string text, Action action)
        {
            Text    = text;
            Action  = action;
        }
    }

    public sealed class MenuSwitchItem : MenuItem
    {
        private readonly Func<bool> isOnGetter;
        private readonly Action<bool> isOnSetter;

        public bool IsOn
        {
            get { return isOnGetter(); }
            set { isOnSetter(value); }
        }

        public MenuSwitchItem(string text) : this(text, null, null)
        {

        }

        public MenuSwitchItem(string text, Func<bool> isOnGetter, Action<bool> isOnSetter) : base(text, null)
        {
            if (isOnSetter != null)
            {
                ExceptionHelper.ValidateObjectNotNull(isOnGetter, nameof(isOnGetter));
            }

            bool _isOn = false;

            this.isOnGetter = isOnGetter ?? (() => _isOn);
            this.isOnSetter = isOnSetter ?? (value => _isOn = value);
        }
    }
}
