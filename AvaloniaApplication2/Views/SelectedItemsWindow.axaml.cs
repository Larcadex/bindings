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

        public SelectedItemsWindow(object? MainWindowViewModel)
        {
            InitializeComponent();
            DataContext = MainWindowViewModel;
            this.KeyDown += HandleKeyDown;
            searchBox = this.FindControl<TextBox>("searchBox");


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
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string sortBy = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (sortBy)
            {
                case "Подешевле":
                    listbox.ItemsSource = ((MainWindowViewModel)this.DataContext).products.OrderBy(p => p.Price);
                    break;
                case "Подороже":
                    listbox.ItemsSource = ((MainWindowViewModel)this.DataContext).products.OrderByDescending(p => p.Price);
                    break;
                case "По имени (А-Я)":
                    listbox.ItemsSource = ((MainWindowViewModel)this.DataContext).products.OrderBy(p => p.Name);
                    break;
                case "По имени (Я-А)":
                    listbox.ItemsSource = ((MainWindowViewModel)this.DataContext).products.OrderByDescending(p => p.Name);
                    break;
                
            }
        }
        
        
        private void SearchBox_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
        {
            string searchText = searchBox.Text.ToLower(); 
    
            string[] searchParts = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
            string nameSearchText = searchParts.Length > 0 ? searchParts[0] : "";
            string priceSearchText = searchParts.Length > 1 ? searchParts[1] : "";

            var filteredList = ((MainWindowViewModel)this.DataContext).products
                .Where(product => product.Name.ToLower().Contains(nameSearchText))
                .Where(product => priceSearchText == "" || product.Price.ToString().Contains(priceSearchText));
    
            listbox.ItemsSource = filteredList;
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
        
        private void Decrease_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.DataContext;

            product.Count--;
            if (product.Count < 0)
                product.Count = 0;
            UpdateTotalPrice();
        }

        private void Increase_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.DataContext;

            product.Count++;
            UpdateTotalPrice();
        }
        private void ToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var Second = new SecondWindow(DataContext);
            Second.Show();
            this.Close();

        }

        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }
        
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}