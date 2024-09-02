using Blater.Frontend.Client.Auto.AutoModels.Base;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormComponentConfiguration : BaseComponentConfiguration
{
    public InlineValidator<object>? InlineValidator { get; set; }
}