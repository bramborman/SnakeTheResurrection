namespace SnakeTheResurrection.Utilities.UI
{
    public abstract class ContentElement : UIElement
    {
        public Thickness Padding
        {
            get { return (Thickness)GetValue(); }
            set { SetValue(value); }
        }
        public override int ActualWidth
        {
            get
            {
                int baseActualWidth = base.ActualWidth;
                return baseActualWidth == Size.StretchSize ? Size.StretchSize : Padding.Left + baseActualWidth + Padding.Right;
            }
        }
        public override int ActualHeight
        {
            get
            {
                int baseActualHeight = base.ActualHeight;
                return baseActualHeight == Size.StretchSize ? Size.StretchSize : Padding.Top + baseActualHeight + Padding.Bottom;
            }
        }

        public ContentElement()
        {
            RegisterProperty(nameof(Padding), typeof(Thickness), new Thickness());
        }

        protected virtual Rectangle GetContentArea(in Rectangle area)
        {
            return new Rectangle(
                area.Left + BorderThickness.Left + Padding.Left,
                area.Top + BorderThickness.Top + Padding.Top,
                area.Right - BorderThickness.Right - Padding.Right,
                area.Bottom - BorderThickness.Bottom - Padding.Bottom
                );
        }
    }
}
