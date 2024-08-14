using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoConfigurations;
using Blater.Frontend.Client.Auto.AutoModels.Table;
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

    public IAutoTableConfigurationBuilder<T> ToTable(string tableName)
    {
        _tableConfiguration.ToTable = tableName;
        return this;
    }

    public IAutoTableConfigurationBuilder<T> EnableFixedHeader(bool value = true)
    {
        _tableConfiguration.EnabledFixedHeader = value;
        return this;
    }

    public IAutoTableConfigurationBuilder<T> EnableFixedFooter(bool value = true)
    {
        _tableConfiguration.EnabledFixedFooter = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        var propertyName = GetPropertyName(propertyExpression);

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName not found");
        }

        var currentColumnConfig = new ColumnConfiguration
        {
            Name = propertyName,
            PropertyInfo = typeof(T).GetProperty(propertyName)!
        };
        
        _tableConfiguration.Columns.Add(currentColumnConfig);

        return new AutoColumnConfigurationBuilder<T, TProperty>(currentColumnConfig);
    }

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

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
    {
        return propertyExpression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression { Operand: MemberExpression operand } => operand.Member
                .Name,
            _ => throw new ArgumentException("Invalid expression")
        };
    }
}