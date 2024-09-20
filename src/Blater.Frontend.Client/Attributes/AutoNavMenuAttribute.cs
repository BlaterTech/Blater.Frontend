namespace Blater.Frontend.Client.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class AutoNavMenuAttribute(int order, string userRoles, string userPermissions, string? icon = null) : Attribute
{
    public int Order { get; set; } = order;

    public string? Icon { get; set; } = icon;

    public string? NavMenuParentName { get; set; }
    
    public string UserRoles { get; set; } = userRoles;
    public string UserPermissions { get; set; } = userPermissions;
}