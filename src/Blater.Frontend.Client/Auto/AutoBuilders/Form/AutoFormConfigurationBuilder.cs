using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder(Type type, FormConfiguration configuration)
{
    #region Group

    public AutoFormConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.Form, action);

    public AutoFormConfigurationBuilder AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);
    
    public AutoFormConfigurationBuilder AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);
    
    private AutoFormConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder> action)
    {
        var currentGroupConfiguration = new FormGroupConfiguration
        {
            Title = $"Auto{displayType.ToString()}-Title-{type.Name}",
            SubTitle = $"Auto{displayType.ToString()}-SubTitle-{type.Name}",
            DisplayType = displayType
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