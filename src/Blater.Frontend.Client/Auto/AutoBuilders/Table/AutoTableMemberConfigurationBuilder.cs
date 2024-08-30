using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder(Type type, TableModelConfiguration configuration)
{
    public AutoTableComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var componentConfiguration = new TableComponentConfiguration
        {
            Property = type.GetProperty(propertyName)!,
            AutoComponentTypes =
            {
                [AutoComponentDisplayType.Table] = type.GetDefaultAutoTableComponentForType()
            }
        };
        
        configuration.Properties.Add(componentConfiguration);

        return new AutoTableComponentConfigurationBuilder<TType>(componentConfiguration);
    }
    
    public AutoTableMemberConfigurationBuilder EnableFixedHeader(bool value = true)
    {
        configuration.EnableFixedHeader = value;
        return this;
    }

    public AutoTableMemberConfigurationBuilder EnableFixedFooter(bool value = true)
    {
        configuration.EnableFixedFooter = value;
        return this;
    }
}