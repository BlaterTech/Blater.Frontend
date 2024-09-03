using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder(Type type, TableModelConfiguration configuration)
{
    public AutoTableComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression)
    {
        var property = expression.GetPropertyInfoForType(type);
        
        var componentConfiguration = new TableComponentConfiguration
        {
            Property = property,
            AutoComponentTypes =
            {
                [AutoComponentDisplayType.Table] = property.GetDefaultAutoTableComponentForType()
            }
        };
        
        configuration.Configurations.Add(componentConfiguration);

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