﻿using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Models;
using System.Text.Json;

namespace DidAuthDemo.Core.Common;

public static class KeyUtility
{
    public static string UnlockPrivateKey(this Key key, string password)
    {
        //Deserialize the Private Key so we can Decrypt it
        //Re-serialize and return so we can sign/verify against
        //  DID Document that is being resolved
        return JsonSerializer.Serialize(
            JsonSerializer.Deserialize<PrivateKey>(key.PrivateKey)
                .Decrypt(password)
        );
    }
}
