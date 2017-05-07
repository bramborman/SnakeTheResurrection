using NotifyPropertyChangedBase;
using System;

namespace SnakeTheResurrection.Data
{
    public sealed class Profile : NotifyPropertyChanged
    {
        public string Name
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public ConsoleColor Color
        {
            get { return (ConsoleColor)GetValue(); }
            set { SetValue(value); }
        }
        
        public Profile()
        {
            RegisterProperty(nameof(Name), typeof(string), null);
            RegisterProperty(nameof(Color), typeof(ConsoleColor), default(ConsoleColor));
        }
    }
}
