using Blater.Frontend.Client.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public AutoFormGroupScope FormGroupScope { get; set; } = AutoFormGroupScope.All;
    public IList<FormPropertyConfiguration> PropertyConfigurations { get; set; } = [];
}