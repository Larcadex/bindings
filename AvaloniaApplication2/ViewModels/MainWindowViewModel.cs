using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Product> products { get; set; } = new();

    public ObservableCollection<Product> select { get; set; } = new();
    
}

