using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoExtensions;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Table;
using Blater.Frontend.Client.Auto.AutoModels.Types.Table;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Table;

public class AutoTableConfigurationBuilder<TModel> : IAutoTableConfigurationBuilder<TModel>
{
    private readonly AutoTableConfiguration<TModel> _configuration;

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

    public AutoTablePropertyConfiguration<TModel, TPropertyType> AddMemberOnly<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return propertyConfiguration;
    }
    
    public IAutoTableEventConfigurationBuilder<TModel, TPropertyType> AddMemberWithEvent<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        AddMember(expression, propertyConfiguration);

        return new AutoTableEventConfigurationBuilder<TModel, TPropertyType>(propertyConfiguration);
    }
    
    private void AddMember<TPropertyType>(Expression<Func<TModel, TPropertyType>> expression, AutoTablePropertyConfiguration<TModel, TPropertyType> propertyConfiguration)
    {
        var propertyInfo = expression.GetPropertyInfo();
        
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