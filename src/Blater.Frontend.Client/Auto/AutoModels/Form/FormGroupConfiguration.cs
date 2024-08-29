using Blater.Frontend.Client.Auto.AutoModels.Enumerations;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration
{
    public string Title { get; set; } = string.Empty;
    public string SubTitle { get; set; } = string.Empty;
    
    public Dictionary<AutoComponentDisplayType, List<FormPropertyConfiguration>> Properties { get; set; } = [];
}