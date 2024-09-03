using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder<TModel>(AutoFormModelConfiguration<TModel> configuration) : IAutoFormConfigurationBuilder<TModel>
{
    public IAutoFormConfigurationBuilder<TModel> Configure(AutoFormModelConfiguration<TModel> model)
    {
        configuration = model;

        return this;
    }
    
    public IAutoFormConfigurationBuilder<TModel> Configure(string? title, Action<AutoFormGroupConfigurationBuilder<TModel>> action)
    {
        var autoFormMemberConfigurationBuilder = new AutoFormGroupConfigurationBuilder<TModel>(configuration);

        action(autoFormMemberConfigurationBuilder);
        
        return this;
    }
}