using System.Diagnostics.CodeAnalysis;

namespace Blater.Frontend.Client.Models;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public class NavMenuRouteInfo
{
    public required string Icon { get; init; }
    
    public required string Name { get; init; }
    
    public string? Route { get; set; }
    
    public int Priority { get; set; }
    
    public List<string>? UserRoles { get; init; }
    
    public List<string>? UserPermissions { get; init; }

    public List<NavMenuRouteInfo> ChildRoutes { get; set; } = [];

    public bool IsSubMenu => ChildRoutes.Count != 0;
    
    public Type? ComponentType { get; set; }

    public string? NavMenuParentName { get; set; }
}