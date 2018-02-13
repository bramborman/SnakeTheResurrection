using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SnakeTheResurrection.Utilities.UI
{
    public abstract class Panel : ContentElement
    {
        public ObservableCollection<UIElement> Items { get; } = new ObservableCollection<UIElement>();

        public Panel()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                return;
            }

            if (e.OldItems != null)
            {
                foreach (UIElement oldItem in e.OldItems)
                {
                    if (ReferenceEquals(this, oldItem.Parent))
                    {
                        SetParent(oldItem, null);
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (UIElement newItem in e.NewItems)
                {
                    SetParent(newItem, this);
                }
            }
        }
    }
}
