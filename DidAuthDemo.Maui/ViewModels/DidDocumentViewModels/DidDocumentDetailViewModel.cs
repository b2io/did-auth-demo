using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Core.Common;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using DidAuthDemo.Core.Resolvers;
using System.Text.Json;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Derivers;
using System.Text;
using CardanoSharp.Wallet.Extensions.Models;

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

        var message = Encoding.UTF8.GetBytes("message");
        KeyPair didKeyPair = DeriverFactory
            .GetKeyDeriver((DidType)did.DidType)
            .DeriveKey(key, did.IndexDerivation, password);
        var signature = didKeyPair.PrivateKey.Sign(message);

        VerificationResult = await resolver.VerifyDidDocument(Did, Key, signature);
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
