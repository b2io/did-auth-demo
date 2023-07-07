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
    string verificationResult;

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
        var didDocument = await DidUtility.GetDidDocument(Did, Key);
        var didDocumentString = JsonSerializer.Serialize(didDocument);
        await Clipboard.Default.SetTextAsync(didDocumentString);
    }

    [RelayCommand]
    async void VerifyDidDocument()
    {
        var resolver = ResolverFactory.GetResolver(Enum.Parse<ResolutionType>(Did.ResolutionType));
        var isVerified = await resolver.VerifyDidDocument(Did, Password);
        if(isVerified)
        {
            VerificationResult = "Verification Successful!";
        }else
        {
            VerificationResult = "Verification Failed.";
        }
    }
}
