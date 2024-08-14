using System.Linq.Expressions;
using Blater.Frontend.Client.Enumerations;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormPropertyConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T, TProperty> AddPartner<TProperty>(Expression<Func<T, TProperty>> expression, AutoFormScope value = AutoFormScope.All);
}