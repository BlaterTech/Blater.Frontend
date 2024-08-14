using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Validation;

public class AutoValidationConfigurationBuilder<T> : AbstractValidator<T> where T : BaseDataModel
{
    
}