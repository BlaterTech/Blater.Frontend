using Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Form;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormConfiguration<T> where T : BaseDataModel
{
    void Configure(AutoFormConfigurationBuilder<T> builder);
}