using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder(Type type, FormConfiguration configuration)
{
    public AutoFormPropertyConfigurationBuilder<TProperty> AddMember<TProperty>(Expression<Func<TProperty>> expression, AutoFormScope value = AutoFormScope.All)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new FormPropertyConfiguration
        {
            FormScope = value,
            PropertyInfo = type.GetProperty(propertyName)!
        }; 
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);
        
        return new AutoFormPropertyConfigurationBuilder<TProperty>(currentPropertyConfig);
    }
    
    public AutoFormMemberConfigurationBuilder AddGroup(string groupName, AutoFormGroupScope groupScope, Action<AutoFormGroupConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new FormGroupConfiguration
        {
            Title = groupName,
            FormGroupScope = groupScope
        };

        var groupsConfigurations = configuration.GroupsConfigurations ??= new List<FormGroupConfiguration>();
        groupsConfigurations.Add(currentGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormGroupConfigurationBuilder(type, currentGroupConfiguration);

        action(autoFormGroupConfigBuilder);
        
        return this;
    }
}