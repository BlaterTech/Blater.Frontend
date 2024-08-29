using Blater.Frontend.Client.Auto.AutoModels.Base;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormPropertyConfiguration : BasePropertyConfiguration
{
    public AbstractValidator<object>? Validator { get; set; }
}