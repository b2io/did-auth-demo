using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Resolvers;

public interface IWebResolver : IBaseResolver { }

/// <summary>
/// This resolver is used to verify DID's utilizing the did:web method specification
/// https://w3c-ccg.github.io/did-method-web/
/// </summary>
public class WebResolver : BaseResolver, IWebResolver
{
    public override bool VerifyDidDocument(Did did, string password)
    {
        throw new NotImplementedException();
    }
}
