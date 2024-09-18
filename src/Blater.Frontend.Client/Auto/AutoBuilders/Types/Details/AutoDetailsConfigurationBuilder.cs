using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Details;
using Blater.Frontend.Client.Auto.AutoModels.Types.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Details;

public class AutoDetailsConfigurationBuilder<TModel> : IAutoDetailsConfigurationBuilder<TModel>
{
    private readonly AutoDetailsConfiguration<TModel> _configuration;

    public AutoDetailsConfigurationBuilder(object instance)
    {
        if (instance is IAutoDetailsConfiguration<TModel> configuration)
        {
            _configuration = configuration.DetailsConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoDetailsConfiguration");
        }
    }

    public AutoDetailsGroupConfiguration<TModel> AddGroup(AutoDetailsGroupConfiguration<TModel> detailsGroupConfiguration, Action<IAutoDetailsMemberConfigurationBuilder<TModel>> action)
    {
        var index = _configuration.Groups.IndexOf(detailsGroupConfiguration);
        if (index != -1)
        {
            _configuration.Groups[index] = detailsGroupConfiguration;
        }
        else
        {
            _configuration.Groups.Add(detailsGroupConfiguration);
        }

        var builder = new AutoDetailsPropertyConfigurationBuilder<TModel>(detailsGroupConfiguration);
        
        action.Invoke(builder);

        return detailsGroupConfiguration;
    }
}