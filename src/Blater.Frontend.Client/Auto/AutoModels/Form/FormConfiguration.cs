using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoModels.Form;

public class FormConfiguration<T> : BaseConfiguration where T : BaseDataModel
{
    public IList<FormGroupConfiguration<T>>? GroupsConfigurations { get; set; }

    public IList<FormPropertyConfiguration<T>> PropertyConfigurations { get; set; } = [];
    
    public AbstractValidator<T>? ModelValidator { get; set; }
}