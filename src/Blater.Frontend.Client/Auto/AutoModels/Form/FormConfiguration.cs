namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormConfiguration
{
    public string Name { get; set; } = string.Empty;

    public int Spacing { get; set; } = 2;
    
    public AutoAvatarConfiguration AutoAvatarConfiguration { get; set; } = new();
    public AutoActionConfiguration AutoActionConfiguration { get; set; } = new();

    public IList<FormGroupConfiguration> GroupsConfigurations { get; set; } = [];
}