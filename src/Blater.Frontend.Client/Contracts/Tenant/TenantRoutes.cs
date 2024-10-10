using Blater.Frontend.Client.Auto.AutoBuilders.Types.Routes;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Routes;

using MudBlazor;

namespace Blater.Frontend.Client.Contracts.Tenant;

public abstract class TenantRoutes : IAutoRouteConfiguration
{
    public virtual void ConfigureRoute(AutoRouteConfigurationBuilder builder)
    {
        builder.AddRouteLink("/HomePage", "Home", Icons.Material.Filled.Home);
    }
}