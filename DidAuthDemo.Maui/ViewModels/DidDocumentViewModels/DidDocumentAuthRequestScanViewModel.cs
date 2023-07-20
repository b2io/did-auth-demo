using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using CommunityToolkit.Mvvm.ComponentModel;
using DidAuthDemo.Core.Common;
using DidAuthDemo.Core.Derivers;
using DidAuthDemo.Core.Models;
using DidAuthDemo.Core.Resolvers;
using DidAuthDemo.Maui.Clients;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using SimpleBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

[QueryProperty("CurrentDid", "CurrentDid")]
[QueryProperty("KeyId", "KeyId")]
[QueryProperty("Password", "Password")]
public partial class DidDocumentAuthRequestScanViewModel : ObservableObject
{
    [ObservableProperty]
    string barcodePayload;

    [ObservableProperty]
    string currentDid;

    [ObservableProperty]
    string keyId;

    [ObservableProperty]
    string password;

    private readonly IAuthClient _authClient;
    private readonly KeyDatabase _keyDatabase;
    private readonly DidDatabase _didDatabase;

    public DidDocumentAuthRequestScanViewModel(IAuthClient authClient, KeyDatabase keyDatabase, DidDatabase didDatabase)
    {
        _authClient = authClient;
        _keyDatabase = keyDatabase;
        _didDatabase = didDatabase;
    }

    async partial void OnBarcodePayloadChanged(string oldValue, string newValue)
    {
        //var requestDecoded = Base58.Bitcoin.Decode(newValue);
        var request = JsonSerializer.Deserialize<AuthRequest>(newValue);

        var key = await _keyDatabase.GetAsync(Convert.ToInt32(KeyId));
        var did = await _didDatabase.GetByDIDAsync(CurrentDid);

        KeyPair didKeyPair = DeriverFactory
            .GetKeyDeriver((DidType)did.DidType)
            .DeriveKey(key, did.IndexDerivation, Password);

        var authResponse = new AuthResponse(didKeyPair.PrivateKey, $"{CurrentDid}#key-1", CurrentDid, request.Challenge);

        var response = await _authClient.SendAsync(authResponse);

        var result = (response.Error is null && response.Content.successful) ;

        await Shell.Current.GoToAsync($"../{nameof(DidDocumentAuthRequestResultView)}?IsSuccessful={result}");
    }
}
