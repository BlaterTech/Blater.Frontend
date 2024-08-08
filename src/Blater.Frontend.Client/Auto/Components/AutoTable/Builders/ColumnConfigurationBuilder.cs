using Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;
using Blater.Frontend.Client.Auto.Components.AutoTable.Models;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Builders;

public class ColumnConfigurationBuilder<T>(ColumnConfiguration configuration) : IColumnConfigurationBuilder<T> where T : BaseDataModel
{
    public IColumnConfigurationBuilder<T> Name(string columnName)
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

    public IColumnConfigurationBuilder<T> DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IColumnConfigurationBuilder<T> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IColumnConfigurationBuilder<T> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
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
    
    public IColumnConfigurationBuilder<T> LocalizationId(string style)
    {
        throw new NotImplementedException();
    }

    public IColumnConfigurationBuilder<T> ComponentType(string style)
    {
        throw new NotImplementedException();
    }
}