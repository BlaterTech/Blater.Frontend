using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoTable.Interfaces;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface ITableConfigurationBuilder<TTable> where TTable : BaseDataModel
{
    ITableConfigurationBuilder<TTable> ToTable(string tableName);
    ITableConfigurationBuilder<TTable> EnabledFixedHeader();
    ITableConfigurationBuilder<TTable> EnabledFixedFooter();
    
    IColumnConfigurationBuilder<TTable> Property<TProperty>(Expression<Func<TTable, TProperty>> propertyExpression);
    
    TTable GetInstance();
    TProperty GetPropertyValue<TProperty>(Func<TTable, TProperty> value);
    TTable SetValue<TProperty>(Func<TTable, TProperty> setter);
    TTable SetValue(TTable setter);
}