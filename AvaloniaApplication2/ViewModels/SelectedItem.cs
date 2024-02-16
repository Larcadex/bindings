using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace AvaloniaApplication2.ViewModels
{
    public class SelectedItemModel
    {
        private ObservableCollection<string> _selectedItems;
        public ObservableCollection<string> SelectedItems
        {
            get => _selectedItems;
            set
            {
                if (_selectedItems != null)
                    _selectedItems.CollectionChanged -= CollectionChanged;

                _selectedItems = value;

                if (_selectedItems != null)
                    _selectedItems.CollectionChanged += CollectionChanged;

                UpdateSum();
            }
        }

        private ObservableCollection<int> _selectedItemsPrice;
        public ObservableCollection<int> SelectedItemsPrice
        {
            get => _selectedItemsPrice;
            set
            {
                if (_selectedItemsPrice != null)
                    _selectedItemsPrice.CollectionChanged -= CollectionChanged;

                _selectedItemsPrice = value;

                if (_selectedItemsPrice != null)
                    _selectedItemsPrice.CollectionChanged += CollectionChanged;

                UpdateSum();
            }
        }

        private string _summa;
        public string Summa
        {
            get => _summa;
            set
            {
                if (_summa != value)
                {
                    _summa = value;
                }
            }
        }

        public SelectedItemModel()
        {
            _selectedItems = new ObservableCollection<string>();
            _selectedItems.CollectionChanged += CollectionChanged;

            _selectedItemsPrice = new ObservableCollection<int>();
            _selectedItemsPrice.CollectionChanged += CollectionChanged;

            UpdateSum();
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSum();
        }

        private void UpdateSum()
        {
            int sum = SelectedItemsPrice.Sum();
            Summa = sum.ToString();
        }
    }
}
