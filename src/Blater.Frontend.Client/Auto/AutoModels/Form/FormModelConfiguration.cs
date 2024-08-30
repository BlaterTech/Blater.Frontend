namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormModelConfiguration
{
    public string Title { get; set; } = string.Empty;

    public int Spacing { get; set; } = 2;
    
    public AutoAvatarConfiguration AutoAvatarConfiguration { get; set; } = new();
    public AutoActionConfiguration AutoActionConfiguration { get; set; } = new();
    
    public List<FormGroupConfiguration> Configurations { get; set; } = [];
}