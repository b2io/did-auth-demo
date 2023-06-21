using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.Utilities;
using DidAuthDemo.App.Data;
using DidAuthDemo.App.Models;
using Microsoft.AspNetCore.Components;
using SimpleBase;

namespace DidAuthDemo.App.ViewModels;

public class HomePageViewModel
{
    private readonly NavigationManager _navigationManager;
    private readonly DidService _didService;

    public HomePageViewModel(NavigationManager navigationManager, DidService didService)
    {
        _navigationManager = navigationManager;
        _didService = didService;
    }

    string EncodePublicKey(byte[] key) => $"did:b2i:{Base58.Bitcoin.Encode(HashUtility.Blake2b224(key))}";

    public void GenerateHolder()
    {
        var didDocument = GenerateNewDidAuthDocument();

        _didService.AddHolderDocument(didDocument);

        _navigationManager.NavigateTo($"holder/{didDocument.Id}");
    }

    public void GenerateVerifier()
    {
        var didDocument = GenerateNewDidAuthDocument();

        _didService.AddVerifierDocument(didDocument);

        _navigationManager.NavigateTo($"verifier/{didDocument.Id}");
    }

    DidDocument GenerateNewDidAuthDocument()
    {
        var didKeyPair = KeyPair.GenerateKeyPair();
        var did = EncodePublicKey(didKeyPair.PublicKey.Key);

        return new DidDocument()
        {
            Context = new[] { "https://www.w3.org/ns/did/v1" },
            Id = did,
            KeyPair = didKeyPair
        };
    }
}
