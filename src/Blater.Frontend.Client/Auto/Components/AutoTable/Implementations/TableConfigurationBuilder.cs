using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.Components.AutoTable.Interfaces;
using Blater.Frontend.Client.Auto.Components.AutoTable.Models;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Components.AutoTable.Implementations;

public class TableConfigurationBuilder<TTable>
    : ITableConfigurationBuilder<TTable>
    where TTable : BaseDataModel
{
    private readonly TableConfiguration<TTable> _tableConfiguration;
    public TableConfigurationBuilder(TableConfiguration<TTable> tableConfiguration)
    {
        _tableConfiguration = tableConfiguration;
        TableConfigurations<TTable>.Configurations.Add(typeof(TTable), tableConfiguration);
    }

    public ITableConfigurationBuilder<TTable> ToTable(string tableName)
    {
        _tableConfiguration.ToTable = tableName;
        return this;
    }

    public ITableConfigurationBuilder<TTable> EnableFixedHeader(bool value = true)
    {
        _tableConfiguration.EnabledFixedHeader = value;
        return this;
    }

    public ITableConfigurationBuilder<TTable> EnableFixedFooter(bool value = true)
    {
        _tableConfiguration.EnabledFixedFooter = value;
        return this;
    }

    public bool Test { get => _tableConfiguration.EnabledFixedFooter ; set => _tableConfiguration.EnabledFixedFooter = value; }

    public IColumnConfigurationBuilder<TTable> Property<TProperty>(
        Expression<Func<TTable, TProperty>> propertyExpression)
    {
        var propertyName = GetPropertyName(propertyExpression);

        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new InvalidOperationException("PropertyName not found");
        }

        var currentColumnConfig = new ColumnConfiguration
        {
            HasColumnName = propertyName,
            PropertyInfo = typeof(TTable).GetProperty(propertyName)!
        };
        
        _tableConfiguration.Columns.Add(currentColumnConfig);

        return new ColumnConfigurationBuilder<TTable>(currentColumnConfig);
    }

    public TTable GetInstance()
    {
        throw new NotImplementedException();
    }

    public TProperty GetPropertyValue<TProperty>(Func<TTable, TProperty> value)
    {
        throw new NotImplementedException();
    }

    public TTable SetValue<TProperty>(Func<TTable, TProperty> setter)
    {
        throw new NotImplementedException();
    }

    public TTable SetValue(TTable setter)
    {
        throw new NotImplementedException();
    }

    private static string GetPropertyName<TProperty>(Expression<Func<TTable, TProperty>> propertyExpression)
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