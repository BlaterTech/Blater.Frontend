using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Types.Form;

public class AutoFormConfiguration<TModel>(string title)
{
    public string Title { get; set; } = title;
    
    public Dictionary<AutoComponentDisplayType, AutoGridConfiguration<TModel>> GridConfigurations { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoAvatarModelConfiguration<TModel>> AvatarConfiguration { get; set; } = [];
    public Dictionary<AutoComponentDisplayType, AutoFormActionConfiguration<TModel>> ActionConfiguration { get; set; } = [];
    
    public Dictionary<AutoComponentDisplayType, List<AutoFormGroupConfiguration<TModel>>> Groups { get; set; } = [];
}