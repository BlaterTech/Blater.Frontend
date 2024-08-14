using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

public interface IAutoColumnConfigurationBuilder<T, TProperty> : IAutoPropertyConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    IAutoColumnConfigurationBuilder<T, TProperty> Name(string value);
    IAutoColumnConfigurationBuilder<T, TProperty> DisableFilter(bool value = false);
    IAutoColumnConfigurationBuilder<T, TProperty> DisableSortBy(bool value = false);
}   