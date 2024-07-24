using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Frontend.Auto.AutoTable.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Auto.AutoTable.Implementations;

public class ColumnConfigurationBuilder<T>(ColumnConfiguration configuration) : IColumnConfigurationBuilder<T> where T : BaseDataModel
{
    public IColumnConfigurationBuilder<T> HasColumnName(string columnName)
    {
        configuration.HasColumnName = columnName;
        return this;
    }

    public IColumnConfigurationBuilder<T> MaxLength(int value)
    {
        configuration.MaxLength = value;
        return this;
    }

    public IColumnConfigurationBuilder<T> DataFormat(string format)
    {
        configuration.DataFormat = format;
        return this;
    }

    public IColumnConfigurationBuilder<T> Class(string cssClass)
    {
        configuration.Class = cssClass;
        return this;
    }

    public IColumnConfigurationBuilder<T> Style(string style)
    {
        configuration.Style = style;
        return this;
    }

    public IColumnConfigurationBuilder<T> Order(int order)
    {
        configuration.Order = order;
        return this;
    }

    public IColumnConfigurationBuilder<T> MergeColumn(int merge)
    {
        configuration.MergeColumn = merge;
        return this;
    }

    public IColumnConfigurationBuilder<T> IsRequired()
    {
        configuration.IsRequired = true;
        return this;
    }

    public IColumnConfigurationBuilder<T> HasValidation(Action<IValidationBuilder<T>> configure)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> HasValidation(Func<T, bool> validationRule, string errorMessage)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> ComponentType(string style)
    {
        throw new NotImplementedException();
    }
}