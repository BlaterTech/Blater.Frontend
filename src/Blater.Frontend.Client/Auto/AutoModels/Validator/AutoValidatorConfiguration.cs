using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Validator;

public class AutoValidatorConfiguration<TModel>
{
    public Dictionary<AutoComponentDisplayType, AbstractValidator<TModel>> Validators { get; set; } = [];
}