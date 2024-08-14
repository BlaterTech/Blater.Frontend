using Blater.Models.Bases;
using FluentValidation;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public abstract class FormConfiguration<T> where T : BaseDataModel
{
    public string Title { get; set; } = $"Blater-AutoForm-{nameof(T)}";

    public Dictionary<Breakpoint, int> Breakpoints { get; set; } = [];

    public IList<FormGroupConfiguration<T>>? Groups { get; set; }

    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
    
    public AbstractValidator<T>? ModelValidator { get; set; }
}