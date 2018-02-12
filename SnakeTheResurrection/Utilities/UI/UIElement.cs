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
        public virtual int ActualWidth
        {
            get
            {
                return HorizontalAlignment == HorizontalAlignment.Stretch ? Size.StretchSize : BorderThickness.Left + Width + BorderThickness.Right;
            }
        }
        public virtual int ActualHeight
        {
            get
            {
                return VerticalAlignment == VerticalAlignment.Stretch ? Size.StretchSize : BorderThickness.Top + Height + BorderThickness.Bottom;
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
        
        public virtual Rectangle Render(in Rectangle bounds)
        {
            if (!IsVisible)
            {
                return Rectangle.Empty;
            }

            Rectangle area = GetArea(in bounds);

            if (area.Width <= 0 || area.Height <= 0)
            {
                return Rectangle.Empty;
            }

            if (BorderColor != Colors.Transparent)
            {
                if (BorderThickness.Left > 0)
                {
                    Renderer.Safe.AddToBuffer(BorderColor, area.X, area.Y, BorderThickness.Left, area.Height, in bounds);
                }

                if (BorderThickness.Top > 0)
                {
                    Renderer.Safe.AddToBuffer(BorderColor, area.X, area.Y, area.Width, BorderThickness.Top, in bounds);
                }

                if (BorderThickness.Right > 0)
                {
                    Renderer.Safe.AddToBuffer(BorderColor, area.Right - BorderThickness.Right, area.Y, BorderThickness.Right, area.Height, in bounds);
                }

                if (BorderThickness.Bottom > 0)
                {
                    Renderer.Safe.AddToBuffer(BorderColor, area.X, area.Bottom - BorderThickness.Bottom, area.Width, BorderThickness.Bottom, in bounds);
                }
            }

            if (BackgroundColor != Colors.Transparent)
            {
                if ((HorizontalAlignment == HorizontalAlignment.Stretch || Width > 0)
                    && (VerticalAlignment == VerticalAlignment.Stretch || Height > 0))
                {
                    Renderer.Safe.AddToBuffer(
                        BackgroundColor,
                        area.X + BorderThickness.Left,
                        area.Y + BorderThickness.Top,
                        area.Width - BorderThickness.Left - BorderThickness.Right,
                        area.Height - BorderThickness.Top - BorderThickness.Bottom,
                        in bounds);
                }
            }

            return area;
        }

        protected Rectangle GetArea(in Rectangle bounds)
        {
            int left = Size.InvalidSize;
            int top = Size.InvalidSize;
            int right = Size.InvalidSize;
            int bottom = Size.InvalidSize;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    left = bounds.Left + Margin.Left;
                    right = left + ActualWidth;
                    break;
                case HorizontalAlignment.Center:
                    left = ((bounds.Width - ActualWidth) / 2) + Margin.Left;
                    right = left + ActualWidth;
                    break;
                case HorizontalAlignment.Right:
                    right = bounds.Right - Margin.Right;
                    left = right - ActualWidth;
                    break;
                case HorizontalAlignment.Stretch:
                    left = bounds.Left + Margin.Left;
                    right = bounds.Right - Margin.Right;
                    break;
            }

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    top = bounds.Top + Margin.Top;
                    bottom = top + ActualHeight;
                    break;
                case VerticalAlignment.Center:
                    top = ((bounds.Height - ActualHeight) / 2) + Margin.Top;
                    bottom = top + ActualHeight;
                    break;
                case VerticalAlignment.Bottom:
                    bottom = bounds.Bottom - Margin.Bottom;
                    top = bottom - ActualHeight;
                    break;
                case VerticalAlignment.Stretch:
                    top = bounds.Top + Margin.Top;
                    bottom = bounds.Bottom - Margin.Bottom;
                    break;
            }

            return new Rectangle(left, top, right, bottom);
        }
    }
}
