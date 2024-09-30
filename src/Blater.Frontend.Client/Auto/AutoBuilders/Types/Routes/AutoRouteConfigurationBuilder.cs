using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;
using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Routes;

public class AutoRouteConfigurationBuilder(AutoRouteConfiguration routeConfiguration) : IAutoRouteConfigurationBuilder
{
    public IAutoRouteConfigurationBuilder ConfigureNavMenu(AutoRouteNavMenuConfiguration navMenuConfiguration)
    {
        routeConfiguration.NavMenu = navMenuConfiguration;
        return this;
    }

    public IAutoRouteConfigurationBuilder AddRouteLink(string href, string title, string icon)
        => AddRouteLink(new AutoRouteLinkConfiguration
        {
            Href = href,
            Title = title,
            Icon = icon
        });

    public IAutoRouteConfigurationBuilder AddRouteLink(AutoRouteLinkConfiguration linkConfiguration)
    {
        routeConfiguration.Links.Add(linkConfiguration);
        return this;
    }

    public IAutoRouteConfigurationBuilder AddGroup(string title, string icon, Action<IAutoRouteGroupConfigurationBuilder> action)
        => AddGroup(new AutoRouteGroupConfiguration
        {
            Title = title,
            Icon = icon
        }, action);

    public IAutoRouteConfigurationBuilder AddGroup(AutoRouteGroupConfiguration groupConfiguration, Action<IAutoRouteGroupConfigurationBuilder> action)
    {
        routeConfiguration.Groups.Add(groupConfiguration);
        
        var builder = new AutoRouteGroupConfigurationBuilder(groupConfiguration);
        
        action.Invoke(builder);

        return this;
    }
}