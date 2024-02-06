using System.Collections.ObjectModel;
using AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _textBox1;
    private string _textBox2;
    private ObservableCollection<string> _listItems;
    
    public ObservableCollection<string> ListItems { get; set; }


    public MainWindowViewModel()
    {
        ListItems = new ObservableCollection<string>();
    }
    
}