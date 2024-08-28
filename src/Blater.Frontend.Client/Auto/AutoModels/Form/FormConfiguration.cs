namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormConfiguration
{
    public string Name { get; set; } = string.Empty;

    public int Spacing { get; set; } = 2;
    
    public FormAvatarConfiguration FormAvatarConfiguration { get; set; } = new();
    public FormActionConfiguration FormActionConfiguration { get; set; } = new();

    public IList<FormGroupConfiguration> GroupsConfigurations { get; set; } = [];
}