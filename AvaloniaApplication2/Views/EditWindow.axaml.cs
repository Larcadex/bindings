using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using AvaloniaApplication2.Models;

namespace AvaloniaApplication2.Views;

public partial class EditWindow : Window
{
    private Product _selectedProduct;
    private readonly TextBox? _NameTextBox;
    private readonly TextBox? __priceTextBoxBox;
    private Bitmap _image1;

    private readonly object _mainWindowViewModel;

    public EditWindow(object mainWindowViewModel, Product selectedProduct)
    {
        InitializeComponent();
        _NameTextBox = this.FindControl<TextBox>("name");
        __priceTextBoxBox = this.FindControl<TextBox>("price");
        _NameTextBox.Text = selectedProduct.Name; 
        __priceTextBoxBox.Text = selectedProduct.Price.ToString();
        _image1 = selectedProduct.ImageSource;
        DataContext = mainWindowViewModel;
        _mainWindowViewModel = mainWindowViewModel;
        _selectedProduct = selectedProduct;
    }
    

    private void ToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var Second = new SecondWindow(DataContext, App.GlobalVariables.isAdmin);
        Second.Show();
        this.Close();

    }
    
    private void EditProduct_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _selectedProduct.Name = _NameTextBox.Text;
        _selectedProduct.Price = int.Parse(__priceTextBoxBox.Text);
        _selectedProduct.ImageSource = _image1;
        var Second = new SecondWindow(DataContext, App.GlobalVariables.isAdmin);
        Second.Show();
        this.KeyDown += HandleKeyDown;

        
        Close();
    }
    
    private async void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filters.Add(new FileDialogFilter() { Name = "Images", Extensions = { "jpg", "png", "jpeg" } });
        openFileDialog.AllowMultiple = false;
        var selectedFiles = await openFileDialog.ShowAsync(this);

        if (selectedFiles != null)
        {
                
            var imagePath = selectedFiles[0];
            var bitmap = new Bitmap(imagePath);
            _image1 = bitmap;

        }
    }
    
    private void HandleKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Close();
        }
    }
}