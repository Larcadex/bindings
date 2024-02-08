using System.Collections.ObjectModel;

namespace AvaloniaApplication2.ViewModels
{
    public class SelectedItemModel
    {
        public ObservableCollection<string> SelectedItems { get; set; }

        public SelectedItemModel()
        {
            SelectedItems = new ObservableCollection<string>();
        }
    }
}