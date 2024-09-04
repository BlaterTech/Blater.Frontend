using Blater.Frontend.Client.Auto.AutoBuilders.Form;

namespace Blater.Frontend.Client.Auto.Interfaces.Form;

public interface IAutoFormConfigurationBuilder
{
    IAutoFormConfigurationBuilder AddGroupAvatar(Action<AutoFormAvatarConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupAvatarCreateOnly(Action<AutoFormAvatarConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupAvatarEditOnly(Action<AutoFormAvatarConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder> action);
    IAutoFormConfigurationBuilder Actions(Action<AutoFormActionConfigurationBuilder> action);
    IAutoFormConfigurationBuilder ActionsCreateOnly(Action<AutoFormActionConfigurationBuilder> action);
    IAutoFormConfigurationBuilder ActionsEditOnly(Action<AutoFormActionConfigurationBuilder> action);
}