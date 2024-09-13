using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form;

public class AutoFormConfigurationBuilder : IAutoFormConfigurationBuilder
{
    private readonly AutoFormConfiguration _configuration;
    private readonly Type _type;

    public AutoFormConfigurationBuilder(Type type, object instance)
    {
        _type = type;
        if (instance is IAutoFormConfiguration configuration)
        {
            _configuration = configuration.FormConfiguration;
        }
        else
        {
            throw new InvalidCastException("Instance is not implement IAutoFormConfiguration");
        }
    }

    #region Group

    public IAutoFormConfigurationBuilder AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.Form, avatarConfiguration);

    public IAutoFormConfigurationBuilder AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormCreate, avatarConfiguration);

    public IAutoFormConfigurationBuilder AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormEdit, avatarConfiguration);

    public IAutoFormConfigurationBuilder AddGroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.Form, groupConfiguration, action);

    public IAutoFormConfigurationBuilder AddGroupCreateOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, groupConfiguration, action);

    public IAutoFormConfigurationBuilder AddGroupEditOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, groupConfiguration, action);

    #endregion

    #region Actions

    public IAutoFormConfigurationBuilder Actions(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.Form, actionConfiguration);

    public IAutoFormConfigurationBuilder ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormCreate, actionConfiguration);

    public IAutoFormConfigurationBuilder ActionsEditOnly(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormEdit, actionConfiguration);

    #endregion

    private AutoFormConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action)
    {
        if (!_configuration.GroupConfigurations.TryGetValue(displayType, out var value))
        {
            value ??= [];
            _configuration.GroupConfigurations.TryAdd(displayType, value);
        }

        var index = value.IndexOf(groupConfiguration);
        if (index != -1)
        {
            value[index] = groupConfiguration;
        }
        else
        {
            value.Add(groupConfiguration);
        }

        _configuration.GroupConfigurations[displayType] = value;
        
        var builder = new AutoFormMemberConfigurationBuilder(_type, groupConfiguration);
        
        action.Invoke(builder);

        return this;
    }

    private AutoFormConfigurationBuilder AddGroupAvatar(AutoComponentDisplayType displayType, AutoAvatarModelConfiguration avatarConfiguration)
    {
        if (_configuration.AvatarConfiguration.TryGetValue(displayType, out var value))
        {
            _configuration.AvatarConfiguration[displayType] = avatarConfiguration;
        }
        else
        {
            value = avatarConfiguration;
            _configuration.AvatarConfiguration.TryAdd(displayType, value);
        }
        
        return this;
    }

    private AutoFormConfigurationBuilder Actions(AutoComponentDisplayType displayType, AutoFormActionConfiguration actionConfiguration)
    {
        if (_configuration.ActionConfiguration.TryGetValue(displayType, out var value))
        {
            _configuration.ActionConfiguration[displayType] = actionConfiguration;
        }
        else
        {
            value = actionConfiguration;
            _configuration.ActionConfiguration.TryAdd(displayType, value);
        }
        
        return this;
    }
}