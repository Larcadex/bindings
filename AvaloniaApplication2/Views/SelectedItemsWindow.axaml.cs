using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class SelectedItemsWindow : Window
    {
        public SelectedItemsWindow(SelectedItemModel selectedItemModel)
        {
            InitializeComponent();
            DataContext = selectedItemModel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}