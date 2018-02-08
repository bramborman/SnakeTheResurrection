using NotifyPropertyChangedBase;

namespace SnakeTheResurrection.Utilities.UI
{
    public class UIElement : NotifyPropertyChanged
    {
        public int Width
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        public int Height
        {
            get { return (int)GetValue(); }
            set { SetValue(value); }
        }
        public Thickness Margin
        {
            get { return (Thickness)GetValue(); }
            set { SetValue(value); }
        }
        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(); }
            set { SetValue(value); }
        }
        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(); }
            set { SetValue(value); }
        }
        public bool IsEnabled
        {
            get { return (bool)GetValue(); }
            set { SetValue(value); }
        }
        public bool IsVisible
        {
            get { return (bool)GetValue(); }
            set { SetValue(value); }
        }
        public Color BackgroundColor
        {
            get { return (Color)GetValue(); }
            set { SetValue(value); }
        }
        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(); }
            set { SetValue(value); }
        }
        public Color BorderColor
        {
            get { return (Color)GetValue(); }
            set { SetValue(value); }
        }
        public UIElement Parent
        {
            get { return (UIElement)GetValue(); }
            set { SetValue(value); }
        }
        public int ActualWidth
        {
            get
            {
                return Margin.Left + BorderThickness.Left + Width + BorderThickness.Right + Margin.Right;
            }
        }
        public int ActualHeight
        {
            get
            {
                return Margin.Top + BorderThickness.Top + Height + BorderThickness.Bottom + Margin.Bottom;
            }
        }
        
        public UIElement()
        {
            RegisterProperty(nameof(Width), typeof(int), 0);
            RegisterProperty(nameof(Height), typeof(int), 0);
            RegisterProperty(nameof(Margin), typeof(Thickness), new Thickness());
            RegisterProperty(nameof(HorizontalAlignment), typeof(HorizontalAlignment), HorizontalAlignment.Left);
            RegisterProperty(nameof(VerticalAlignment), typeof(VerticalAlignment), VerticalAlignment.Top);
            RegisterProperty(nameof(IsEnabled), typeof(bool), true);
            RegisterProperty(nameof(IsVisible), typeof(bool), true);
            RegisterProperty(nameof(BackgroundColor), typeof(Color), Colors.Transparent);
            RegisterProperty(nameof(BorderThickness), typeof(Thickness), new Thickness());
            RegisterProperty(nameof(BorderColor), typeof(Color), Colors.Transparent);
            RegisterProperty(nameof(Parent), typeof(UIElement), null);
        }
        
        public virtual bool Render()
        {
            if (!IsVisible)
            {
                return false;
            }

            Rectangle measure = Measure();

            if (measure.Width <= 0 || measure.Height <= 0)
            {
                return false;
            }

            if (BorderColor != Colors.Transparent)
            {
                Renderer.AddToBuffer((short)BorderColor, measure.X, measure.Y, measure.Width, measure.Height);
            }

            if (BackgroundColor != Colors.Transparent)
            {
                Renderer.AddToBuffer(
                    (short)BackgroundColor,
                    measure.X + BorderThickness.Left,
                    measure.Y + BorderThickness.Top,
                    measure.Width - BorderThickness.Left - BorderThickness.Right,
                    measure.Height - BorderThickness.Top - BorderThickness.Bottom
                    );
            }

            return true;
        }

        protected virtual Rectangle Measure()
        {
            Rectangle parentMeasure = Parent?.Measure() ?? new Rectangle(0, 0, new Size(Window.Width, Window.Height));
            int actualWidth = ActualWidth;
            int actualHeight = ActualHeight;
            int x = -1;
            int y = -1;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = parentMeasure.Left;
                    break;
                case HorizontalAlignment.Center:
                    x = (parentMeasure.Width - actualWidth) / 2;
                    break;
                case HorizontalAlignment.Right:
                    x = parentMeasure.Right - actualWidth;
                    break;
            }

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = parentMeasure.Top;
                    break;
                case VerticalAlignment.Center:
                    y = (parentMeasure.Height - actualHeight) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    y = parentMeasure.Bottom - actualHeight;
                    break;
            }

            return new Rectangle(x, y, new Size(actualWidth, actualHeight));
        }
    }
}
