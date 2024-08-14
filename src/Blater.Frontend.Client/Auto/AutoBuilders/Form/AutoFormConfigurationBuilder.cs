using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public abstract class AutoFormConfigurationBuilder<T> : IAutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly FormConfiguration<T> _formConfiguration;

    public AutoFormConfigurationBuilder(FormConfiguration<T> formConfiguration)
    {
        _formConfiguration = formConfiguration;
    }

    public IAutoFormConfigurationBuilder<T> Form(string formName, Action<IAutoFormPropertyConfigurationBuilder<T>> action)
    {
        throw new NotImplementedException();
    }

    public IAutoFormConfigurationBuilder<T> FormGroup(string groupName, Action<IAutoFormGroupConfigurationBuilder<T>> action)
    {
        throw new NotImplementedException();
    }
}