namespace Blater.Frontend.Attributes.Auto;

[AttributeUsage(AttributeTargets.All)]
public class AutoRequiredPermissions(List<string> permissions) : Attribute
{
    public List<string> Permissions { get; set; } = permissions;
}