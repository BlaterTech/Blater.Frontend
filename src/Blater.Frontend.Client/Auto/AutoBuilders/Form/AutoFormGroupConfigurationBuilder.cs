using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormGroupConfigurationBuilder<TModel>(AutoFormModelConfiguration<TModel> configuration)
{
    private static Type ModelType => typeof(TModel);

    #region Group

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupAvatar(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.Form, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupAvatarCreateOnly(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.FormCreate, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupAvatarEditOnly(Action<AutoFormAvatarConfigurationBuilder> action)
        => AddGroupAvatar(AutoComponentDisplayType.FormEdit, action);
    
    public AutoFormGroupConfigurationBuilder<TModel> AddGroup(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.Form, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupCreateOnly(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormCreate, action);

    public AutoFormGroupConfigurationBuilder<TModel> AddGroupEditOnly(Action<AutoFormMemberConfigurationBuilder<TModel>> action)
        => AddGroup(AutoComponentDisplayType.FormEdit, action);

    #endregion

    #region Actions

    public AutoFormGroupConfigurationBuilder<TModel> Actions(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.Form, action);

    public AutoFormGroupConfigurationBuilder<TModel> ActionsCreateOnly(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.FormCreate, action);

    public AutoFormGroupConfigurationBuilder<TModel> ActionsEditOnly(Action<AutoFormActionConfigurationBuilder> action)
        => Actions(AutoComponentDisplayType.FormEdit, action);

    #endregion
    
    private AutoFormGroupConfigurationBuilder<TModel> AddGroup(AutoComponentDisplayType displayType, Action<AutoFormMemberConfigurationBuilder<TModel>> action)
    {
        var countGroup = configuration.GroupConfigurations.Count;
        var title = $"Auto{displayType.ToString()}Group{countGroup}-Title-{ModelType.Name}";
        var formGroupConfiguration = configuration.GroupConfigurations.FirstOrDefault(x => x.Title == title);

        if (formGroupConfiguration != null)
        {
            var existentConfiguration = new AutoFormMemberConfigurationBuilder<TModel>(ModelType, formGroupConfiguration);

            action(existentConfiguration);

            return this;
        }
        
        formGroupConfiguration = new AutoFormGroupConfiguration
        {
            Title = title,
            ComponentConfigurations =
            {
                [displayType] = []
            }
        };

        configuration.GroupConfigurations.Add(formGroupConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormMemberConfigurationBuilder<TModel>(ModelType, formGroupConfiguration);

        action(autoFormGroupConfigBuilder);

        return this;
    }
    
    private AutoFormGroupConfigurationBuilder<TModel> AddGroupAvatar(AutoComponentDisplayType displayType, Action<AutoFormAvatarConfigurationBuilder> action)
    {
        if (configuration.AvatarConfiguration.TryGetValue(displayType, out var autoFormActionConfiguration))
        {
            var existentFormGroupConfiguration = new AutoFormAvatarConfigurationBuilder(autoFormActionConfiguration);

            action(existentFormGroupConfiguration);
            
            return this;
        }

        autoFormActionConfiguration = new AutoAvatarModelConfiguration();
        configuration.AvatarConfiguration.TryAdd(displayType, autoFormActionConfiguration);

        var autoFormGroupConfigBuilder = new AutoFormAvatarConfigurationBuilder(autoFormActionConfiguration);

        action(autoFormGroupConfigBuilder);
            
        return this;
    }

    private AutoFormGroupConfigurationBuilder<TModel> Actions(AutoComponentDisplayType displayType, Action<AutoFormActionConfigurationBuilder> action)
    {
        if (configuration.ActionConfiguration.TryGetValue(displayType, out var actionConfiguration))
        {
            var existentFormActionConfiguration = new AutoFormActionConfigurationBuilder(actionConfiguration);

            action(existentFormActionConfiguration);
            
            return this;
        }
        
        actionConfiguration = new AutoFormActionConfiguration();
        configuration.ActionConfiguration.TryAdd(displayType, actionConfiguration);
        
        var formActionConfiguration = new AutoFormActionConfigurationBuilder(actionConfiguration);

        action(formActionConfiguration);

        return this;
    }
}