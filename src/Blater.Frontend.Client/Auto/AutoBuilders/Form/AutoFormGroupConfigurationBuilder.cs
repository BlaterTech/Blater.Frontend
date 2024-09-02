﻿using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder(Type type, FormModelConfiguration configuration)
{
    #region Group

    public AutoFormGroupConfigurationBuilder AddGroup(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.Form, action);

    public AutoFormGroupConfigurationBuilder AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);

    public AutoFormGroupConfigurationBuilder AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);

    private AutoFormGroupConfigurationBuilder AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder> action)
    {
        var formGroupConfiguration = configuration.Configurations
                                                  .FirstOrDefault(x => x.ComponentConfigurations.ContainsKey(displayType));

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
            ComponentConfigurations =
            {
                [displayType] = []
            }
        };

        configuration.Configurations.Add(formGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder(type, formGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    #endregion

    public AutoFormGroupConfigurationBuilder AddAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
    {
        configuration.AutoAvatarConfiguration.EnableAvatarModel = true;
        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(configuration.AutoAvatarConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    public AutoFormGroupConfigurationBuilder ConfigureActions(Action<AutoFormActionConfigurationBuilder> action)
    {
        var autoFormGroupConfigBuilder = new AutoFormActionConfigurationBuilder(configuration.AutoActionConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
}