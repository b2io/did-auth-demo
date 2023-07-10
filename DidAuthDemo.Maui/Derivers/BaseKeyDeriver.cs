﻿using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Common;
using DidAuthDemo.Maui.Models;
using System.Text.Json;

namespace DidAuthDemo.Maui.Derivers;

public interface IBaseKeyDeriver
{
    KeyPair DeriveKey(Key key, int indexDerivation, string password);
}

public abstract class BaseKeyDeriver : IBaseKeyDeriver
{
    public abstract KeyPair DeriveKey(Key key, int indexDerivation, string password);

    protected KeyPair GetDidKeyPair(Key key, string derivationPath, string password)
    {
        key.UnlockPrivateKey(password);

        PrivateKey privateKeyRoot = JsonSerializer.Deserialize<PrivateKey>(key.PrivateKey);

        PrivateKey privateKeyDid = privateKeyRoot.Derive(derivationPath);
        PublicKey publicKeyDid = privateKeyDid.GetPublicKey(false);

        return new KeyPair(privateKeyDid, publicKeyDid);
    }
}