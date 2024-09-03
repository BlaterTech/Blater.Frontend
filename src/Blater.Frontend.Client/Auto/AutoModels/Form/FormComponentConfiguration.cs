using System.Reflection;
using Blater.Frontend.Client.Auto.AutoModels.Base;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormComponentConfiguration : BaseComponentConfiguration
{
    public Dictionary<string, object> InlineValidator { get; set; } = [];


    public void SetValidator<T>(PropertyInfo info, InlineValidator<T> validator)
    {
        InlineValidator.TryAdd(info.PropertyType.Name, validator);
    }

    public InlineValidator<T>? GetValidator<T>(string propName)
    {
        if (InlineValidator.TryGetValue(propName, out var validator))
        {
            if (validator is InlineValidator<T> typedValidator)
            {
                return typedValidator;
            }
            throw new InvalidCastException($"Cannot cast the validator to AbstractValidator");
        }
        
        return null;
    }
}