using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormConfiguration<TModel>(string title)
{
    public string Title { get; } = title;
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration<TModel>> GridConfigurations { get; } = [];
    public Dictionary<AutoComponentDisplayType, AutoAvatarModelConfiguration<TModel>> AvatarConfiguration { get; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFormActionConfiguration<TModel>> ActionConfiguration { get; } = [];
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration<TModel>>> Groups { get; } = [];
}