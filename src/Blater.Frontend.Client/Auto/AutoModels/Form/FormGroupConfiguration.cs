using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormGroupConfiguration<T>
{
    public string Title { get; set; } = $"Blater-AutoFormGroup-{nameof(T)}";
    
    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];

    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
}