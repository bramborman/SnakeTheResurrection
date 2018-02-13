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
            Text = text;
            Action = action;
        }
    }
}
