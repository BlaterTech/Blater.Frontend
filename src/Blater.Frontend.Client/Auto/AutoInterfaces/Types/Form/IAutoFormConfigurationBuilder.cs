using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Form;

public interface IAutoFormConfigurationBuilder
{
    IAutoFormConfigurationBuilder AddGroupAvatar(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormConfigurationBuilder AddGroupAvatarCreateOnly(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormConfigurationBuilder AddGroupAvatarEditOnly(AutoAvatarModelConfiguration avatarConfiguration);
    IAutoFormMemberConfigurationBuilder AddGroup(AutoFormGroupConfiguration groupConfiguration);
    IAutoFormMemberConfigurationBuilder AddGroupCreateOnly(AutoFormGroupConfiguration groupConfiguration);
    IAutoFormMemberConfigurationBuilder AddGroupEditOnly(AutoFormGroupConfiguration groupConfiguration);
    IAutoFormConfigurationBuilder Actions(AutoFormActionConfiguration actionConfiguration);
    IAutoFormConfigurationBuilder ActionsCreateOnly(AutoFormActionConfiguration actionConfiguration);
    IAutoFormConfigurationBuilder ActionsEditOnly(AutoFormActionConfiguration actionConfiguration);
}