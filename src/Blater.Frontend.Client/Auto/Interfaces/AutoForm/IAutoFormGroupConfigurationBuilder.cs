using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormGroupConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoFormPropertyConfigurationBuilder<T> AddPartner<TProperty>(Expression<Func<T, TProperty>> expression);
}