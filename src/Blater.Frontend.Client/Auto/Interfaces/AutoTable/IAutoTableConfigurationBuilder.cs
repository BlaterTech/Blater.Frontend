using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoTableConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoTableConfigurationBuilder<T> ToTable(string tableName);
    IAutoTableConfigurationBuilder<T> EnableFixedHeader(bool value = true);
    IAutoTableConfigurationBuilder<T> EnableFixedFooter(bool value = true);
    
    IAutoColumnConfigurationBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> propertyExpression);
    
    T GetInstance();
    TProperty GetPropertyValue<TProperty>(Func<T, TProperty> value);
    T SetValue<TProperty>(Func<T, TProperty> setter);
    T SetValue(T setter);
}