using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Models;
using Kotlin.Contracts;
using SimpleBase;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static Android.Icu.Text.RelativeDateTimeFormatter;

namespace DidAuthDemo.Maui.Resolvers;

public interface IWebResolver : IBaseResolver { }

/// <summary>
/// This resolver is used to verify DID's utilizing the did:web method specification
/// https://w3c-ccg.github.io/did-method-web/
/// </summary>
public class WebResolver : BaseResolver, IWebResolver
{
    public override async Task<bool> VerifyDidDocument(Did did, string password)
    {
        // Unlocked the PrivateKey using the provided password
        var key = await UnlockKey(did.KeyId, password);

        // Grab the DID Document from the Website associated with DID
        using var client = new HttpClient();

        var didDomainUrl = $"{did.Domain}/.well-known/did.json";
        HttpResponseMessage res = await client.GetAsync(didDomainUrl);
        DidDocument didDocument;
        if (res.IsSuccessStatusCode)
        {
            didDocument = await res.Content.ReadFromJsonAsync<DidDocument>();
        }
        else
        {
            string msg = await res.Content.ReadAsStringAsync();
            Console.WriteLine(msg);
            return false;
        }

        if (didDocument is null) return false;

        // Validate DID Document using stored Private Key 
        //    and Public Key in DID Document
        var privateKey = JsonSerializer.Deserialize<PrivateKey>(key.PrivateKey);
        var publicKeyObj = didDocument.PublicKeys.FirstOrDefault(x => x.Id == $"{did.Identifier}#key-1");
        var publicKey = new PublicKey(Base58.Bitcoin.Decode(publicKeyObj.PublicKeyBase58), null);
        var message = Encoding.UTF8.GetBytes("message");
        var signature = privateKey.Sign(message);
        return publicKey.Verify(message, signature);
    }
}
