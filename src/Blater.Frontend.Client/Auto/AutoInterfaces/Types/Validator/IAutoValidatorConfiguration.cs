using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoInterfaces.Types.Validator;

public interface IAutoValidatorConfiguration<T>
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await Validator
           .ValidateAsync(ValidationContext<T>
                             .CreateWithOptions((T)model,
                                                x => x
                                                   .IncludeProperties(propertyName)));
        return result.IsValid ? [] : result.Errors.Select(e => e.ErrorMessage);
    };
    
    InlineValidator<T> Validator { get; set; }
}