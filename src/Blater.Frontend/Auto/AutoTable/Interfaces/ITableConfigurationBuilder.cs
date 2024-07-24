using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoTable.Interfaces;

public interface ITableConfigurationBuilder<TTable> where TTable : BaseDataModel
{
    ITableConfigurationBuilder<TTable> ToTable(string tableName);
    
    IColumnConfigurationBuilder<TTable> Property<TProperty>(Expression<Func<TTable, TProperty>> propertyExpression);
    
    TTable GetInstance();
    TProperty GetPropertyValue<TProperty>(Func<TTable, TProperty> value);
    TTable SetValue<TProperty>(Func<TTable, TProperty> setter);
    TTable SetValue(TTable setter);
}