using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Resolvers;

public interface IGithubResolver : IBaseResolver { }

/// <summary>
/// This resolver is used to verify DID's utilizing the did:github method specification
/// https://github.com/decentralized-identity/github-did
/// </summary>
public class GithubResolver : BaseResolver, IGithubResolver
{
    public override bool VerifyDidDocument(Did did, string password)
    {
        throw new NotImplementedException();
    }
}
