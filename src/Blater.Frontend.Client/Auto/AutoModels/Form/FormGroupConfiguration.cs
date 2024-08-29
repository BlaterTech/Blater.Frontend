using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    
    public AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Form;
    public List<FormPropertyConfiguration> Properties { get; set; } = [];
}