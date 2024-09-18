using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

public class AutoTableConfigurationBuilder<TModel> : IAutoTableConfigurationBuilder<TModel>
{
    private readonly AutoTableConfiguration _configuration;

    public AutoTableConfigurationBuilder(object instance)
    {
        if (instance is IAutoTableConfiguration<TModel> configuration)
        {
            _configuration = configuration.TableConfiguration;
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoTableConfiguration<{typeof(TModel).Name}>");
        }
    }

    public AutoTablePropertyConfiguration<TPropertyType> AddMemberOnly<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return propertyConfiguration;
    }
    
    public IAutoTableEventConfigurationBuilder<TPropertyType> AddMemberWithEvent<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return new AutoTableEventConfigurationBuilder<TPropertyType>(propertyConfiguration);
    }
    
    private void AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TPropertyType> propertyConfiguration)
    {
        var modelType = typeof(TModel);
        var propType = typeof(TPropertyType);
        var propertyInfo = modelType.GetProperty(propType.Name);

        if (propertyInfo == null)
        {
            throw new Exception($"Property {propType.Name} not found in {modelType.Name}");
        }
        
        var index = _configuration.Configurations.IndexOf(propertyConfiguration);
        if (index != -1)
        {
            _configuration.Configurations[index] = propertyConfiguration;
        }
        else
        {
            propertyConfiguration.Property = propertyInfo;
            propertyConfiguration.AutoComponentType ??= propertyInfo.GetDefaultComponentForType();
            _configuration.Configurations.Add(propertyConfiguration);
        }
    }
}