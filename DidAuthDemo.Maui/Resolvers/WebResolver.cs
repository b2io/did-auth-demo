using DidAuthDemo.Maui.Models;
using System.Net.Http.Json;

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
        var key = UnlockKey(did.KeyId, password);

        //get did document
        using var client = new HttpClient();

        var didDomainUrl = $"{did.Domain}/.well-known/did.json";
        var response = await client.GetFromJsonAsync<DidDocument>(didDomainUrl);

        //couldn't successfully get document
        if (!response.suc)
            return false;



        //validate did document public key

    }
}
