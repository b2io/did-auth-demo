using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using System.Collections.ObjectModel;
using DidAuthDemo.Maui.Views.KeyViews;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

public partial class DidDocumentCreateViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Key> keys;

    [ObservableProperty]
    Did selectedKey;

    [ObservableProperty]
    bool hasKeys;

    private readonly KeyDatabase _keyDatabase;

    public DidDocumentCreateViewModel()
    {
        _keyDatabase = new KeyDatabase();
        HasKeys = false;
    }

    [RelayCommand]
    async void LoadKeys()
    {
        Keys = (await _keyDatabase.ListAsync()).ToObservableCollection();
        HasKeys = Keys.Any();
    }

    [RelayCommand]
    async void KeySelected(Key key)
    {
        await Shell.Current.GoToAsync(nameof(PickDocumentTypeListView), new Dictionary<string, object>() { { "Key", key } });
    }

    [RelayCommand]
    async Task CreateNewKey()
    {
        await Shell.Current.GoToAsync(nameof(ShowMnemonicView));
    }
}
