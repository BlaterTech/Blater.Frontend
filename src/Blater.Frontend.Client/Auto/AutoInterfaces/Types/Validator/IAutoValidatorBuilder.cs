using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Validator;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;

public interface IAutoValidatorBuilder<T>
{
    IAutoValidatorBuilder<T> TableValidate(Action<ModelValidator<T>> action);
    IAutoValidatorBuilder<T> DetailsValidate(Action<ModelValidator<T>> action);
    IAutoValidatorBuilder<T> FormValidate(Action<ModelValidator<T>> action);
    IAutoValidatorBuilder<T> FormCreateOnlyValidate(Action<ModelValidator<T>> action);
    IAutoValidatorBuilder<T> FormEditOnlyValidate(Action<ModelValidator<T>> action);
    IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, Action<ModelValidator<T>> action);
}