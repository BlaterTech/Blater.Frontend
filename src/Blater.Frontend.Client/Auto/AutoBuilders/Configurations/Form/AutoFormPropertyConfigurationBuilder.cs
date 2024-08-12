using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Form;

public class AutoFormPropertyConfigurationBuilder<T, TProperty> : IAutoPropertyConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    private readonly Expression<Func<T, TProperty>> _expression;

    public AutoFormPropertyConfigurationBuilder(Expression<Func<T, TProperty>> expression)
    {
        _expression = expression;
    }
}