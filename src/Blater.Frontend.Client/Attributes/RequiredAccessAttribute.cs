namespace Blater.Frontend.Client.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class RequiredAccessAttribute(List<string> userRoles, List<string> userPermissions) : Attribute
{
    public List<string> UserRoles { get; } = userRoles;
    public List<string> UserPermissions { get; } = userPermissions;
}