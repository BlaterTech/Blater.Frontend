using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder(Type type, AutoGroupConfiguration configuration)
    : AutoPropertyConfigurationBuilder(new TablePropertyConfiguration())
{
    public IAutoTablePropertyConfigurationBuilder AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new TablePropertyConfiguration
        {
            Property = type.GetProperty(propertyName)!
        };
        
        configuration.Properties.Add(currentPropertyConfig);

        return new AutoTablePropertyConfigurationBuilder(currentPropertyConfig);
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