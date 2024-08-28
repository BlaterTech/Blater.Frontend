using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Details;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder(Type type, DetailsConfiguration configuration)
{
    public AutoDetailsPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new DetailsPropertyConfiguration
        {
            PropertyInfo = type.GetProperty(propertyName)!
        }; 
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);
        
        return new AutoDetailsPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
    
    public AutoDetailsMemberConfigurationBuilder AddGroup(string groupName, bool disableEditButton, Action<AutoDetailsGroupConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new DetailsGroupConfiguration
        {
            Title = groupName,
            DisableEditButton = disableEditButton
        };

        var groupsConfigurations = configuration.GroupsConfigurations ??= new List<DetailsGroupConfiguration>();
        groupsConfigurations.Add(currentGroupConfiguration);

        var autoDetailsGroupConfigurationBuilder = new AutoDetailsGroupConfigurationBuilder(type, currentGroupConfiguration);

        action(autoDetailsGroupConfigurationBuilder);
        
        return this;
    }
}