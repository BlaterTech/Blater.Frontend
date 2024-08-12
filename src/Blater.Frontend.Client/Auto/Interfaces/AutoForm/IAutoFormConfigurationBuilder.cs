using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

[SuppressMessage("Naming", "CA1716:Identificadores não devem corresponder a palavras-chave")]
public interface IAutoFormConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> expression);
}