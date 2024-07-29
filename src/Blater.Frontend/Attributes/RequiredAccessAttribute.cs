using Blater.Frontend.Enumerations;

namespace Blater.Frontend.Attributes;

[AttributeUsage(AttributeTargets.All)]
public class RequiredAccessAttribute(List<string> userRoles, List<string> userPermissions, UserTypes userTypes) : Attribute
{
    public List<string> UserRoles { get; } = userRoles;
    public List<string> UserPermissions { get; } = userPermissions;
    public UserTypes UserTypes { get; } = userTypes;
}