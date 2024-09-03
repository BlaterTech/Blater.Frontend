using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces.Validator;

public interface IAutoValidatorBuilder<T>
{
    IAutoValidatorBuilder<T> Validate(AutoValidatorConfiguration<T> validator);
    IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, AbstractValidator<T> validator);
}