using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder<T>(TableConfiguration tableConfiguration, TablePropertyConfiguration configuration)
    : AutoPropertyConfigurationBuilder<T>(configuration)
    where T : BaseDataModel
{
    public AutoTableMemberConfigurationBuilder<T> AddMember<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
    
    public AutoTableMemberConfigurationBuilder<T> EnableFixedHeader(bool value = true)
    {
        tableConfiguration.EnableFixedHeader = value;
        return this;
    }

    public AutoTableMemberConfigurationBuilder<T> EnableFixedFooter(bool value = true)
    {
        tableConfiguration.EnableFixedFooter = value;
        return this;
    }
}