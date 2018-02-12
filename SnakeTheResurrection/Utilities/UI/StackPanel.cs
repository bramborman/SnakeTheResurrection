using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SnakeTheResurrection.Utilities.UI
{
    public class StackPanel : ContentElement
    {
        public ObservableCollection<UIElement> Items { get; } = new ObservableCollection<UIElement>();
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(); }
            set { SetValue(value); }
        }
        
        public StackPanel()
        {
            RegisterProperty(nameof(Orientation), typeof(Orientation), Orientation.Vertical);
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                return;
            }

            foreach (UIElement oldItem in e.OldItems)
            {
                if (ReferenceEquals(this, oldItem.Parent))
                {
                    SetParent(oldItem, null);
                }
            }

            foreach (UIElement newItem in e.NewItems)
            {
                SetParent(newItem, this);
            }
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
