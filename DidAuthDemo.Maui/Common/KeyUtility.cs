using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Models;
using System.Text.Json;

namespace DidAuthDemo.Maui.Common;

public static class KeyUtility
{
    public static void UnlockPrivateKey(this Key key, string password)
    {
        //Deserialize the Private Key so we can Decrypt it
        //Re-serialize and return so we can sign/verify against
        //  DID Document that is being resolved
        key.PrivateKey = JsonSerializer.Serialize(
            JsonSerializer.Deserialize<PrivateKey>(key.PrivateKey)
                .Decrypt(password)
        );
    }
}
