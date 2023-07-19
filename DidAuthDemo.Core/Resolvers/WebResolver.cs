using DidAuthDemo.Core.Models;
using System.Linq;

namespace DidAuthDemo.Core.Resolvers;

public interface IWebResolver : IBaseResolver { }

/// <summary>
/// This resolver is used to verify DID's utilizing the did:web method specification
/// https://w3c-ccg.github.io/did-method-web/
/// </summary>
public class WebResolver : BaseResolver, IWebResolver
{
    public async override Task<DidDocument?> GetDidDocument(string did)
    {
        var didParts = did.Split(':');
        var webPath = $"https://{string.Join("/", didParts.Skip(2))}";

        var uri = new Uri(webPath);
        
        var uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "https";
        uriBuilder.Host = uri.Host;

        if (uri.AbsolutePath.Equals("/"))
        {
            uriBuilder.Path = ".well-known/did.json";
        }
        else
        {
            uriBuilder.Path = $"{uri.AbsolutePath}/did.json";
        }

        var didDocumentResponse = await DownloadDidDocument(uriBuilder);

        if (!didDocumentResponse.IsSuccessful) return null;

        return didDocumentResponse.DidDocument;
    }

    public override async Task<bool> VerifyDidDocument(Did did, Key key, byte[] signature)
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
        return await VerifyDidDocument(didDocumentResponse.DidDocument, did, key, signature);
    }
}
