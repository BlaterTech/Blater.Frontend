using System.Linq.Expressions;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.Interfaces;

public interface IAutoMemberConfigurationBuilder<T> where T : BaseDataModel
{
    IAutoPropertyConfigurationBuilder<T, TProperty> AddMember<TProperty>(Expression<Func<T, TProperty>> expression);
}