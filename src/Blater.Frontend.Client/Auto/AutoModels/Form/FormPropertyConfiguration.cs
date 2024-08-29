using Blater.Frontend.Client.Auto.AutoModels.Base;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration : BasePropertyConfiguration
{
    public AutoComponentDisplayType DisplayType { get; set; } = AutoComponentDisplayType.Form;
    
    public AbstractValidator<object>? Validator { get; set; }
}