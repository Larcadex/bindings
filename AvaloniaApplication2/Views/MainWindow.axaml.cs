using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.ViewModels;
using System.Linq;
using Avalonia.LogicalTree;

namespace AvaloniaApplication2.Views
{
    public partial class MainWindow : Window
    {
        private TextBox _firstNameTextBox;
        private TextBox _lastNameTextBox;
        private ListBox _listBox;
        private ListBox _listBox1;
        private SelectedItemModel _selectedItemModel;

        public MainWindow()
        {
            InitializeComponent();
            KeyUp += MainWindow_KeyDown;

            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");
            _listBox = this.FindControl<ListBox>("listbox");
            _listBox1 = this.FindControl<ListBox>("listbox1");

            _selectedItemModel = new SelectedItemModel();

            _listBox.SelectionChanged += ListBox_SelectionChanged;
            _listBox1.SelectionChanged += ListBox_SelectionChanged;
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
            int lastName;

            if (int.TryParse(_lastNameTextBox.Text, out lastName))
            {
                if (firstName != null && firstName != "")
                {
                    (DataContext as MainWindowViewModel)?.ListItems.Add(firstName);
                    (DataContext as MainWindowViewModel)?.ListItemsPrise.Add(lastName);
                }
            }

            _firstNameTextBox.Text = "";
            _lastNameTextBox.Text = "";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == _listBox)
            {
                UpdateListBoxSelection(_listBox, _listBox1);
            }
            else if (sender == _listBox1)
            {
                UpdateListBoxSelection(_listBox1, _listBox);
            }
        }

        private void UpdateListBoxSelection(ListBox sourceListBox, ListBox targetListBox)
        {
            targetListBox.SelectionChanged -= ListBox_SelectionChanged;

            try
            {
                targetListBox.SelectedItems.Clear();

                foreach (var selectedItem in sourceListBox.SelectedItems)
                {
                    var index = sourceListBox.Items.IndexOf(selectedItem);
                    if (index >= 0 && index < targetListBox.Items.Count)
                    {
                        targetListBox.SelectedItems.Add(targetListBox.Items[index]);
                    }
                }
            }
            finally
            {
                targetListBox.SelectionChanged += ListBox_SelectionChanged;
            }
        }
        
        private void ShowSelectedItems_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selectedItems = _listBox.SelectedItems.OfType<string>().ToList();
            _selectedItemModel.SelectedItems = new ObservableCollection<string>(selectedItems);

            var selectedItems1 = _listBox1.SelectedItems.OfType<int>().ToList();
            _selectedItemModel.SelectedItemsPrice = new ObservableCollection<int>(selectedItems1);
            
            var selectedItemsWindow = new SelectedItemsWindow(_selectedItemModel);
            selectedItemsWindow.Show();
        }

    }
}
