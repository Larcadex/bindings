using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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

            _firstNameTextBox = this.FindControl<TextBox>("first_name");
            _lastNameTextBox = this.FindControl<TextBox>("last_name");
            _listBox = this.FindControl<ListBox>("listbox");
        }
        

        private void AddToListBox_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string firstName = _firstNameTextBox.Text;
            string lastName = _lastNameTextBox.Text;

            string fullName = $"{firstName} {lastName}";

            if (firstName != "" && lastName != "")
            {
                (DataContext as MainWindowViewModel)?.ListItems.Add(fullName);
            }
                
            first_name.Text = "";
            last_name.Text = "";


        }
    }
}