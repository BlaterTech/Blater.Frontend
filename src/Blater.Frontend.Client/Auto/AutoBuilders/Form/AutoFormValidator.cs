using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormValidator<T> : AbstractValidator<T>
{
    public AutoFormValidator(List<InlineValidator<T>> validators)
    {
        foreach (var validator in validators)
        {
            Include(validator);
        }
    }
}