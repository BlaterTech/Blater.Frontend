using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly DetailsConfiguration _detailsConfiguration;

    public AutoDetailsConfigurationBuilder(DetailsConfiguration detailsConfiguration)
    {
        _detailsConfiguration = detailsConfiguration;
        AutoConfigurations<T, DetailsConfiguration>.Configurations.Add(typeof(T), _detailsConfiguration);
    }

    public AutoDetailsConfigurationBuilder<T> Details(string detailsName, Action<AutoDetailsMemberConfigurationBuilder<T>> action)
    {
        _detailsConfiguration.Name = detailsName;

        var autoFormMemberConfigurationBuilder = new AutoDetailsMemberConfigurationBuilder<T>(_detailsConfiguration);

        action(autoFormMemberConfigurationBuilder);

        return this;
    }
}