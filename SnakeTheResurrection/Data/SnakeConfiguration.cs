using NotifyPropertyChangedBase;
using System;

namespace SnakeTheResurrection.Data
{
    public sealed class SnakeConfiguration : NotifyPropertyChanged
    {
        public char? Body
        {
            get { return (char?)GetValue(); }
            set { SetValue(value); }
        }
        public ConsoleColor? Color
        {
            get { return (ConsoleColor?)GetValue(); }
            set { SetValue(value); }
        }
        
        public SnakeConfiguration()
        {
            RegisterProperty(nameof(Body), typeof(char?), null);
            RegisterProperty(nameof(Color), typeof(ConsoleColor?), null);
        }
    }
}
