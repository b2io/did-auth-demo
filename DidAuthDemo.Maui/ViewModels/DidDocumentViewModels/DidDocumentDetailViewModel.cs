using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

[QueryProperty("Id", "Id")]
public partial class DidDocumentDetailViewModel : ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    Did did;

    [ObservableProperty]
    Key key;

    private DidDatabase didDatabase;
    private KeyDatabase keyDatabase;

    public DidDocumentDetailViewModel()
    {
        didDatabase = new DidDatabase();
        keyDatabase = new KeyDatabase();
    }

    [RelayCommand]
    async void LoadDid()
    {
        Did = await didDatabase.GetAsync(id);
        Key = await keyDatabase.GetAsync(Did.KeyId);
    }
}
