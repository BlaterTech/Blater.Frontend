using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder<T>(TablePropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder<T>(configuration)
    where T : BaseDataModel
{
    public IAutoPropertyConfigurationBuilder<T> AddMember<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}