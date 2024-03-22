﻿using System;
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
    public partial class SecondWindow : Window
    {
        private ListBox _listBox;
        
        public SecondWindow(object mainWindowViewModel)
        {
            InitializeComponent();
           
            DataContext = mainWindowViewModel;
            searchBox = this.FindControl<TextBox>("searchBox");

            
            _listBox = this.FindControl<ListBox>("listbox");
            
            this.KeyDown += HandleKeyDown;
            
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


        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = _listBox.SelectedItems.OfType<Product>().ToList();

            if (selectedProducts.Count == 1 && selectedProducts[0].Count <= 1)
            {
                var selectedProduct = selectedProducts[0];
                var editWindow = new EditWindow(DataContext, selectedProduct);
                editWindow.Show();
                Hide();
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
                            Count = 1,
                            ImageSource = selectedItem.ImageSource
                        };
                        viewModel.select.Add(newItem);
                    }
                }

                _listBox.SelectedItems.Clear();

                var selectedItemsWindow = new SelectedItemsWindow(DataContext);
                selectedItemsWindow.Show();
                this.Hide();
                
                
            }
            
        }
        
        private void Add_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
                
            var newmain = new MainWindow(DataContext);
            newmain.Show();
            this.Close();
        }
        
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
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





    }
}
