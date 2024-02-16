using System.Collections.ObjectModel;
using AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; set; }
    
    public ObservableCollection<int> ListItemsPrise { get; set; }

    public MainWindowViewModel()
    {
        ListItems = new ObservableCollection<string>();
        ListItemsPrise = new ObservableCollection<int>();

    }
    
    
    
}