using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;
using System.Linq;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using AvaloniaApplication2.Models;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private ListBox _listBox;

        public MainWindow()
        {
            InitializeComponent();
            KeyUp += MainWindow_KeyDown;
            DataContext = new MainWindowViewModel();
            
            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");
            _listBox = this.FindControl<ListBox>("listbox");
            
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_firstNameTextBox.Text != null && _lastNameTextBox.Text != null &&
                    _firstNameTextBox.Text != "" && _lastNameTextBox.Text != "")
                {
                    AddToListBox_Click(sender, e);
                }
            }
        }

        private void AddToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string firstName = _firstNameTextBox.Text;
            
            if (firstName != null && firstName != "")
            {
                var newProduct = new Product
                {
                    Name = firstName,
                    Price = int.Parse(_lastNameTextBox.Text)
                };
                
                (DataContext as MainWindowViewModel)?.products.Add(newProduct);

                
            }
            

            _firstNameTextBox.Text = "";
            _lastNameTextBox.Text = "";
        }
        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = _listBox.SelectedItem as Product;

            if (selectedProduct != null)
            {
                var editWindow = new EditWindow(DataContext, this, selectedProduct);
                editWindow.Show();
                this.Hide();

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
                    viewModel.products.Remove(selectedItem);
                }
            }
        }
        
        
        private void ShowSelectedItems_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selectedItems = _listBox.SelectedItems.OfType<Product>().ToList();

            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                foreach (var selectedItem in selectedItems)
                {
                    var existingItem = viewModel.select.FirstOrDefault(p => p.Name == selectedItem.Name);

                    if (existingItem != null)
                    {
                        existingItem.Count++;
                    }
                    else
                    {
                        var newItem = new Product
                        {
                            Name = selectedItem.Name,
                            Price = selectedItem.Price,
                            Count = 1
                        };
                        viewModel.select.Add(newItem);
                    }
                }

                _listBox.SelectedItems.Clear();

                var selectedItemsWindow = new SelectedItemsWindow(DataContext, this);
                selectedItemsWindow.Show();
                this.Hide();
            }
        }



    }
}
