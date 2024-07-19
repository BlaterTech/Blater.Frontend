using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Auto.AutoTable.Interfaces;

public interface ITableConfigurationBuilder<TTable> where TTable : BaseDataModel
{
    ITableConfigurationBuilder<TTable> ToTable(string tableName);
    
    IColumnConfigurationBuilder Property<TProperty>(Expression<Func<TTable, TProperty>> propertyExpression);
}