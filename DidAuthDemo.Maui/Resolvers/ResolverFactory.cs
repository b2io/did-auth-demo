using DidAuthDemo.Maui.Common;

namespace DidAuthDemo.Maui.Resolvers;

public static class ResolverFactory
{
    public static IBaseResolver GetResolver(ResolutionType resolutionType) =>
        resolutionType switch
        { 
            ResolutionType.Web => new WebResolver(),
            ResolutionType.Github => new GithubResolver(),
            _ => throw new InvalidOperationException()
        };
}
