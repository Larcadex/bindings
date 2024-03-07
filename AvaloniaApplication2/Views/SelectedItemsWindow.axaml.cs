using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class SelectedItemsWindow : Window
    {
        private readonly TextBlock? _summa;

        public SelectedItemsWindow(object? dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _summa = this.FindControl<TextBlock>("summa");
            
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
                int totalPrice = viewModel.select.Sum(product => product.Price);
                _summa.Text = $"{totalPrice}";
            }
        }
        
        private void OnClosed(object? sender, EventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.select.Clear();
            }
        }
        

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Closed += OnClosed;

        }
    }
}