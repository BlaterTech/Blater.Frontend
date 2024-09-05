using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Validator;

public interface IAutoValidatorBuilder<T>
{
    IAutoValidatorBuilder<T> TableValidate(AbstractValidator<T> validator);
    IAutoValidatorBuilder<T> DetailsValidate(AbstractValidator<T> validator);
    IAutoValidatorBuilder<T> FormValidate(AbstractValidator<T> validator);
    IAutoValidatorBuilder<T> FormCreateOnlyValidate(AbstractValidator<T> validator);
    IAutoValidatorBuilder<T> FormEditOnlyValidate(AbstractValidator<T> validator);
    IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, AbstractValidator<T> validator);
}