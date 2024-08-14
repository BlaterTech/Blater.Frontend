using System.Linq.Expressions;
using Blater.Models.Bases;
using MudBlazor;

namespace Blater.Frontend.Client.Auto.Interfaces.AutoForm;

public interface IAutoFormGroupConfigurationBuilder<T, TProperty> where T : BaseDataModel
{
    IAutoFormGroupConfigurationBuilder<T, TProperty> Name(string value);
    IAutoFormPropertyConfigurationBuilder<T, TProperty> AddPartner(Expression<Func<T, TProperty>> expression);
}