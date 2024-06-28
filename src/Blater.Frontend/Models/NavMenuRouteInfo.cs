namespace Blater.Frontend.Models;

public class NavMenuRouteInfo
{
    public required string Icon { get; init; }
    public required string Name { get; init; }
    public string? Route { get; init; }
    public int Priority { get; init; }

    public string[] RoleNames { get; init; } = [];
    
    public string[] Permissions { get; init; } = [];

    public List<NavMenuRouteInfo> ChildRoutes { get; } = [];

    public bool IsSubMenu => ChildRoutes.Count != 0;
    
    public Type? ComponentType { get; init; }

    public string? NavMenuParentName { get; set; }
}