using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class SelectedItemsWindow : Window
    {
        private readonly TextBlock? _summa;
        private readonly ListBox? _listBox;
        private readonly TextBlock? _page;

        private int startIndex = 0;
        private int currentPage = 1;
        private decimal allPage;
      

        

        public SelectedItemsWindow(object? MainWindowViewModel)
        {
            InitializeComponent();
            DataContext = MainWindowViewModel;
            this.KeyDown += HandleKeyDown;
            searchBox = this.FindControl<TextBox>("searchBox");
            comboBox = this.FindControl<ComboBox>("comboBox");  


            _summa = this.FindControl<TextBlock>("summa");
            _listBox = this.FindControl<ListBox>("listbox");
            _page = this.FindControl<TextBlock>("page");

            allPage = ((MainWindowViewModel)this.DataContext).select.Count / 2;
            
            if (((MainWindowViewModel)this.DataContext).select.Count % 2 != 0 || ((MainWindowViewModel)this.DataContext).select.Count == 0 )
            {
                allPage += 1;

            }
            
            _page.Text = $"{currentPage}/{allPage}";

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
            string sortBy = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (sortBy)
            {
                case "Подешевле":
                    _listBox.ItemsSource = ((MainWindowViewModel)this.DataContext).select.OrderBy(p => p.Price);
                    break;
                case "Подороже":
                    _listBox.ItemsSource = ((MainWindowViewModel)this.DataContext).select.OrderByDescending(p => p.Price);
                    break;
                case "По имени (А-Я)":
                    _listBox.ItemsSource = ((MainWindowViewModel)this.DataContext).select.OrderBy(p => p.Name);
                    break;
                case "По имени (Я-А)":
                    _listBox.ItemsSource = ((MainWindowViewModel)this.DataContext).select.OrderByDescending(p => p.Name);
                    break;        
            }
        }

        
        
        private void SearchBox_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
        {
            string searchText = searchBox.Text.ToLower(); 
    
            string[] searchParts = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
            string nameSearchText = searchParts.Length > 0 ? searchParts[0] : "";
            string priceSearchText = searchParts.Length > 1 ? searchParts[1] : "";

            var filteredList = ((MainWindowViewModel)this.DataContext).select
                .Where(product => product.Name.ToLower().Contains(nameSearchText))
                .Where(product => priceSearchText == "" || product.Price.ToString().Contains(priceSearchText));
    
            _listBox.ItemsSource = filteredList;
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
            var Second = new SecondWindow(DataContext, App.GlobalVariables.isAdmin);
            Second.Show();
            this.Close();

        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {      

            if (startIndex - 2 >= 0)
            {
                startIndex -= 2;
                currentPage -= 1;
                _page.Text = $"{currentPage}/{allPage}";

                UpdateListBox();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (startIndex + 2 <((MainWindowViewModel)this.DataContext).select.Count)
            {
                startIndex += 2;
                currentPage += 1;
                _page.Text = $"{currentPage}/{allPage}";

                UpdateListBox();
            }
        }

        private void UpdateListBox()
        {
            _listBox.ItemsSource = ((MainWindowViewModel)this.DataContext).select.Skip(startIndex).Take(2);
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