using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models;

namespace AvaloniaApplication2.Views;

public partial class EditWindow : Window
{
    private Product _selectedProduct;
    private readonly TextBox? _firstNameTextBox;
    private readonly TextBox? _lastNameTextBox;
    private readonly MainWindow _mainWindow;

    public EditWindow(object mainWindowViewModel, MainWindow mainWindow, Product selectedProduct)
    {
        InitializeComponent();
        _firstNameTextBox = this.FindControl<TextBox>("first_name");
        _lastNameTextBox = this.FindControl<TextBox>("last_name");
        DataContext = mainWindowViewModel;
        _selectedProduct = selectedProduct;
        Closed += OnClosed;

    }
    private void OnClosed(object? sender, EventArgs e)
    {
        _mainWindow.Show();
    }
    
    private void EditProduct_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _selectedProduct.Name = _firstNameTextBox.Text;
        _selectedProduct.Price = int.Parse(_lastNameTextBox.Text);
        

        Close();
    }
}