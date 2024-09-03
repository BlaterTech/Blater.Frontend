using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Extensions;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder<TModel>(AutoTableModelConfiguration<TModel> configuration)
{
    public AutoTableComponentConfigurationBuilder<TType> AddMember<TType>(Expression<Func<TType>> expression)
    {
        var type = typeof(TModel);
        var property = expression.GetPropertyInfoForType(type);
        
        var componentConfiguration = new AutoTableComponentConfiguration
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
    
    public AutoTableMemberConfigurationBuilder<TModel> EnableFixedHeader(bool value = true)
    {
        configuration.EnableFixedHeader = value;
        return this;
    }

    public AutoTableMemberConfigurationBuilder<TModel> EnableFixedFooter(bool value = true)
    {
        configuration.EnableFixedFooter = value;
        return this;
    }
}