using Blater.Frontend.Client.Auto.AutoConfigurations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableConfigurationBuilder<T>
    : IAutoTableConfigurationBuilder<T>
    where T : BaseDataModel
{
    private readonly TableConfiguration<T> _tableConfiguration;
    public AutoTableConfigurationBuilder(TableConfiguration<T> tableConfiguration)
    {
        _tableConfiguration = tableConfiguration;
        TableConfigurations<T>.Configurations.Add(typeof(T), tableConfiguration);
    }
    
    public IAutoTableConfigurationBuilder<T> Table(string value, Action<IAutoMemberConfigurationBuilder<T>> action)
    {
        throw new NotImplementedException();
    }

    public IAutoTableConfigurationBuilder<T> EnableFixedHeader(bool value = true)
    {
        _tableConfiguration.EnableFixedHeader = value;
        return this;
    }

    public IAutoTableConfigurationBuilder<T> EnableFixedFooter(bool value = true)
    {
        _tableConfiguration.EnableFixedFooter = value;
        return this;
    }
    
    /*public IAutoTableConfigurationBuilder<T> Column<TProperty>(Expression<Func<T, TProperty>> expression, Action<AutoColumnConfigurationBuilder<T, TProperty>> action)
    {
        var propertyName = expression.GetPropertyName();

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName not found");
        }

        var currentColumnConfig = new ColumnConfiguration<T>()
        {
            Name = propertyName,
            PropertyInfo = typeof(T).GetProperty(propertyName)!
        };

        _tableConfiguration.Columns.Add(currentColumnConfig);
        
        var columnBuilder = new AutoColumnConfigurationBuilder<T, TProperty>(expression, currentColumnConfig);
        
        action(columnBuilder);
        
        return this;
    }*/

    public T GetInstance()
    {
        throw new NotImplementedException();
    }

    public TProperty GetPropertyValue<TProperty>(Func<T, TProperty> value)
    {
        throw new NotImplementedException();
    }

    public TProperty GetPropertyValue<TProperty>(Func<T> value)
    {
        throw new NotImplementedException();
    }

    public T SetValue<TProperty>(Func<T, TProperty> setter)
    {
        throw new NotImplementedException();
    }

    public T SetValue(T setter)
    {
        throw new NotImplementedException();
    }
}