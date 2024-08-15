using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Details;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Details;

public class AutoDetailsMemberConfigurationBuilder<T>(DetailsConfiguration configuration) where T : BaseDataModel
{
    public AutoDetailsPropertyConfigurationBuilder<T, TProperty> AddMember<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new DetailsPropertyConfiguration
        {
            PropertyInfo = typeof(T).GetProperty(propertyName)!
        }; 
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);
        
        return new AutoDetailsPropertyConfigurationBuilder<T, TProperty>(currentPropertyConfig);
    }
    
    public AutoDetailsMemberConfigurationBuilder<T> AddGroup(string groupName, bool disableEditButton, Action<AutoDetailsGroupConfigurationBuilder<T>> action)
    {
        var currentGroupConfiguration = new DetailsGroupConfiguration
        {
            Title = groupName,
            DisableEditButton = disableEditButton
        };

        var groupsConfigurations = configuration.GroupsConfigurations ??= new List<DetailsGroupConfiguration>();
        groupsConfigurations.Add(currentGroupConfiguration);

        var autoDetailsGroupConfigurationBuilder = new AutoDetailsGroupConfigurationBuilder<T>(currentGroupConfiguration);

        action(autoDetailsGroupConfigurationBuilder);
        
        return this;
    }
}