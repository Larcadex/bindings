using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class SelectedItemsWindow : Window
    {
        private SelectedItemModel _selectedItemModel;

        public SelectedItemsWindow(SelectedItemModel selectedItemModel)
        {
            _selectedItemModel = selectedItemModel;
            DataContext = _selectedItemModel;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}