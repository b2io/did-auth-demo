using CardanoSharp.Wallet.Enums;

namespace DidAuthDemo.Core.Derivers;

public abstract class CardanoKeyDeriver : BaseKeyDeriver
{
    protected string GetDerivationPath(RoleType role, int indexDerivation) =>
        $"m/1852'/1815'/0'/{(int)role}/{indexDerivation}";
}
