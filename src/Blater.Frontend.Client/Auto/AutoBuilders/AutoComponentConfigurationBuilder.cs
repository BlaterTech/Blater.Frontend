using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoComponentConfigurationBuilder
{
    private readonly AutoModelConfiguration _autoModelConfiguration;

    public AutoComponentConfigurationBuilder(Type modelType)
    {
        _autoModelConfiguration = new AutoModelConfiguration
        {
            ModelType = modelType,
            ModelName = modelType.Name
        };
        
        AutoConfigurations.Configurations.Add(modelType, _autoModelConfiguration);
    }
    
    public void Details(string detailsName, Action<AutoDetailsMemberConfigurationBuilder> action)
    {
        _autoModelConfiguration.Details = new DetailsConfiguration
        {
            Name = detailsName
        };

        var autoFormMemberConfigurationBuilder = new AutoDetailsMemberConfigurationBuilder(_autoModelConfiguration.ModelType, _autoModelConfiguration.Details);

        action(autoFormMemberConfigurationBuilder);
    }
    
    public void Table(string tableName, Action<AutoTableMemberConfigurationBuilder> action)
    {
        _autoModelConfiguration.Table = new TableConfiguration
        {
            Name = tableName
        };

        var autoTableMemberConfigurationBuilder = new AutoTableMemberConfigurationBuilder(_autoModelConfiguration.ModelType, _autoModelConfiguration.Table);

        action(autoTableMemberConfigurationBuilder);
    }
    
    public void Form(string formName, Action<AutoFormConfigurationBuilder> action)
    {
        _autoModelConfiguration.Form = new FormConfiguration
        {
            Name = formName
        };

        var autoFormMemberConfigurationBuilder = new AutoFormConfigurationBuilder(_autoModelConfiguration.ModelType, _autoModelConfiguration.Form);

        action(autoFormMemberConfigurationBuilder);
    }
}