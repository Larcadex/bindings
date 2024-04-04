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
using Avalonia.Media.Imaging;
using AvaloniaApplication2.Models;
using Avalonia.Media.Imaging;
using System.IO;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
        private TextBox _NameTextBox;
        private TextBox __priceTextBoxBox;
        private Bitmap _image;
        

        public MainWindow(object mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;

            _NameTextBox = this.FindControl<TextBox>("name");
            __priceTextBoxBox = this.FindControl<TextBox>("price");

        }


        private void AddToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string firstName = _NameTextBox.Text;

            if (firstName != null && firstName != "")
            {
                var newProduct = new Product
                {
                    Name = firstName,
                    Price = int.Parse(__priceTextBoxBox.Text),
                    ImageSource = _image

                };

                (DataContext as MainWindowViewModel)?.products.Add(newProduct);


            }


            _NameTextBox.Text = "";
            __priceTextBoxBox.Text = "";

        }

        private void ToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var Second = new SecondWindow(DataContext, App.GlobalVariables.isAdmin);
            Second.Show();
            this.Close();

        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
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
                _image = bitmap;

            }
        }

    }
}
