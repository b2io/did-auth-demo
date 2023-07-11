using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Derivers;

public class SimpleDeriver : BaseKeyDeriver
{
    public override KeyPair DeriveKey(Key key, int indexDerivation, string password)
    {
        var derivationPath = $"m/646964'/{indexDerivation}";

        return GetDidKeyPair(key, derivationPath, password);
    }
}
