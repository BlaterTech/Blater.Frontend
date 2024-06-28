namespace Blater.Frontend.Attributes.Auto;

[AttributeUsage(AttributeTargets.All)]
public class AutoRequiredRoles(List<string> rolenames) : Attribute
{
    public List<string> RoleNames { get; set; } = rolenames;
}