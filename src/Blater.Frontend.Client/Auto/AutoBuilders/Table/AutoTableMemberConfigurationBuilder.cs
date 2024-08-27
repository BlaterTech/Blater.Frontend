using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoTableMemberConfigurationBuilder(Type type, TableConfiguration tableConfiguration)
    : AutoPropertyConfigurationBuilder(new TablePropertyConfiguration())
{
    public AutoTableMemberConfigurationBuilder AddMember<TProperty>(Expression<Func<TProperty>> expression)
    {
        var abc = type.IsGenericType;
        throw new NotImplementedException();
    }
    
    public AutoTableMemberConfigurationBuilder EnableFixedHeader(bool value = true)
    {
        tableConfiguration.EnableFixedHeader = value;
        return this;
    }

    public AutoTableMemberConfigurationBuilder EnableFixedFooter(bool value = true)
    {
        tableConfiguration.EnableFixedFooter = value;
        return this;
    }
}