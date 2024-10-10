using Blater.Frontend.Client.Auto.AutoInterfaces.Base;

using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Base;

public abstract class BaseModelValidator<T> : AbstractValidator<T>, IBaseAutoValidator
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<T>
                                            .CreateWithOptions((T)model,
                                                               x => x
                                                                  .IncludeProperties(propertyName)));

        return result.IsValid ? [] : result.Errors.Select(e => e.ErrorMessage);
    };
}