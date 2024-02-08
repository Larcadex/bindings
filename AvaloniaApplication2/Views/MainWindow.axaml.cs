using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private ListBox _listBox;
        private SelectedItemModel _selectedItemModel;

        public MainWindow()
        {
            InitializeComponent();
            KeyUp += MainWindow_KeyDown;

            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");
            _listBox = this.FindControl<ListBox>("listbox");
            _selectedItemModel = new SelectedItemModel();
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
            string lastName = _lastNameTextBox.Text;
            string fullName = $"{firstName} {lastName}";

            if (firstName != null && lastName != null &&
                firstName != "" && lastName != "")
            {
                (DataContext as MainWindowViewModel)?.ListItems.Add(fullName);
            }

            _firstNameTextBox.Text = "";
            _lastNameTextBox.Text = "";
        }

        private void ShowSelectedItems_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selectedItems = _listBox.SelectedItems.OfType<string>().ToList();
            _selectedItemModel.SelectedItems = new ObservableCollection<string>(selectedItems);

            var selectedItemsWindow = new SelectedItemsWindow(_selectedItemModel);
            selectedItemsWindow.Show();
        }

    }
}
