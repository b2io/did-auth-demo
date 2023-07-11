using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Models.Keys;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.Derivers;

public abstract class CardanoKeyDeriver : BaseKeyDeriver
{
    protected string GetDerivationPath(RoleType role, int indexDerivation) =>
        $"m/1852'/1815'/0'/{(int)role}/{indexDerivation}";
}
