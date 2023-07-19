using DidAuthDemo.Core.Models;

namespace DidAuthDemo.Core.Resolvers;

public interface IGithubResolver : IBaseResolver { }

/// <summary>
/// This resolver is used to verify DID's utilizing the did:github method specification
/// https://github.com/decentralized-identity/github-did
/// </summary>
public class GithubResolver : BaseResolver, IGithubResolver
{
    public async override Task<DidDocument?> GetDidDocument(string did)
    {
        var didParts = did.Split(':');
        var githubUsername = didParts.Last();

        // Grab the DID Document from the Website associated with DID
        var uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "https";
        uriBuilder.Host = "raw.githubusercontent.com";
        uriBuilder.Path = $"{githubUsername}/ghdid/master/index.jsonld";

        var didDocumentResponse = await DownloadDidDocument(uriBuilder);

        if (!didDocumentResponse.IsSuccessful) return null;

        return didDocumentResponse.DidDocument;
    }

    public override async Task<bool> VerifyDidDocument(Did did, Key key, byte[] signature)
    {
        // Grab the DID Document from the Website associated with DID
        var uriBuilder = new UriBuilder();
        uriBuilder.Scheme = "https";
        uriBuilder.Host = "raw.githubusercontent.com";
        uriBuilder.Path = $"{did.GithubUsername}/ghdid/master/index.jsonld";

        var didDocumentResponse = await DownloadDidDocument(uriBuilder);

        if (!didDocumentResponse.IsSuccessful) return false;

        // Validate DID Document using stored Private Key 
        //    and Public Key in DID Document
        return await VerifyDidDocument(didDocumentResponse.DidDocument, did, key, signature);
    }
}
