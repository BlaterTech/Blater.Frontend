using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces.Types.Validator;

public interface IAutoValidatorBuilder<T>
{
    IAutoValidatorBuilder<T> TableValidate(ModelValidator<T> validator);
    IAutoValidatorBuilder<T> DetailsValidate(ModelValidator<T> validator);
    IAutoValidatorBuilder<T> FormValidate(ModelValidator<T> validator);
    IAutoValidatorBuilder<T> FormCreateOnlyValidate(ModelValidator<T> validator);
    IAutoValidatorBuilder<T> FormEditOnlyValidate(ModelValidator<T> validator);
    IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, ModelValidator<T> validator);
}