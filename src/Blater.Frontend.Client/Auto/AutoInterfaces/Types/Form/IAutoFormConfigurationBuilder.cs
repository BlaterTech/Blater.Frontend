using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    AutoAvatarModelConfiguration AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration);
    AutoAvatarModelConfiguration AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration);
    AutoAvatarModelConfiguration AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration);
    AutoFormGroupConfiguration AddGroup(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration AddGroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration AddGroupCreateOnly(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration AddGroupCreateOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration AddGroupEditOnly(string groupName, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormGroupConfiguration AddGroupEditOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder<TModel>> action);
    AutoFormActionConfiguration Actions(AutoFormActionConfiguration actionConfiguration);
    AutoFormActionConfiguration ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration);
    AutoFormActionConfiguration ActionsEditOnly(AutoFormActionConfiguration actionConfiguration);
}