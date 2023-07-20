using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidAuthDemo.Maui.ViewModels.CredentialViewModels;

public partial class CredentialListViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<string> credentials;

    [ObservableProperty]
    bool hasCredentials;

    [RelayCommand]
    async void LoadCredentials()
    {
        HasCredentials = false;
    }

    [RelayCommand]
    async void ScanCredentialRequest()
    {
        await Shell.Current.GoToAsync(nameof(ScannerView));
    }
}
