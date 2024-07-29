using System.Diagnostics.CodeAnalysis;
using Blater.Frontend.Enumerations;

namespace Blater.Frontend.Models;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public class NavMenuRouteInfo
{
    public required string Icon { get; init; }
    
    public required string Name { get; init; }
    
    public string? Route { get; init; }
    
    public int Priority { get; init; }

    public UserTypes? UserTypeRequired { get; init; }
    
    public List<string>? UserRoles { get; init; }
    
    public List<string>? UserPermissions { get; init; }

    public List<NavMenuRouteInfo> ChildRoutes { get; } = [];

    public bool IsSubMenu => ChildRoutes.Count != 0;
    
    public Type? ComponentType { get; init; }

    public string? NavMenuParentName { get; set; }
}