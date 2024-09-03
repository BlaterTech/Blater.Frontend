using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Extensions;
using Blater.Frontend.Client.Auto.Interfaces;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder<TModel>(Type type, FormModelConfiguration configuration)
{
    #region Group

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
    {
        configuration.AutoAvatarModelConfiguration.EnableAvatarModel = true;
        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(configuration.AutoAvatarModelConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
    
    public AutoFormGroupConfigurationBuilder<TModel> AddGroup(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);

    private AutoFormGroupConfigurationBuilder<TModel> AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder<TModel>> action)
    {
        var countGroup = configuration.Configurations.Count;
        var title = $"Auto{displayType.ToString()}Group{countGroup}-Title-{type.Name}";
        var formGroupConfiguration = configuration.Configurations.FirstOrDefault(x => x.Title == title);

        if (formGroupConfiguration != null)
        {
            var existentConfiguration = new AutoFormMemberConfigurationBuilder<TModel>(type, formGroupConfiguration);

            action(existentConfiguration);

            return this;
        }
        
        formGroupConfiguration = new FormGroupConfiguration
        {
            Title = title,
            ComponentConfigurations =
            {
                [displayType] = []
            }
        };

        configuration.Configurations.Add(formGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder<TModel>(type, formGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }

    #endregion

    public AutoFormGroupConfigurationBuilder<TModel> Actions(Action<AutoFormActionConfigurationBuilder> action)
    {
        var autoFormGroupConfigBuilder = new AutoFormActionConfigurationBuilder(configuration.AutoFormActionConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
}