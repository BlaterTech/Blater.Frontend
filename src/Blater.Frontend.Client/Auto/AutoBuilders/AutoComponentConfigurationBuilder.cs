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
    
    public void Details(string? title, Action<AutoDetailsMemberConfigurationBuilder> action)
    {
        var groupConfiguration = new AutoGroupConfiguration
        {
            Title = title ?? $"AutoDetails-Model-{_autoModelConfiguration.ModelName}"
        };
        _autoModelConfiguration.AutoGroupConfigurations.Add(groupConfiguration);

        var autoFormMemberConfigurationBuilder = new AutoDetailsMemberConfigurationBuilder(_autoModelConfiguration.ModelType, groupConfiguration);

        action(autoFormMemberConfigurationBuilder);
    }
    
    public void Table(string? title, Action<AutoTableMemberConfigurationBuilder> action)
    {
        var groupConfiguration = new AutoGroupConfiguration
        {
            Title = title ?? $"AutoTable-Model-{_autoModelConfiguration.ModelName}"
        };
        _autoModelConfiguration.AutoGroupConfigurations.Add(groupConfiguration);

        var autoTableMemberConfigurationBuilder = new AutoTableMemberConfigurationBuilder(_autoModelConfiguration.ModelType, groupConfiguration);

        action(autoTableMemberConfigurationBuilder);
    }
    
    public void Form(string? title, Action<AutoFormConfigurationBuilder> action)
    {
        var groupConfiguration = new AutoGroupConfiguration
        {
            Title = title ?? $"AutoForm-Model-{_autoModelConfiguration.ModelName}"
        };
        _autoModelConfiguration.AutoGroupConfigurations.Add(groupConfiguration);

        var autoFormMemberConfigurationBuilder = new AutoFormConfigurationBuilder(_autoModelConfiguration.ModelType, groupConfiguration);

        action(autoFormMemberConfigurationBuilder);
    }
}