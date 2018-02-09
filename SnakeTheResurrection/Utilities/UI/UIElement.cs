using NotifyPropertyChangedBase;

namespace SnakeTheResurrection.Utilities.UI
{
    public abstract class UIElement : NotifyPropertyChanged
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
        public Thickness Padding
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
                return BorderThickness.Left + Padding.Left + Width + Padding.Right + BorderThickness.Right;
            }
        }
        public int ActualHeight
        {
            get
            {
                return BorderThickness.Top + Padding.Top + Height + Padding.Bottom + BorderThickness.Bottom;
            }
        }
        
        public UIElement()
        {
            RegisterProperty(nameof(Width), typeof(int), 0);
            RegisterProperty(nameof(Height), typeof(int), 0);
            RegisterProperty(nameof(Margin), typeof(Thickness), new Thickness());
            RegisterProperty(nameof(Padding), typeof(Thickness), new Thickness());
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
            if (!IsVisible)
            {
                return Rectangle.Empty;
            }

            Rectangle parentMeasure = Parent?.MeasureContent() ?? new Rectangle(0, 0, new Size(Window.Width, Window.Height));

            if (parentMeasure.Width <= 0 || parentMeasure.Height <= 0)
            {
                return Rectangle.Empty;
            }

            int fullWidth = Margin.Left + ActualWidth + Margin.Right;
            int fullHeight = Margin.Top + ActualHeight + Margin.Bottom;
            int x = Size.InvalidSize;
            int y = Size.InvalidSize;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = parentMeasure.Left;
                    break;
                case HorizontalAlignment.Center:
                    x = (parentMeasure.Width - fullWidth) / 2;
                    break;
                case HorizontalAlignment.Right:
                    x = parentMeasure.Right - fullWidth;
                    break;
                case HorizontalAlignment.Stretch:
                    x = parentMeasure.Left + Margin.Left;
                    fullWidth = parentMeasure.Right - parentMeasure.Left - Margin.Left - Margin.Right;
                    break;
            }

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = parentMeasure.Top;
                    break;
                case VerticalAlignment.Center:
                    y = (parentMeasure.Height - fullHeight) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    y = parentMeasure.Bottom - fullHeight;
                    break;
                case VerticalAlignment.Stretch:
                    y = parentMeasure.Top + Margin.Top;
                    fullHeight = parentMeasure.Bottom - parentMeasure.Top - Margin.Top - Margin.Bottom;
                    break;
            }

            return new Rectangle(x, y, new Size(fullWidth, fullHeight));
        }

        protected virtual Rectangle MeasureContent()
        {
            Rectangle measure = Measure();

            if (measure == Rectangle.Empty)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                measure.Left + BorderThickness.Left + Padding.Left,
                measure.Top + BorderThickness.Top + Padding.Top,
                measure.Right - BorderThickness.Right - Padding.Bottom,
                measure.Bottom - BorderThickness.Bottom - Padding.Bottom);
        }
    }
}
