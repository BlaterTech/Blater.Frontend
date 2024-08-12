using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Validation;

public class AutoValidationConfigurationBuilder<T> : AbstractValidator<T> where T : BaseDataModel
{
    
}