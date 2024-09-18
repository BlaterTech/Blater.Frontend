namespace Blater.Frontend.Client.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class RequiredAccessAttribute(List<string> userRoles, List<string> userPermissions) : Attribute
{
    public List<string> UserRoles { get; set; } = userRoles;
    public List<string> UserPermissions { get; set; } = userPermissions;
}