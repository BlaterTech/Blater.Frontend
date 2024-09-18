using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    AutoAvatarModelConfiguration AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration);
    AutoAvatarModelConfiguration AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration);
    AutoAvatarModelConfiguration AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration);
    AutoFormGroupConfiguration<TModel> AddGroup(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroup(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupCreateOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupEditOnly(string groupName, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration<TModel> AddGroupEditOnly(AutoFormGroupConfiguration<TModel> groupConfiguration, Action<IAutoFormPropertyConfigurationBuilder<TModel>> action);
    AutoFormActionConfiguration Actions(AutoFormActionConfiguration actionConfiguration);
    AutoFormActionConfiguration ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration);
    AutoFormActionConfiguration ActionsEditOnly(AutoFormActionConfiguration actionConfiguration);
}