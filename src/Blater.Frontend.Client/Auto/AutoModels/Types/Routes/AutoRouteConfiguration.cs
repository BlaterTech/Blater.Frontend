using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Routes;

public class AutoRouteConfiguration
{
    public AutoRouteNavMenuConfiguration NavMenu { get; set; } = new();
    public bool CanCreateGroupFirst { get; set; }
    public List<AutoRouteLinkConfiguration> Links { get; set; } = [];
    public List<AutoRouteGroupConfiguration> Groups { get; set; } = [];
}

public class AutoRouteNavMenuConfiguration
{
    public Color NavMenuColor { get; set; } = Color.Inherit;
    public Margin Margin { get; set; } = Margin.Dense;
    public bool Rounded { get; set; }
    public bool Bordered { get; set; } = true;
    public bool Dense { get; set; }
    public string ExtraClass { get; set; } = string.Empty;
    public string MiniWidth { get; set; } = "75px";
}

public class AutoRouteAuthConfiguration
{
    public List<string> Roles { get; set; } = [];
    public List<string> Permissions { get; set; } = [];
}

public class AutoRouteLinkConfiguration
{
    public required string Href { get; set; }
    public required string Title { get; set; }
    public string? Icon { get; set; }
    public Color IconColor { get; set; }
    public bool Disabled { get; set; }
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
    public AutoRouteAuthConfiguration AuthConfiguration { get; set; } = new();
}

public class AutoRouteGroupConfiguration
{
    public required string Title { get; set; }
    public required string Icon { get; set; }
    public bool Expanded { get; set; }
    public AutoRouteAuthConfiguration AuthConfiguration { get; set; } = new();
    public List<AutoRouteLinkConfiguration> Links { get; set; } = [];
    public List<AutoRouteGroupConfiguration> SubGroups { get; set; } = [];
}