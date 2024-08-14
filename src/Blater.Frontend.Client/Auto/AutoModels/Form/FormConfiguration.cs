using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public abstract class FormConfiguration<T> where T : BaseDataModel
{
    public string Title { get; set; } = $"Blater-AutoForm-{nameof(T)}";

    public IList<FormGroupConfiguration<T>>? GroupsConfigurations { get; set; }

    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
    
    public AbstractValidator<T>? ModelValidator { get; set; }
}