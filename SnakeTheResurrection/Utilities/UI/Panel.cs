using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SnakeTheResurrection.Utilities.UI
{
    public abstract class Panel : ContentElement
    {
        public ObservableCollection<UIElement> Items { get; } = new ObservableCollection<UIElement>();
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(); }
            set { SetValue(value); }
        }

        public Panel()
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
    }
}
