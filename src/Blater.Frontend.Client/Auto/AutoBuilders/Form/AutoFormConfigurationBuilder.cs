using Blater.Frontend.Client.Auto.AutoModels.Form;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Form;

public class AutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    private readonly FormConfiguration<T> _formConfiguration;

    public AutoFormConfigurationBuilder(FormConfiguration<T> formConfiguration)
    {
        _formConfiguration = formConfiguration;
        AutoConfigurations<T, FormConfiguration<T>>.Configurations.Add(typeof(T), _formConfiguration);
    }

    public AutoFormConfigurationBuilder<T> Form(string formName, Action<AutoFormMemberConfigurationBuilder<T>> action)
    {
        _formConfiguration.Name = formName;

        var autoFormMemberConfigurationBuilder = new AutoFormMemberConfigurationBuilder<T>(_formConfiguration);

        action(autoFormMemberConfigurationBuilder);

        return this;
    }
}