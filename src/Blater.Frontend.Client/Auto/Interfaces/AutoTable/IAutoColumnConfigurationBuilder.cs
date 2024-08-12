using System.Diagnostics.CodeAnalysis;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoColumnConfigurationBuilder<out T> where T : BaseDataModel
{
    IAutoColumnConfigurationBuilder<T> Name(string columnName);
    IAutoColumnConfigurationBuilder<T> MaxLength(int value);
    IAutoColumnConfigurationBuilder<T> DataFormat(string format);
    IAutoColumnConfigurationBuilder<T> Class(string cssClass);
    IAutoColumnConfigurationBuilder<T> Style(string style);
    IAutoColumnConfigurationBuilder<T> Order(int order);   
    IAutoColumnConfigurationBuilder<T> DisableColumn(bool value = false);
    IAutoColumnConfigurationBuilder<T> DisableFilter(bool value = false);
    IAutoColumnConfigurationBuilder<T> DisableSortBy(bool value = false);
    IAutoColumnConfigurationBuilder<T> ComponentType(string style);
    IAutoColumnConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> callback);
    IAutoColumnConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> callback);
    IAutoColumnConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> callback);
}   