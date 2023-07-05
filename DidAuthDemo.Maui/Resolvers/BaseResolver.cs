using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using System.Text.Json;

namespace DidAuthDemo.Maui.Resolvers;

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

    protected async Task<Key> UnlockKey(int keyId, string password)
    {
        var key = await _keyDatabase.GetAsync(keyId);
        if (key == null) return null;

        //Deserialize the Private Key so we can Decrypt it
        //Re-serialize and return so we can sign/verify against
        //  DID Document that is being resolved
        key.PrivateKey = JsonSerializer.Serialize(
            JsonSerializer.Deserialize<PrivateKey>(key.PrivateKey)
                .Decrypt(password)
        );

        return key;
    }
}