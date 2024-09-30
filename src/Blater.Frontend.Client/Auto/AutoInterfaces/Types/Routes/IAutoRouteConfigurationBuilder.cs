using Blater.Frontend.Client.Auto.AutoModels.Types.Routes;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;

public interface IAutoRouteConfigurationBuilder
{
    IAutoRouteConfigurationBuilder ConfigureNavMenu(AutoRouteNavMenuConfiguration navMenuConfiguration);
    IAutoRouteConfigurationBuilder AddRouteLink(string href, string title, string icon);
    IAutoRouteConfigurationBuilder AddRouteLink(AutoRouteLinkConfiguration linkConfiguration);
    IAutoRouteConfigurationBuilder AddGroup(string title, string icon, Action<IAutoRouteGroupConfigurationBuilder> action);
    IAutoRouteConfigurationBuilder AddGroup(AutoRouteGroupConfiguration groupConfiguration, Action<IAutoRouteGroupConfigurationBuilder> action);
}