using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class ConsolidatedValidator<T> : AbstractValidator<T>
{
    public ConsolidatedValidator(IEnumerable<InlineValidator<T>> inlineValidators)
    {
        foreach (var validator in inlineValidators)
        {
            Include(validator);
        }
    }
}