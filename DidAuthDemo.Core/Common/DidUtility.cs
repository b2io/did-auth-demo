﻿using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Models;
using SimpleBase;

namespace DidAuthDemo.Core.Common;

public static class DidUtility
{
    public static async Task<DidDocument> GetDidDocument(string didIdentifier, PublicKey publicKey)
    {
        var keyId = $"{didIdentifier}#key-1";
        var didDocumentPublicKeys = new List<DidPublicKey>()
        {
            new DidPublicKey()
            {
                Id = keyId,
                Type = "Ed25519VerificationKey2018",
                Owner = didIdentifier,
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
            Id = didIdentifier,
            Authentications = didDocumentAuthentications.ToArray(),
            PublicKeys = didDocumentPublicKeys.ToArray(),
        };
    }
}
