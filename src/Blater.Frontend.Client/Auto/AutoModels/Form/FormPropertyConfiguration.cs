using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration : BasePropertyConfiguration
{
    public AutoFormDisplayType AutoFormDisplayType { get; set; } = AutoFormDisplayType.All;
    
    public string HelpMessage { get; set; } = string.Empty;
    public string LabelName { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    
    public AbstractValidator<object>? Validator { get; set; }
    
    public bool IsReadOnly { get; set; }
    public bool Disable { get; set; }
}