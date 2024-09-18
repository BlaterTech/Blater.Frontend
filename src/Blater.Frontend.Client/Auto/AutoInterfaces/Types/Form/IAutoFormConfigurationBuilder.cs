using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    AutoAvatarModelConfiguration<TModel> AddGroupAvatar(AutoAvatarModelConfiguration<TModel> avatarConfiguration);
    AutoAvatarModelConfiguration<TModel> AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration<TModel> avatarConfiguration);
    AutoAvatarModelConfiguration<TModel> AddGroupAvatarEditOnly(AutoAvatarModelConfiguration<TModel> avatarConfiguration);
    AutoFormGroupConfiguration<TModel> AddGroup(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroup(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupEditOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupEditOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormActionConfiguration<TModel> Actions(AutoFormActionConfiguration<TModel> actionConfiguration);
    AutoFormActionConfiguration<TModel> ActionsCreateOnly(AutoFormActionConfiguration<TModel> actionConfiguration);
    AutoFormActionConfiguration<TModel> ActionsEditOnly(AutoFormActionConfiguration<TModel> actionConfiguration);
}