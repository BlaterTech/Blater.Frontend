using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoColumnConfigurationBuilder<T, TProperty>(Expression<Func<T, TProperty>> expression, ColumnConfiguration<T> configuration)
    : AutoPropertyConfigurationBuilder<T, TProperty>(expression, configuration),
      IAutoMemberConfigurationBuilder<T>
    where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T, TProperty1> AddMember<TProperty1>(Expression<Func<T, TProperty1>> expression)
    {
        throw new NotImplementedException();
    }
}