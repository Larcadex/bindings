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
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private Bitmap _image;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");
            this.KeyDown += HandleKeyDown;
        }

        public MainWindow(object mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;

            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");

        }


        private void AddToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string firstName = _firstNameTextBox.Text;

            if (firstName != null && firstName != "")
            {
                var newProduct = new Product
                {
                    Name = firstName,
                    Price = int.Parse(_lastNameTextBox.Text),
                    ImageSource = _image

                };

                (DataContext as MainWindowViewModel)?.products.Add(newProduct);


            }


            _firstNameTextBox.Text = "";
            _lastNameTextBox.Text = "";

        }

        private void ToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var Second = new SecondWindow(DataContext);
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
