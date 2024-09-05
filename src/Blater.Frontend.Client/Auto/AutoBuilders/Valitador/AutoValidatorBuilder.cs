using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Validator;
using Blater.Frontend.Client.Auto.Interfaces.Types.Validator;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Valitador;

public class AutoValidatorBuilder<T> : IAutoValidatorBuilder<T>
{
    private readonly AutoValidatorConfiguration<T> _configuration;
    public AutoValidatorBuilder(object instance)
    {
        if (instance is IAutoValidatorConfiguration<T> configuration)
        {
            _configuration = configuration.ValidatorConfiguration;
        }
        else
        {
            throw new InvalidCastException($"Instance is not implement IAutoValidatorConfiguration<{typeof(T).Name}>");
        }
    }

    public IAutoValidatorBuilder<T> TableValidate(AbstractValidator<T> validator)
        => Validate(AutoComponentDisplayType.Table, validator);

    public IAutoValidatorBuilder<T> DetailsValidate(AbstractValidator<T> validator)
        => Validate(AutoComponentDisplayType.Details, validator);

    public IAutoValidatorBuilder<T> FormValidate(AbstractValidator<T> validator)
        => Validate(AutoComponentDisplayType.Form, validator);

    public IAutoValidatorBuilder<T> FormCreateOnlyValidate(AbstractValidator<T> validator)
        => Validate(AutoComponentDisplayType.FormCreate, validator);

    public IAutoValidatorBuilder<T> FormEditOnlyValidate(AbstractValidator<T> validator)
        => Validate(AutoComponentDisplayType.FormEdit, validator);

    public IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, AbstractValidator<T> validator)
    {
        if (_configuration.Validators.TryGetValue(displayType, out _))
        {
            _configuration.Validators[displayType] = validator;
        }
        else
        {
            _configuration.Validators.TryAdd(displayType, validator);
        }
        
        return this;
    }
}