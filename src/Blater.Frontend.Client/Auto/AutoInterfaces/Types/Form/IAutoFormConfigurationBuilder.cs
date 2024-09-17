using Blater.Frontend.Client.Auto.AutoModels.Types.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfigurationBuilder<TModel>
{
    IAutoFormConfigurationBuilder AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormConfigurationBuilder AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormConfigurationBuilder AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormMemberConfigurationBuilder<TModel> AddGroup(string groupName, IAutoFormMemberConfigurationBuilder<TModel> config);
    IAutoFormConfigurationBuilder AddGroup(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupCreateOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupEditOnly(AutoFormGroupConfiguration groupConfiguration, Action<IAutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder Actions(AutoFormActionConfiguration actionConfiguration);
    IAutoFormConfigurationBuilder ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration);
    IAutoFormConfigurationBuilder ActionsEditOnly(AutoFormActionConfiguration actionConfiguration);
}