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

    public partial class Sign_in : Window
    {
        private TextBox _nickname;
        private TextBox _password;



        public Sign_in()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            _nickname = this.FindControl<TextBox>("nickname");
            _password = this.FindControl<TextBox>("password");
            this.KeyDown += HandleKeyDown;
        }
        

        private void Sign_in_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (_nickname.Text == "admin" && _password.Text == "admin")
            {
                App.GlobalVariables.isAdmin = true;
            }
            var Second = new SecondWindow(DataContext, App.GlobalVariables.isAdmin);
            Second.Show();
            this.Close();

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
        

    }
}
