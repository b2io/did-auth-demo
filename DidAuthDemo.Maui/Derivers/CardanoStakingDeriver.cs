using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Derivers;

public class CardanoStakingDeriver : CardanoKeyDeriver
{
    public override KeyPair DeriveKey(Key key, int indexDerivation, string password)
    {
        var derivationPath = GetDerivationPath(RoleType.Staking, indexDerivation);

        return GetDidKeyPair(key, derivationPath, password);
    }
}
