using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoTableConfigurationBuilder<TTable> where TTable : BaseDataModel
{
    IAutoTableConfigurationBuilder<TTable> ToTable(string tableName);
    IAutoTableConfigurationBuilder<TTable> EnableFixedHeader(bool value = true);
    IAutoTableConfigurationBuilder<TTable> EnableFixedFooter(bool value = true);
    
    IAutoColumnConfigurationBuilder<TTable> Property<TProperty>(Expression<Func<TTable, TProperty>> propertyExpression);
    
    TTable GetInstance();
    TProperty GetPropertyValue<TProperty>(Func<TTable, TProperty> value);
    TTable SetValue<TProperty>(Func<TTable, TProperty> setter);
    TTable SetValue(TTable setter);
}