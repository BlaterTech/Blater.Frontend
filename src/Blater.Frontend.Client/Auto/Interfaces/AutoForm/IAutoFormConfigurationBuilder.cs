using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoFormConfigurationBuilder<T> Form(string formName, Action<IAutoFormPropertyConfigurationBuilder<T>> action);

    IAutoFormConfigurationBuilder<T> FormGroup(string groupName, Action<IAutoFormGroupConfigurationBuilder<T>> action);
}