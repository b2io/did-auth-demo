using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Core.Models;

namespace DidAuthDemo.Core.Derivers;

public class CardanoPaymentDeriver : CardanoKeyDeriver
{
    public override KeyPair DeriveKey(Key key, int indexDerivation, string password)
    {
        var derivationPath = GetDerivationPath(RoleType.ExternalChain, indexDerivation);

        return GetDidKeyPair(key, derivationPath, password);
    }
}
