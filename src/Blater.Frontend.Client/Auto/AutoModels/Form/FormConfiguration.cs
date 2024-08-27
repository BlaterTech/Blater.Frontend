namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormConfiguration
{
    public string Name { get; set; } = string.Empty;
    public IList<FormGroupConfiguration>? GroupsConfigurations { get; set; }

    public IList<FormPropertyConfiguration> PropertyConfigurations { get; set; } = [];
}