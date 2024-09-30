using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;

public interface IAutoRouteGroupConfigurationBuilder
{
    IAutoRouteGroupConfigurationBuilder AddRouteLink(string href, string title);
    IAutoRouteGroupConfigurationBuilder AddRouteLink(string href, string title, string icon);
    IAutoRouteGroupConfigurationBuilder AddRouteLink(AutoRouteLinkConfiguration linkConfiguration);
    IAutoRouteGroupConfigurationBuilder AddSubGroup(string title, string icon, Action<IAutoRouteGroupConfigurationBuilder> action);
    IAutoRouteGroupConfigurationBuilder AddSubGroup(AutoRouteGroupConfiguration subGroupConfiguration, Action<IAutoRouteGroupConfigurationBuilder> action);
}