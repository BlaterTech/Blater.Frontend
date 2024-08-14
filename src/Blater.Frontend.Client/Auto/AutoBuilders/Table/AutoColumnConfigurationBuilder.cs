using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.AutoForm;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Models.Bases;
using Microsoft.AspNetCore.Components;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoColumnConfigurationBuilder<T, TProperty>(ColumnConfiguration configuration) : IAutoColumnConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T, TProperty> DataFormat(string value)
    {
        configuration.DataFormat = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssClass(string value)
    {
        configuration.CssClass = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> CssStyle(string value)
    {
        configuration.CssStyle = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Order(int value)
    {
        configuration.Order = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> LocalizationId(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> ComponentType(string value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> Disable(bool value = false)
    {
        configuration.DisableColumn = value;
        return this;
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnValueChanged(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnClick(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }

    public IAutoPropertyConfigurationBuilder<T, TProperty> OnParameterSet(EventCallback<TProperty> value)
    {
        throw new NotImplementedException();
    }
    
    public IAutoColumnConfigurationBuilder<T, TProperty> Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T, TProperty> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T, TProperty> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }
}