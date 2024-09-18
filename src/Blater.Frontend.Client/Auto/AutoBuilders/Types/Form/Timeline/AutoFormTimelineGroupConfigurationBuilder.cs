﻿using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;
using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form.Timeline;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Form.Timeline;

public class AutoFormTimelineGroupConfigurationBuilder<TModel>(AutoFormConfiguration<TModel> configuration) 
    : IAutoFormTimelineGroupConfigurationBuilder<TModel>
{
    #region Group

    #region Avatar

    public AutoAvatarModelConfiguration<TModel> AddGroupAvatar(AutoAvatarModelConfiguration<TModel> avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.Form, avatarConfiguration);

    public AutoAvatarModelConfiguration<TModel> AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration<TModel> avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormCreate, avatarConfiguration);

    public AutoAvatarModelConfiguration<TModel> AddGroupAvatarEditOnly(AutoAvatarModelConfiguration<TModel> avatarConfiguration)
        => AddGroupAvatar(AutoComponentDisplayType.FormEdit, avatarConfiguration);

    #endregion

    public AutoFormGroupConfiguration<TModel> AddGroup(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, new AutoFormGroupConfiguration<TModel>(groupName), action);

    public AutoFormGroupConfiguration<TModel> AddGroup(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, groupConfiguration, action);
    
    public AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, new AutoFormGroupConfiguration<TModel>(groupName), action);
    
    public AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, groupConfiguration, action);

    public AutoFormGroupConfiguration<TModel> AddGroupEditOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, new AutoFormGroupConfiguration<TModel>(groupName), action);
    
    public AutoFormGroupConfiguration<TModel> AddGroupEditOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, groupConfiguration, action);

    #endregion

    #region Actions

    public AutoFormActionConfiguration<TModel> Actions(AutoFormActionConfiguration<TModel> actionConfiguration)
        => Actions(AutoComponentDisplayType.Form, actionConfiguration);

    public AutoFormActionConfiguration<TModel> ActionsCreateOnly(AutoFormActionConfiguration<TModel> actionConfiguration)
        => Actions(AutoComponentDisplayType.FormCreate, actionConfiguration);

    public AutoFormActionConfiguration<TModel> ActionsEditOnly(AutoFormActionConfiguration<TModel> actionConfiguration)
        => Actions(AutoComponentDisplayType.FormEdit, actionConfiguration);

    #endregion
    
    private AutoFormGroupConfiguration<TModel> AddGroup(AutoComponentDisplayType displayType, AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action)
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

    private AutoAvatarModelConfiguration<TModel> AddGroupAvatar(AutoComponentDisplayType displayType, AutoAvatarModelConfiguration<TModel> avatarConfiguration)
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

    private AutoFormActionConfiguration<TModel> Actions(AutoComponentDisplayType displayType, AutoFormActionConfiguration<TModel> actionConfiguration)
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