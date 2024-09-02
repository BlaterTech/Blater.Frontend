using Blater.Frontend.Client.Auto.AutoBuilders.Details;
using Blater.Frontend.Client.Auto.AutoBuilders.Form;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Services;

namespace Blater.Frontend.Client.Auto.AutoBuilders;

public class AutoModelConfigurationBuilder<TModel>
{
    private readonly AutoModelConfiguration _autoModelConfiguration;
    public AutoModelConfigurationBuilder(Type modelType)
    {
        _autoModelConfiguration = new AutoModelConfiguration
        {
            ModelType = modelType,
            ModelName = modelType.Name
        };
        
        AutoConfigurations<TModel>.Configurations.Add(modelType, _autoModelConfiguration);
    }
    
    public void Details(string? title, Action<AutoDetailsGroupConfigurationBuilder> action)
    {
        _autoModelConfiguration.DetailsModelConfiguration = new DetailsModelConfiguration
        {
            Title = title ?? $"AutoDetails-Model-{_autoModelConfiguration.ModelName}",
        };

        var autoFormMemberConfigurationBuilder = new AutoDetailsGroupConfigurationBuilder(_autoModelConfiguration.ModelType, _autoModelConfiguration.DetailsModelConfiguration);

        action(autoFormMemberConfigurationBuilder);
    }
    
    public void Table(string? title, Action<AutoTableMemberConfigurationBuilder> action)
    {
        _autoModelConfiguration.TableModelConfiguration = new TableModelConfiguration
        {
            Title = title ?? $"AutoTable-Model-{_autoModelConfiguration.ModelName}",
        };

        var autoTableMemberConfigurationBuilder = new AutoTableMemberConfigurationBuilder(_autoModelConfiguration.ModelType, _autoModelConfiguration.TableModelConfiguration);

        action(autoTableMemberConfigurationBuilder);
    }
    
    public void Form(string? title, Action<AutoFormGroupConfigurationBuilder<TModel>> action)
    {
        _autoModelConfiguration.FormModelConfiguration = new FormModelConfiguration
        {
            Title = title ?? $"AutoForm-Model-{_autoModelConfiguration.ModelName}",
        };

        var autoFormMemberConfigurationBuilder = new AutoFormGroupConfigurationBuilder<TModel>(_autoModelConfiguration.ModelType, _autoModelConfiguration.FormModelConfiguration);

        action(autoFormMemberConfigurationBuilder);
    }
}