using Blater.Frontend.Client.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration<T> : BasePropertyConfiguration
{
    public AutoFormScope FormScope { get; set; } = AutoFormScope.All;
    
    public string HelpMessage { get; set; } = string.Empty;
    public string LabelName { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    
    public AbstractValidator<T>? Validator { get; set; }
    
    public bool IsReadOnly { get; set; }
    public bool Disable { get; set; }
}