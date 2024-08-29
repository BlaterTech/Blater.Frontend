using Blater.Frontend.Client.Auto.AutoModels;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder(Type type, AutoGroupConfiguration configuration)
{
    #region Group

    public AutoFormConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action)
    {
        AddGroup(AutoComponentDisplayType.FormCreate, action);
        return AddGroup(AutoComponentDisplayType.FormEdit, action);
    }

    public AutoFormConfigurationBuilder AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);
    
    public AutoFormConfigurationBuilder AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);
    
    private AutoFormConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder> action)
    {
        var formGroupConfiguration = configuration.GroupsConfigurations.FirstOrDefault(x => x.Properties.ContainsKey(displayType));

        if (formGroupConfiguration != null)
        {
            var existentConfiguration = new AutoFormMemberConfigurationBuilder(type, formGroupConfiguration);

            action(existentConfiguration);

            return this;
        }
        
        formGroupConfiguration = new FormGroupConfiguration
        {
            Title = $"Auto{displayType.ToString()}-Title-{type.Name}",
            SubTitle = $"Auto{displayType.ToString()}-SubTitle-{type.Name}",
            Properties =
            {
                [displayType] = []
            }
        };

        configuration.GroupsConfigurations.Add(formGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder(type, formGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    #endregion

    public AutoFormConfigurationBuilder AddAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
    {
        configuration.AutoAvatarConfiguration.EnableAvatarModel = true;
        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(configuration.AutoAvatarConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    public AutoFormConfigurationBuilder ConfigureActions(Action<AutoFormActionConfigurationBuilder> action)
    {
        var autoFormGroupConfigBuilder = new AutoFormActionConfigurationBuilder(configuration.AutoActionConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
}