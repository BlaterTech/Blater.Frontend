using System.Linq.Expressions;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoValidation;

public interface IAutoValidationConfigurationBuilder<T> where T : BaseDataModel
{
    IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression);
}