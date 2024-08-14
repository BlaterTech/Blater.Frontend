using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

public interface IAutoTableConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoTableConfigurationBuilder<T> Table(string value, Action<IAutoMemberConfigurationBuilder<T>> action);
    IAutoTableConfigurationBuilder<T> EnableFixedHeader(bool value = true);
    IAutoTableConfigurationBuilder<T> EnableFixedFooter(bool value = true);
}