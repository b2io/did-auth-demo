using DidAuthDemo.Maui.Common;

namespace DidAuthDemo.Maui.Derivers;

public static class DeriverFactory
{
    public static IBaseKeyDeriver GetKeyDeriver(DidType didType) =>
        didType switch
        {
            DidType.Simple => new SimpleDeriver(),
            DidType.CardanoPayment => new CardanoPaymentDeriver(),
            DidType.CardanoStaking => new CardanoStakingDeriver(),
            _ => throw new InvalidOperationException()
        };
}
