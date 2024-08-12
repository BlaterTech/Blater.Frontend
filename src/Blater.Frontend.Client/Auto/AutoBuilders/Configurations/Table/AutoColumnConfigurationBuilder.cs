using Blater.Frontend.Client.Auto.AutoModels.Configurations.Table;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Configurations.Table;

public class AutoColumnConfigurationBuilder<T>(ColumnConfiguration configuration) : IAutoColumnConfigurationBuilder<T> where T : BaseDataModel
{
    public IAutoColumnConfigurationBuilder<T> Name(string columnName)
    {
        configuration.HasColumnName = columnName;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> MaxLength(int value)
    {
        configuration.MaxLength = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> DataFormat(string format)
    {
        configuration.DataFormat = format;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> Class(string cssClass)
    {
        configuration.Class = cssClass;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> Style(string style)
    {
        configuration.Style = style;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> Order(int order)
    {
        configuration.Order = order;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> MergeColumn(int merge)
    {
        configuration.MergeColumn = merge;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> DisableColumn(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T> OnValueChanged<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }

    public IAutoColumnConfigurationBuilder<T> OnClick<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }

    public IAutoColumnConfigurationBuilder<T> OnParameterSet<TProperty>(EventCallback<TProperty> callback)
    {
        throw new NotImplementedException();
    }
    
    public IAutoColumnConfigurationBuilder<T> LocalizationId(string style)
    {
        throw new NotImplementedException();
    }

    public IAutoColumnConfigurationBuilder<T> ComponentType(string style)
    {
        throw new NotImplementedException();
    }
}