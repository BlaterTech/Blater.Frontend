using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoComponentConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly DetailsConfiguration _detailsConfiguration;
    private readonly FormConfiguration<T> _formConfiguration;
    private readonly TableConfiguration _tableConfiguration;

    public AutoComponentConfigurationBuilder(DetailsConfiguration detailsConfiguration, FormConfiguration<T> formConfiguration, TableConfiguration tableConfiguration)
    {
        _detailsConfiguration = detailsConfiguration;
        _formConfiguration = formConfiguration;
        _tableConfiguration = tableConfiguration;
        AutoConfigurations<T, DetailsConfiguration>.Configurations.Add(typeof(T), _detailsConfiguration);
        AutoConfigurations<T, FormConfiguration<T>>.Configurations.Add(typeof(T), _formConfiguration);
        AutoConfigurations<T, TableConfiguration>.Configurations.Add(typeof(T), _tableConfiguration);
    }
    
    public AutoComponentConfigurationBuilder<T> Details(string detailsName, Action<AutoDetailsMemberConfigurationBuilder<T>> action)
    {
        _detailsConfiguration.Name = detailsName;

        var autoFormMemberConfigurationBuilder = new AutoDetailsMemberConfigurationBuilder<T>(_detailsConfiguration);

        action(autoFormMemberConfigurationBuilder);

        return this;
    }
    
    public AutoComponentConfigurationBuilder<T> Table(string tableName, Action<AutoTableMemberConfigurationBuilder<T>> action)
    {
        _tableConfiguration.Name = tableName;

        var autoTableMemberConfigurationBuilder = new AutoTableMemberConfigurationBuilder<T>(_tableConfiguration, new TablePropertyConfiguration());

        action(autoTableMemberConfigurationBuilder);

        return this;
    }
    
    public AutoComponentConfigurationBuilder<T> Form(string formName, Action<AutoFormMemberConfigurationBuilder<T>> action)
    {
        _formConfiguration.Name = formName;

        var autoFormMemberConfigurationBuilder = new AutoFormMemberConfigurationBuilder<T>(_formConfiguration);

        action(autoFormMemberConfigurationBuilder);

        return this;
    }
}