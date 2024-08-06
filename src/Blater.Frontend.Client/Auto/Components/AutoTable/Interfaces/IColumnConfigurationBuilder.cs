using System.Diagnostics.CodeAnalysis;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IColumnConfigurationBuilder<out T> where T : BaseDataModel
{
    IColumnConfigurationBuilder<T> HasColumnName(string columnName);
    IColumnConfigurationBuilder<T> MaxLength(int value);
    IColumnConfigurationBuilder<T> DataFormat(string format);
    IColumnConfigurationBuilder<T> Class(string cssClass);
    IColumnConfigurationBuilder<T> Style(string style);
    IColumnConfigurationBuilder<T> Order(int order);   
    IColumnConfigurationBuilder<T> MergeColumn(int merge);
    IColumnConfigurationBuilder<T> DisabledColumn();
    IColumnConfigurationBuilder<T> DisabledFilter();
    IColumnConfigurationBuilder<T> DisabledSortBy();
    IColumnConfigurationBuilder<T> HasValidation(Action<IValidationBuilder<T>> configure);
    IColumnConfigurationBuilder<T> HasValidation(Func<T, bool> validationRule, string errorMessage);
    IColumnConfigurationBuilder<T> ComponentType(string style);
    IColumnConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> callback);
    IColumnConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> callback);
    IColumnConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> callback);
}   