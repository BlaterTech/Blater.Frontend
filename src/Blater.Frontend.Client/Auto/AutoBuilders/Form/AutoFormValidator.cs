using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormValidator : AbstractValidator<object>
{
    public AutoFormValidator(List<InlineValidator<object>> validators)
    {
        foreach (var validator in validators)
        {
            Include(validator);
        }
    }
}