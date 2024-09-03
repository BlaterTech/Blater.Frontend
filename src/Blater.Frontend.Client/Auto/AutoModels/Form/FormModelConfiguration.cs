namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormModelConfiguration
{
    public string Title { get; set; } = string.Empty;

    public int Spacing { get; set; } = 2;
    
    public AutoAvatarModelConfiguration AutoAvatarModelConfiguration { get; set; } = new();
    public AutoFormActionConfiguration AutoFormActionConfiguration { get; set; } = new();
    
    public List<FormGroupConfiguration> Configurations { get; set; } = [];
}