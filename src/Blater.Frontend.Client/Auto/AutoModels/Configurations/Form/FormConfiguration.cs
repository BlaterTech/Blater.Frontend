using Blater.Models.Bases;
using FluentValidation;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Configurations.Form;

public abstract class FormConfiguration<T> where T : BaseDataModel
{
    public string Title { get; set; } = $"Blater-AutoForm-{nameof(T)}";

    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];

    public IList<FormGroupConfiguration<T>> Groups { get; set; } = [];
    
    public AbstractValidator<T>? ModelValidator { get; set; }
}

public class FormGroupConfiguration<T>
{
    public string Title { get; set; } = $"Blater-AutoFormGroup-{nameof(T)}";
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];
}

public class FormPropertiesConfiguration<T>
{
    public string Label { get; set; } = $"Blater-AutoLabel-{nameof(T)}";
    public string Placeholder { get; set; } = $"Blater-AutoPlaceholder-{nameof(T)}";
    public bool DisableProperty { get; set; }
    public bool ReadOnlyProperty { get; set; }
}