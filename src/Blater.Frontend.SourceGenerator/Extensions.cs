using Microsoft.CodeAnalysis;

namespace Blater.Frontend.SourceGenerator;

public static class Extensions
{
    public static IEnumerable<INamespaceSymbol> GetAllNamespaces(this INamespaceSymbol namespaceSymbol)
    {
        foreach (var member in namespaceSymbol.GetNamespaceMembers())
        {
            yield return member;

            foreach (var nestedNamespace in member.GetAllNamespaces())
            {
                yield return nestedNamespace;
            }
        }
    }

    public static IEnumerable<INamespaceOrTypeSymbol> GetAllMembers(this INamespaceSymbol namespaceSymbol)
    {
        foreach (var member in namespaceSymbol.GetMembers())
        {
            yield return member;

            if (member is INamespaceSymbol nestedNamespace)
            {
                foreach (var nestedMember in nestedNamespace.GetAllMembers())
                {
                    yield return nestedMember;
                }
            }
        }
    }
}