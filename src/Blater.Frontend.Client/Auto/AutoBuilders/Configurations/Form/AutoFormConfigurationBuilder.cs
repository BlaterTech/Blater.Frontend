using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Configurations.Form;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Frontend.Client.Auto.Interfaces.AutoValidation;
using Blater.Models.Bases;
using FluentValidation;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Form;

public abstract class AutoFormConfigurationBuilder<T> :
    IAutoValidationConfigurationBuilder<T>,
    IAutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly FormConfiguration<T> _formConfiguration;

    public AutoFormConfigurationBuilder(FormConfiguration<T> formConfiguration)
    {
        _formConfiguration = formConfiguration;
    }

    public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        _formConfiguration.ModelValidator ??= new InlineValidator<T>();

        return _formConfiguration.ModelValidator.RuleFor(expression);
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        return new AutoFormPropertyConfigurationBuilder<T, TProperty>(expression);
    }
}