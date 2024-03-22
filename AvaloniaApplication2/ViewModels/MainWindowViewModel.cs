using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Product> products { get; set; } = new()
    {
        new Product() {Name = "Лисица", Price = 707, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\30-1.jpg")},
        new Product() {Name = "Суслик", Price = 276, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\31.jpg")},
        new Product() {Name = "Мухоловка", Price = 437, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\1920x1080_1558062_[www.ArtFile.ru].jpg")},
        new Product() {Name = "Савка", Price = 131, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\1692885017_pushinka-top-p-kartinki-na-zastavku-volki-pinterest-8.jpg")},
        new Product() {Name = "Конусохвост", Price = 265, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\d80cf0ad99d12c94be2cbee4ca4658ef.jpg")},
        new Product() {Name = "Каменушка", Price = 456, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\kartinki-na-avu-dlya-parnei-krutie.jpg")},
        new Product() {Name = "Лисица", Price = 265, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\d80cf0ad99d12c94be2cbee4ca4658ef.jpg")},
        new Product() {Name = "Лисица", Price = 456, ImageSource = new Bitmap("C:\\Users\\aswag\\OneDrive\\Desktop\\крутые аватарки вк 2011\\kartinki-na-avu-dlya-parnei-krutie.jpg")},
        
    };

    public ObservableCollection<Product> select { get; set; } = new();

    public MainWindowViewModel()
    {
        products.CollectionChanged += Products_CollectionChanged;
    }

    private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
        {
            foreach (Product removedProduct in e.OldItems)
            {
                var correspondingItem = select.FirstOrDefault(p => p.Name == removedProduct.Name);
                if (correspondingItem != null)
                {
                    select.Remove(correspondingItem);
                }
            }
        }
    }
}