using DidAuthDemo.Maui.Models;

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
        // Grab the DID Document from the Website associated with DID
        var uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "https";
        uriBuilder.Host = did.Domain;
        uriBuilder.Path = ".well-known/did.json";

        var didDocumentResponse = await DownloadDidDocument(uriBuilder);
        
        if(!didDocumentResponse.IsSuccessful) return false;

        // Validate DID Document using stored Private Key 
        //    and Public Key in DID Document
        return await VerifyDidDocument(didDocumentResponse.DidDocument, did, password);
    }
}
