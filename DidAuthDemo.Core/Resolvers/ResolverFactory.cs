using DidAuthDemo.Core.Common;

namespace DidAuthDemo.Core.Resolvers;

public static class ResolverFactory
{
    public static IBaseResolver GetResolver(ResolutionType resolutionType) =>
        resolutionType switch
        {
            ResolutionType.Web => new WebResolver(),
            ResolutionType.Github => new GithubResolver(),
            _ => throw new InvalidOperationException()
        };

    public static IBaseResolver GetResolver(string did)
    {
        var didParts = did.Split(':');
        var didMethod = didParts[1];

        return didMethod switch
        {
            "web" => new WebResolver(),
            "github" => new GithubResolver(),
            _ => throw new InvalidOperationException()
        };
    }
}
