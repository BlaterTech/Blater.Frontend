using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.Form;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder<TModel> : IAutoFormConfigurationBuilder<TModel>
{
    public AutoFormModelConfiguration<TModel> Configuration { get; set; } = new();
    
    public IAutoFormConfigurationBuilder<TModel> Configure(string? title, Action<AutoFormGroupConfigurationBuilder<TModel>> action)
    {
        var autoFormMemberConfigurationBuilder = new AutoFormGroupConfigurationBuilder<TModel>(Configuration);

        action(autoFormMemberConfigurationBuilder);
        
        return this;
    }
}