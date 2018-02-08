namespace SnakeTheResurrection.Utilities.UI
{
    public abstract class ContentElement : UIElement
    {
        public Thickness Padding
        {
            get { return (Thickness)GetValue(); }
            set { SetValue(value); }
        }

        public ContentElement()
        {
            RegisterProperty(nameof(Padding), typeof(Thickness), new Thickness());
        }
    }
}
