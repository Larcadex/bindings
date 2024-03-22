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
    private readonly TextBox? _firstNameTextBox;
    private readonly TextBox? _lastNameTextBox;
    private Bitmap _image1;

    private readonly object _mainWindowViewModel;

    public EditWindow(object mainWindowViewModel, Product selectedProduct)
    {
        InitializeComponent();
        _firstNameTextBox = this.FindControl<TextBox>("first_name");
        _lastNameTextBox = this.FindControl<TextBox>("last_name");
        _firstNameTextBox.Text = selectedProduct.Name; 
        _lastNameTextBox.Text = selectedProduct.Price.ToString(); 
        DataContext = mainWindowViewModel;
        _mainWindowViewModel = mainWindowViewModel;
        _selectedProduct = selectedProduct;
    }
    

    private void ToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var Second = new SecondWindow(DataContext);
        Second.Show();
        this.Close();

    }
    
    private void EditProduct_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _selectedProduct.Name = _firstNameTextBox.Text;
        _selectedProduct.Price = int.Parse(_lastNameTextBox.Text);
        _selectedProduct.ImageSource = _image1;
        var Second = new SecondWindow(DataContext);
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