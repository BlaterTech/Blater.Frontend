using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using Blater.Frontend.Client.Auto.Interfaces.Validator;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Valitador;

public class AutoValidatorBuilder<T>(AutoValidatorConfiguration<T> configuration) : IAutoValidatorBuilder<T>
{
    public IAutoValidatorBuilder<T> Validate(AutoValidatorConfiguration<T> validator)
    {
        configuration = validator;
        return this;
    }
    
    public IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, AbstractValidator<T> validator)
    {
        configuration.Validators.TryAdd(displayType, validator);
        return this;
    }
}