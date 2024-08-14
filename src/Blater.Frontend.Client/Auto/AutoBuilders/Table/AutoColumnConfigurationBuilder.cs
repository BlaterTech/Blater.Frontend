using System.Linq.Expressions;
using Blater.Frontend.Client.Auto.AutoModels.Table;
using Blater.Frontend.Client.Auto.Interfaces.AutoTable;
using Blater.Models.Bases;

namespace Blater.Frontend.Client.Auto.AutoBuilders.Table;

public class AutoColumnConfigurationBuilder<T, TProperty>(Expression<Func<T, TProperty>> expression, ColumnConfiguration<T> configuration)
    : AutoPropertyConfigurationBuilder<T, TProperty>(expression, configuration),
      IAutoColumnConfigurationBuilder<T, TProperty>
    where T : BaseDataModel
{
    public IAutoColumnConfigurationBuilder<T, TProperty> Name(string value)
    {
        configuration.Name = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T, TProperty> DisableFilter(bool value = false)
    {
        configuration.DisableFilter = value;
        return this;
    }

    public IAutoColumnConfigurationBuilder<T, TProperty> DisableSortBy(bool value = false)
    {
        configuration.DisableSortBy = value;
        return this;
    }
}