using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

public partial class DidDocumentListViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Did> dids;

    [ObservableProperty]
    Did selectedDid;

    [ObservableProperty]
    bool hasDids;

    private readonly DidDatabase _didDatabase;

    public DidDocumentListViewModel()
    {
        _didDatabase = new DidDatabase();
        HasDids = false;
    }

    [RelayCommand]
    async void LoadDIDs()
    {
        Dids = (await _didDatabase.ListAsync()).ToObservableCollection();
        HasDids = Dids.Any();
    }

    [RelayCommand]
    async void DidSelected(Did did)
    {
        await Shell.Current.GoToAsync(nameof(DidDocumentDetailView), new Dictionary<string, object>() { { "Id", did.Id } });
    }

    [RelayCommand]
    async Task CreateNewDidDocument()
    {
        await Shell.Current.GoToAsync(nameof(DidDocumentCreateView));
    }
}
