using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;
using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Routes;

public class AutoRouteGroupConfigurationBuilder(AutoRouteGroupConfiguration groupConfiguration) : IAutoRouteGroupConfigurationBuilder
{
    public IAutoRouteGroupConfigurationBuilder AddRouteLink(string href, string title)
        => AddRouteLink(new AutoRouteLinkConfiguration
        {
            Href = href,
            Title = title
        });

    public IAutoRouteGroupConfigurationBuilder AddRouteLink(string href, string title, string icon)
        => AddRouteLink(new AutoRouteLinkConfiguration
        {
            Href = href,
            Title = title,
            Icon = icon
        });

    public IAutoRouteGroupConfigurationBuilder AddRouteLink(AutoRouteLinkConfiguration linkConfiguration)
    {
        groupConfiguration.Links.Add(linkConfiguration);
        return this;
    }

    public IAutoRouteGroupConfigurationBuilder AddSubGroup(string title, string icon, Action<IAutoRouteGroupConfigurationBuilder> action)
        => AddSubGroup(new AutoRouteGroupConfiguration
        {
            Title = title,
            Icon = icon
        }, action);

    public IAutoRouteGroupConfigurationBuilder AddSubGroup(AutoRouteGroupConfiguration subGroupConfiguration, Action<IAutoRouteGroupConfigurationBuilder> action)
    {
        groupConfiguration.SubGroups.Add(subGroupConfiguration);

        var builder = new AutoRouteGroupConfigurationBuilder(subGroupConfiguration);

        action.Invoke(builder);

        return this;
    }
}