namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

public interface IAutoColumnConfigurationBuilder<T, TProperty>
{
    IAutoColumnConfigurationBuilder<T, TProperty> Name(string value);
    IAutoColumnConfigurationBuilder<T, TProperty> DisableFilter(bool value = false);
    IAutoColumnConfigurationBuilder<T, TProperty> DisableSortBy(bool value = false);
}   