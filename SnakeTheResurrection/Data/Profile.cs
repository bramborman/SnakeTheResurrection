using NotifyPropertyChangedBase;

namespace SnakeTheResurrection.Data
{
    public sealed class Profile : NotifyPropertyChanged
    {
        public string Name
        {
            get { return (string)GetValue(); }
            set { SetValue(value); }
        }
        public SnakeConfiguration SnakeConfiguration
        {
            get { return (SnakeConfiguration)GetValue(); }
            set { SetValue(value); }
        }
        
        public Profile()
        {
            RegisterProperty(nameof(Name), typeof(string), null);
            RegisterProperty(nameof(SnakeConfiguration), typeof(SnakeConfiguration), null);
        }
    }
}
