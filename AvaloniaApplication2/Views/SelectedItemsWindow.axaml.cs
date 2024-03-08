using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class SelectedItemsWindow : Window
    {
        private readonly TextBlock? _summa;
        private readonly ListBox? _listBox;
        private readonly MainWindow _mainWindow;

        public SelectedItemsWindow(object? MainWindowViewModel, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = MainWindowViewModel;
            _mainWindow = mainWindow;

            _summa = this.FindControl<TextBlock>("summa");
            _listBox = this.FindControl<ListBox>("listbox");

            UpdateTotalPrice();
            SubscribeToselectChanges();
        }

        private void SubscribeToselectChanges()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.select.CollectionChanged += (sender, args) => { UpdateTotalPrice(); };
            }
        }

        private void UpdateTotalPrice()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                int totalPrice = viewModel.select.Sum(product => product.Price * product.Count);
                _summa.Text = $"{totalPrice}";
            }
        }
        
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = (TextBox)sender;
                var product = (Product)textBox.DataContext;

                if (int.TryParse(textBox.Text, out int count))
                {
                    product.Count = count;
                    UpdateTotalPrice();
                }
            }
        }

        
        private void OnClosed(object? sender, EventArgs e)
        {
            _mainWindow.Show();
        }
        
        private void Remove_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selectedItems = _listBox.SelectedItems.OfType<Product>().ToList();
            

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                foreach (var selectedItem in selectedItems)
                {
                    viewModel.select.Remove(selectedItem);
                }
            }
        }

        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Closed += OnClosed;

        }
    }
}