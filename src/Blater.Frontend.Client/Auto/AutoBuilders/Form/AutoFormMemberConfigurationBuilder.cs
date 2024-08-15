using System.Linq.Expressions;
using Blater.Extensions;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormMemberConfigurationBuilder<T>(FormConfiguration<T> configuration)  where T : BaseDataModel
{
    public AutoFormPropertyConfigurationBuilder<T, TProperty> AddMember<TProperty>(Expression<Func<T, TProperty>> expression, AutoFormScope value = AutoFormScope.All)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName is null");
        }

        var currentPropertyConfig = new FormPropertyConfiguration<T>
        {
            FormScope = value,
            PropertyInfo = typeof(T).GetProperty(propertyName)!
        }; 
        
        configuration.PropertyConfigurations.Add(currentPropertyConfig);
        
        return new AutoFormPropertyConfigurationBuilder<T, TProperty>(expression, currentPropertyConfig);
    }
    
    public AutoFormMemberConfigurationBuilder<T> AddGroup(string groupName, AutoFormGroupScope groupScope, Action<AutoFormGroupConfigurationBuilder<T>> action)
    {
        var currentGroupConfiguration = new FormGroupConfiguration<T>
        {
            Title = groupName,
            FormGroupScope = groupScope
        };

        var groupsConfigurations = configuration.GroupsConfigurations ??= new List<FormGroupConfiguration<T>>();
        groupsConfigurations.Add(currentGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormGroupConfigurationBuilder<T>(currentGroupConfiguration);

        action(autoFormGroupConfigBuilder);
        
        return this;
    }
}