using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Models;
using System.Net.Http.Json;
using SimpleBase;
using System.Text;
using DidAuthDemo.Core.Common;
using DidAuthDemo.Core.Derivers;

namespace DidAuthDemo.Core.Resolvers;
public record DownloadDidDocumentResponse(bool IsSuccessful, DidDocument DidDocument);

public interface IBaseResolver
{
    Task<bool> VerifyDidDocument(Did did, Key key, byte[] signature);
    Task<DidDocument?> GetDidDocument(string did);
}

public abstract class BaseResolver : IBaseResolver
{
    public abstract Task<bool> VerifyDidDocument(Did did, Key key, byte[] signature);
    public abstract Task<DidDocument?> GetDidDocument(string did);

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

    protected async Task<bool> VerifyDidDocument(DidDocument didDocument, Did did, Key key, byte[] signature)
    {
        var publicKeyObj = didDocument.PublicKeys
            .FirstOrDefault(x => x.Id == $"{did.Identifier}#key-1");
        if(publicKeyObj == null) return false;

        var publicKey = new PublicKey(Base58.Bitcoin.Decode(publicKeyObj.PublicKeyBase58), null);
        var message = Encoding.UTF8.GetBytes("message");

        return publicKey.Verify(message, signature);
    }
}

