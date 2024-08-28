using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder(Type type, FormConfiguration configuration)
{
    #region Group

    public AutoFormConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup($"AutoForm-Title-{type.Name}", $"AutoForm-SubTitle-{type.Name}", AutoFormGroupDisplayType.All, action);

    public AutoFormConfigurationBuilder AddGroup(AutoFormGroupDisplayType groupDisplayType, Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup($"AutoForm-Title-{type.Name}", $"AutoForm-SubTitle-{type.Name}", groupDisplayType, action);
    

    public AutoFormConfigurationBuilder AddGroup(string title, AutoFormGroupDisplayType groupDisplayType, Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(title, $"AutoForm-SubTitle-{type.Name}", groupDisplayType, action);

    public AutoFormConfigurationBuilder AddGroup(string title, string subTitle, AutoFormGroupDisplayType groupDisplayType, Action<AutoFormMemberConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new FormGroupConfiguration
        {
            Title = title,
            SubTitle = subTitle,
            AutoFormGroupDisplayType = groupDisplayType
        };

        configuration.GroupsConfigurations.Add(currentGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder(type, currentGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    #endregion
    
    public AutoFormConfigurationBuilder AddAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
    {
        configuration.FormAvatarConfiguration.EnableAvatarModel = true;
        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(configuration.FormAvatarConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
    
    public AutoFormConfigurationBuilder ConfigureActions(Action<AutoFormActionConfigurationBuilder> action)
    {
        var autoFormGroupConfigBuilder = new AutoFormActionConfigurationBuilder(configuration.FormActionConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
}