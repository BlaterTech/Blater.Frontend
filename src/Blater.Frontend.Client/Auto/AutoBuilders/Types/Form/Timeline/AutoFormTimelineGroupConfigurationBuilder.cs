using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineGroupConfigurationBuilder<TModel>(AutoFormConfiguration configuration) 
    : IAutoFormTimelineGroupConfigurationBuilder<TModel>
{
    #region Group

    #region Avatar

    public AutoAvatarModelConfiguration AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.Form, avatarConfiguration);

    public AutoAvatarModelConfiguration AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormCreate, avatarConfiguration);

    public AutoAvatarModelConfiguration AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormEdit, avatarConfiguration);

    #endregion

    public AutoFormGroupConfiguration AddGroup(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, new AutoFormGroupConfiguration(groupName), action);

    public AutoFormGroupConfiguration AddGroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, groupConfiguration, action);
    
    public AutoFormGroupConfiguration AddGroupCreateOnly(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, new AutoFormGroupConfiguration(groupName), action);
    
    public AutoFormGroupConfiguration AddGroupCreateOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, groupConfiguration, action);

    public AutoFormGroupConfiguration AddGroupEditOnly(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, new AutoFormGroupConfiguration(groupName), action);
    
    public AutoFormGroupConfiguration AddGroupEditOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, groupConfiguration, action);

    #endregion

    #region Actions

    public AutoFormActionConfiguration Actions(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.Form, actionConfiguration);

    public AutoFormActionConfiguration ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormCreate, actionConfiguration);

    public AutoFormActionConfiguration ActionsEditOnly(AutoFormActionConfiguration actionConfiguration)
        => Actions(AutoComponentDisplayType.FormEdit, actionConfiguration);

    #endregion
    
    private AutoFormGroupConfiguration AddGroup(AutoComponentDisplayType displayType, AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action)
    {
        if (!configuration.Groups.TryGetValue(displayType, out var value))
        {
            value ??= [];
            configuration.Groups.TryAdd(displayType, value);
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

        configuration.Groups[displayType] = value;
        
        var builder = new AutoFormPropertyConfigurationBuilder<TModel>(displayType, groupConfiguration);
        
        action.Invoke(builder);

        return groupConfiguration;
    }

    private AutoAvatarModelConfiguration AddGroupAvatar(AutoComponentDisplayType displayType, AutoAvatarModelConfiguration avatarConfiguration)
    {
        if (configuration.AvatarConfiguration.TryGetValue(displayType, out var value))
        {
            configuration.AvatarConfiguration[displayType] = avatarConfiguration;
        }
        else
        {
            value = avatarConfiguration;
            configuration.AvatarConfiguration.TryAdd(displayType, value);
        }
        
        return value;
    }

    private AutoFormActionConfiguration Actions(AutoComponentDisplayType displayType, AutoFormActionConfiguration actionConfiguration)
    {
        if (configuration.ActionConfiguration.TryGetValue(displayType, out var value))
        {
            configuration.ActionConfiguration[displayType] = actionConfiguration;
        }
        else
        {
            value = actionConfiguration;
            configuration.ActionConfiguration.TryAdd(displayType, value);
        }
        
        return value;
    }
}