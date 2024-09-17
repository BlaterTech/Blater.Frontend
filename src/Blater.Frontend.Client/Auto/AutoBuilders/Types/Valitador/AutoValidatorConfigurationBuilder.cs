using Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;
using Blater.Frontend.Client.Auto.AutoModels.Enumerations;
using Blater.Frontend.Client.Auto.AutoModels.Types.Validator;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Types.Valitador;

public class AutoValidatorConfigurationBuilder<T> : AbstractValidator<T>, IAutoValidatorBuilder<T>
{
    private readonly AutoValidatorConfiguration<T> _configuration;
    public AutoValidatorConfigurationBuilder(object instance)
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

    public IAutoValidatorBuilder<T> TableValidate(Action<ModelValidator<T>> action)
        => Validate(AutoComponentDisplayType.Table, action);

    public IAutoValidatorBuilder<T> DetailsValidate(Action<ModelValidator<T>> action)
        => Validate(AutoComponentDisplayType.Details, action);

    public IAutoValidatorBuilder<T> FormValidate(Action<ModelValidator<T>> action)
        => Validate(AutoComponentDisplayType.Form, action);

    public IAutoValidatorBuilder<T> FormCreateOnlyValidate(Action<ModelValidator<T>> action)
        => Validate(AutoComponentDisplayType.FormCreate, action);

    public IAutoValidatorBuilder<T> FormEditOnlyValidate(Action<ModelValidator<T>> action)
        => Validate(AutoComponentDisplayType.FormEdit, action);

    public IAutoValidatorBuilder<T> Validate(AutoComponentDisplayType displayType, Action<ModelValidator<T>> action)
    {
        if (_configuration.Validators.TryGetValue(displayType, out var validator))
        {
            _configuration.Validators[displayType] = validator;
        }
        else
        {
            validator = new ModelValidator<T>();
            _configuration.Validators.TryAdd(displayType, validator);
        }
        
        action.Invoke(validator);
        
        return this;
    }
}