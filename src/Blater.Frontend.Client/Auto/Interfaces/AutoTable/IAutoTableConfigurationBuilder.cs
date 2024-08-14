using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoBuilders.Table;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoTable;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoTableConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoTableConfigurationBuilder<T> Table(string tableName);
    IAutoTableConfigurationBuilder<T> EnableFixedHeader(bool value = true);
    IAutoTableConfigurationBuilder<T> EnableFixedFooter(bool value = true);

    IAutoTableConfigurationBuilder<T> Column<TProperty>(Expression<Func<T, TProperty>> expression, Action<AutoColumnConfigurationBuilder<T, TProperty>> action);
    
    T GetInstance();
    TProperty GetPropertyValue<TProperty>(Func<T, TProperty> value);
    T SetValue<TProperty>(Func<T, TProperty> setter);
    T SetValue(T setter);
}