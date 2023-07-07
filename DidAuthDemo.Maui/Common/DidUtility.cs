using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet.Utilities;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using SimpleBase;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DidAuthDemo.Maui.Common;

public static class DidUtility
{
    public static async Task<DidDocument> GetDidDocument(Did did, Key key)
    {
        var publicKey = JsonSerializer.Deserialize<PublicKey>(key.PublicKey);

        var keyId = $"{did.Identifier}#key-1";
        var didDocumentPublicKeys = new List<DidPublicKey>()
        {
            new DidPublicKey()
            {
                Id = keyId,
                Type = "Ed25519VerificationKey2018",
                Owner = did.Identifier,
                PublicKeyBase58 = Base58.Bitcoin.Encode(publicKey.Key)
            }
        };

        var didDocumentAuthentications = new List<DidAuthentication>()
        {
            new DidAuthentication()
            {
                Type = "Ed25519SignatureAuthentication2018",
                PublicKey = keyId
            }
        };

        return new DidDocument()
        {
            Context = new[] { "https://www.w3.org/ns/did/v1" },
            Id = did.Identifier,
            Authentications = didDocumentAuthentications.ToArray(),
            PublicKeys = didDocumentPublicKeys.ToArray(),
        };
    }
}
