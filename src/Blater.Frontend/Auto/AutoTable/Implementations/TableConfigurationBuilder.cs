﻿using System.Linq.Expressions;
using Blater.Frontend.Auto.AutoTable.Interfaces;
using Blater.Frontend.Auto.AutoTable.Models;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoTable.Implementations;

public class TableConfigurationBuilder<TTable>(TableConfiguration<TTable> tableConfiguration)
    : ITableConfigurationBuilder<TTable>
    where TTable : BaseDataModel
{
    private ColumnConfiguration? _currentColumnConfig;

    public ITableConfigurationBuilder<TTable> ToTable(string tableName)
    {
        tableConfiguration.ToTable = tableName;
        return this;
    }

    public IColumnConfigurationBuilder<TTable> Property<TProperty>(
        Expression<Func<TTable, TProperty>> propertyExpression)
    {
        var propertyName = GetPropertyName(propertyExpression);
        var columnConfiguration = tableConfiguration.Columns.FirstOrDefault(c => c.HasColumnName == propertyName);

        if (columnConfiguration != null) return new ColumnConfigurationBuilder<TTable>(columnConfiguration);
        
        _currentColumnConfig = new ColumnConfiguration { HasColumnName = propertyName };
        tableConfiguration.Columns.Add(_currentColumnConfig);

        return new ColumnConfigurationBuilder<TTable>(_currentColumnConfig);
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