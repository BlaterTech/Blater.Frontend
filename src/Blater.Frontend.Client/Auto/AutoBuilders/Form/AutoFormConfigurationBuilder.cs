using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly FormConfiguration<T> _formConfiguration;

    public AutoFormConfigurationBuilder(FormConfiguration<T> formConfiguration)
    {
        _formConfiguration = formConfiguration;
    }

    public AutoFormConfigurationBuilder<T> Form(string formName, Action<AutoFormMemberConfigurationBuilder<T>> action)
    {
        throw new NotImplementedException();
    }

    public AutoFormConfigurationBuilder<T> FormGroup(string groupName, Action<AutoFormGroupConfigurationBuilder<T>> action)
    {
        throw new NotImplementedException();
    }
}