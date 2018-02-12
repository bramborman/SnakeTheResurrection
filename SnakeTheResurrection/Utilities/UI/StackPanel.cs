using System;
using System.Collections.Generic;

namespace SnakeTheResurrection.Utilities.UI
{
    public class StackPanel : ContentElement
    {
        public List<UIElement> Items { get; } = new List<UIElement>();
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(); }
            set { SetValue(value); }
        }
        
        public StackPanel()
        {
            RegisterProperty(nameof(Orientation), typeof(Orientation), Orientation.Vertical);
        }

        public override Rectangle Render(in Rectangle bounds)
        {
            Rectangle area = base.Render(in bounds);

            if (area == Rectangle.Empty || Items.Count == 0)
            {
                return area;
            }

            Rectangle contentArea = GetContentArea(area);

            foreach (UIElement item in Items)
            {
                Rectangle takenArea = item.Render(in contentArea);

                if (!UpdateContentArea(ref contentArea, in takenArea))
                {
                    break;
                }
            }

            return area;
        }

        private bool UpdateContentArea(ref Rectangle contentArea, in Rectangle takenArea)
        {
            if (Orientation == Orientation.Horizontal)
            {
                contentArea = new Rectangle(
                    Math.Max(takenArea.Right, contentArea.Left),
                    contentArea.Top,
                    contentArea.Right,
                    contentArea.Bottom
                    );

                return contentArea.Left < contentArea.Right;
            }
            else
            {
                contentArea = new Rectangle(
                    contentArea.Left,
                    Math.Max(takenArea.Bottom, contentArea.Top),
                    contentArea.Right,
                    contentArea.Bottom
                    );

                return contentArea.Top < contentArea.Bottom;
            }
        }
    }
}
