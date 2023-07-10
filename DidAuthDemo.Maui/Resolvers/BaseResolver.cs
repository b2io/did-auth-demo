using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using System.Net.Http.Json;
using System;
using System.Text.Json;
using SimpleBase;
using System.Text;
using DidAuthDemo.Maui.Common;
using DidAuthDemo.Maui.Derivers;

namespace DidAuthDemo.Maui.Resolvers;
public record DownloadDidDocumentResponse(bool IsSuccessful, DidDocument DidDocument);

public interface IBaseResolver
{
    Task<bool> VerifyDidDocument(Did did, string password);
}

public abstract class BaseResolver : IBaseResolver
{
    private readonly KeyDatabase _keyDatabase;

    protected BaseResolver()
    {
        _keyDatabase = new KeyDatabase();
    }

    public abstract Task<bool> VerifyDidDocument(Did did, string password);

    protected async Task<DownloadDidDocumentResponse> DownloadDidDocument(UriBuilder uriBuilder)
    {
        using var client = new HttpClient();

        HttpResponseMessage res = await client.GetAsync(uriBuilder.Uri);
        DidDocument didDocument;
        if (res.IsSuccessStatusCode)
        {
            didDocument = await res.Content.ReadFromJsonAsync<DidDocument>();
        }
        else
        {
            string msg = await res.Content.ReadAsStringAsync();
            Console.WriteLine(msg);
            return new DownloadDidDocumentResponse(false, null);
        }

        if (didDocument is null) return new DownloadDidDocumentResponse(false, null);

        return new DownloadDidDocumentResponse(true, didDocument);
    }

    protected async Task<bool> VerifyDidDocument(DidDocument didDocument, Did did, string password)
    {
        // Unlocked the PrivateKey using the provided password
        var key = await _keyDatabase.GetAsync(did.KeyId);
        if (key == null) return false;

        KeyPair didKeyPair = DeriverFactory
            .GetKeyDeriver((DidType)did.DidType)
            .DeriveKey(key, did.IndexDerivation, password);

        var publicKeyObj = didDocument.PublicKeys
            .FirstOrDefault(x => x.Id == $"{did.Identifier}#key-1");
        if(publicKeyObj == null) return false;

        var publicKey = new PublicKey(Base58.Bitcoin.Decode(publicKeyObj.PublicKeyBase58), null);
        var message = Encoding.UTF8.GetBytes("message");
        var signature = didKeyPair.PrivateKey.Sign(message);
        return publicKey.Verify(message, signature);
    }
}

