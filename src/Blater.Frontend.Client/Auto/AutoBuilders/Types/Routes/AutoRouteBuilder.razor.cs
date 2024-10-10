using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;
using Blater.Frontend.Client.Contracts.Tenant;
using Blater.Frontend.Client.EasyRenderTree;
using Blater.Frontend.Client.Interfaces;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Routes;

public partial class AutoRouteBuilder : ComponentBase
{
    [Inject]
    private AutoConfigurations AutoConfigurations { get; set; } = null!;

    [Inject]
    private ILocalizationService LocalizationService { get; set; } = null!;

    [Parameter]
    [EditorRequired]
    public TenantData TenantData { get; set; } = null!;

    [Parameter]
    public bool EnableDrawerHead { get; set; } = true;

    [Parameter]
    public bool DrawerOpen { get; set; } = true;

    [Parameter]
    public bool EnableToggleBottom { get; set; }

    [Parameter]
    public string? ToggleIcon { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter]
    public DrawerClipMode ClipMode { get; set; }

    private AutoRouteConfiguration Route => AutoConfigurations.Route;

    private string GetNavMenuLocalization(AutoRouteLinkConfiguration routeLinkConfiguration)
    {
        var value = LocalizationService.GetValueOrDefault($"NavMenu-{routeLinkConfiguration.Title}");
        if (string.IsNullOrWhiteSpace(value))
        {
            value = routeLinkConfiguration.Title;
        }

        return value;
    }

    private RenderFragment RenderRoutes(List<AutoRouteGroupConfiguration> groupConfigurations) => builder =>
    {
        var easyRenderTreeBuilder = new EasyRenderTreeBuilder(builder);

        foreach (var groupConfiguration in groupConfigurations)
        {
            Group(easyRenderTreeBuilder, groupConfiguration);
        }

        return;

        void Route(EasyRenderTreeBuilder treeBuilder, AutoRouteLinkConfiguration routeLinkConfiguration)
        {
            treeBuilder
               .OpenComponent<MudNavLink>()
               .AddAttribute(nameof(MudNavLink.Icon), routeLinkConfiguration.Icon)
               .AddAttribute(nameof(MudNavLink.IconColor), routeLinkConfiguration.IconColor)
               .AddAttribute(nameof(MudNavLink.Disabled), routeLinkConfiguration.Disabled)
               .AddAttribute(nameof(MudNavLink.Href), routeLinkConfiguration.Href)
               .AddAttribute(nameof(MudNavLink.Match), routeLinkConfiguration.Match)
               .AddChildContent(renderTreeBuilder =>
                {
                    if (DrawerOpen)
                    {
                        renderTreeBuilder.AddContent(GetNavMenuLocalization(routeLinkConfiguration));
                    }
                })
               .Close();
        }

        void Group(EasyRenderTreeBuilder renderTreeBuilder, AutoRouteGroupConfiguration groupConfiguration)
        {
            var groupBuilder = renderTreeBuilder
                              .OpenComponent<MudNavGroup>()
                              .AddAttribute(nameof(MudNavGroup.Title), groupConfiguration.Title)
                              .AddAttribute(nameof(MudNavGroup.Icon), groupConfiguration.Icon)
                              .AddAttribute(nameof(MudNavGroup.Expanded), groupConfiguration.Expanded);

            if (groupConfiguration.Links.Count > 0)
            {
                groupBuilder.AddChildContent(treeBuilder =>
                {
                    foreach (var link in groupConfiguration.Links)
                    {
                        Route(treeBuilder, link);
                    }
                });
            }

            if (groupConfiguration.SubGroups.Count > 0)
            {
                groupBuilder.AddChildContent(treeBuilder =>
                {
                    foreach (var subGroup in groupConfiguration.SubGroups)
                    {
                        Group(treeBuilder, subGroup);
                    }
                });
            }

            groupBuilder.Close();
        }
    };
}