using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder : IAutoFormConfigurationBuilder
{
    private readonly AutoFormConfiguration _configuration;
    private readonly Type _type;
    public AutoFormConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoFormConfiguration configuration)
        {
            _configuration = configuration.Configuration;
        }
        else
        {
            throw new Exception("Instance is not IAutoFormConfiguration");
        }
    }
    
    #region Group

    public IAutoFormConfigurationBuilder AddGroupAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.Form, action);

    public IAutoFormConfigurationBuilder AddGroupAvatarCreateOnly(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.FormCreate, action);

    public IAutoFormConfigurationBuilder AddGroupAvatarEditOnly(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.FormEdit, action);
    
    public IAutoFormConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.Form, action);

    public IAutoFormConfigurationBuilder AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);

    public IAutoFormConfigurationBuilder AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);

    #endregion

    #region Actions

    public IAutoFormConfigurationBuilder Actions(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.Form, action);

    public IAutoFormConfigurationBuilder ActionsCreateOnly(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.FormCreate, action);

    public IAutoFormConfigurationBuilder ActionsEditOnly(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.FormEdit, action);

    #endregion
    
    private AutoFormConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder> action)
    {
        var countGroup = _configuration.GroupConfigurations.Count;
        var title = $"Auto{displayType.ToString()}Group{countGroup}-Title-{_type.Name}";
        var formGroupConfiguration = _configuration.GroupConfigurations.FirstOrDefault(x => x.Title == title);

        if (formGroupConfiguration != null)
        {
            var existentConfiguration = new AutoFormMemberConfigurationBuilder(_type, formGroupConfiguration);

            action(existentConfiguration);

            return this;
        }
        
        formGroupConfiguration = new AutoFormGroupConfiguration
        {
            Title = title,
            ComponentConfigurations =
            {
                [displayType] = []
            }
        };

        _configuration.GroupConfigurations.Add(formGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder(_type, formGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
    
    private AutoFormConfigurationBuilder AddGroupAvatar(AutoComponentDisplayType displayType, Action<AutoFormAvatarConfigurationBuilder> action)
    {
        if (_configuration.AvatarConfiguration.TryGetValue(displayType, out var autoFormActionConfiguration))
        {
            var existentFormGroupConfiguration = new AutoFormAvatarConfigurationBuilder(autoFormActionConfiguration);

            action(existentFormGroupConfiguration);
            
            return this;
        }

        autoFormActionConfiguration = new AutoAvatarModelConfiguration();
        _configuration.AvatarConfiguration.TryAdd(displayType, autoFormActionConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(autoFormActionConfiguration);

        action(autoFormGroupConfigBuilder);
            
        return this;
    }

    private AutoFormConfigurationBuilder Actions(AutoComponentDisplayType displayType, Action<AutoFormActionConfigurationBuilder> action)
    {
        if (_configuration.ActionConfiguration.TryGetValue(displayType, out var actionConfiguration))
        {
            var existentFormActionConfiguration = new AutoFormActionConfigurationBuilder(actionConfiguration);

            action(existentFormActionConfiguration);
            
            return this;
        }
        
        actionConfiguration = new AutoFormActionConfiguration();
        _configuration.ActionConfiguration.TryAdd(displayType, actionConfiguration);
        
        var formActionConfiguration = new AutoFormActionConfigurationBuilder(actionConfiguration);

        action(formActionConfiguration);

        return this;
    }
}