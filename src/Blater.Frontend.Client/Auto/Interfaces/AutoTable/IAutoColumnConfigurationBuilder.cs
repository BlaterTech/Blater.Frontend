namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

public interface IAutoColumnConfigurationBuilder<T>
{
    IAutoColumnConfigurationBuilder<T> Name(string value);
    IAutoColumnConfigurationBuilder<T> DisableFilter(bool value = false);
    IAutoColumnConfigurationBuilder<T> DisableSortBy(bool value = false);
}   