using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Common;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using DidAuthDemo.Maui.Resolvers;
using System.Text.Json;

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

    [ObservableProperty]
    string password;

    [ObservableProperty]
    bool verificationAttempted;

    [ObservableProperty]
    bool showSpinner;

    [ObservableProperty]
    bool verificationResult;

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

    [RelayCommand]
    async void CopyDidDocument()
    {
        //move to did creation and just copy from object
        await Clipboard.Default.SetTextAsync(Did.DidDocument);
    }

    [RelayCommand(CanExecute = nameof(CanVerifyDocument))]
    async void VerifyDidDocument()
    {
        ShowSpinner = true;
        VerificationAttempted = false;

        var resolver = ResolverFactory.GetResolver(Enum.Parse<ResolutionType>(Did.ResolutionType));
        VerificationResult = await resolver.VerifyDidDocument(Did, Password);
        await Task.Delay(1000);

        ShowSpinner = false;
        VerificationAttempted = true;
    }

    bool CanVerifyDocument() => !string.IsNullOrEmpty(Password)
        && !ShowSpinner;

    partial void OnShowSpinnerChanged(bool oldValue, bool newValue)
    {
        VerifyDidDocumentCommand?.NotifyCanExecuteChanged();
    }

    partial void OnPasswordChanged(string oldValue, string newValue)
    {
        VerifyDidDocumentCommand?.NotifyCanExecuteChanged();
    }
}
